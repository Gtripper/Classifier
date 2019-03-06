using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Classifier
{
    public interface IBTI
    {
        List<string> btiCodes { get; }
        bool Lo_lvl { get; }
        bool Mid_lvl { get; }
        bool Hi_lvl { get; }
    }

    /// <summary>
    /// 
    /// </summary>
    class BTI : IBTI
    {
        public List<string> btiCodes { get; }
        public bool Lo_lvl { get; }
        public bool Mid_lvl { get; }
        public bool Hi_lvl { get; }

        public BTI()
        {
            btiCodes = new List<string>();
            Lo_lvl = false;
            Mid_lvl = false;
            Hi_lvl = false;
        }
        /// <summary>
        /// Конструктор объекта БТИ
        /// </summary>
        /// <param name="codes"></param>
        /// <param name="lo"></param>
        /// <param name="mid"></param>
        /// <param name="hi"></param>
        public BTI(string _codes, bool _lo, bool _mid, bool _hi)
        {
            btiCodes = ConvertStringOfCodesToList(_codes);
            Lo_lvl = _lo;
            Mid_lvl = _mid;
            Hi_lvl = _hi;
        }

        internal List<string> ConvertStringOfCodesToList(string inputCodes)
        {
            var pattern = @"\d+[.]\d+[.]\d+([.]\d+)?";             
            var result = Regex.Matches(inputCodes, pattern).Cast<Match>().Select(p => p.Value).ToList();
            return result;
        }
    }
}
