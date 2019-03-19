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
        [TestCase("автомобильный транспорт (7.2) (земельные участки, предназначенные для разработки полезных ископаемых, размещения железнодорожных путей, ав",
            76, "6.7.0, 7.1.2", false, false, false, "7.2.1", 500, 5000)]
        [TestCase("эксплуатации прогулочной площадки детского сада",
            1416, "", false, false, false, "12.0.1", 400, 4001)]
        [TestCase("земельные участки, предназначенные для размещения газопроводов (1.2.13), земельные участки, занятые особо охраняемыми территориями и объект",
            4, "", false, false, false, "3.1.1", 300, 3004)]
        public void IFactory_FullDataTest(string _vri_doc, int _area, string _btiVri, bool _lo, bool _mid, bool _hi, string vri_list, int type, int kind)
        {
            IInputData data = new InputData(_vri_doc, _area, _btiVri, _lo, _mid, _hi);
            IFactory factory = new Factory(data);

            factory.Execute();

            Assert.AreEqual(vri_list, factory.outputData.VRI_List);
            Assert.AreEqual(type, factory.outputData.Type);
            Assert.AreEqual(kind, factory.outputData.Kind);
        }

        [TestCase("осуществления учебно-воспитательной деятельности (прогулочная площадка)", "12.0.1")]
        [TestCase("организации отдыха, культурного проведения свободного времени, укрепления здоровья, а так же для выращивания плодовых, ягодных, овощных и", "13.2.0")]
        [TestCase("для размещения комплекса зданий объекта Узел связи с инженерно - техническим центром в п.Газопровод", "6.8.0")]
        public void IFactory_OnlyVRYTest(string _vri_doc, string exceptedCodes)
        {
            IInputData data = new InputData(_vri_doc, 0, "", false, false, false);
            IFactory factory = new Factory(data);

            factory.Execute();

            Assert.AreEqual(exceptedCodes, factory.outputData.VRI_List);
        }
    }
}
