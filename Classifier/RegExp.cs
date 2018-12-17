using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Classifier
{
    class RegExp
    {
        private readonly string[] RegExpArray;
        private readonly string simpleDescription;
        private readonly string vri540;
        private readonly string vri;
        private string input;
        private Match value;
        private bool fastFederalSearch;
        private bool fastPZZSearch;
        private bool mainSearch;

        public RegExp(string[] RegExpArray, string simpleDescription, string vri540, string vri, string input)
        {
            this.RegExpArray = RegExpArray;
            this.simpleDescription = simpleDescription;
            this.vri540 = vri540;
            this.vri = vri;
            this.input = input;
            fastFederalSearch = false;
            fastPZZSearch = false;
            mainSearch = false;
        }

        public RegExp(string[] RegExpArray, string input)
        {
            simpleDescription = null;
            vri540 = null;
            vri = null;
            this.RegExpArray = RegExpArray;
            this.input = input;
            fastFederalSearch = false;
            fastPZZSearch = false;
            mainSearch = false;
        }

        public RegExp(Node mnstr, string input)
        {
            this.input = input;
            RegExpArray = mnstr.GetPatterns();
            vri540 = mnstr.Vri540;
            vri = mnstr.Vri;
            simpleDescription = mnstr.GetSimpleDescription();
            fastFederalSearch = false;
            fastPZZSearch = false;
            mainSearch = false;
        }

        public RegExp(Node mnstr)
        {
            input = "";
            RegExpArray = mnstr.GetPatterns();
            vri540 = mnstr.Vri540;
            vri = mnstr.Vri;
            simpleDescription = mnstr.GetSimpleDescription();
            fastFederalSearch = false;
            fastPZZSearch = false;
            mainSearch = false;
        }

        public bool IsFastFederalSearch()
        {
            return fastFederalSearch;
        }

        public bool IsFastPZZSearch()
        {
            return fastPZZSearch;
        }

        public bool IsMainSearch()
        {
            return mainSearch;
        }

        public Match Value()
        {
            return value;
        }

        #region MainSearch
        private Match Cycle()
        {
            for (int i = 0; i < RegExpArray.Length; i += 2)
            {
                var val = GetMatch(RegExpArray[i], RegExpArray[i + 1]);

                if (val != null)
                {
                    return val;
                }
            }
            return null;
        }

        private Match GetMatch(string neagtivePatern, string positivePattern)
        {
            if (!neagtivePatern.Equals(""))
            {
                return NegativePatternIsNotNull(neagtivePatern, positivePattern);
            }
            else
            {
                return NegativePatternIsNull(positivePattern);
            }


        }

        private Match NegativePatternIsNotNull(string neagtivePatern, string positivePattern)
        {
            var excluded = Regex.IsMatch(input, neagtivePatern, RegexOptions.IgnoreCase);
            var included = Regex.IsMatch(input, positivePattern, RegexOptions.IgnoreCase);

            if (!excluded & included)
            {
                var val = Regex.Match(input, positivePattern, RegexOptions.IgnoreCase);
                return val;
            }
            return null;
        }

        private Match NegativePatternIsNull(string positivePattern)
        {
            var included = Regex.IsMatch(input, positivePattern, RegexOptions.IgnoreCase);

            if (included)
            {
                var val = Regex.Match(input, positivePattern, RegexOptions.IgnoreCase);
                return val;
            }
            return null;
        }
        
        private Match FederalSearh()            
        {
            //var pattern = @"[-\s]*\b" + simpleDescription + @"\b[-\s]*[(]" + vri540 +
            //        @"[)]|[-\s]*\b" + simpleDescription + @"[-\s]|[-\s]*[(]" + vri540 + @"[)][-\s]*";


            var afterReplace = Regex.Replace(vri540, @"[.]", @"\s*[.]\s*",RegexOptions.IgnoreCase);

            var pattern = @"[(]\s*" + afterReplace + @"\s*[)]";

            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            if (regex.IsMatch(input))
            {
                var val = regex.Match(input);
                return val;
            }
            return null;
        }
        
        private Match PZZSearh()
        {
            var pattern = @"[-\s]*[(]" + vri + @"[)][-\s]*";           
            
            Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);

            if (!vri.Equals("") && regex.IsMatch(input))
            {
                var val = regex.Match(input);
                return val;
            }
            return null;
        }

        #endregion

        public void FullSearch()
        {
            if (FederalSearh() != null)
            {
                value = FederalSearh();
                fastFederalSearch = true;
            }

            else if (PZZSearh() != null)
            {
                value = PZZSearh();
                fastPZZSearch = true;
            }

            else
                value = Cycle();

                if (value != null) mainSearch = true;            
        }

        public void MainSearch()
        {
            value = Cycle();

            if (value != null) mainSearch = true;
        }

        public bool OnlyFederalSearch()
        {
            if (FederalSearh() != null)
            {
                value = FederalSearh();
                fastFederalSearch = true;
                return true;
            }
            else return false;
        }
    }


}
