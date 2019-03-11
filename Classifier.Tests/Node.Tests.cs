using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    public class NodeTests
    {
        public NodeFeed mf = new NodeFeed();

        [Test]
        public void GetParentTest()
        {
            var child = mf.getM("2.5.0");
            var parent = mf.getM("2.0.0");
            Assert.True(child.GetParent(parent));
        }

        [TestCase("3.5", "3.5.1.0, 3.5.2.0" )]
        [TestCase("3.2", "3.2.1, 3.2.2, 3.2.3, 3.2.4" )]
        public void EmptyVRI_WhenCalled_ReturnsCorrectResult(string fCode, string vriCodes)
        {
            var node = mf.GetNodeBasedFCode(fCode);            

            var result = node.EmptyVRI();
            var codes = new Codes(mf);
            codes.AddNodes(result);
            

            Assert.AreEqual(codes.ToString(), vriCodes);
        }

        [Test]
        public void regexpPatterns_ForAllNodes_ArrayCountIsEven()
        {            
            foreach (var node in mf.GetNodes())
            {
                bool isEven = node.regexpPatterns.Count() % 2 == 0;
                Assert.AreEqual(isEven, true);

            }
        }

        [Test]
        public void regexpPatterns_ForAllNodes_PositivePaatternsIsNotNull()
        {
            var pattern = "";
            bool isEmpty = false;

            foreach (var node in mf.GetNodes())
            {
                for (int i = 1; i < node.regexpPatterns.Length; i += 2)
                {
                    pattern = node.regexpPatterns[i];
                    if (string.IsNullOrEmpty(pattern) || string.IsNullOrWhiteSpace(pattern))
                        isEmpty = true;
                }

            }

            Assert.AreEqual(isEmpty, false);
        }
    }

    [TestFixture]
    public class NodeFeedTests
    {

    }
}
