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
            //Bti bti = new Bti(btiArrayStr[0], btiCount, btiArrayBool[0], btiArrayBool[1], btiArrayBool[2], btiArrayBool[3]);

            //var Sample = new Sorter(array[0], ZU_area, bti);

            //Sample.GetVRI_FullSearh();
            //array[1] = Sample.CodesVri;
            //array[2] = Sample.Mathes;

            //arrBool[0] = Sample.maintenance;
            //arrBool[1] = Sample.accomplishment;
            //arrBool[2] = Sample.linear;
            //arrBool[3] = Sample.temporary;


            //btiArrayStr[1] = bti.GetVri();
            //btiArrayBool[4] = bti.CorrectedByBti;

            //if (Sample.IsFastFederalSearch) arrInt[0] = 1; else arrInt[0] = 0;
            //if (Sample.IsFastPZZSearch) arrInt[1] = 1; else arrInt[1] = 0;
            //if (Sample.IsMainSearch) arrInt[2] = 1; else arrInt[2] = 0;

            //var success = int.TryParse(Sample.Type(), out int type);
            //if (success)
            //{
            //    arrInt[3] = type;
            //}
            //else arrInt[3] = 0;
            //success = int.TryParse(Sample.Kind(), out int kind);
            //if (success)
            //{
            //    arrInt[4] = kind;
            //}
            //else arrInt[4] = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arrStr"></param>
        /// <param name="arrInt"></param>
        /// <param name="arrBool"></param>
        /// <remark>
        /// arrStr[0] - vry_doc
        /// arrStr[1] - vriBti
        /// arrStr[2] - VRI_List
        /// arrStr[3] - Matches
        /// arrInt[0] - Area
        /// arrInt[1] - Type
        /// arrInt[2] - Kind
        /// arrBool[0] - lo_lvl
        /// arrBool[1] - mid_lvl
        /// arrBool[2] - hi_lvl
        /// arrBool[3] - Maintenance;
        /// arrBool[4] - Landscape;
        /// arrBool[5] - FederalSearch;
        /// </remark> 
        public static void FullSearch(string[] arrStr, int[] arrInt, bool[] arrBool)
        {
            IFactory factory = new Factory(new InputData(arrStr[0], arrInt[0], arrStr[1], arrBool[0], arrBool[1], arrBool[2]));
            factory.Execute();

            arrStr[2] = factory.outputData.VRI_List;
            arrStr[3] = factory.outputData.Matches;

            arrBool[3] = factory.outputData.IsMaintenance;
            arrBool[4] = factory.outputData.IsLandscape;
            arrBool[5] = factory.outputData.IsFederalSearch;

            arrInt[1] = factory.outputData.Type;
            arrInt[2] = factory.outputData.Kind;
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

        /// <summary>
        /// Сортировка строки VRI_List по возрастанию значений кодов.
        /// </summary>
        /// <param name="array">[0] - строка, [1] - результат</param>
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

        public static bool isCadNum(string input)
        {
            return Regex.IsMatch(input, @"\d+:\d+:\d+:\d+");
        }

        /// <summary>
        /// Сортировка строки вида "2b 2c 2a 1c 2h"
        /// </summary>
        /// <param name="input"></param>
        /// <returns>string result</returns>
        public static string sortStrings(string input)
        {
            string result = "";

            var match = Regex.Matches(input, @"\b\d\w\b", RegexOptions.Compiled);

            if (input.Length > 0 && match.Count == 0)
            {
                result = "Unexpected format of input string";
            }

            var list = match.Cast<Match>().Select(p => p.Value).ToList();

            list.Sort();

            foreach (var val in list)
            {
                result += val + " ";
            }
            result = result.TrimEnd();
            return result;
        }
    }
}
