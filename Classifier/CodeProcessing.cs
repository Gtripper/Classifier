using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Classifier
{
    /// <summary>
    /// Содержит методы обработки кодов ПЗЗ
    /// </summary>
    class CodeProcessing
    {
        string input;
        public int area; /// TODO: Пока так. Пока не готов полноценный интерфейс со всеми данными из MapInfo
        public List<string> Codes { get; private set; }
        IBTI bti;

        /// <summary>
        /// Эксплуатация
        /// </summary>
        public bool Maintenance { get => _maintenance(); }
        /// <summary>
        /// Благоустройство
        /// </summary>
        public bool Landscaping { get => _landscaping(); }

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="Codes"></param>
        /// <param name="bti"></param>
        /// <param name="input"></param>
        public CodeProcessing(List<string> _Codes, IBTI _bti, string _input)
        {
            // Проверка на null
            Codes = _Codes ?? new List<string>();
            bti = _bti ?? new BTI();
            input = _input;
        }

        /// <summary>
        /// Удаляет базовые коды при наличии уточняющих
        /// </summary>
        /// <remark>
        /// Например если есть коды {6.0.0, 6.2.0} на 
        /// выходе должен остаться только индекс {6.2.0}
        /// </remark>
        internal void RemoveBaseCodes()
        {
            #region Dictionary
            var baseCodes = new Dictionary<string, List<string>>();
            baseCodes.Add("1.0.0", new List<string> { "1.1.0", "1.2.0", "1.3.0", "1.4.0", "1.5.0",
                "1.6.0", "1.7.0", "1.8.0", "1.9.0", "1.10.0", "1.11.0", "1.11.0", "1.12.0", "1.13.0",
                    "1.14.0", "1.15.0", "1.16.0", "1.17.0", "1.18.0"});
            baseCodes.Add("1.1.0", new List<string> { "1.2.0", "1.3.0", "1.4.0", "1.5.0", "1.6.0" });
            baseCodes.Add("1.7.0", new List<string> { "1.8.0", "1.9.0", "1.10.0", "1.11.0" });
            baseCodes.Add("2.0.0", new List<string> { "2.1.0", "2.1.1.0", "2.2.0", "2.3.0", "2.5.0",
                "2.6.0", "2.7.1.0", "2.7.0"});
            baseCodes.Add("2.7.0", new List<string> { "3.1.1", "3.1.2", "3.1.3", "3.2.2", "3.2.3",
                "3.2.4", "3.3.0", "3.4.1.0", "3.5.1.0", "3.6.1", "3.7.1", "3.8.2", "3.10.1.0", "4.1.0",
                    "4.4.0", "4.6.0" });
            baseCodes.Add("3.0.0", new List<string> { "3.1.2", "3.1.3", "3.2.1", "3.2.2", "3.2.3",
                "3.2.4", "3.3.0", "3.4.0", "3.4.1.0", "3.4.2.0", "3.5.1.0", "3.5.2.0", "3.6.1",
                    "3.6.2", "3.6.3", "3.7.1", "3.7.2", "3.8.1", "3.8.2", "3.8.3", "3.9.2", "3.10.1.0", "3.10.2.0"});
            baseCodes.Add("3.4.0", new List<string> { "3.4.1.0", "3.4.2.0" });
            baseCodes.Add("4.0.0", new List<string> { "4.1.0", "4.2.0", "4.3.0", "4.4.0", "4.5.0",
                "4.6.0", "4.8.0", "4.9.0", "4.10.0" });
            baseCodes.Add("5.0.1", new List<string> { "5.0.2", "5.1.1", "5.1.2", "5.1.3", "5.1.4",
                "5.1.5", "5.2.1.0", "5.2.0", "5.3.0", "5.4.0", "5.5.0" });
            baseCodes.Add("6.0.0", new List<string> { "6.2.0", "6.2.1.0", "6.3.0", "6.3.1.0",
                "6.4.0", "6.5.0", "6.6.0", "6.7.0", "6.8.0", "6.11.0" });
            #endregion

            // Коллекция базовых кодов, присутствующая в Codes
            var intersect = baseCodes.Keys.ToList().Intersect(Codes);

            if (intersect.Count() > 0)
            {
                foreach (var val in intersect)
                {
                    // Пересечение производных от базовых кодов
                    // и Codes
                    if (baseCodes[val].Intersect(Codes).Count() > 0)
                    {
                        Codes.RemoveAll(p => p.Equals(val));
                    }
                }
            }
        }

        /// <summary>
        /// Определяет код исходя из этажности объекта БТИ
        /// </summary>
        /// <remark>
        /// Проперти lo, mid и hi для объекта БТИ проставляются только
        /// для многоэтажных многоквартирных домов. Эти индексы никак не касаются 
        /// общественной, деловой, промышленной и прочей застройки
        /// </remark>
        /// TODO: Посмотреть на реальных участках, где в ВРИ указано
        /// малоэтажные многоквартирные дома
        internal void NumberDeterminant()
        {

            bool IsApartmentOrBaseHouse = Codes.Exists(p => p.Equals("2.0.0") ||
                    p.Equals("2.1.1.0") || p.Equals("2.5.0") || p.Equals("2.6.0"));

            if (IsApartmentOrBaseHouse && bti.Lo_lvl)
            {
                Codes.RemoveAll(p => p.Equals("2.0.0") ||
                    p.Equals("2.1.1.0") || p.Equals("2.5.0") || p.Equals("2.6.0"));
                Codes.Add("2.1.1.0");
            }
            if (IsApartmentOrBaseHouse && bti.Mid_lvl)
            {
                Codes.RemoveAll(p => p.Equals("2.0.0") ||
                    p.Equals("2.1.1.0") || p.Equals("2.5.0") || p.Equals("2.6.0"));
                Codes.Add("2.5.0");
            }
            if (IsApartmentOrBaseHouse && bti.Hi_lvl)
            {
                Codes.RemoveAll(p => p.Equals("2.0.0") ||
                    p.Equals("2.1.1.0") || p.Equals("2.5.0") || p.Equals("2.6.0"));
                Codes.Add("2.6.0");
            }

            Codes.Sort(new CodeComparer());  
        }

        /// <summary>
        /// Маркирует участок как "Эксплуатация"
        /// </summary>
        /// <remark>
        /// Логика: в ВРИ пристуствет слово "эксплуатация", на участке есть строение бти с жилым
        /// кодом, а в полученных из ВРИ кодах жилые индексы отстуствуют
        /// </remark>
        /// TODO: логика для "обслуживание части жилого дома"
        internal bool _maintenance()
        {
            //var pattern = @"\bэксплуатац\w*\s*(пристройк\w*|((в|при)строен\w*\s*)?помещен\w*|част\w*\s*здан\w*|нежилы\w*|" +
            //    @"(в\s*здан\w*)?служебн\w*\s*помещен\w*)\b";
            var pattern = @"\bэксплуатац\w*\b";
            bool IsMaintenanceInString = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
            bool IsCodesNotHousing = !IsHousingCodes(Codes);
            bool IsBTICodesHousing = IsHousingCodes(bti.btiCodes);
            return IsMaintenanceInString && IsCodesNotHousing && IsBTICodesHousing;
        }

        /// <summary>
        /// Обрабатывает коды 12.3.0
        /// </summary>
        /// TODO: refactoring this
        internal void FixCode_Other()
        {
            bool IsOtherExist = Codes.Exists(p => p.Equals("12.3.0"));

            if (IsOtherExist && Codes.Count > 1)
            {
                Codes.RemoveAll(p => p.Equals("12.3.0"));
            }
            else if (IsOtherExist && Codes.Count == 1 && bti.btiCodes.Count > 0)
            {
                Codes.RemoveAll(p => p.Equals("12.3.0"));
                Codes.AddRange(bti.btiCodes);
            }
        }

        /// <summary>
        /// Маркирует участок как "Благоустройство"
        /// </summary>
        /// <remark>
        /// Если участок имеет в ВРИ слово благоустройство (удовлетворяющее паттерну регулярки)
        /// и имеет единственный код ПЗЗ 12.0.1, то он маркируется как благоустройство.
        /// </remark>
        internal bool _landscaping()
        {
            var pattern = @"\bблагоустр\w*\b";
            bool IsLandScaping = Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
            bool CodesIsLandScapingOnly = Codes.Exists(p => p.Equals("12.0.1")) && Codes.Count == 1;

            if (IsLandScaping && CodesIsLandScapingOnly)
                return true;
            else
                
return false;
        }

        /// <summary>
        /// Меняет код ПЗЗ с 6.7.0 на 3.1.1 в случае, если площадь участка меньше 300
        /// </summary>
        internal void ElectricityStationsWithAreaLessThan300()
        {
            bool IsElectricityStation = Codes.Exists(p => p.Equals("6.7.0"));

            if (IsElectricityStation && area < 300)
            {
                var i = Codes.FindIndex(p => p.Equals("6.7.0"));
                Codes[i] = "3.1.1";
            }
        }

        /// <summary>
        /// Проверяет коллекцию кодов на наличие жилых кодов
        /// </summary>
        /// <param name="Codes"></param>
        /// <returns></returns>
        internal bool IsHousingCodes(List<string> _codes)
        {
            return _codes.Exists(p => p.Equals("2.0.0") ||
                p.Equals("2.1.0") || p.Equals("2.2.0") || p.Equals("2.3.0") ||
                            p.Equals("2.1.1.0") || p.Equals("2.5.0") || p.Equals("2.6.0"));
        }
    }
}
