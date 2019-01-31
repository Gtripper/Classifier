using NUnit.Framework;
using Classifier;

namespace Tests
{
    public class NodeTest
    {
        [SetUp]
        public void Setup()
        {
            
        }

        [Test]
        public void GetParentTest()
        {
            var child = new NodeFeed().getM("2.5.0");
            var parent = new NodeFeed().getM("2.0.0");
            Assert.True(child.GetParent(parent));
        }
    }
}