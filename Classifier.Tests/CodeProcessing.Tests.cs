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
        public NodeFeed mf = new NodeFeed();


        [TestCase("4.3.0, 6.2.0", "4.3.0, 6.2.0")]
        [TestCase("6.2.0, 6.0.0", "6.2.0")]
        public void RemoveBaseCodes_Intersect_correctWork(string Codes, string exceptedCodes)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(Codes);
            ICodes excepted = new Codes(mf);
            excepted.AddNodes(exceptedCodes);
            CodeProcessing processing = new CodeProcessing(codes, new BTI(), "");

            processing.RemoveBaseCodes();

            Assert.AreEqual(excepted.ToString(), processing.Codes.ToString());
        }


        [TestCase("2.1.1.0, 2.5.0, 2.6.0", "2.6.0")]
        [TestCase("2.0.0, 2.1.1.0", "2.6.0")]
        [TestCase("2.0.0, 2.1.1.0, 2.5.0, 2.6.0, 3.1.1", "2.6.0, 3.1.1")]
        public void NumberDeterminant_BTI_HiLvlIsTrue_ReturnsCorrectResult(string Codes, string exceptedCodes)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(Codes);
            ICodes excepted = new Codes(mf);
            excepted.AddNodes(exceptedCodes);
            IBTI bti = new BTI("2.5.0, 2.6.0, 2.7.1.0" , false, false, true);
            CodeProcessing processing = new CodeProcessing(codes, bti, "");

            processing.NumberDeterminant();

            Assert.AreEqual(excepted.ToString(), processing.Codes.ToString());
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
            ICodes Codes = new Codes(mf);
            Codes.AddNodes("12.3.0");
            CodeProcessing processing = new CodeProcessing(Codes, buildings, "");
            ICodes result = new Codes(mf);
            result.AddNodes("2.6.0");

            processing.FixCode_Other();

            Assert.AreEqual(result.ToString(), Codes.ToString());
        }

        [Test]
        public void FixCode_Other_NotASingleOtherCode_RetunsCodesWithoutOther()
        {
            IBTI buildings = new BTI("2.6.0", false, false, true);
            ICodes Codes = new Codes(mf);
            Codes.AddNodes("4.9.0, 12.3.0");
            CodeProcessing processing = new CodeProcessing(Codes, buildings, "");
            ICodes result = new Codes(mf);
            result.AddNodes("4.9.0");

            processing.FixCode_Other();

            Assert.AreEqual(result.ToString(), Codes.ToString());
        }

        [Test]
        public void FixCode_Other_SingleOtherCodeBtiCodesIsEmpty_DoNothing()
        {
            IBTI buildings = new BTI("", false, false, true);
            ICodes Codes = new Codes(mf);
            Codes.AddNodes("12.3.0");
            ICodes excepted = new Codes(mf);
            excepted.AddNodes("12.3.0");
            CodeProcessing processing = new CodeProcessing(Codes, buildings, "");

            processing.FixCode_Other();

            Assert.AreEqual(excepted.ToString(), Codes.ToString());
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

        [Test]
        public void Type230Fix_3004Exist_RemoveCodes3004()
        {
            ICodes Codes = new Codes(mf);
            Codes.AddNodes("2.0.0, 3.1.1, 2.7.1.0, 4.9.0, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1");
            ICodes excepted = new Codes(mf);
            excepted.AddNodes("2.0.0");
            CodeProcessing processing = new CodeProcessing(Codes, new BTI(), "");

            processing.Type230Fix();

            Assert.AreEqual(excepted.ToString(), Codes.ToString());
        }

        [Test]
        public void Type230Fix_3004IsNotExist_DoNothing()
        {
            ICodes Codes = new Codes(mf);
            Codes.AddNodes("2.0.0");
            ICodes excepted = new Codes(mf);
            excepted.AddNodes("2.0.0");
            CodeProcessing processing = new CodeProcessing(Codes, new BTI(), "");

            processing.Type230Fix();

            Assert.AreEqual(excepted.ToString(), Codes.ToString());
        }

        [Test]
        public void Type230Fix_HousingCodesIsNotExist_DoNothing()
        {
            ICodes Codes = new Codes(mf);
            Codes.AddNodes("3.1.1, 2.7.1.0, 4.9.0, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4");
            ICodes excepted = new Codes(mf);
            excepted.AddNodes("3.1.1, 2.7.1.0, 4.9.0, 4.9.1.1, 4.9.1.2, 4.9.1.3, 4.9.1.4");
            CodeProcessing processing = new CodeProcessing(Codes, new BTI(), "");

            processing.Type230Fix();

            Assert.AreEqual(excepted.ToString(), Codes.ToString());
        }
    }
}
