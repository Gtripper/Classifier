﻿using System;
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
            1416, "", false, false, false, "3.5.1.0, 12.0.1", 100, 2003)]
        [TestCase("земельные участки, предназначенные для размещения газопроводов (1.2.13), земельные участки, занятые особо охраняемыми территориями и объект",
            4, "", false, false, false, "3.1.1", 300, 3004)]
        [TestCase("(3.1)",
            4, "", false, false, false, "3.1.1, 3.1.2, 3.1.3", 300, 3004)]
        [TestCase("(3.1)",
            4, "3.1.2", false, false, false, "3.1.2", 100, 1004)]
        [TestCase("(3.1) , (4.1)",
            4, "3.1.2", false, false, false, "3.1.2, 4.1.0", 100, 1000)]
        [TestCase("(3.1) , (4.1)",
            4, "", false, false, false, "3.1.2, 3.1.3, 4.1.0", 100, 1001)]
        [TestCase("(7.1) , (4.1)",
            4, "", false, false, false, "4.1.0, 7.1.1", 100, 1001)]
        [TestCase("(7.2) , (4.1)",
            4, "", false, false, false, "4.1.0, 7.2.1", 100, 1001)]
        [TestCase("Объекты размещения помещений и технических устройств общественных туалетов (1.2.9)",
            4, "", false, false, false, "3.1.2", 100, 1004)]
        [TestCase("Размещение железнодорожных путей (7.1.1), Размещение, зданий и сооружений, в том числе железнодорожных вокзалов и станций, а также устройств",
            4, "", false, false, false, "7.1.1, 7.1.2", 999, 999)]
        [TestCase("Коммунальное обслуживание (3.1). Охрана природных территорий (9.1).",
            4, "", false, false, false, "3.1.1, 3.1.2, 3.1.3, 9.1.0", 340, 999)]
        public void IFactory_FullDataTest(string _vri_doc, int _area, string _btiVri, bool _lo, bool _mid, bool _hi, string vri_list, int type, int kind)
        {
            IInputData data = new InputData(_vri_doc, _area, _btiVri, _lo, _mid, _hi);
            IFactory factory = new Factory(data);

            factory.Execute();

            Assert.AreEqual(vri_list, factory.outputData.VRI_List);
            Assert.AreEqual(type, factory.outputData.Type);
            Assert.AreEqual(kind, factory.outputData.Kind);
        }

        [TestCase("осуществления учебно-воспитательной деятельности (прогулочная площадка)", "3.5.1.0, 12.0.1")]
        [TestCase("организации отдыха, культурного проведения свободного времени, укрепления здоровья, а так же для выращивания плодовых, ягодных, овощных и", "13.2.0")]
        [TestCase("для размещения комплекса зданий объекта Узел связи с инженерно - техническим центром в п.Газопровод", "6.8.0")]
        [TestCase("Многоквартирный дом с подземной автопарковкой", "2.1.1.0, 2.5.0, 2.6.0")]
        [TestCase("земельные участки, предназначенные для сельскохозяйственного использования (1.2.15), занятые водными объектами (береговая полоса), ограниче", "1.1.0, 11.0.0")]
        [TestCase("ЭКСПЛУАТАЦИИ ЗДАНИЯ ДЕТСКОЙ ГОРОДСКОЙ ПОЛИКЛИНИКИ № 58", "3.4.1.0")]
        [TestCase("участки размещения административно-деловых объектов: объекты размещения офисных помещений (1.2.7), участки размещения жилищно-коммунальных", "4.1.0")]
        [TestCase("проектирования, строительства и дальнейшей эксплуатации торгово-коммерческого комплекса и трансформаторной подстанции (11 обособленных", "4.2.0")]
        [TestCase("эксплуатации гаражного бокса для хранения индивидуального транспортного средства", "2.7.1.0")]
        [TestCase("эксплуатации гаражного бокса для хранения личного траспортного средства", "2.7.1.0")]
        [TestCase("ЭКСПЛУАТАЦИИ ВРЕМЕННОГО МЕТАЛЛИЧЕСКОГО СБОРНО-РАЗБОРНОГО ГАРАЖА.", "2.7.1.0")]
        [TestCase(@"ПОД УСТАНОВКУ МЕТАЛЛИЧЕСКОГО УКРЫТИЯ ТИПА ""ПЕНАЛ"" ДЛЯ ХРАНЕНИЯ АВТОМАШИНЫ ВАЗ-21053", "2.7.1.0")]
        [TestCase(@"ПОД УСТАНОВКУ МЕТАЛЛИЧЕСКОГО УКРЫТИЯ ТИПА ""РАКУШКА"" ДЛЯ ХРАНЕНИЯ АВТОМАШИНЫ МАРКИ ""ОКА - 11113-02""", "2.7.1.0")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ИНДИВИДУАЛЬНОГО КИРПИЧНОГО ГАРАЖА", "2.7.1.0")]
        [TestCase(@"ЭКСПЛУАТАЦИИ КРЫТАЯ А/С №59 НА 8 МАШИНОМЕСТ", "4.9.0")]
        [TestCase(@"ЭКСПЛУАТАЦИИ гаража-тента типа ""Ракушка""", "2.7.1.0")]
        [TestCase(@"участки размещения многоквартирных жилых домов: объекты размещения жилых и нежилых помещений, инженерного оборудования многоквартирных", "2.1.1.0, 2.5.0, 2.6.0")]
        [TestCase(@"(3.1) , (4.2)", "3.1.2, 3.1.3, 4.2.0")]
        [TestCase(@"ЭКСПЛУАТАЦИИ СУЩЕСТВУЮЩИХ ЖЕЛЕЗНОДОРОЖНЫХ ПОДЪЕЗДНЫХ ПУТЕЙ", "7.1.1")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств линейных объектов железн", "7.1.2")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств линейных объектов скорос", "7.1.2")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств аэропортов, аэродромов", "7.4.1")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств технических служб обеспеч", "3.1.1")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств конечных станций", "7.2.2")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств речных портов", "7.3.0")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств причалов", "7.3.0")]
        [TestCase(@"участки размещения объектов транспортной инфраструктуры: объекты размещения помещений и технических устройств скоростного внеуличного транспорта", "7.1.2")]
        [TestCase(@"Для строительства подъездной дороги и разворотной площадки", "12.0.2")]
        [TestCase(@"Для строительства подъездной автодороги и разворотной площадки", "12.0.2")]
        [TestCase(@"Для строительства подъездной автомобильной дороги и разворотной площадки", "12.0.2")]
        [TestCase(@"использование территории и эксплуатация зданий детской городской больницы", "3.4.2.0")]
        [TestCase(@"земельные участки, занятые особо охраняемыми природными территориями и объектами (1.2.14)", "9.0.0")]
        [TestCase("Размещение жилого дачного дома 13.3.0 (не предназначенного для раздела на " +
            "квартиры, пригодного для отдыха и проживания, высотой не выше трех", "13.3.0")]
        [TestCase("эксплуатации издательства", "6.11.0")]
        [TestCase("Благоустройство территории", "12.0.1")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ЗЕМЕЛЬНЫХ УЧАСТКОВ САДОВ.", "13.2.0")]
        [TestCase(@"Многоквартирный дом", "2.1.1.0, 2.5.0, 2.6.0")]
        [TestCase(@"строительства и последующей эксплуатации комплекса дипломатического представительства республики корея и других связанных с ним сооруж", "3.8.3")]
        [TestCase(@"для размещения в/ч", "8.0.1")]
        [TestCase(@"эксплуатации здания и территории под учебную деятельность", "3.5.1.0, 3.5.2.0")]
        [TestCase(@"Размещение объектов капитального строительства, предназначенных для профессионального образования и просвещения (профессиональные тех", "3.5.2.0")]
        [TestCase(@"земельные участки, предназначенные для размещения зданий, строений, сооружений материально-технического, продовольственного снабжения,", "6.9.0")]
        [TestCase(@"размещение жилых помещений различного вида и обеспечение проживания в них Содержание данного вида разрешенного использования включает в", "2.0.0")]
        [TestCase(@"размещение сооружений, имеющих назначение по временному хранению, распределению и перевалке грузов (за исключением хранения стратегичес", "6.9.0")]
        [TestCase(@"проектирования, строительства и последующей эксплуатации одно- двухэтажных гаражей боксового типа для машин индивидуального пользовани", "2.7.1.0")]
        [TestCase(@"размещение объектов капитального строительства, необходимых для подготовки и поддержания в боевой готовности Вооруженных Сил Российско", "8.0.1")]
        [TestCase(@"воздушный транспорт (земельные участки, предназначенные для размещения портов, водных, железнодорожных вокзалов, автодорожных вокзалов,", "7.4.1, 7.4.2")]
        [TestCase(@"временного размещения автоматической станции контроля загрязнения атмосферного воздуха, являющегося нестационарным объектом, движимым", "3.9.1.0")]
        [TestCase(@"гидротехнические сооружения (11.3) (земельные участки улиц, проспектов, площадей, шоссе, аллей, бульваров, застав, переулков, проездов, тупико", "11.3.0")]
        [TestCase(@"дальнейшей эксплуатации гаражей боксового типа (железобетонные гаражи) на 59 (пятьдесят девять) машиномест с целью хранения личного автотр", "2.7.1.0")]
        [TestCase(@"для ведения садового хозяйства", "13.2.0")]
        [TestCase(@"для ВЗУ № 2-5", "3.1.1")]
        [TestCase(@"для иных сельскохозяйственных целей", "1.0.0")]
        [TestCase(@"ДЛЯ ИСПОЛЬЗОВАНИЯ ПОДЪЕЗДНЫХ ПУТЕЙ НАХОДЯЩИХСЯ В СОВМЕСТНОМ ПОЛЬЗОВАНИИ С ОАО ""МОСКОВСКАЯ ТОПЛИВНАЯ КОМПАНИЯ"" (2 УЧАСТКА); ИСПОЛЬЗОВАНИЯ П", "7.1.1")]
        [TestCase(@"для проектирования, строительства и дальнейшей эксплуатации базы хранения нерудных материалов со зданием весовой (общая площадь объекта", "6.9.0")]
        [TestCase(@"Для размещения и эксплуатации объектов жележнодорожного транспорта", "7.1.1, 7.1.2")]
        [TestCase(@"Для размещения объектов обононы и безопасности", "8.0.1")]
        [TestCase(@"для учебно-производст. базы", "3.5.2.0")]
        [TestCase(@"защинтые леса", "9.1.0")]
        [TestCase(@"Защитные леса", "9.1.0")]
        [TestCase(@"земельные участки, занятые особо охраняемыми природными территориями и объектами (1.2.14)", "9.0.0")]
        [TestCase(@"земельные участки, предназначенные для размещения искусственно созданных внутренних водных путей (1.2.13), земельные участки, занятые сквер", "11.0.0")]
        [TestCase(@"квартал жилого типа низкоплотной застройки с высотой не более 15 м", "2.0.0")]
        [TestCase(@"на период разработки исходно-разрешительной документации на строительство здания 19 пожарной части, в том числе участок площадью 0,1413 га по", "8.3.0")]
        [TestCase(@"на период разработки исходно-разрешительной и проектной документации на размещение центра всесезонных видов спорта и проведения первооч", "5.1.3")]
        [TestCase(@"объекты размещения некоммерческих организаций, связанные с обслуживанием проживающего населения (1.2.17); объекты размещения финансово-кре", "3.2.4")]
        [TestCase(@"объекты размещения организаций и учреждений обеспечения безопасности (1.2.17)", "8.3.0")]
        [TestCase(@"объекты размещения помещений и технических устройств линейных объектов железнодорожного и скоростного внеуличного транспорта, тяговых", "7.1.1")]
        [TestCase(@"ДЛЯ ЭКСПЛУАТАЦИИ ЗДАНИЙ И СООРУЖЕНИЙ 147 АВТОМОБИЛЬНОЙ БАЗЫ И ГОСУДАРСТВЕННОЙ РЕГИСТРАЦИИ ИМУЩЕСТВЕННЫХ ПРАВ В УСТАНОВЛЕННОМ ПОРЯДКЕ.", "7.2.2")]
        [TestCase(@"ЗАВЕРШЕНИЯ РАЗРАБОТКИ АКТА РАЗРЕШЕННОГО ИСПОЛЬЗОВАНИЯ, ПРОЕКТНОЙ ДОКУМЕНТАЦИИ И СТРОИТЕЛЬСТВА СОЦИАЛЬНОГО ОБЪЕКТА ШАГОВОЙ ДОСТУПНОСТИ", "4.2.0, 4.4.0")]
        [TestCase(@"Основной вид разрешенного использования: 6.2.0 - Размещение объектов капитального строительства горно-обогатительной и горно-перерабатыва", "6.2.0")]
        [TestCase(@"ПОД ЭКСПЛУАТАЦИЮ ПОДЗЕМНОГО БЛОКА ДВОЙНОГО НАЗНАЧЕНИЯ", "8.3.0")]
        [TestCase(@"ПОД ЭКСПЛУАТАЦИЮ ФИЛИАЛОМ УВО ПРИ ГУВД Г.МОСКВЫ - 3 МЕЖРАЙОННЫМ ОТДЕЛОМ ВНЕВЕДОМСТВЕННОЙ ОХРАНЫ ОТДЕЛА ПО РУКОВОДСТВУ СЛУЖБАМИ ВНЕВЕДОМСТ", "8.3.0")]
        [TestCase(@"ПРОЕКТИРОВАНИЯ, СТРОИТЕЛЬСТВА И ПОСЛЕДУЮЩЕЙ ЭКСПЛУАТАЦИИ СТАЦИОНАРНОГО ТЕХНИЧЕСКОГО ПОСТА ПО ЗАМЕНЕ И СБОРУ ОТРАБОТАННЫХ АВТОМОБИЛЬНЫХ", "12.2.0")]
        [TestCase(@"ПРОКЛАДКА дождевой канализации, в том числе состоящий из двенадцати обособленных участков: Р1-79 кв.м; Р2- 104 кв.м; Р3-136 кв.м; Р4-106 кв.м; Р5-142 кв.м;", "3.1.1")]
        [TestCase(@"РАЗВИТИЯ И ЭКСПЛУАТАЦИИ РЕЗИДЕНЦИИ ПАТРИАРХОВ МОСКОВСКИХ И ВСЕЯ РУСИ, ЦЕНТРА ПРАВОСЛАВНОГО НАСЛЕДИЯ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ ЭКСПЛУА", "3.7.1")]
        [TestCase(@"РАЗМЕЩЕНИЯ   ХРАНЕНИЯ МЕДИКАМЕНТОВ", "6.9.0")]
        [TestCase(@"РАЗРАБОТКИ АРИ УЧАСТКА ТЕРРИТОРИИ ГО, ВОЗВЕДЕНИЯ И ПОСЛЕДУЮЩЕЙ ЭКСПЛУАТАЦИИ ПРЕДПРИЯТИЯ ПО РЕМОНТУ И ТЕХ. ОБСЛУЖИВАНИЮ ИТС ОБЩЕЙ ПЛОЩАДЬ", "8.3.0")]
        [TestCase(@"РАЗРАБОТКИ ИСХОДНО-РАЗРЕШИТЕЛЬНОЙ И ПРОЕКТНОЙ ДОКУМЕНТАЦИИ, СТРОИТЕЛЬСТВА И ДАЛЬНЕЙШЕЙ  ЭКСПЛУАТАЦИИ МНОГОФУНКЦИОНАЛЬНОГО АДМИНИСТРАТ", "4.0.0")]
        [TestCase(@"Размещение жилых помещений различного вида и обеспечение проживания в них. Содержание данного вида разрешенного использования включает", "2.0.0")]
        [TestCase(@"Размещение объектов капитального строительства горно-обогатительной и горно-перерабатывающей, металлургической, машиностроительной пр", "6.1.0")]
        [TestCase(@"Размещение объектов капитального строительства, размещение которых предусмотрено видами разрешенного использования с кодами 3.1.2, 3.1.3, 3.2.2", "3.1.2, 3.1.3, 3.2.2")]
        [TestCase(@"СТРОИТЕЛЬСТВА И ПОСЛЕДУЮЩЕЙ ЭКСПЛУАТАЦИИ ПЛОЩАДКИ ДЛЯ ХРАНЕНИЯ АВТОМОБИЛЕЙ НА 90 М/М, ОБОРУДОВАННОЙ СБОРНО-РАЗБОРНЫМИ МЕТАЛЛИЧЕСКИМИ ТЕН", "2.7.1.0")]
        [TestCase(@"СТРОИТЕЛЬСТВА ОБЪЕКТА ГОРОДСКОГО ЗАКАЗА № 01-038 ""СТР - ВО ТРАНСПОРТНОЙ РАЗВЯЗКИ НА ПР.МИРА С РЕКОНСТРУКЦИЕЙ СЕВЕРЯНИНСКОГО ПУТЕПРОВОДА"" (Р1-58", "7.2.1")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ЗДАНИЙ ИНТЕРНАТА И ПРИЛЕГАЮЩЕЙ ТЕРРИТОРИИ", "3.2.1")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ЗДАНИЯ АВТОСТАНЦИИ ""ВЫХИНО""", "7.2.2")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ЗДАНИЯ ВАКЦИННОГО КОРПУСА", "3.4.1.0, 3.9.3")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ЗДАНИЯ И СООРУЖЕНИЯ МЧС", "8.3.0")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ПОСТА ПО ОТБОРУ И АНАЛИЗУ ПРОБ АТМОСФЕРНОГО ВОЗДУХА", "3.9.1.0")]
        [TestCase(@"ЭКСПЛУАТАЦИИ СУЩЕСТВУЮЩИХ ЗДАНИЙ И СООРУЖЕНИЙ С ЦЕЛЬЮ СОДЕРЖАНИЯ РОТЫ ОХРАНЫ СПЕЦКОНТИНГЕНТА", "8.4.0")]
        [TestCase(@"ЭКСПЛУАТАЦИИ СУЩЕСТВУЮЩИХ КИРПИЧНЫХ ГАРАЖЕЙ БОКСОВОГО ТИПА НА 380 МАШИНОМЕСТ ДЛЯ ХРАНЕНИЯ ЛЕГКОВОГО АВТОТРАНСПОРТА ИНДИВИДУАЛЬНОГО ПОЛЬ", "2.7.1.0")]
        [TestCase(@"ЭКСПЛУАТАЦИИ ШТАБА ПО ОРГАНИЗАЦИИ И ОБЕСПЕЧЕНИЮ РЕЙСОВ ВОЗДУШНЫХ СУДОВ СПЕЦИАЛЬНОГО НАЗНАЧЕНИЯ", "7.4.1")]
        [TestCase(@"ЭКСПЛУАТАЦИИ зданий и сооружений иститута", "3.5.2.0")]
        [TestCase(@"воздушный транспорт (земельные участки, предназначенные для размещения портов, водных, железнодорожных вокзалов, автодорожных вокзалов,", "7.4.1, 7.4.2")]
        [TestCase(@"Под пожарную часть", "8.3.0")]
        [TestCase(@"гидротехнические сооружения (11.3) (земельные участки улиц, проспектов, площадей, шоссе, аллей, бульваров, застав, переулков, проездов, тупико", "11.3.0")]
        [TestCase(@"дальнейшей эксплуатации существующих зданий и сооружений 5-го района водопроводной сети, без права возведения капитальных строений и соо", "3.1.1")]
        [TestCase(@"для обслуживания и использования муниципального недвижимого имущества:  разгрузочное устройство открытое (повышенный путь) -""железобетон""", "7.1.2")]
        [TestCase(@"для проведения комплекса ремонтно-реставрационных работ, приспособления ансамбля Марфо-Мариинской Обители и дальнейшей эксплуатации зд", "3.7.1")]
        [TestCase(@"для размещения узла регулирования питьевой воды", "3.1.1")]
        [TestCase(@"для строительства геофизического шурфа испытательной станции", "6.1.0")]
        [TestCase(@"земельные участки предназначенные для размещения зданий, строений, сооружений материально-технического, продовольственного снабжения, с", "6.9.0")]
        [TestCase(@"на период проектирования, строительства и дальнейшей эксплуатации временного технического поста по замене и сбору отработанных автомоби", "12.2.0")]
        [TestCase(@"на период разработки исходно-разрешительной документации на строительство здания 19 пожарной части, в том числе участок площадью 0,1413 га по", "8.3.0")]
        [TestCase(@"объекты размещения помещений, технических устройств и сооружений технической инфраструктуры железнодорожного транспорта, грузовых и со", "7.1.2")]
        [TestCase(@"объекты размещения предприятий по стирке, чистке, крашению, иной обработке бытовых изделий из ткани, кожи, меха и других материалов (1.2.5); об", "3.3.0")]
        [TestCase(@"объекты размещения технических устройств линейных объектов железнодорожного транспорта (1.2.13); участки смешанного размещения общественн", "7.1.1")]
        [TestCase(@"под инженерно-техническими объектами жилого поселка", "3.1.1")]
        [TestCase(@"под площадку для сбора и хранения брошенного и разукомплектованного автотранспорта (площадь 0,9507га) и под площадку для разбора и первичной", "12.2.0")]
        [TestCase(@"под проектирование, строительство и дальнейшую эксплуатацию жилых малоэтажных домов", "2.0.0")]
        [TestCase(@"под размещение комплекса под реализацию сельскохозяйственной продукции,продовольственных продуктов и иных необходимых товаров с обустр", "4.2.0")]
        [TestCase(@"проектирования, строительства и последующей эксплуатации одно- двухэтажных гаражей боксового типа для машин индивидуального пользовани", "2.7.1.0")]
        [TestCase(@"размещ-я объектов капитального строит-ва в целях обеспеч-я физ и юр. лиц коммунал. услугами, в частности: поставки воды, тепла, электричеств", "3.1.3")]
        [TestCase(@"размещение сооружений, имеющих назначение по временному хранению, распределению и перевалке грузов (за исключением хранения стратегичес", "6.9.0")]
        [TestCase(@"разработки Акта разрешенного использования Москомархитектуры на размещение площадки централизованного сбора, сортировки и временного х", "12.2.0")]
        [TestCase(@"разработки разрешительной документации для реконструкции и строительства диагностического корпуса технического осмотра автотранспорт", "4.9.1.4")]
        [TestCase(@"реконструкции здания на размещение наркодиспансера № 9", "3.2.1")]
        [TestCase(@"участки размещения специальных объектов: объекты размещения помещений и технических устройств водопроводных регулирующих узлов, водоза", "3.1.1")]
        [TestCase(@"участки размещения специальных объектов: объекты размещения помещений и технических устройств городских канализационных очистных соору", "3.1.1")]
        [TestCase(@"участки размещения специальных объектов: объекты размещения помещений и технических устройств пунктов перехода ВЛЭП (1.2.13), земельные уча", "3.1.1")]
        [TestCase(@"участки размещения специальных объектов: объекты размещения помещений и технических устройств специального назначения в т.ч. обеспечени", "8.0.1")]
        [TestCase(@"участки размещения стационарно-профилактических учреждений (в т.ч. клинических) без специальных требований к размещению (1.2.17); участки раз", "3.4.2.0")]
        [TestCase(@"участки размещения учебно-восспитательных объектов: объекты размещения учреждений кружковой деятельности и учреждений для организации", "3.5.1.0")]
        [TestCase(@"эксплуатации базы по хранению противогололедных материалов", "3.1.1")]
        [TestCase(@"эксплуатации зданий автохозяйства № 6 ГУВД.", "8.3.0")]
        [TestCase(@"эксплуатации зданий в учебных и хозяйственных целях", "3.5.1.0, 3.5.2.0")]
        [TestCase(@"эксплуатации зданий и сооружений для организации спортивных и культурно-массовых мероприятий.", "4.8.0, 5.1.3")]
        [TestCase(@"эксплуатации зданий и строений центра планирования семьи и репродукции № 2", "3.4.2.0")]
        [TestCase(@"эксплуатации зданий и территории под учебную деятельность", "3.5.1.0, 3.5.2.0")]
        [TestCase(@"эксплуатации зданий инженерно-технического комплекса", "3.9.3")]
        [TestCase(@"эксплуатации зданий кафедры оперативной хирургии.", "3.5.2.0")]
        [TestCase(@"эксплуатации зданий культурного и спортивного центров", "3.5.1, 5.1.2")]
        [TestCase(@"эксплуатации зданий школы высшего спортивного мастерства", "5.1.2")]
        [TestCase(@"эксплуатации здания медико-генетического центра", "3.4.2.0, 3.9.2")]
        [TestCase(@"эксплуатации здания мобилизационного назначения", "8.0.1")]
        [TestCase(@"эксплуатации здания спортивного интерната ЦСКА", "3.5.1.0")]
        [TestCase(@"эксплуатации здания тира", "5.1.5")]
        [TestCase(@"эксплуатации здания центра восстановительного лечения", "9.2.1.0")]
        [TestCase(@"эксплуатации здания центра для несовершеннолетних", "3.2.2")]
        [TestCase(@"эксплуатации здания центра творчества", "3.6.1")]
        [TestCase(@"эксплуатации имущественного комплекса дирекции", "4.1.0")]
        [TestCase(@"эксплуатации имущественного комплекса центра восстановительного лечения детей без права изменения функционального назначения", "9.2.1.0")]
        [TestCase(@"эксплуатации остановки пригородного автобуса (маршрут Москва-Тула); размещения и эксплуатации остановки пригородного автобуса (маршрут М", "7.2.2")]
        [TestCase(@"эксплуатации открытой площадки под организацию погрузочно-разгрузочных работ, отстоя и текущего ремонта спецвагонов", "7.1.2")]
        [TestCase(@"эксплуатации проведение учебно- тренировочных занятий  и спортивно- массовых мероприятий", "5.1.3")]
        [TestCase(@"эксплуатации радиометеорологического центра", "3.9.1.0")]
        [TestCase(@"эксплуатации существующего здания станции юных туристов", "5.2.1.0")]
        [TestCase(@"эксплуатации существующих зданий, строений и сооружений МЧС", "8.3.0")]
        [TestCase(@"эксплуатации территории под временное размещение гостевого автотранспорта ТК ""Ярославский"" и проведения  проектно-изыскательских работ", "4.9.0")]
        [TestCase(@"эксплуатация Природного комплекса №37 ""Коробовские сады(памятник природы)""", "9.0.0")]
        [TestCase(@"эксплуатация базы строительной и дорожной техники, являющейся некапитальным объектом (движимым имуществом) (земельные участки, предназна", "3.1.1")]
        [TestCase(@"эксплуатация зданий и сооружений дома аспиранта и студента", "3.5.2.0")]
        [TestCase(@"эксплуатация здания, используемого для удовлетворения социально-бытовых нужд студентов и аспирантов МИФИ (земельные участки, предназнач", "3.3.0")]
        [TestCase(@"эксплуатация зоны коммунальных предприятий", "3.1.1")]
        [TestCase(@"эксплуатация зоны общественного центра", "3.0.0, 4.0.0")]
        [TestCase(@"сплуатация и дальнейшая работа исследовательского центра", "3.9.3")]
        [TestCase(@"эксплуатация территории природно-общественного смешанного использования", "5.0.1")]
        [TestCase(@"для иных целей", "12.3.0")]
        [TestCase(@"", "12.3.0")]
        [TestCase(@"-", "12.3.0")]
        [TestCase(@".", "12.3.0")]
        [TestCase(@"отсутствует", "12.3.0")]
        public void IFactory_OnlyVRYTest(string _vri_doc, string exceptedCodes)
        {
            IInputData data = new InputData(_vri_doc, 0, "", false, false, false);
            IFactory factory = new Factory(data);

            factory.Execute();

            Assert.AreEqual(exceptedCodes, factory.outputData.VRI_List);
        }
    }
}
