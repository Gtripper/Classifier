using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    class TypeAndKindTests
    {
        NodeFeed mf = new NodeFeed();

        public ITypeAndKind Maker(string vri)
        {
            ICodes codes = new Codes(mf);
            codes.AddNodes(vri);
            ITypeAndKind type = new TypeAndKind(codes);
            return type;
        }

        [TestCase("2.6.0, 4.4.0", 120)]
        [TestCase("3.2.1, 3.2.2, 3.2.3, 3.2.4", 100)]
        [TestCase("2.6.0, 3.1.1", 230)]
        [TestCase("2.6.0, 9.3.0", 999)]
        [TestCase("7.4.1, 7.4.2", 300)]
        [TestCase("", 333)]
        public void ITypeAndKind_CheckCorrectType(string vri, int type)
        {
            var codes = Maker(vri);

            var res = codes.Type;

            Assert.AreEqual(res, type);
        }


        [TestCase("3.2.1, 3.2.2, 3.2.3, 3.2.4", 1000)]
        [TestCase("3.6.1, 5.1.3", 1000)]
        [TestCase("", 333)]
        public void ITypeAndKind_CheckCorrectKind(string vri, int kind)
        {
            var codes = Maker(vri);

            var res = codes.Kind;

            Assert.AreEqual(res, kind);
        }
    }
}
