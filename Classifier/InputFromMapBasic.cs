using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Classifier
{
    public class InputFromMapBasic
    {
        public static void SearchVRI(string[] array, int[] arrInt, bool[] arrBool, int btiCount, int ZU_area, string[] btiArrayStr, bool[] btiArrayBool)
        {
            Bti bti = new Bti(btiArrayStr[0], btiCount, btiArrayBool[0], btiArrayBool[1], btiArrayBool[2], btiArrayBool[3]);

            var Sample = new Sorter(array[0], ZU_area, bti);

            Sample.GetVRI_FullSearh();
            array[1] = Sample.CodesVri;
            array[2] = Sample.Mathes;

            arrBool[0] = Sample.maintenance;
            arrBool[1] = Sample.accomplishment;
            arrBool[2] = Sample.linear;
            arrBool[3] = Sample.temporary;


            btiArrayStr[1] = bti.GetVri();
            btiArrayBool[4] = bti.CorrectedByBti;

            if (Sample.IsFastFederalSearch) arrInt[0] = 1; else arrInt[0] = 0;
            if (Sample.IsFastPZZSearch) arrInt[1] = 1; else arrInt[1] = 0;
            if (Sample.IsMainSearch) arrInt[2] = 1; else arrInt[2] = 0;
        }

        public static void SimpleSearch(string[] array)
        {
            var area = 0;
            Sorter Sample = new Sorter(array[0], area);
            Sample.SimpleSearch();
            array[1] = Sample.CodesVri;
            array[2] = Sample.Mathes;
        }

        public static void LinearAndTemporary(string inpt, bool[] ArrBool)
        {
            Sorter Sample = new Sorter(inpt, 0);
            ArrBool[0] = Sample.temporary;
            ArrBool[1] = Sample.linear;
        }

        public static void Placement(string inpt, string bti, string cad_num, string doc, bool[] ArrBool)
        {
            NodeFeed mf = new NodeFeed();
            var vri = "";
            var pathErrorLog = @"C:\Users\vtsvetkov\source\repos\Classifier\Classifier\ErrorLogs\placementError.txt";

            Regex regInput = new Regex(@"((?:\d+[.]*)+)", RegexOptions.IgnoreCase);
            Regex regPlacemant = new Regex(@"\bразмещен\w*\b", RegexOptions.IgnoreCase);
            Regex regMaintenance = new Regex(@"\bэксплуатац\w*\b", RegexOptions.IgnoreCase);

            try
            {
                MatchCollection matchesInpt = regInput.Matches(inpt);
                MatchCollection matchesBti = regInput.Matches(bti);

                var codesInpt = new List<string>();
                var codesBti = new List<string>();

                foreach (Match iter in matchesInpt)
                {
                    vri = iter.Groups[1].Value;
                    codesInpt.Add(vri);
                }

                foreach (Match iter in matchesBti)
                {
                    vri = iter.Groups[1].Value;
                    codesBti.Add(vri);
                }

                var conditionsBti = codesBti.Exists(p => p.Equals("2.0.0") ||
                    p.Equals("2.1.1.0") ||
                        p.Equals("2.5.0") ||
                            p.Equals("2.6.0")) && codesBti.Count() > 0;

                var condition = codesInpt.Exists(p => p.Equals("2.0.0") ||
                    p.Equals("2.1.1.0") || p.Equals("2.2.0") ||
                        p.Equals("2.5.0") || p.Equals("2.1.0") ||
                            p.Equals("2.6.0")) || codesInpt.Count() == 0;

                if (!condition && conditionsBti && regPlacemant.IsMatch(doc))
                {
                    ArrBool[0] = true;
                }
                else
                {
                    ArrBool[0] = false;
                }
                //if (!condition && conditionsBti && regMaintenance.IsMatch(doc))
                //{
                //    ArrBool[1] = true;
                //}
                //else
                //{
                //    ArrBool[1] = false;
                //}

            }
            catch (Exception e)
            {
                StreamWriter sw = File.AppendText(pathErrorLog);
                sw.WriteLine(cad_num + "    " + e);
                sw.WriteLine();
            }
        }

        public static string BtiFuncDoublicateDelete(string inputstr)
        {
            string pattern = @"((\b\w+\s).*)\2";
            bool isMatch = true;

            while (isMatch)
            {
                isMatch = Regex.IsMatch(inputstr, pattern, RegexOptions.IgnoreCase);
                inputstr = Regex.Replace(inputstr, pattern, "$1", RegexOptions.IgnoreCase);
            }
            return inputstr;
        }

        public static string BtiFunc(string inputstr)
        {
            var val = new HashSet<string>();

            MatchCollection matchCollection = Regex.Matches(inputstr, @"<([-\w\s]+)>", RegexOptions.IgnoreCase);

            foreach (Match iter in matchCollection)
            {
                val.Add(iter.Groups[1].Value);
            }

            var result = "";

            foreach (var iter in val)
            {
                result += result.Equals("") ? iter : ", " + iter;
            }
            return result;
        }

        public static string ExtendedFederalCode(string inputstr)
        {
            var val = new List<string>();

            MatchCollection matchCollection = Regex.Matches(inputstr, @"((?:\d+[.]*)+)", RegexOptions.IgnoreCase);

            foreach (Match iter in matchCollection)
            {
                if (iter.Groups[1].Value.Equals("3.5"))
                {
                    val.Add("3.5.1");
                    val.Add("3.5.2");
                    continue;
                }
                if (iter.Groups[1].Value.Equals("3.10"))
                {
                    val.Add("3.10.1");
                    val.Add("3.10.2");
                    continue;
                }
                if (iter.Groups[1].Value.Equals("6.7.1"))
                {
                    val.Add("6.7.0");
                    continue;
                }
                val.Add(iter.Groups[1].Value);
            }

            var result = "";

            foreach (var iter in val)
            {
                result += result.Equals("") ? iter : ", " + iter;
            }
            return result;
        }

        public static string ToQuadroCode(string[] array, int countBti, int fastFed)
        {
            var inputstr = array[0];

            var toilets = "3.1.3, 3.1.2, 3.1.1";
            if (inputstr.Contains(toilets))
            {
                var n = inputstr.IndexOf(toilets);
                inputstr = inputstr.Remove(n, toilets.Length) + ", 3.1.1";
                // inputstr += "3.1.1";
            }

            var mnstr = new List<Node>();
            NodeFeed mf = new NodeFeed();
            var vri = "";
            var set = new HashSet<String>();
            var mixedIndexSet = new HashSet<String>();
            var result = "";
            var mixedIndexResult = "";
            var types = new SortedSet<string>();//new HashSet<string>();
            var sorterList = new SortedSet<string>();
            var typesStr = "";


            MatchCollection matchCollection = Regex.Matches(inputstr, @"((?:\d+[.]*)+)", RegexOptions.IgnoreCase);
            try
            {
                foreach (Match iter in matchCollection)
                {
                    vri = iter.Groups[1].Value;

                    if ((vri.Equals("1.0.0") || vri.Equals("1.1.0") || vri.Equals("1.2.0")
                            || vri.Equals("1.3.0") || vri.Equals("1.4.0")
                                || vri.Equals("1.5.0") || vri.Equals("1.6.0") || vri.Equals("1.7.0")
                                    || vri.Equals("1.8.0") || vri.Equals("1.9.0")
                                        || vri.Equals("1.10.0") || vri.Equals("1.11.0")
                                            || vri.Equals("1.12.0") || vri.Equals("1.13.0")
                                                || vri.Equals("1.17.0") || vri.Equals("10.3.0")) && countBti > 0)
                    {
                        set.Add("3006");
                    }

                    else if ((vri.Equals("1.0.0") || vri.Equals("1.1.0") || vri.Equals("1.2.0")
                            || vri.Equals("1.3.0") || vri.Equals("1.4.0")
                                || vri.Equals("1.5.0") || vri.Equals("1.6.0") || vri.Equals("1.7.0")
                                    || vri.Equals("1.8.0") || vri.Equals("1.9.0")
                                        || vri.Equals("1.10.0") || vri.Equals("1.11.0")
                                            || vri.Equals("1.12.0") || vri.Equals("1.13.0")
                                                || vri.Equals("1.17.0") || vri.Equals("10.3.0")) && countBti == 0)
                    {
                        set.Add("3006");
                    }

                    else if (vri.Equals("7.2.1"))
                    {
                        set.Add("5000");
                    }

                    else if (vri.Equals("7.2.2") && fastFed == 1)
                    {
                        set.Add("5000");
                    }

                    else
                    {

                        Node m = mf.getM(vri);
                        foreach (var it in m.GetQCode())
                        {
                            set.Add(it);
                        }
                    }
                }
                foreach (var iter in set)
                {
                    if (set.Count() == 1)
                    {
                        if (iter.Equals("130"))
                        {
                            mixedIndexSet.Add("1300");
                        }
                        else mixedIndexSet.Add(iter);
                        result = iter;
                    }
                    else
                    {
                        result += result.Equals("") ? iter : ", " + iter;

                        if (iter.Equals("1000") || iter.Equals("1001") || iter.Equals("1002") || iter.Equals("1003")
                                    || iter.Equals("1004") || iter.Equals("1005") || iter.Equals("1006") || iter.Equals("1007"))
                        {
                            mixedIndexSet.Add("1000");
                        }

                        else if (iter.Equals("2000") || iter.Equals("2001") || iter.Equals("2002")
                                || iter.Equals("2003") || iter.Equals("2004"))
                        {
                            mixedIndexSet.Add("2000");
                        }

                        else if (iter.Equals("3000") || iter.Equals("3001") || iter.Equals("3002") || iter.Equals("3003")
                                    || iter.Equals("3004") || iter.Equals("3005"))
                        {
                            mixedIndexSet.Add("3000");
                        }

                        else if (iter.Equals("4000") || iter.Equals("4001") || iter.Equals("4002"))
                        {
                            mixedIndexSet.Add("4000");
                        }

                        else if (iter.Equals("130"))
                        {
                            mixedIndexSet.Add("1300");
                        }
                        else if (iter.Equals("3006"))
                        {
                            mixedIndexSet.Add("800");
                        }
                        else mixedIndexSet.Add(iter);
                    }
                }

                foreach (var mix in mixedIndexSet)
                {
                    mixedIndexResult += mixedIndexResult.Equals("") ? mix : ", " + mix;

                    if (mixedIndexSet.Count() == 1)
                    {
                        types.Add(mix.Remove(3, 1));
                    }
                    else
                    {
                        if (mix.Contains("100"))
                        {
                            sorterList.Add("1");
                        }
                        else if (mix.Contains("200"))
                        {
                            sorterList.Add("2");
                        }
                        else if (mix.Contains("300") && !mix.Contains("130"))
                        {
                            sorterList.Add("3");
                        }
                        else if (mix.Contains("400"))
                        {
                            sorterList.Add("4");
                        }
                        else if (mix.Contains("130"))
                        {
                            sorterList.Add("1");
                            sorterList.Add("3");
                        }
                        else types.Add(mix.Remove(3, 1));
                    }
                }

                //sorterList.Sort();
                //var distinctItems = sorterList.Distinct();
                var stringToTypes = "";
                if (sorterList.Count() > 0)
                {
                    foreach (var iter in sorterList)
                    {
                        stringToTypes += iter;
                    }

                    while (stringToTypes.Length < 3)
                    {
                        stringToTypes += "0";
                    }
                }
                types.Add(stringToTypes);

                foreach (var iter in types.OrderBy(p => p))
                {
                    typesStr += typesStr.Equals("") ? iter : ", " + iter;
                }

                array[1] = mixedIndexResult;
                array[2] = typesStr;
            }
            catch
            {
                return "";
            }
            return result;


        }
        /// <summary>
        /// Сортировка строки VRI_List по возрастанию значений кодов.
        /// </summary>
        /// <param name="inputstr"></param>
        /// <returns>Отсортированная строка кодов ВРИ</returns>
        public static void Sort(string[] array)
        {
            MatchCollection matchCollection = Regex.Matches(array[0], @"((?:\d+[.]*)+)", RegexOptions.IgnoreCase);

            var list = matchCollection.Cast<Match>().Select(match => match.Value).ToList();

            var comp = new VRI_Comparer();
            list.Sort(comp);

            string result = "";

            foreach (var val in list)
            {
                result += result.Equals("") ? val : ", " + val;
            }
            array[1] = result;
        }
    }
}
