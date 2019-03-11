using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Classifier
{
    public interface ISearchCodes
    {
        void MainLoop();

        string Matches { get; }
        ICodes Codes { get; }
        bool IsFederalSearch { get; }
        bool IsPZZSearch { get; }
        bool IsMainSearch { get; }
    }


    /// <summary>
    /// Определние кодов ПЗЗ по строке ВРИ по документу
    /// </summary>
    /// <remark>
    /// Выполняется поиск в три этапа:
    /// 1. Проверка на наличие федерального кода в строке вида (fCode)
    /// 1.1 В случае нахождения такого кода: 
    ///     - Отчистить Codes и Matches 
    ///     - Выполнить поиск по федеральным кодам
    /// 2. Проверка на наличие кодов ПЗЗ в начале строки
    /// 2.2 В случае успешной проверки - остановка цикла поиска
    /// 3. Поиск совпадений с регулярными выражениями regexpPatterns
    /// </remark>
    public class SearchCodes : ISearchCodes
    {
        private readonly string input; // ВРИ по документу
        private StringBuilder _matches;
        private NodeFeed mf = new NodeFeed();

        public string Matches { get  { return _matches.ToString(); } }
        public ICodes Codes { get; }
        public bool IsFederalSearch { get; private set; }
        public bool IsPZZSearch { get; private set; }
        public bool IsMainSearch { get; private set; }

        public SearchCodes(string Input)
        {
            input = Input;
            _matches = new StringBuilder("");
            Codes = new Codes(mf);

            IsFederalSearch = false;
            IsPZZSearch = false;
            IsMainSearch = false;
        }

        public void MainLoop()
        {
            var nodes = mf.GetNodes();

            foreach (var node in nodes)
            {
                // Search Federal codes
                if (!node.vri540.Equals(""))
                {
                    var reg = FederalSearchRegexp(node.vri540);
                    if (reg.IsMatch(input))
                    {
                        SearchFederalCodes();
                        IsFederalSearch = true;
                        IsPZZSearch = false;
                        IsMainSearch = false;
                        break;
                    }
                }
                // Search PZZ code
                if (!node.vri.Equals(""))
                {
                    var reg = CodePZZSearchRegexp(node.vri);
                    var match = "";
                    if (reg.IsMatch(input))
                    {
                        ClearOutputFields();
                        Codes.Add(node);
                        match = reg.Match(input).Value.Trim();
                        AddMatches(match);
                        IsFederalSearch = false;
                        IsPZZSearch = true;
                        IsMainSearch = false;
                        break;
                    }
                }
                // Searching based node.regexpPatterns
                regexpPatternsSearch(node);
            }
        }

        #region FederalSearch
        /// <summary>
        /// Создание регулярного выражения дла поиска в строке 
        /// кодов из федерального классификатора, заключенного
        /// в круглые скобки
        /// </summary>
        /// <param name="fCode"></param>
        /// <returns></returns>
        /// <remark>
        /// Строка afterReplace необходима для того, что бы разделяющие "." в 
        /// федеральных кодах не считались любым символом
        /// </remark>
        private Regex FederalSearchRegexp(string fCode)
        {
            var afterReplace = Regex.Replace(fCode, @"[.]", @"\s*[.]\s*", RegexOptions.IgnoreCase);

            var pattern = @"[(]\s*" + afterReplace + @"\s*[)]";

            return new Regex(pattern, RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// Добавление в Codes кодов ПЗЗ из массива CodeMapping
        /// </summary>
        /// <param name="codes"></param>
        private void AddCodesFromCodeMapping(List<Node> codes)
        {
            Codes.AddNodes(codes);
        }

        /// <summary>
        /// Полный поиск федеральных кодов во входящей строке
        /// </summary>
        internal void SearchFederalCodes()
        {
            ClearOutputFields();
            var match = "";
            var nodes = new CodesMapping();
            foreach (var node in nodes.Map.Keys)
            {
                var reg = FederalSearchRegexp(node);

                if (reg.IsMatch(input))
                {
                    Codes.AddNodes(nodes.Map[node]);
                    match = reg.Match(input).Value;
                    AddMatches(match);
                }
            }
        }
        #endregion

        #region PZZSearch
        /// <summary>
        /// Создает регулярное выражение для поиска кода ПЗЗ без скобок в начале строки
        /// </summary>
        /// <param name="pCode"></param>
        /// <returns></returns>
        private Regex CodePZZSearchRegexp(string pCode)
        {
            {
                var afterReplace = Regex.Replace(pCode, @"[.]", @"\s*[.]\s*", RegexOptions.IgnoreCase);

                var pattern = @"^\s*" + afterReplace + @"\s*";

                return new Regex(pattern, RegexOptions.IgnoreCase);
            }            
        }
        #endregion

        #region regexpPatternSearch
        /// <summary>
        /// Поиск, базирующийся на регулярных выражениях из NodeFeed
        /// </summary>
        /// <remark>
        /// В цикле по массиву Node.regexpPatterns[] с шагом два выполняется 
        /// поиск двух паттернов для регулярного выражения: позитивного и негативного.
        /// Если негативный паттерн не равен "", то ищутся совпадения только по позитивному паттерну
        /// и отсутствие совпадений с негативным.
        /// В обратном случае поиск ведется только по позитивному паттерну.
        /// </remark>
        internal void regexpPatternsSearch(Node node)
        {
            var positivePattern = "";
            var negativePattern = "";
            var match = "";
            for (int i = 0; i < node.regexpPatterns.Length; i += 2)
            {
                negativePattern = node.regexpPatterns[i];
                positivePattern = node.regexpPatterns[i + 1];
                if (negativePattern.Equals(""))
                    match = NegativePatternIsNull(positivePattern);
                else
                    match = NegativePatternIsNotNull(negativePattern, positivePattern);

                if (!match.Equals(""))
                {
                    AddCodesVriByNode(node);
                    AddMatches(match);
                    IsFederalSearch = false;
                    IsPZZSearch = false;
                    IsMainSearch = true;
                    break;
                }
            }
        }

        /// <summary>
        /// Негативный паттерн не пустой
        /// </summary>
        /// <param name="negativePattern"></param>
        /// <param name="positivePattern"></param>
        /// <returns></returns>
        internal string NegativePatternIsNotNull(string negativePattern, string positivePattern)
        {
            var excluded = Regex.IsMatch(input, negativePattern, RegexOptions.IgnoreCase);
            var included = Regex.IsMatch(input, positivePattern, RegexOptions.IgnoreCase);

            if (!excluded & included)
            {
                var val = Regex.Match(input, positivePattern, RegexOptions.IgnoreCase).Value;
                return val;
            }
            return "";
        }

        /// <summary>
        /// Негативный паттерн пустой
        /// </summary>
        /// <param name="positivePattern"></param>
        /// <returns></returns>
        internal string NegativePatternIsNull(string positivePattern)
        {
            var included = Regex.IsMatch(input, positivePattern, RegexOptions.IgnoreCase);

            if (included)
            {
                var val = Regex.Match(input, positivePattern, RegexOptions.IgnoreCase).Value;
                return val;
            }
            return "";
        }
        #endregion

        /// <summary>
        /// Добавление нового совпадения в строку Matches
        /// </summary>
        /// <param name="match"></param>
        internal void AddMatches(string match)
        {
            if (!match.Equals(""))
                if (_matches.Length == 0)
                    _matches.Append(match);
                else
                    _matches.Append(", " + match);
        }

        /// <summary>
        /// Добавление нового кода ПЗЗ в Codes
        /// </summary>
        /// <param name="node"></param>
        /// <remark>
        /// В случае, если нашлась node с пустым кодом ПЗЗ,
        /// добавляются все коды ПЗЗ с одинаковым федеральным кодом
        /// (смотри для примера 7.0 или 3.4)
        /// </remark>
        internal void AddCodesVriByNode(Node node)
        {
            if (node.vri.Equals(""))
            {
                var arr = node.EmptyVRI();
                Codes.AddNodes(arr);
            }
            else
                Codes.Add(node);
        }

        /// <summary>
        /// Обнуление полей Codes и Matches
        /// </summary>
        internal void ClearOutputFields()
        {
            Codes.Clear();
            _matches.Clear();
        }
    }
}
