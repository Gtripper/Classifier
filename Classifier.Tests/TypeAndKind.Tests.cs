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
            var result = (ICodesTypes)codes;
            ITypeAndKind type = new TypeAndKind(result);
            return type;
        }

        [TestCase("2.6.0, 4.4.0", 120)]
        [TestCase("2.6.0, 3.1.1", 230)]
        [TestCase("2.6.0, 9.3.0", 999)]
        public void ITypeAndKind_CheckCorrectType(string vri, int type)
        {
            var codes = Maker(vri);

            var res = codes.Type;

            Assert.AreEqual(res, type);
        }
    }
}
