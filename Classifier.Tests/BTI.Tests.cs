using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    class BTITests
    {
        private static object[] ConvertStringOfCodesToList_sourseList =
            {new object[] { "2.1.1.0, 2.5.0, 2.6.0" , new List<string> { "2.1.1.0", "2.5.0", "2.6.0" } },
             new object[] { "fake, 4.0, 2.6.0" , new List<string> {"2.6.0" } } };

        [TestCaseSource("ConvertStringOfCodesToList_sourseList")]
        public void ConvertStringOfCodesToList_correctInputString_ReturnsCorrectResult(string input, List<string> output)
        {
            IBTI bti = new BTI(input, false, false, false);

            CollectionAssert.AreEqual(output, bti.btiCodes);
        }
    }
}
