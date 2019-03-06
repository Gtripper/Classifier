using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    class CodeProcessingTests
    {
        private static object[] RemoveBaseCodes_sourseList =
            {new object[] { new List<string> { "6.2.0", "4.3.0" }, new List<string> { "6.2.0", "4.3.0" } },
             new object[] { new List<string> { "6.2.0", "6.0.0" }, new List<string> { "6.2.0" } } };

        [TestCaseSource("RemoveBaseCodes_sourseList")]
        public void RemoveBaseCodes_Intersect_correctWork(List<string> Codes, List<string> expected)
        {
            CodeProcessing processing = new CodeProcessing(Codes, new BTI(), "");

            processing.RemoveBaseCodes();

            CollectionAssert.AreEqual(expected, Codes);
        }

        private static object[] NumberDeterminant_sourseList =            
            {new object[] { new List<string> { "2.1.1.0", "2.5.0", "2.6.0" }, new List<string> { "2.6.0" } },
             new object[] { new List<string> { "2.0.0", "2.1.1.0" }, new List<string> { "2.6.0" } }, 
             new object[] { new List<string> { "2.0.0", "2.1.1.0", "2.5.0", "2.6.0", "3.1.1" }, new List<string> { "2.6.0", "3.1.1" } } };

        [TestCaseSource("NumberDeterminant_sourseList")]
        public void NumberDeterminant_BTI_HiLvlIsTrue_ReturnsCorrectResult(List<string> Codes, List<string> expected)
        {
            IBTI bti = new BTI("2.5.0, 2.6.0, 2.7.1.0" , false, false, true);
            CodeProcessing processing = new CodeProcessing(Codes, bti, "");

            processing.NumberDeterminant();

            CollectionAssert.AreEqual(expected, Codes);
        }

        [TestCase("эксплуатации части здания под медицинские цели", "2.1.1.0, 2.5.0, 2.6.0")]
        public void _Maintenance_RealZonesFromMaintenanceMap_ReturnsTrue(string input, string btiCodes)
        {
            IBTI buiding = new BTI(btiCodes, false, false, true);
            ISearchCodes searchResult = new SearchCodes(input);
            CodeProcessing processing = new CodeProcessing(searchResult.Codes, buiding, input);

            var result = processing._maintenance();

            Assert.AreEqual(true, result);
        }

        [TestCase("части здания под медицинские цели", "2.1.1.0, 2.5.0, 2.6.0")]
        public void _Maintenance_InputStringIsNotMaintenance_ReturnsFalse(string input, string btiCodes)
        {
            IBTI buiding = new BTI(btiCodes, false, false, true);
            ISearchCodes searchResult = new SearchCodes(input);
            CodeProcessing processing = new CodeProcessing(searchResult.Codes, buiding, input);

            var result = processing._maintenance();

            Assert.AreEqual(false, result);
        }

        [Test]
        public void FixCode_Other_SingleOtherCodeBTICodeNotNull_RetunsBTICodes()
        {
            IBTI buildings = new BTI("2.6.0", false, false, true);
            var Codes = new List<string> { "12.3.0" };
            CodeProcessing processing = new CodeProcessing(Codes, buildings, "");
            var result = new List<string> { "2.6.0" };

            processing.FixCode_Other();

            CollectionAssert.AreEqual(result, processing.Codes);
        }

        [Test]
        public void FixCode_Other_NotASingleOtherCode_RetunsCodesWithoutOther()
        {
            IBTI buildings = new BTI("2.6.0", false, false, true);
            var Codes = new List<string> { "4.9.0", "12.3.0" };
            CodeProcessing processing = new CodeProcessing(Codes, buildings, "");
            var result = new List<string> { "4.9.0" };

            processing.FixCode_Other();

            CollectionAssert.AreEqual(result, processing.Codes);
        }

        [Test]
        public void FixCode_Other_SingleOtherCodeBtiCodesIsEmpty_DoNothing()
        {
            IBTI buildings = new BTI("", false, false, true);
            var Codes = new List<string> { "12.3.0" };
            CodeProcessing processing = new CodeProcessing(Codes, buildings, "");
            var result = new List<string> { "12.3.0" };

            processing.FixCode_Other();

            CollectionAssert.AreEqual(result, processing.Codes);
        }

        [Test]
        public void Landscaping_SingleLandscapeCode_returnsTrue()
        {
            IBTI bti = new BTI();
            string input = "Благоустройство территории";
            ISearchCodes searchCodes = new SearchCodes(input);
            searchCodes.MainLoop();
            CodeProcessing processing = new CodeProcessing(searchCodes.Codes, bti, input);

            processing._landscaping();

            Assert.AreEqual(true, processing.Landscaping);
        }

        [Test]
        public void Landscaping_NotSingleLandscapeCode_returnsFalse()
        {
            IBTI bti = new BTI();
            string input = "Благоустройство территории и гараж";
            ISearchCodes searchCodes = new SearchCodes(input);
            searchCodes.MainLoop();
            CodeProcessing processing = new CodeProcessing(searchCodes.Codes, bti, input);

            processing._landscaping();

            Assert.AreEqual(false, processing.Landscaping);
        }
    }
}
