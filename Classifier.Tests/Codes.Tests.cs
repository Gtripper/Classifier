using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using NSubstitute;

namespace Classifier.Tests
{
    [TestFixture]
    class CodesTests
    {
        public NodeFeed mf = new NodeFeed();

        [TestCase("3.1.1, 2.6.0", "2.6.0, 3.1.1")]
        [TestCase("3.1.1, 4.6.0", "3.1.1, 4.6.0")]
        public void Sort_unSortedList_toSortedResult(string unsortedCodes, string sortedCodes)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(unsortedCodes);

            codes.Sort();

            Assert.AreEqual(codes.ToString(), sortedCodes);
        }

        [Test]
        public void Sort_CodesIsEmpty_CorrectWorks()
        {
            ICodes codes = new Codes(mf);

            codes.Sort();

            Assert.AreEqual(codes.ToString(), "");
        }

        [TestCase("2.5.0, 2.6.0, 3.1.1, 2.7.0", "3.1.1, 2.7.0", "2.5.0, 2.6.0")]
        [TestCase("", "3.1.1, 2.7.0", "")]
        [TestCase("2.5.0, 2.6.0, 3.1.1, 2.7.0", "qwerty", "2.5.0, 2.6.0, 2.7.0, 3.1.1")]
        public void RevmoveAll_StringInput_CorrectRemove(string codesVri, string codesRem, string expected)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(codesVri);

            codes.RemoveAll(codesRem);

            Assert.AreEqual(expected, codes.ToString());
        }
    }
}
