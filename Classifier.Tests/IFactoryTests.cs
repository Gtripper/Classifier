using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Classifier.Tests
{
    [TestFixture]
    class IFactoryTests
    {
        [TestCase("объекты размещения помещений и технических устройств  крытых спортивных сооружений массового посещения (1.2.17); объекты размещения досугов",
            37181, "3.1.1", false, false, false, "3.6.1, 5.1.1", 100, 1000)]
        public void IFactory_FullDataTest(string _vri_doc, int _area, string _btiVri, bool _lo, bool _mid, bool _hi, string vri_list, int type, int kind)
        {
            IInputData data = new InputData(_vri_doc, _area, _btiVri, _lo, _mid, _hi);
            IFactory factory = new Factory(data);

            factory.Execute();

            Assert.AreEqual(factory.outputData.VRI_List, vri_list);
            Assert.AreEqual(factory.outputData.Type, type);
            Assert.AreEqual(factory.outputData.Kind, kind);
        }
    }
}
