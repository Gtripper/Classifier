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
        private string input;
        private int area; /// TODO: Пока так. Пока не готов полноценный интерфейс со всеми данными из MapInfo
        private NodeFeed mf;
        public ICodes Codes { get; private set; }
        private IBTI bti;

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
        public CodeProcessing(ICodes _Codes, IBTI _bti, string _input, int _area, NodeFeed mf)
        {
            // Проверка на null
            Codes = _Codes ?? new Codes(mf);
            bti = _bti ?? new BTI();
            input = _input;
            area = _area;
            this.mf = mf;
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

            var listOfVri = Codes.Nodes.Select(p => p.vri);
            // Коллекция базовых кодов, присутствующая в Codes
            var intersect = baseCodes.Keys.ToList().Intersect(listOfVri);

            if (intersect.Count() > 0)
            {
                foreach (var val in intersect)
                {
                    // Пересечение производных от базовых кодов
                    // и Codes
                    if (baseCodes[val].Intersect(listOfVri).Count() > 0)
                    {
                        Codes.RemoveAll(val);
                    }
                }
            }
            Codes.Sort();
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
            var list = new List<string> { "2.0.0", "2.1.1.0", "2.5.0", "2.6.0" };
            bool IsApartmentOrBaseHouse = Codes.Exists(list);

            if (IsApartmentOrBaseHouse && bti.Lo_lvl)
            {
                Codes.RemoveAll(list);
                Codes.AddNodes("2.1.1.0");
            }
            if (IsApartmentOrBaseHouse && bti.Mid_lvl)
            {
                Codes.RemoveAll(list);
                Codes.AddNodes("2.5.0");
            }
            if (IsApartmentOrBaseHouse && bti.Hi_lvl)
            {
                Codes.RemoveAll(list);
                Codes.AddNodes("2.6.0");
            }

            Codes.Sort();
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
            bool IsOtherExist = Codes.Exists("12.3.0");

            if (IsOtherExist && Codes.Count > 1)
            {
                Codes.RemoveAll("12.3.0");
            }
            else if (IsOtherExist && Codes.Count == 1 && bti.btiCodes.Count > 0)
            {
                Codes.RemoveAll("12.3.0");
                Codes.AddNodes(bti.btiCodes.Nodes);
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
            bool CodesIsLandScapingOnly = Codes.Exists("12.0.1") && Codes.Count == 1;

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
            bool IsElectricityStation = Codes.Exists("6.7.0");

            if (IsElectricityStation && area < 300)
            {
                Codes.RemoveAll("6.7.0");
                Codes.AddNodes("3.1.1");
            }
        }

        /// <summary>
        /// Удаляет коды с видом 3004 в случае, если есть жиоые коды
        /// </summary>
        internal void Type230Fix()
        {
            var isKindCode3004Exist = Codes.Exists("2.7.1.0, 3.1.1, 4.9.0, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4");

            if (IsHousingCodes(Codes) && isKindCode3004Exist)
            {
                Codes.RemoveAll("2.7.1.0, 3.1.1, 4.9.0, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4");
            }
        }

        /// <summary>
        /// Удаляет
        /// </summary>
        internal void LandscapingFix()
        {
            var isCodesNeedToDelete = Codes.Exists("12.0.1") && 
                Regex.IsMatch(input, @"\bблагоустр\w*\b", RegexOptions.IgnoreCase);

            if (isCodesNeedToDelete && Codes.Count > 1)
                Codes.RemoveAll("12.0.1");
        }

        /// <summary>
        /// Проверяет коллекцию кодов на наличие жилых кодов
        /// </summary>
        /// <param name="Codes"></param>
        /// <returns></returns>
        internal bool IsHousingCodes(ICodes _codes)
        {
            return _codes.Exists("2.0.0, 2.1.0, 2.2.0, 2.3.0, 2.1.1.0, 2.5.0, 2.6.0");
        }

        public void FullProcessing()
        {
            RemoveBaseCodes();
            NumberDeterminant();
            FixCode_Other();
            ElectricityStationsWithAreaLessThan300();
            Type230Fix();
            LandscapingFix();
        }
    }
}
