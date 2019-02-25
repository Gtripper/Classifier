using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    /// <summary>
    /// Содержит методы обработки кодов ПЗЗ
    /// </summary>
    class CodeProcessing
    {
        public List<string> Codes { get; private set; }

        public CodeProcessing(List<string> _Codes)
        {
            // Проверка на null
            Codes = _Codes ?? new List<string>();
        }

        /// <summary>
        /// Удаление базовых кодов при наличии уточняющих
        /// </summary>
        /// <remark>
        /// Например если есть коды {6.0.0, 6.2.0} на 
        /// выходе должен остаться только индекс {6.2.0}
        /// </remark>
        internal void RemoveBaseCodes()
        {
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
                    "3.6.2", "3.6.3", "3.7.1", "3.7.2", "3.8.1", "3.8.2", "3.8.3", "3.9.2", "3.10.1.0",
                        "3.10.2.0"});
            baseCodes.Add("3.4.0", new List<string> { "3.4.1.0", "3.4.2.0" });
            baseCodes.Add("4.0.0", new List<string> { "4.1.0", "4.2.0", "4.3.0", "4.4.0", "4.5.0",
                "4.6.0", "4.8.0", "4.9.0", "4.10.0" });
            baseCodes.Add("5.0.1", new List<string> { "5.0.2", "5.1.1", "5.1.2", "5.1.3", "5.1.4",
                "5.1.5", "5.2.1.0", "5.2.0", "5.3.0", "5.4.0", "5.5.0" });
            baseCodes.Add("6.0.0", new List<string> { "6.2.0", "6.2.1.0", "6.3.0", "6.3.1.0",
                "6.4.0", "6.5.0", "6.6.0", "6.7.0", "6.8.0", "6.11.0" });            
        }
    }
}
