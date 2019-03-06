using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    public class SearchCodesTests
    {
        [Test]
        public void FederalSearchPattern_WhenCalled_RetunsCorrectedResult()
        {
            var search = new Classifier.SearchCodes("");

            
        }

        [TestCase("магазины(4.4); общественное питание(4.6); развлечения(4.8)", "4.4.0, 4.6.0, 4.8.0")]
        [TestCase("магазины 4.4; общественное питание(4.6.0); развлечения(4.8.0)", "")]
        [TestCase("магазины 4.4; общественное питание 4.6; развлечения 4.8", "")]
        [TestCase("", "")]
        public void SearchFederalCodes_ReturnsCorrectedResult(string input, string output)
        {
            var Sample = new Classifier.SearchCodes(input);
            Sample.Codes.Add("1.14.0");
            Sample.Codes.Add("2.2.0");
            Sample.AdddMatches("JUST TEST THIS SH~");

            Sample.SearchFederalCodes();
                        
            var result = "";
            foreach (var code in Sample.Codes)
            {
                result += (result.Length == 0) ? code : ", " + code;
            }

            Assert.AreEqual(result, output);
        }

        [TestCase("shops", "shops")]
        public void AdddMatches_AddSomeStringToEmptyMatches_ReturnCorrectResult(string input, string output)
        {
            var Sample = new Classifier.SearchCodes("");

            Sample.AdddMatches(input);

            Assert.AreEqual(Sample.Matches.ToString(), output);
        }

        [TestCase("shops", "current match, shops")]
        public void AdddMatches_AddSomeStringToNotEmptyMatches_ReturnCorrectResult(string input, string output)
        {
            var Sample = new Classifier.SearchCodes("");

            Sample.AdddMatches("current match");
            Sample.AdddMatches(input);

            Assert.AreEqual(Sample.Matches.ToString(), output);
        }

        [Test]
        public void AdddMatches_AddEmptyString_DoNothing()
        {
            var Sample = new Classifier.SearchCodes("");
            Sample.AdddMatches("some match");

            Sample.AdddMatches("");

            Assert.AreEqual(Sample.Matches.ToString(), "some match");
        }

        [Test]
        public void ClearOutputFilds__WorkingCorrect()
        {
            var Sample = new Classifier.SearchCodes("");

            Sample.AdddMatches("some match");
            Sample.Codes.Add("1.1.0");
            

            Sample.ClearOutputFields();

            Assert.AreEqual(Sample.Matches.ToString(), "");
            Assert.AreEqual(Sample.Codes.Count, 0);
        }        
        
        [TestCase("13.3.0 - Размещение жилого дачного дома (не предназначенного для раздела на " +
            "квартиры, пригодного для отдыха и проживания, высотой не выше трех", "13.3.0", "13.3.0")]
        [Category("MainLoop.PZZSearch")]
        public void MainLoop_PZZSearchTest_ReturnsCorrectResult(string input, string expMatches, string expVRI_List)
        {
            var Sample = new Classifier.SearchCodes(input);
            var Vri_List = "";

            Sample.MainLoop();
            foreach (var code in Sample.Codes)
            {
                Vri_List += (Vri_List.Length == 0) ? code : ", " + code;
            }
        }
        
        [TestCase("   13.3.0 - Размещение жилого дачного дома (не предназначенного для раздела на " +
            "квартиры, пригодного для отдыха и проживания, высотой не выше трех", "13.3.0", "13.3.0")]
        [TestCase("13 . 3 . 0 - Размещение жилого дачного дома (не предназначенного для раздела на " +
            "квартиры, пригодного для отдыха и проживания, высотой не выше трех", "13.3.0", "13.3.0")]
        [TestCase("Размещение жилого дачного дома 13.3.0 (не предназначенного для раздела на " +
            "квартиры, пригодного для отдыха и проживания, высотой не выше трех", "", "")]
        [Category("MainLoop.PZZSearch")]
        public void MainLoop_PZZSearchTest_TestPatternPosition(string input, string expMatches, string expVRI_List)
        {
            var Sample = new SearchCodes(input);
            var Vri_List = "";

            Sample.MainLoop();
            foreach (var code in Sample.Codes)
            {
                Vri_List += (Vri_List.Length == 0) ? code : ", " + code;
            }
        }

        [TestCase("водозаборный узел газопровод", "3.1.1", "водозаборный")]
        [TestCase("фотоателье дом быта", "3.3.0", "дом быта")]
        public void regexpPatternSearch_regexpPatternsLoopBreak_MatchesEqualFirstMatch(string input, string vri, string match)
        {
            var Sample = new SearchCodes(input);
            var node = new NodeFeed().getM(vri);

            Sample.regexpPatternsSearch(node);

            Assert.AreEqual(match, Sample.Matches.ToString());
        }
    }

    [TestFixture]
    public class ISearchCodesTests
    {
        [TestCase("Размещение жилого дачного дома 13.3.0 (не предназначенного для раздела на " +
            "квартиры, пригодного для отдыха и проживания, высотой не выше трех", "13.3.0")]
        [TestCase("эксплуатации издательства", "6.11.0")]
        [TestCase("Благоустройство территории", "12.0.1")]
        public void ISearchCodes_MainLoop_ReturnsCorrectVRI(string input, string vri)
        {
            ISearchCodes Sample = new SearchCodes(input);
            var Vri_List = "";

            Sample.MainLoop();
            foreach (var code in Sample.Codes)
            {
                Vri_List += (Vri_List.Length == 0) ? code : ", " + code;
            }
        }

        [TestCase("магазины(4.4); общественное питание(4.6); развлечения(4.8)", true)]
        public void ISearch_IsFederalSearch_ReturnsTrue(string input, bool expected)
        {
            ISearchCodes Sample = new SearchCodes(input);

            Sample.MainLoop();

            Assert.AreEqual(expected, Sample.IsFederalSearch);
        }
    }
}
