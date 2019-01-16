using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace Classifier
{
   
    public interface Analizer {
        void GetVRI_FullSearh();
    }
    
    public class Sorter : Analizer
    {
        #region Fields
        private List<Node> mf;
        private List<Codes> codes;
        private string input;
        private string codesVri;
        private string mathes;
        private int area;
        private Bti bti;
        #endregion
        #region Methods
        public Sorter(string input, int area)
        {
            mf = new NodeFeed().getMonster();
            Input = input;
            this.area = area;
            codes = new List<Codes>();
            codesVri = "";
            IsFastFederalSearch = false;
            IsFastPZZSearch = false;
            IsMainSearch = false;
            Results = new string[10];
        }

        public Sorter(string input, int area, Bti bti)
        {
            mf = new NodeFeed().getMonster();
            this.area = area;
            Input = input;
            codes = new List<Codes>();
            codesVri = "";
            IsFastFederalSearch = false;
            IsFastPZZSearch = false;
            IsMainSearch = false;
            this.bti = bti;
            Results = new string[10];
        }

        private void GetCodes_FullSearh()
        {
            foreach (var iter in mf)
            {
                var reg = new RegExp(iter, input);
                reg.FullSearch();

                if (reg.IsFastFederalSearch())
                {
                    RemoveAllCodes();                    
                    IsMainSearch = false;
                    IsFastFederalSearch = true;
                    ForFederalSearch();
                    break;
                }
                else if (reg.IsFastPZZSearch())
                {
                    RemoveAllCodes();
                    IsMainSearch = false;
                    AddCode(reg.Value(), iter);
                    IsFastPZZSearch = true;
                    break;
                }
                else if (reg.IsMainSearch())
                {
                    AddCode(reg.Value(), iter);
                    IsMainSearch = true;
                }
            }
        }

        private void ForFederalSearch()
        {
            var map = new CodesMapping().Map;

            foreach (var iter in map.Keys)
            {
                var reg = new RegExp(iter, input);

                if (reg.OnlyFederalSearch())
                {
                    var val = new CodesMapping().CreateNodes(map[iter]);
                    foreach (var it in val)
                    {
                        AddCode(reg.Value(), it);
                    }
                }
            }
            codes  = codes.GroupBy(p => p.mnstr.vri).Select(grp => grp.FirstOrDefault()).ToList();           
        }

        private void GetCodes_MainSearh()
        {
            foreach (var iter in mf)
            {
                var reg = new RegExp(iter, input);
                reg.MainSearch();

                if (reg.IsMainSearch())
                {
                    AddCode(reg.Value(), iter);
                }
            }
        }
        #region BehaviorMethods
        private bool MarkTemporary()
        {
            var pattern = @"\bна\s*период|\bвременн\w*\b|\b(бытов\w*|строит\w*)\s*город\w*|" +
                @"\bштаб\w*\sстроительст\b|\bнекаптал\w*\b|\bстоительств\w*\b|\bнестационарн\w*\b";
            return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }
        private bool MarkLinearObjs()
        {
            var pattern = @"\bк?лэп\b|\bпередач\w*\s*(электро)?энерг\w*\b|\bвысоковольт\w*\s*каб\w*\b" +
                @"\bвл\s*\b\d+\b\s*кв\b";
            return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }
        private bool MarkMaintenance()
        {
            //var pattern = @"\bэксплуатац\w*\s*(пристройк\w*|((в|при)строен\w*\s*)?помещен\w*|част\w*\s*здан\w*|нежилы\w*|" +
            //    @"(в\s*здан\w*)?служебн\w*\s*помещен\w*)\b";
            var pattern = @"\bэксплуатац\w*\b";            
            return codes.Exists(p => !p.mnstr.isHousing()) && Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }
        private bool MarkPlacement()
        {
            //var pattern = @"\bэксплуатац\w*\s*(пристройк\w*|((в|при)строен\w*\s*)?помещен\w*|част\w*\s*здан\w*|нежилы\w*|" +
            //    @"(в\s*здан\w*)?служебн\w*\s*помещен\w*)\b";
            var pattern = @"\bразмещен\w*\b";
            return codes.Exists(p => !p.mnstr.isHousing()) && Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }
        private bool MarkAccomplishment()
        {
            var pattern = @"\bблагоустр\w*\b";
            return Regex.IsMatch(input, pattern, RegexOptions.IgnoreCase);
        }        
        private bool Maintenance() 
        {
            if (MarkMaintenance() && bti.Codes.Exists(p => p.mnstr.isHousing()))
            {
                return true;
            }
            else
                return false;
        }
        private bool Placement()
        {
            if (MarkPlacement() && bti.Codes.Exists(p => p.mnstr.isHousing()))
            {
                return true;
            }
            else
                return false;
        }
        private void Accomplishment()
        {
            if (MarkAccomplishment() && codes.Count > 1)
            {
                RemoveVRI("12.0.1");
            }
        }
        private void InDaHouse()
        {
            var bl = codes.Exists(p => p.mnstr.vri.Equals("2.0.0") ||
                    p.mnstr.vri.Equals("2.1.1.0") ||
                        p.mnstr.vri.Equals("2.5.0") ||
                            p.mnstr.vri.Equals("2.6.0"));

            if (bl && ! bti.NullBti)
            {
                var btiList = new List<Node>();
                if (bti.low)
                {
                    btiList.Add(mf.Find(p => p.vri.Equals("2.1.1.0")));
                }
                if (bti.mid)
                {
                    btiList.Add(mf.Find(p => p.vri.Equals("2.5.0")));
                }
                if (bti.hight)
                {
                    btiList.Add(mf.Find(p => p.vri.Equals("2.6.0")));
                }
                if (btiList.Count > 0)
                {
                    var m = codes.Find(p => p.mnstr.vri.Equals("2.0.0") ||
                        p.mnstr.vri.Equals("2.1.1.0") ||
                            p.mnstr.vri.Equals("2.5.0") ||
                                p.mnstr.vri.Equals("2.6.0"));
                    var match = m.match;

                    codes.RemoveAll(p => p.mnstr.vri.Equals("2.0.0") ||
                        p.mnstr.vri.Equals("2.1.1.0") ||
                            p.mnstr.vri.Equals("2.5.0") ||
                                p.mnstr.vri.Equals("2.6.0"));

                    bti.CorrectedByBti = true;

                    foreach (var iter in btiList)
                    {
                        AddCode(match, iter);
                    }
                }
            }
        }
        private void ThashZU()
        {
            var val = codes.Where(p => p.mnstr.vri.Equals("12.3.0"));

            if (codes.Count() > 1)
            {
                codes.RemoveAll(p => p.mnstr.vri.Equals("12.3.0"));
            }

            else if (val.Count() == 1 && !bti.vri.Equals(""))
            {                
                foreach (var iter in bti.Codes)
                {
                    codes.RemoveAll(p => p.mnstr.vri.Equals("12.3.0"));
                    AddCode(iter.match, iter.mnstr);
                }
            }
        }
        private void CleanFederalCodes()
        {
            var val = codes.Where(p => p.mnstr.vri.Equals("3.5"));
            if (val.Count() > 0)
            {
                var i = mf.FindIndex(p => p.vri.Equals("3.5.1.0"));
                var j = mf.FindIndex(p => p.vri.Equals("3.5.2.0"));

                var match = codes.Find(p => p.mnstr.vri.Equals("3.5")).match;
                codes.RemoveAll(p => p.mnstr.vri.Equals("3.5"));
                if (bti.Codes.Exists(p => p.mnstr.vri.Equals("3.5.1.0")))
                {
                    AddCode(match, mf.ElementAt(i));
                    bti.CorrectedByBti = true;
                }
                else if (bti.Codes.Exists(p => p.mnstr.vri.Equals("3.5.2.0")))
                {
                    AddCode(match, mf.ElementAt(j));
                    bti.CorrectedByBti = true;
                }
                else
                {
                    AddCode(match, mf.ElementAt(i));
                    AddCode(match, mf.ElementAt(j));
                    bti.CorrectedByBti = false;
                }
            }
            val = codes.Where(p => p.mnstr.vri.Equals("6.7"));
            if (val.Count() > 0)
            {
                codes.RemoveAll(p => p.mnstr.vri.Equals("6.7"));
            }
            val = codes.Where(p => p.mnstr.vri.Equals("3.10"));
            if (val.Count() > 0)
            {
                var i = mf.FindIndex(p => p.vri.Equals("3.10.1.0"));
                var j = mf.FindIndex(p => p.vri.Equals("3.10.2.0"));

                var match = codes.Find(p => p.mnstr.vri.Equals("3.10")).match;
                codes.RemoveAll(p => p.mnstr.vri.Equals("3.10"));
                if (bti.Codes.Exists(p => p.mnstr.vri.Equals("3.10.1.0")))
                {
                    AddCode(match, mf.ElementAt(i));
                    bti.CorrectedByBti = true;
                }
                else if (bti.Codes.Exists(p => p.mnstr.vri.Equals("3.10.2.0")))
                {
                    AddCode(match, mf.ElementAt(j));
                    bti.CorrectedByBti = true;
                }
                else
                {
                    AddCode(match, mf.ElementAt(i));
                    AddCode(match, mf.ElementAt(j));
                    bti.CorrectedByBti = false;
                }
            }
        }
        private void PromAreaLess300()
        {
            bool prom;
            prom = codes.Exists(p => p.mnstr.vri.Equals("6.7.0"));
            if (prom && area <= 300)
            {
                var match = codes.Find(p => p.mnstr.vri.Equals("6.7.0")).match;
                var mnstr = mf.Find(p => p.vri.Equals("3.1.1"));
                codes.RemoveAll(p => p.mnstr.vri.Equals("6.7.0"));
                AddCode(match, mnstr);
            }
        }

        /// <summary>
        /// Проверка наличия кодов жилья
        /// </summary>
        /// <returns></returns>
        private bool ResidentialArea()
        {
            return codes.Exists(p => p.mnstr.vri.Equals("2.0.0") ||
                p.mnstr.vri.Equals("2.1.0") ||
                    p.mnstr.vri.Equals("2.2.0") ||
                        p.mnstr.vri.Equals("2.3.0") ||
                            p.mnstr.vri.Equals("2.1.1.0") ||
                                p.mnstr.vri.Equals("2.5.0") ||
                                    p.mnstr.vri.Equals("2.6.0"));
        }

        /// <summary>
        /// Удаление индексов 3.1.1, 2.7.1.0, 4.9.0 из участков с жильем
        /// </summary>
        private void Type230Fix()
        {
            var bl = codes.Exists(p => p.mnstr.kindCode.Equals("3004"));
            if (bl && ResidentialArea())
            {
                codes.RemoveAll(p => p.mnstr.kindCode.Equals("3004"));
                Results[9] = "230FIXED";
            }
        }

        /// <summary>
        /// Определение типа земучастка
        /// </summary>
        /// <returns><string></returns>
        /// TODO: Исключить типы аля 138
        private string Type()
        {
            try
            {
                if (codes.Exists(p => p.mnstr.typeCode.Equals("333")))
                {
                    return "333";
                }
                else if (codes.Count == 1)
                {
                    return codes[0].mnstr.typeCode;
                }
                else
                {
                    var result = "";
                    var set = new SortedSet<string>();
                    foreach (var val in codes)
                    {
                        if (val.mnstr.typeCode != "")
                        set.Add(val.mnstr.typeCode.Remove(1));
                    }
                    foreach (var val in set)
                    {
                        result += val;
                    }
                    while (result.Length < 3)
                    {
                        result += "0";
                    }
                    return result;
                }
            }
            catch (Exception e)
            {
                
                Console.WriteLine(e.Message);
                return e.Message;
            }
        }


        /// <summary>
        /// Определние вида земучастка
        /// </summary>
        /// TODO: Как-то переделать codes.All(p => p.mnstr.kindCode.Equals(codes[0].mnstr.kindCode)))
        /// <returns><string></returns>
        private string Kind()
        {
            if (codes.Count == 1 ||
                    codes.All(p => p.mnstr.kindCode.Equals(codes[0].mnstr.kindCode)))
            {
                return codes[0].mnstr.kindCode;
            }
            else
            {
                var result = "";
                var set = new SortedSet<string>();
                foreach (var val in codes)
                {
                    if (val.mnstr.kindCode != "")
                    set.Add(val.mnstr.kindCode.Remove(1));
                }
                foreach (var val in set)
                {
                    result += val;
                }
                while (result.Length < 4)
                {
                    result += "0";
                }
                return result;
            }
        }

        #endregion
        #region ListOfCodesMethods
        private void RemoveAllCodes()
        {
            codes.Clear();
        }
        private void AddCode(Match matchesCollection, Node mnstr)
        {
            var val = new Codes(matchesCollection, mnstr);
            codes.Add(val);
        }
        private void SortCodesIndex()
        {
            Codes.CodeComparer cc = new Codes.CodeComparer();
            codes.Sort(cc);
        }
        private void DeleteGenericVRI()
        {
            var delList = new List<Node>();

            var val = codes.Where(p => p.mnstr.isBaseClass() == true);
            foreach (var iter in val)
            {
                var temp = codes.Where(p => p.mnstr.GetParent(iter.mnstr) || p.mnstr.GetGParent(iter.mnstr));
                if (temp.Count() > 0) delList.Add(iter.mnstr);
            }

            foreach (var it in delList)
            {
                codes.RemoveAll(p => p.mnstr.Equals(it));
            }
        }
        private string StringOfVRI()
        {
            var result = "";
            foreach (Codes str in codes)
            {
                result += result.Equals("") ? str.mnstr.vri : ", " + str.mnstr.vri;
            }
            return result;
        }
        private string StringOfMatches()
        {
            var result = "";
            foreach (Codes str in codes)
            {

                result += result.Equals("") ? str.match.Value : ", " + str.match.Value;
            }
            return result;
        }
        private dynamic WhereHouse()
        {
            return codes.Exists(p => p.mnstr.isHousing());
        }
        private dynamic RemoveVRI(string vri)
        {
            return codes.RemoveAll(p => p.mnstr.vri.Equals(vri));
        }
        private dynamic ExistsVRI(string vri)
        {
            return codes.Exists(p => p.mnstr.Equals(vri));
        }
        private dynamic GetMatchWithVRI(string vri)
        {
            var temp = codes.Find(p => p.mnstr.vri.Equals(vri));
            return temp.match;
        }
        #endregion
        #endregion
        #region SettersAndGetters

        public bool IsFastFederalSearch { get; private set; }
        public bool IsFastPZZSearch { get; private set; }
        public bool IsMainSearch { get; private set; }
        /// <summary>
        /// Все результаты работы в этом массиве
        /// </summary>
        public string[] Results { get; private set; }

        public Bti Bti => bti;

        public List<Codes> Codes => codes;

        public string Input
        {
            get => input;
            private set {
                if (value is string) input = value;
            }
        }
        public float Area { get => area; private set { } }
        public string CodesVri { get => codesVri; }
        public string Mathes { get => mathes; private set { } }

        public bool linear { get => MarkLinearObjs(); }
        public bool temporary { get => MarkTemporary(); }
        public bool accomplishment { get => MarkAccomplishment(); }
        public bool maintenance { get => Maintenance(); }
        
        #endregion
        public void GetVRI_FullSearh()
        {              
            GetCodes_FullSearh();
            //SortCodesIndex(); <== Сортировка индексов не нужна, поскольку лишь увеличивает количество вариантов, ни на что толком не влияя.
            InDaHouse();
            DeleteGenericVRI();
            mathes = StringOfMatches();
            Maintenance();
            ThashZU();
            CleanFederalCodes();
            Accomplishment();
            PromAreaLess300();         
            

            codesVri = StringOfVRI();           
        }
        public void GetStringOfVRI()
        {            
            GetCodes_MainSearh();
            SortCodesIndex();
            DeleteGenericVRI();
            codesVri = StringOfVRI();
            //Sample.InDaHouse();         
        }
        public void SimpleSearch()
        {
            GetCodes_FullSearh();
            SortCodesIndex();
            DeleteGenericVRI();
            mathes = StringOfMatches();
            codesVri = StringOfVRI();
        }
        public bool PlacementSearch()
        {
            return Placement();
        }

        public void TestBehaviorSearchWithoutBti()
        {
            GetCodes_FullSearh();
            if (codes.Count > 0)
            {
                Results[0] = StringOfVRI(); // Коды ВРИ после работы основного цикла
                Results[1] = StringOfMatches(); // Все найденные совпадения
                DeleteGenericVRI();
                Results[2] = StringOfVRI(); // Без базовых кодов (6.0.0, 3.0.0, 4.0.0 etc)
                ThashZU();
                Results[3] = StringOfVRI();
                Type230Fix();
                Results[4] = StringOfVRI();
                Results[5] = Type();
                Results[6] = Kind();                
            }
            else
            {
                Results[0] = "не успех";
            }
        }

        ///TODO: Навести порядок в сортировщках и их методах
        ///Сделать ветвление. Нет необходмости прогонять все методы
        ///для всех участков.   
        
        ///TODO: Проверка ИЖС. Если участок 2.0.0 и ИЖС true => ИЖС

        public void Voronezh_search()
        {
            GetCodes_FullSearh();
            if (codes.Count > 0)
            {
                Results[0] = StringOfVRI(); // Коды ВРИ после работы основного цикла
                Results[1] = StringOfMatches(); // Все найденные совпадения
                DeleteGenericVRI();
                Results[2] = StringOfVRI(); // Без базовых кодов (6.0.0, 3.0.0, 4.0.0 etc)                
                Type230Fix();
                Results[4] = StringOfVRI();
                Results[5] = Type();
                Results[6] = Kind();
            }
            else
            {
                Results[0] = "не успех";
            }
        }
    }

    public class Bti
    {
        private string func;                    // Строка функционального назначения всех объектов БТИ на участке
        private List<Codes> codes;              // Коды ВРИ объектов участка
        public string vri;
        private int btiCount;                   // Кол-во объектов БТИ на участке
        private bool nullBti;                   // Маркер отсутствия информации об объектах участка
        private bool is2_1_1;                   // Признаки возможных ВРИ на основе этажности
        private bool is2_5;                     // Повыше
        private bool is2_6;                     // Ещё повыше
        private bool isIndividualHousing;       // Признак ИЖС
        private bool correctedByBti;

        public Bti()
        {
            func = "";
            codes = new List<Codes>();
            vri = "";
            btiCount = 0;
            is2_1_1 = false;
            is2_5 = false;
            is2_6 = false;
            isIndividualHousing = false;
            correctedByBti = false;
            nullBti = true;
        }

        public Bti(string func, int btiCount, bool is2_1_1, bool is2_5, bool is2_6, bool isIndividualHousing)
        {
            this.func = func;
            this.btiCount = btiCount;
            vri = "";
            codes = new List<Codes>();
            this.is2_1_1 = is2_1_1;
            this.is2_5 = is2_5;
            this.is2_6 = is2_6;
            this.isIndividualHousing = isIndividualHousing;
            correctedByBti = false;
            if (func.Equals(""))
            {
                nullBti = true;
            }
            else
            {
                nullBti = false;
                Sorter temp = new Sorter(func, 0);
                temp.GetStringOfVRI();
                codes = temp.Codes;
                vri = temp.CodesVri;
            }
        }

        public List<Codes> Codes => codes;

        public bool NullBti { get => nullBti; }

        public string GetVri()
        {
            return vri;
        }

        public bool low { get => is2_1_1; }
        public bool mid { get => is2_5; }
        public bool hight { get => is2_6; }
        public bool CorrectedByBti { get => correctedByBti; set { correctedByBti = value; } }
    }

    public class Codes
    {
        internal Match match;
        internal Node mnstr;
        
        public Codes(Match match, Node mnstr)
        {
            this.match = match;
            this.mnstr = mnstr;
        }       

        public class CodeComparer : IComparer<Codes>
        {
            public int Compare(Codes x, Codes y)
            {
                try
                {
                    return x.match.Index - y.match.Index;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                throw new NotImplementedException();

            }            
        }   
    }
}
