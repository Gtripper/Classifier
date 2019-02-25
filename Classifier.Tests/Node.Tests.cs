using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    public class NodeTests
    {
        [Test]
        public void GetParentTest()
        {
            var child = new NodeFeed().getM("2.5.0");
            var parent = new NodeFeed().getM("2.0.0");
            Assert.True(child.GetParent(parent));
        }

        [TestCase("3.5", new string[] { "3.5.1.0", "3.5.2.0" })]
        [TestCase("3.2", new string[] { "3.2.1", "3.2.2", "3.2.3", "3.2.4" })]
        public void EmptyVRI_WhenCalled_ReturnsCorrectResult(string fCode, string[] vriCodes)
        {
            var node = new NodeFeed().GetNodeBasedFCode(fCode);

            var result = node.EmptyVRI();

            CollectionAssert.AreEqual(result, vriCodes);
        }

        [Test]
        public void regexpPatterns_ForAllNodes_ArrayCountIsEven()
        {
            var mf = new Classifier.NodeFeed().GetNodes();
            foreach (var node in mf)
            {
                bool isEven = node.regexpPatterns.Count() % 2 == 0;
                Assert.AreEqual(isEven, true);

            }
        }

        [Test]
        public void regexpPatterns_ForAllNodes_PositivePaatternsIsNotNull()
        {
            var mf = new Classifier.NodeFeed().GetNodes();
            var pattern = "";
            bool isEmpty = false;

            foreach (var node in mf)
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
