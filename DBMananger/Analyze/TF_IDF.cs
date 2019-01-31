using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DBMananger.Analyze
{
    static class TF_IDF
    {
        /// <summary>
        /// Term Frequency * IDF
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public static Dictionary<Token, double> NoNormalyzeTF_IDF
                                        (List<Token> document, List<List<Token>> documents)
        {
            var countOfTokens = document.Select(p => p.Value).Distinct().Count();
            var tokensInDoc = 0.0;

            var tf_idf_koeff = 0.0;
            var result = new Dictionary<Token, double>();

            IEnumerable<Token> noduplicates = document.Distinct(new TokenComparer());

            foreach (var token in noduplicates)
            {
                tokensInDoc = document.Where(p => p.Equals(token)).Count();
                tf_idf_koeff = tokensInDoc / countOfTokens * IDF(token, documents);
                result.Add(token, tf_idf_koeff);
            }
            return result;
        }

        /// <summary>
        /// Double nomalyzed Term Frequency * IDF
        /// </summary>
        /// <param name="document"></param>
        /// <param name="documents"></param>
        /// <returns></returns>
        public static Dictionary<Token, double> DoubleNormalyzedTF_IDF
                                        (List<Token> document, List<List<Token>> documents)
        {
            var countOfTokens = document.Select(p => p.Value).Distinct().Count();
            var tokensInDoc = 0.0;
            var maxFreq = 0.0;

            var tf_idf_koeff = 0.0;
            var result = new Dictionary<Token, double>();
            IEnumerable<Token> noduplicates = document.Distinct(new TokenComparer());

            foreach (var token in noduplicates)
            {
                tokensInDoc = document.Where(p => p.Equals(token)).Count();
                if (tokensInDoc / countOfTokens > maxFreq) maxFreq = tokensInDoc / countOfTokens;
            }

            foreach (var token in noduplicates)
            {
                tokensInDoc = document.Where(p => p.Equals(token)).Count();
                tf_idf_koeff = (0.5 + 0.5 * (tokensInDoc / countOfTokens / maxFreq)) * IDF(token, documents);
                result.Add(token, tf_idf_koeff);
            }
            return result;
        }

        /// <summary>
        /// Inverse Document Frequency IDF
        /// </summary>
        /// <param name="token"></param>
        /// <param name="documents"></param>
        /// <returns></returns>
        public static double IDF(Token token, List<List<Token>> documents)
        {
            int sum = 0;
            foreach (var doc in documents)
            {
                if (doc.Exists(p => p.Equals(token))) sum++;
            }
            return Math.Log10((double)documents.Count / sum);
        }
    }
}
