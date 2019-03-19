using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;




namespace Classifier
{
    public abstract class Node
    {
        public Node() { }

        public Node(string vri, string vri540
            , string kindCode, string typeCode, string description, string simpleDescription
            , string funcUse, string simpleFuncUse, string[] regexpPatterns)
        {
            this.vri = vri;
            this.vri540 = vri540;
            this.kindCode = kindCode;
            this.typeCode = typeCode;
            this.description = description;
            this.simpleDescription = simpleDescription;
            this.funcUse = funcUse;
            this.simpleFuncUse = simpleFuncUse;
            this.regexpPatterns = regexpPatterns;
        }

        public Node(Node mnstr)
        {
            vri = mnstr.vri;
            vri540 = mnstr.vri540;
            kindCode = mnstr.kindCode;
            typeCode = mnstr.typeCode;
            description = mnstr.description;
            simpleDescription = mnstr.simpleDescription;
            funcUse = mnstr.funcUse;
            simpleFuncUse = mnstr.simpleFuncUse;
            regexpPatterns = mnstr.regexpPatterns;
        }

        public string vri { get; set; }

        public string vri540 { get; set; }

        public string kindCode { get; set; }

        public string typeCode { get; set; }

        public string description { get; set; }

        public string simpleDescription { get; set; }

        public string funcUse { get; set; }

        public string simpleFuncUse { get; set; }

        public string[] regexpPatterns { get; set; }

        /// <summary>
        /// Поиск ВРИ, соответствующих федеральному коду
        /// </summary>
        /// <returns></returns>
        public virtual List<Node> EmptyVRI()
        {
            var mf = new NodeFeed();
            var list = new CodesMapping().Map[vri540];
            var result = new List<Node>();
            foreach (var val in list)
            {
                result.Add(mf.getM(val));
            }
            return result;
        }

        public virtual bool GetParent(Node mstr)
        {
            return GetType().BaseType.Equals(mstr.GetType());
        }

        public virtual bool GetGParent(Node mstr)
        {
            if (!GetType().BaseType.BaseType.Name.Equals("Node"))
                return GetType().BaseType.BaseType.Equals(mstr.GetType());
            else return false;
        }

        public virtual bool isBaseClass()
        {
            return false;
        }

        public virtual bool isHousing()
        {
            return false;
        }

        public virtual bool Equals(Node node)
        {
            return vri.Equals(node.vri);
        }
    }

    #region SomeClasses
    class Agriculture : Node
    {
        public Agriculture(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }

        public override bool isBaseClass()
        {
            return true;
        }
    }

    class GrowPlanting : Agriculture
    {
        public GrowPlanting(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class LiveStocking : Agriculture
    {
        public LiveStocking(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class Agronomy : Agriculture
    {
        public Agronomy(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class Dwelling : Node
    {
        public Dwelling(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }

        public override bool isBaseClass()
        {
            return true;
        }

        public override bool isHousing()
        {
            return true;
        }
    }

    class Housing : Dwelling
    {
        public Housing(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }

        public override bool isHousing()
        {
            return true;
        }
    }

    class HParking : Node
    {
        public HParking(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }

        public override bool isHousing()
        {
            return false;
        }
    }

    class MaintenanceOfResidentialBuildings : Node
    {
        public MaintenanceOfResidentialBuildings(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }

        public override bool isBaseClass()
        {
            return false;
        }

        public override bool isHousing()
        {
            return false;
        }
    }

    class BaseCommunity : Node
    {
        public BaseCommunity(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return true;
        }
    }

    class Community : BaseCommunity
    {
        public Community(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class Education : BaseCommunity
    {
        public Education(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return true;
        }
    }

    class ProfessionalEducation : Education
    {
        public ProfessionalEducation(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class SchoolEducation : Education
    {
        public SchoolEducation(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class HealthCare : BaseCommunity
    {
        public HealthCare(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return true;
        }
    }

    class Clinic : HealthCare
    {
        public Clinic(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class Hospital : HealthCare
    {
        public Hospital(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class Sсience : BaseCommunity
    {
        public Sсience(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class WeatherStation : BaseCommunity
    {
        public WeatherStation(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class Veterenary : BaseCommunity
    {
        public Veterenary(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return true;
        }
    }

    class VetClinic : Veterenary
    {
        public VetClinic(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class AnimalShelter : Veterenary
    {
        public AnimalShelter(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class BaseBuisness : Node
    {
        public BaseBuisness(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return true;
        }
    }

    class Buisness : BaseBuisness
    {
        public Buisness(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class Hotel : BaseBuisness
    {
        public Hotel(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class Hostel : BaseBuisness
    {
        public Hostel(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class Dormitory : BaseBuisness
    {
        public Dormitory(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class GasStation : BaseBuisness
    {
        public GasStation(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class Motel : BaseBuisness
    {
        public Motel(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class CarWashingStation : BaseBuisness
    {
        public CarWashingStation(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class BodyShop : BaseBuisness //автомастеская
    {
        public BodyShop(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class BaseRecreation : Node
    {
        public BaseRecreation(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return true;
        }
    }

    class Recreation : BaseRecreation
    {
        public Recreation(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class Sport : BaseRecreation
    {
        public Sport(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class BaseIndustry : Node
    {
        public BaseIndustry(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return true;
        }
    }

    class Industry : BaseIndustry
    {
        public Industry(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class BaseTransport : Node
    {
        public BaseTransport(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return true;
        }
    }

    class Transport : BaseTransport
    {
        public Transport(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class SecurityForces : Node
    {
        public SecurityForces(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
    }

    class Environment : Node
    {
        public Environment(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
    }

    class BaseForestry : Node
    {
        public BaseForestry(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return true;
        }
    }

    class Forestry : BaseForestry
    {
        public Forestry(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
        public override bool isBaseClass()
        {
            return false;
        }
    }

    class WaterObjs : Node
    {
        public WaterObjs(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
    }

    class Infrastructure : Node
    {
        public Infrastructure(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
    }

    class OtherFunction : Node
    {
        public OtherFunction(string vri, string vri540, string kindCode, string typeCode, string description,
            string simpleDescription, string funcUse, string simpleFuncUse, string[] regexpPatterns) :
                base(vri, vri540, kindCode, typeCode, description, simpleDescription, funcUse, simpleFuncUse, regexpPatterns)
        {
        }
    }
    #endregion


    /// <summary>
    /// 
    /// </summary>
    /// TODO: Реализовать создание одного экземпляра Node без создания List<Node>
    /// Заменить методы свойствами.
    /// Переработать кх
    public class NodeFeed : Node
    {
        private List<Node> nodes;

        public NodeFeed()
        {
            Feeding();
        }

        public List<Node> GetNodes()
        {
            return nodes;
        }

        /// <summary>
        /// Get Node with vri code
        /// </summary>
        /// <param name="vri"></param>
        /// <returns></returns>
        public Node getM(string vri)
        {
            return nodes.Find(p => p.vri.Equals(vri));
        }

        /// <summary>
        /// Get Node with federal code
        /// </summary>
        /// <param name="fCode"></param>
        /// <returns></returns>
        public Node GetNodeBasedFCode(string fCode)
        {
            return nodes.Find(p => p.vri540.Equals(fCode));
        }



        private void Feeding()
        {

            nodes = new List<Node>();
            nodes.Add(new Agriculture("1.0.0", "1.0", "3006", "800",

                "Ведение сельского, в т.ч. фермерского хозяйства. " +
                "Содержание данного вида разрешенного использования включает в себя содержание видов " +
                "разрешенного использования с кодами 1.1-1.18, в том числе размещение зданий и " +
                "сооружений, используемых для хранения и переработки сельскохозяйственной продукции ",

                "Сельскохозяйственное использование",

                "",
                "",
                new string[] {
                    @"",
                    @"\bвыращиван\w*\s*сель\w*\s*хоз\w*\s*(культур\w*)?|\bживотновод\w*\b",

                    @"",
                    @"\bвыращиван\w*\s*с/х|\bсель\w*[-.\s]*хоз\w*[-.\s]деятель\w*\b",

                    @"",
                    @"\b(крестьян\w*|сельск\w*)\s*(.*фермерск\w*.*)?\bхозяйст\w*\b",

                    @"",
                    @"\bфермер\w*\s*хоз\w*\b",

                    @"",
                    @"\bдля\s*сельскохоз\w*\s*цел\w*\b",

                    @"",
                    @"\bсел\w*\s*хоз\w*[-\s]*производ\w*\b" }
                ));

            nodes.Add(new GrowPlanting("1.1.0", "1.1", "3006", "800",

                "Осуществление хозяйственной " +
                "деятельности, связанной с выращиванием сельскохозяйственных культур. Содержание данного " +
                "вида разрешенного использования включает в себя содержание видов разрешенного " +
                "использования с кодами 1.2-1.6 ",

                "Растениеводство",

                "",
                "",
                new string[] {
                    @"",
                    @"\bрастениеводств\w*\b",

                    @"",
                    @"\bсельскохоз\w*\s*(использ\w*|назнач\w*|угодия)\b",

                    @"",
                    @"\bсенкошен\w*\s*и\s*выпас\w*\s*скот\w*" }
                ));

            nodes.Add(new GrowPlanting("1.2.0", "1.2", "3006", "800",

                "Осуществление хозяйственной " +
                "деятельности на сельскохозяйственных угодьях, связанной с производством зерновых, " +
                "бобовых, кормовых, технических, масличных, эфиромасличных, и иных сельскохозяйственных " +
                "культур ",

                "Выращивание зерновых и иных сельскохозяйственных культур",

                "",
                "",
                new string[] {
                    @"центр|переработке|продаже",
                    @"\bзернов\w*\b|\bбобв\w*\b|\bкормов\w*\b|масличн\w*\b" }
                ));

            nodes.Add(new GrowPlanting("1.3.0", "1.3", "3006", "800",

                "Осуществление хозяйственной деятельности на сельскохозяйственных угодьях, связанной " +
                "с производством картофеля, листовых, плодовых, луковичных и бахчевых " +
                "сельскохозяйственных культур, в том числе с использованием теплиц ",

                "Овощеводство",

                "",
                "",
                new string[] {
                    @"картофелехранилищ|научного\sцентра|павильон|\bжил\w*\s*дом\w*\b|\bорганиз\w*\s*отдых\w*\b|" + 
                    @"\bкультурн\w*\s*проведен\w*\s*свободн\w*\s*врем\w*\b|" +
                    @"укрепления здоровья, а так же для выращивания плодовых, ягодных, овощных",
                    @"\bкартофел\w*\b|\bлистов\w*\b|\bплодов\w*\b|\bлуковичн\w*\b|\bбахчев\w*\b" }
                ));

            nodes.Add(new GrowPlanting("1.4.0", "1.4", "3006", "800",

               "Осуществление хозяйственной деятельности, в том числе на сельскохозяйственных " +
               "угодьях, связанной с производством чая, лекарственных и цветочных культур ",

               "Выращивание тонизирующих, лекарственных, цветочных культур",
               "",
               "",
               new string[] {
                    @"\bжил\w*\s*дом\w*\b",
                    @"\b(выращива\w*|сбор\w*)\b?.+\b(лекарств\w*\s*растен\w*|цвет\w*)\b",

                    @"",
                    @"\bвыращиван\w*.*\bмед\w*\b",

                    @"\bкосметик\w*\b|\bжил\w*\s*дом\w*\b|\bпитомн\w*\s*декоратив\w*\b",
                    @"\bдекоратив\w*\s*(культур\w*|растен\w*|кустарн\w*)\b",

                    @"",
                    @"\bнеплодород\w*\s*кустарн\w*\b" }
               ));

            nodes.Add(new GrowPlanting("1.5.0", "1.5", "3006", "800",

               "Осуществление хозяйственной деятельности, в том числе на сельскохозяйственных " +
               "угодьях, связанной с выращиванием многолетних плодовых и ягодных культур, винограда, " +
               "и иных многолетних культур ",

               "Садоводство+",
               "",
               "",
               new string[] {
                    @"",
                    @"\bмноголетн\w*\b[-\s]*(плодов\w*|ягодня\w*)|\bвиноград\w{0,1}\b" }
               ));

            nodes.Add(new GrowPlanting("1.6.0", "1.6", "3006", "800",

               "Осуществление хозяйственной деятельности, в том числе на сельскохозяйственных " +
               "угодьях, связанной с выращиванием льна, конопли ",

               "Выращивание льна и конопли",
               "",
               "",
               new string[] {
                    @"",
                    @"\bл[еёь]н\w{0,1}\b|\bконопл\w*\b" }
               ));

            nodes.Add(new LiveStocking("1.7.0", "1.7", "3006", "800",

               "Осуществление хозяйственной деятельности, связанной с производством " +
               "продукции животноводства, в том числе сенокошение, выпас " +
               "сельскохозяйственных животных, разведение племенных животных, " +
               "производство и использование племенной продукции (материала), " +
               "размещение зданий, сооружений, используемых для содержания и разведения " +
               "сельскохозяйственных животных, производства, хранения и первичной " +
               "переработки сельскохозяйственной продукции. Содержание данного вида " +
               "разрешенного использования включает в себя содержание видов разрешенного " +
               "использования с кодами 1.8-1.11 ",

               "Животноводство",
               "",
               "",
               new string[] {
                   @"",
                   @"\bголубин\w*\s*питомн\w*\b|\bмолоч\w*\b.*\bферм\w*\b",

                   @"",
                   @"\bскот\w*\b",

                   @"",
                   @"\b(разведен\w*|питомн\w*)\b.*\bголуб\w*\b"  }
               ));

            nodes.Add(new LiveStocking("1.8.0", "1.8", "3006", "800",

               "Осуществление хозяйственной деятельности, в том числе на " +
               "сельскохозяйственных угодьях, связанной с разведением сельскохозяйственных " +
               "животных (крупного рогатого скота, овец, коз, лошадей, верблюдов, оленей); " +
               "сенокошение, выпас сельскохозяйственных животных, производство кормов, размещение " +
               "зданий, сооружений, используемых для содержания и разведения сельскохозяйственных " +
               "животных; разведение племенных животных, производство и использование племенной " +
               "продукции (материала) ",

               "Скотоводство",
               "",
               "",
               new string[] {
                    @"торгового",
                    @"\bрогат\w*\s*скот\w*\b|\bове?ц\w*|\bкозо\w*\b|" +
                    @"\bлошад\w*\b|\bверблюд\w*\b|\bолен\w*",

                    @"",
                    @"\bконюшн\w*\b",

                    @"",
                    @"\bмолочн\w*\s*комплекс\w*\b" }
               ));

            nodes.Add(new LiveStocking("1.9.0", "1.9", "3006", "800",

               "Осуществление хозяйственной деятельности, связанной с разведением в неволе " +
               "ценных пушных зверей; размещение зданий, сооружений, используемых для содержания и " +
               "разведения животных, производства, хранения и первичной переработки продукции; " +
               "разведение племенных животных, производство и использование племенной продукции(материала) ",

               "Звероводство",
               "",
               "",
               new string[] {
                    @"",
                    @"\bпушн\w*\s*звер\w*\b|\bзверокомплекс\w*\b" }
               ));

            nodes.Add(new LiveStocking("1.10.0", "1.10", "3006", "800",

               "Осуществление хозяйственной деятельности, связанной с разведением домашних пород птиц, " +
               "в том числе водоплавающих; размещение зданий, сооружений, используемых для содержания и " +
               "разведения животных, производства, хранения и первичной переработки продукции птицеводства;" +
               "разведение племенных животных, производство и использование племенной продукции(материала) ",

               "Птицеводство",

               "",
               "",
               new string[] {
                    @"",
                    @"\bптицевод\w*\b",

                    @"",
                    @"\bразведен\w*\b.*\bдомашн\w*\s*птиц\w*" }
               ));

            nodes.Add(new LiveStocking("1.11.0", "1.11", "3006", "800",

               "Осуществление хозяйственной деятельности, связанной с разведением свиней; размещение зданий, " +
               "сооружений, используемых для содержания и разведения животных, производства, хранения и " +
               "первичной переработки продукции; разведение племенных животных, производство и использование " +
               "племенной продукции (материала) ",

               "Свиноводство",
               "",
               "",
               new string[] {
                    @"",
                    @"\bсвин\w*\b" }
               ));

            nodes.Add(new Agronomy("1.12.0", "1.12", "3006", "800",

               "Осуществление хозяйственной деятельности, в том числе на сельскохозяйственных угодьях, " +
               "по разведению, содержанию и использованию пчел и иных полезных насекомых; размещение ульев, " +
               "иных объектов и оборудования, необходимого для пчеловодства и разведениях иных полезных насекомых; " +
               "размещение сооружений используемых для хранения и первичной переработки продукции пчеловодства ",

               "Пчеловодство",
               "",
               "",
               new string[] {
                    @"по\sпродаж\w*\b",
                    @"\bпч[её]л\w*\b|\bполезн\w*\s*насеком\w*\b|\bпасек\w*\b" }
               ));

            nodes.Add(new Agronomy("1.13.0", "1.13", "3006", "800",

               "Осуществление хозяйственной деятельности, связанной с разведением и (или) содержанием, " +
               "выращиванием объектов рыбоводства (аквакультуры); размещение зданий, сооружений, оборудования, " +
               "необходимых для осуществления рыбоводства (аквакультуры) ",

               "Рыбоводство",
               "",
               "",
               new string[] {
                    @"",
                    @"\bрыбоводств\w*\b|\bаквакультур\w*\b|\bрыбн\w*\s*хоз\w*\b" }
               ));

            nodes.Add(new Agronomy("1.14.0", "1.14", "1001", "100",

               "Осуществление научной и селекционной работы, ведения сельского хозяйства для получения " +
               "ценных с научной точки зрения образцов растительного и животного мира; размещение коллекций " +
               "генетических ресурсов растений ",

               "Научное обеспечение сельского хозяйства",
               "",
               "",
               new string[] {
                   @"",
                   @"науч...исследоват.+сельскохоз",

                   @"",
                   @"\bбезвирусн\w*\s*картоф\w*\b",

                   @"",
                   @"\bагро\w*\s*опытн\w*\s*станц\w*\b",

                   @"",
                   @"\bагро\w*\s*био\w*\s*станц\w*\b",

                   @"",
                   @"\bмосагронаучприбор\w*\b" }
               ));

            nodes.Add(new Agronomy("1.15.0", "1.15", "3006", "800",

               "Размещение зданий, сооружений, используемых для производства, хранения, первичной и " +
               "глубокой переработки сельскохозяйственной продукции ",

               "Хранение и переработка сельскохозяйственной продукции",
               "",
               "",
               new string[] {
                    @"",
                    @"\bтепли\w*\b",

                    @"",
                    @"\bсел\w*\s*хоз\w*[-\s]*(хранен\w*|" +
                    @"переработ\w*)\b|\b(картоф\w*|овощ\w*|капуст\w*)хран\w*\b",

                    @"",
                    @"\bперераб\w*\s*[и\s\,]*\w*\s*сел\w*[\s\-]*хоз\w*[\s\-]*прод\w*\b",

                    @"",
                    @"\bхранен\w*.*\b(растен\w*)\b",

                    @"",
                    @"\bпереработк\w*\s*(зерн\w*|(плодово)?овощн\w*|скот\w*)",

                    @"\bсклад\w*\b",
                    @"\b(плодово)?овощ\w*\s*(баз\w*|хранилищ\w*)\b|\bхладокомбинат\w*\b" }
               ));

            nodes.Add(new Agronomy("1.16.0", "1.16", "3006", "800",

               "Производство сельскохозяйственной продукции без права возведения объектов " +
               "капитального строительства ",

               "Ведение личного подсобного хозяйства на полевых участках",
               "",
               "",
               new string[] { }));

            nodes.Add(new Agronomy("1.17.0", "1.17", "3006", "800",

               "Выращивание и реализация подроста деревьев и кустарников, используемых в сельском хозяйстве, " +
               "а также иных сельскохозяйственных культур для получения рассады и семян; " +
               "размещение сооружений, необходимых для указанных видов сельскохозяйственного производства ",

               "Питомники",
               "",
               "",
               new string[] {
                    @"",
                    @"\bпитомн\w*\b.*\bрастен\w*\b",

                    @"",
                    @"\bнеплодород\w*\s*кустарн\w*\b",

                    @"",
                    @"\bпитомн\w*\s*(садов\w*|декоратив\w*)\b",

                    @"",
                    @"\bвыращиван\w*\s*декоратив\w*\s*растен\w*\b" }
               ));

            nodes.Add(new Agronomy("1.18.0", "1.18", "3006", "800",

               "Размещение машинно-транспортных и ремонтных станций, ангаров и гаражей для " +
               "сельскохозяйственной техники, амбаров, водонапорных башен, трансформаторных станций и " +
               "иного технического оборудования, используемого для ведения сельского хозяйства ",

               "Обеспечение сельскохозяйственного производства",
               "",
               "",
               new string[] {
                    @"",
                    @"\bсел\w*\s*хоз\w*\s*техн\w*\b",

                    @"",
                    @"(?:\bамбар\w*\b|\bводонапор\w*\b|\bтрансформат\w*\b).*\bсел\w*\s*хоз\w*\b|" +
                    @"\bсел\w*\s*хоз\w*\b.*(?:\bамбар\w*\b|\bводонапор\w*\b|\bтрансформат\w*\b)",

                    @"",
                    @"\bобслуживан\w*\s*объект\w*\s*(недвижимого\s*имущества\s*)?\bсельскохоз\w*\s*(использ\w*|назнач\w*|угодия)\b",

                    @"",
                    @"\bобслуживан\w*\s*сара[йяе]в?\b" }
               ));

            nodes.Add(new Dwelling("2.0.0", "2.0", "2000", "200",

               "Размещение жилых помещений различного вида и обеспечение проживания в них. " +
               "К жилой застройке относятся здания (помещения в них), предназначенные для проживания человека; " +
               "Содержание данного вида разрешенного использования включает в себя содержание видов " +
               "разрешенного использования с кодами 2.1-2.7.1 ",

               "Жилая застройка",
               "Жилая зона",
               "",
               new string[] {
                    @"\bпри\sстроительстве\sжил\w*\b",
                    @"\bжил(ого|ых|ую|ые|ая|ом|ой|ищн\w*|ыми?|ому)\s*(дом\w*|строительств\w*|(за)?стройк\w*|" +
                    @"комплекс\w*|зон\w*|микрорай\w*|здан\w*|квартал\w*)\b",

                    @"",
                    @"\bжилое\b|\bздан\w*\s*под\sжилье\b",

                    @"",
                    @"\bжил\w*\s*корпус\w*\b",

                    @"",
                    @"\bпод\s*жилые\s*цели\b",

                    @"",
                    @"\bжил\w*\s*фонд\w*\b",

                    @"",
                    @"\bобъект\w*\s*жил\w*\s*назнач\w*\b|\bжил\w*\s*цел\w*\b",

                    @"",
                    @"\bкондоминиум\w*\b",

                    @"",
                    @"\bэксплуатац\w*\s*жил\w*\s*помещен\w*\b",

                    @"",
                    @"\bзем\w*\s*участ\w*\s*смешан\w*\s*размещен\w*\b",

                    @"",
                    @"\bжил\w*[-\s]*комм?ерч\w*\s*комплекс\w*\b",

                    @"",
                    @"\bжил\w*\s*и\s*вспомогат\w*\s*строен\w*\b" }
               ));

            nodes.Add(new Housing("2.1.0", "2.1", "2001", "200",

               "Размещение индивидуального жилого дома (дом, пригодный для постоянного проживания, " +
               "высотой не выше трех надземных этажей); выращивание плодовых, ягодных, овощных, " +
               "бахчевых или иных декоративных или сельскохозяйственных культур; " +
               "размещение индивидуальных гаражей и подсобных сооружений ",

               "Для индивидуального жилищного строительства",
               "Жилая зона",
               "",
               new string[] {
                    @"",
                    @"\bи?ндивид\w*\b\s*(жил\w*|дом\w*|строит\w*)\b|" +
                    @"\bижс\b|\bкоттедж\w*\b|" +
                    @"\bодноквартир\w*\b|\bчастн\w*\s*дом\w*\b",

                    @"",
                    @"\bиндивид\w*\s*(ж?жи?о?[лд]ищ)",

                    @"",
                    @"\bдомовладен\w*\b",

                    @"",
                    @"\bдол\w*\s*дом\w*\b|\bчаст\w*\s*дом\w*\b|\bсодержан\w*\s*дом\w*\b",

                    @"",
                    @"\bжил\w*\s*индивид\w*\b",

                    @"",
                    @"\bжил\w*\b.*\bдеревян\w*\b.*\bдом\w*\b",

                    @"",
                    @"\bпостройк\w*\s*дом\w*\b",

                    @"",
                    @"\bдом\w*\s*смотрит\w*\b",

                    @"",
                    @"\bинд.\s*стр-в\w*\b|\bдвухэтаж\w*\s*дом\w*\s*под\s*жил\w*\s*цел\w*\b",

                    @"",
                    @"\bжил\w*\s*застр\w*\s*индивид\w*\b|\bиндивид\w*\b.*\bжил\w*\s*застр\w*\b" }
               ));

            nodes.Add(new Housing("2.1.1.0", "2.1.1", "2002", "200",

               "Размещение малоэтажного многоквартирного жилого дома, (дом, пригодный для " +
               "постоянного проживания, высотой до 4 этажей, включая мансардный);" +
               "разведение декоративных и плодовых деревьев, овощных и ягодных культур;" +
               "размещение индивидуальных гаражей и иных вспомогательных сооружений;" +
               "обустройство спортивных и детских площадок, площадок отдыха; размещение объектов " +
               "обслуживания жилой застройки во встроенных, пристроенных и встроенно-пристроенных " +
               "помещениях малоэтажного многоквартирного дома, если общая площадь таких помещений " +
               "в малоэтажном многоквартирном доме не составляет более 15% общей площади помещений дома ",

               "Малоэтажная многоквартирная жилая застройка",
               "Жилая зона",
               "",
               new string[] {
                    @"блокированн?\w*\s*дома|нескольких\sблоков-секций|\bмногоэтажн\w*\b",
                    @"\bмногоквартир\w*\b|\bмалоэтаж\w*\s*многоквартир\w*\b|\bмалоэтаж\w*\s*жил\w*\b",

                    @"",
                    @"\bмалоэтаж\w*\s*(застрой\w*|строит\w*)\b" }
               ));

            nodes.Add(new Housing("2.2.0", "2.2", "2001", "200",

               "Размещение жилого дома, не предназначенного для раздела на квартиры " +
               "(дома, пригодные для постоянного проживания и высотой не выше трех надземных этажей);" +
               "производство сельскохозяйственной продукции; размещение гаража и иных " +
               "вспомогательных сооружений;содержание сельскохозяйственных животных ",

               "Для ведения личного подсобного хозяйства",
               "Жилая зона",
               "",
               new string[] {
                    @"",
                    @"\bиндивид\w*\b\s*жил\w*\b|\bкоттедж\w*\b|" +
                    @"\bодноквартир\w*\b|\bчастн\w*\s*дом\w*\b",

                    @"",
                    @"\bмалоэтажн\w*\s*строит\w*\b",

                    @"",
                    @"\bмалоэтаж\w*\s*(застрой\w*|строит\w*)\b",

                    @"",
                    @"\bличн\w*\s*подсоб[.]?\w*\s*(хозяйств\w*|х-ва|хоз-ва)\b",

                    @"",
                    @"\bличн\w*\s*(хоз\w*|подсобнc?\w*)\b|\bподсобн\w*\s*хоз\w*\b",

                    @"",
                    @"\bи?ндивид\w*\b\s*(жил\w*|дом\w*|строит\w*)\b|" +
                    @"\bижс\b|\bкоттедж\w*\b|" +
                    @"\bодноквартир\w*\b|\bчастн\w*\s*дом\w*\b",

                    @"",
                    @"\bиндивид\w*\s*(ж?жи?о?[лд]ищ)",

                    @"",
                    @"\bинд.\s*стр-в\w*\b|\bдвухэтаж\w*\s*дом\w*\s*под\s*жил\w*\s*цел\w*\b",

                    @"",
                    @"\bжил\w*\s*застр\w*\s*индивид\w*\b" }
               ));

            nodes.Add(new Housing("2.3.0", "2.3", "2002", "200",

               "Размещение жилого дома, не предназначенного для раздела на квартиры, " +
               "имеющего одну или несколько общих стен с соседними жилыми домами " +
               "(количеством этажей не более чем три, при общем количестве совмещенных " +
               "домов не более десяти и каждый из которых предназначен для проживания " +
               "одной семьи, имеет общую стену (общие стены) без проемов с соседним блоком" +
               "или соседними блоками, расположен на отдельном земельном участке и имеет " +
               "выход на территорию общего пользования (жилые дома блокированной застройки); " +
               "разведение декоративных и плодовых деревьев, овощных и ягодных культур; " +
               "размещение индивидуальных гаражей и иных вспомогательных сооружений; " +
               "обустройство спортивных и детских площадок, площадок отдыха ",

               "Блокированная жилая застройка",
               "Жилая зона",
               "",
               new string[] {
                    @"",
                    @"\bблокирован\w*\s*\b(застро[ий]к\w*|таунхаус\w*|(жил\w*\s*)?дом\w*)\b|\bтаунхаус\w*\b",

                    @"",
                    @"\bблокирован\w*\b.*жил\w*\s*застр\w*\b",

                    @"",
                    @"\bмалоэтажн\w*\s*строит\w*\b",

                    @"",
                    @"\bмалоэтаж\w*\s*(застрой\w*|строит\w*)\b" }
               ));

            nodes.Add(new Housing("2.4.0", "2.4", "1004", "100",

               "Размещение сооружений, пригодных к использованию в качестве жилья " +
               "(палаточные городки, кемпинги, жилые вагончики, жилые прицепы) с возможностью " +
               "подключения названных сооружений к инженерным сетям, находящимся на земельном " +
               "участке или на земельных участках, имеющих инженерные сооружения, " +
               "предназначенных для общего пользования ",

               "Передвижное жилье",
               "Жилая зона",
               "",
               new string[] {
                   @"",
                   @"\bпалаточ\w*\s*город\w*\b|\bкемпинг\w*\b|\bжил\w*\sвагон\w*\b",

                   @"",
                   @"\bбытовок\b|\bбытов\w*\s*город\w*\b|\bштаб\w*\s*строит\w*\b",

                   @"",
                   @"\b(жил\w*|строит\w*)\s*(городок|городка)\b|\bпрорабск\w*\b" }
               ));

            nodes.Add(new Housing("2.5.0", "2.5", "2002", "200",

               "Размещение жилых домов, предназначенных для разделения на квартиры, каждая " +
               "из которых пригодна для постоянного проживания (жилые дома, высотой не выше " +
               "восьми надземных этажей, разделенных на две и более квартиры); " +
               "благоустройство и озеленение;размещение подземных гаражей и автостоянок;" +
               "обустройство спортивных и детских площадок, площадок отдыха; " +
               "размещение объектов обслуживания жилой застройки во встроенных, пристроенных и " +
               "встроенно-пристроенных помещениях многоквартирного дома, если общая площадь " +
               "таких помещений в многоквартирном доме не составляет " +
               "более 20% общей площади помещений дома ",

               "Среднеэтажная жилая застройка",
               "Жилая зона",
               "",
               new string[] {
                    @"\bмалоэтаж\w*\b",
                    @"\bмногоквартирн\w*\b|\bмногоэтаж\w*\s*(жил\w*|дом\w*|застрой\w*)\b" }
               ));

            nodes.Add(new Housing("2.6.0", "2.6", "2002", "200",

               "Размещение жилых домов, предназначенных для разделения на квартиры, " +
               "каждая из которых пригодна для постоянного проживания (жилые дома высотой " +
               "девять и выше этажей, включая подземные, разделенных на двадцать и более квартир); " +
               "благоустройство и озеленение придомовых территорий; " +
               "обустройство спортивных и детских площадок, хозяйственных площадок; размещение " +
               "подземных гаражей и наземных автостоянок, размещение объектов обслуживания жилой " +
               "застройки во встроенных, пристроенных и встроенно-пристроенных помещениях " +
               "многоквартирного дома в отдельных помещениях дома, если площадь таких " +
               "помещений в многоквартирном доме не составляет более 15% от общей площади дома ",

               "Многоэтажная жилая застройка (высотная застройка)",
               "Жилая зона",
               "",
               new string[] {
                    @"\bмалоэтаж\w*\b",
                    @"\bмногоквартирн\w*\b|\bмногоэтаж\w*\s*\bжил\w*\b",

                    @"",
                    @"\bмногоэтажн\w*\s*(дом\w*|застрой\w*)\b",

                    @"",
                    @"\b\d+-ти\s*этаж\w*\s*жил\w*\s*дом\w*\b" }
               ));

            nodes.Add(new MaintenanceOfResidentialBuildings("2.7.0", "2.7", "1000", "100",

               "Размещение объектов капитального строительства, размещение которых предусмотрено видами " +
               "разрешенного использования с кодами " +
               "3.1, 3.2, 3.3, 3.4, 3.4.1, 3.5.1, 3.6, 3.7, 3.10.1, 4.1, 4.3, 4.4, 4.6, 4.7, 4.9, " +
               "если их размещение связано с удовлетворением повседневных потребностей жителей, не причиняет " +
               "вреда окружающей среде и санитарному благополучию, не причиняет существенного неудобства жителям, " +
               "не требует установления санитарной зоны ",

               "Обслуживание жилой застройки",
               "",
               "",
               new string[] {
                   @"\bлпх\b|\bличн\w*\s*подсобн\w*\b",
                   @"\bпристройк\w*\s*к\sдому\b|\bжилых\s*и\s*нежилых\b" }
               ));

            nodes.Add(new HParking("2.7.1.0", "2.7.1", "3004", "300",

               "Размещение отдельно стоящих и пристроенных гаражей, в том числе подземных, " +
               "предназначенных для хранения личного автотранспорта граждан, " +
               "с возможностью размещения автомобильных моек ",

               "Объекты гаражного назначения",
               "",
               "",
               new string[] {
                    @"\bавтостоян\w*\b",
                    @"\bхранен\w*\s*личн\w*\b\s*((авто)?транспор\w*|автомоб\w*)\b|" +
                    @"\bметал\w*\s*(тент\w*|гараж\w*)\b|\bиндивидуал\w*\s*гараж\w*\b",

                    @"\bавтостоян\w*\b",
                    @"\bтент\w*\b.*\b(ракушк\w*|\bпенал\w*\b)\b",

                    @"\bавтостоян\w*\b",
                    @"\bсбор\w*[-\s]*разбор\w*\b.*\bтент\w*\b" }
               ));

            nodes.Add(new BaseCommunity("3.0.0", "3.0", "1000", "100",

               "Размещение объектов капитального строительства в целях обеспечения удовлетворения " +
               "бытовых, социальных и духовных потребностей человека. Содержание данного вида " +
               "разрешенного использования включает в себя содержание видов разрешенного " +
               "использования с кодами 3.1-3.10.2 ",

               "Общественное использование объектов капитального строительства",
               "",
               "",
               new string[] {
                    @"",
                    @"смешанного общественного вида",

                    @"",
                    @"\bсоциальн\w*\s*назнач\w*\b|\bкультур\w*[-\s]*быт\w*\b",

                    @"",
                    @"\bмногофункц\w*\s*обществ\w*\s*центр\w*\b",

                    @"",
                    @"\bмногофункц\w*\s*(комплекс\w*|здан\w*|\bназнач\w*)\b",

                    @"",
                    @"\bобъект\w*\s*обществен\w*\s*назначен\w*\b",

                    @"",
                    @"\bзем\w*\s*участ\w*\s*смешан\w*\s*размещен\w*\b",

                    @"",
                    @"\bсоцкультбыт\w*\s*\b" }
               ));

            nodes.Add(new Community("3.1.1", "3.1", "3004", "300",

               "Размещение объектов капитального строительства в целях обеспечения физических и " +
               "юридических лиц коммунальными услугами, в частности: поставки воды, тепла, " +
               "электричества, газа, предоставления услуг связи, отвода канализационных " +
               "стоков, очистки и уборки объектов недвижимости (котельных, водозаборов, " +
               "очистных сооружений, насосных станций, водопроводов, линий электропередач, " +
               "трансформаторных подстанций, газопроводов, линий связи, телефонных станций, " +
               "канализаций, стоянок, гаражей и мастерских для обслуживания уборочной и аварийной техники ",

               "Коммунальное обслуживание",
               "",
               "",
               new string[] {
                    @"личн|авто",
                    @"\bремонт\w*\s*транспорт\w*\b|обслуж\w{0,5}\s*транспорта",

                    @"",
                    @"\bводозабор\w*\b|\bводомерн\w*\s*уз\w*\b",

                    @"",
                    @"\bводопроводн\w{0,5}\s*уз\w{0,2}|\bводопроводн.{1,5}\s*станц\w*\b",

                    @"",
                    @"(газо|водо|трубо)[-\s]*провод",

                    @"",
                    @"\bнасосн\w*\s*станц\w*\b|\bводонапор\w*\b",

                    @"",
                    @"очист\w{0,5}\sсооруж|\bнапорн\w*\s*канализ\w*\b",

                    @"",
                    @"\bтепло\w*\s*(павиль\w*|пункт\w*|подстанц\w*|сет\w*)\b|\bпавиль\w*\s*№\d+|" +
                    @"\bпавиль\w*\s*теплов\w*\s*сет\w*|\bкамер\w*-павиль\w*\b|" +
                    @"\bтепломагистр\w*\b|\bтеплоэнергетич\w*\s*оборудов\w*\b",

                    @"",
                    @"аварийн\w*\s*(сервис\w*\s*)?служб\w*\b",

                    @"",
                    @"телефон\w{0,5}\sстанц|телефон.{1,5}\sАТС|\bАТС\b",

                    @"",
                    @"\bгазо\w{0,5}\s*(распред\w*|регул\w*|хозяйств\w*)\b|\bгрс\b",

                    @"\bмодуль\sтп\b",
                    @"комм?ун\w*\s*(хоз\w*|объек\w*|услуг\w*)\b|\bк[оа]тельн\w*\b|\bктс\b|\bк?тп\b|" +
                    @"\bжилищ\w*[-\s]*комм?унал\w*\b|\bбойлер\w*\b|\bгрпш\b|\bцтп\b|" +
                    @"\bтеплоэнергоснаб\w*\b|\bцтп\w*\b",

                    @"",
                    @"механиз.{0,9}\sуборка",

                    @"",
                    @"водо.{1,13}\sсет\w\s|тепло.{1,13}\sсет\w\s|канализация",

                    @"\bна\s*период\b",
                    @"опор\w?\sлэп|лини.{1,5}\sлэп|опор\w?\sэлектро|лини.{1,5}\sэлектро",

                    @"библио|переработк",
                    @"коллектор|\bмусор\w*\s*контейнер\w*\b|контейнерн\w*\s*площадк\w*\b|\bмусор\w{0,4}\b",

                    @"арбитражного",
                    @"инженерн.{1,5}\sсооруж",

                    @"",
                    @"лини.{1,5}\sсвяз",

                    @"",
                    @"стоян.{1,5}\s*аварйин.{1,5}\s*техн|гараж.{1,5}\s*аварйин.{1,5}\s*техн|" +
                    @"мастерск.{1,5}\s*аварйин.{1,5}\s*техн|\bаварий\w*[-\s]*эксплуа?т\w*\b" +
                    @"\bстоян\w*\s*уборноч\w*\s*(трактор\w*|техник\w*)\b",

                    @"",
                    @"\bконтейнер\w*\s*(ТБО|тверд\w*\s*быт\w*|для\s*картон\w*)\b",

                    @"",
                    @"\bхоз\w*\s*двор\w*\b",

                    @"",
                    @"\bартезианск|w*\s*скваж\w*\b",

                    @"",
                    @"\bкомпрессорн\w*\b|\bзтп\b|\bг?рп\b",

                    @"",
                    @"\bглаз\b|\bкип,кт\b|\bкип\b|\bкран\b|\bкт\b|" +
                    @"\bку(\(скз\))?\b|\bскз\b|\bстол\w*\s*питан\w*",

                    @"",
                    @"\bинженер\w*\s*(инфраструк\w*|комм?уник\w*|служб\w*)\b",

                    @"",
                    @"\bмастерск\w*\s*участ\w*\b|\bгазгольд\w*\b",

                    @"",
                    @"\bузе?л\w*\s*(учет\w*\s*газ\w*|теплосет\w*)\b",

                    @"",
                    @"\bпавиль\w*\s*№",

                    @"",
                    @"\bк?лэп\b|\bпередач\w*\s*(электро)?энерг\w*\b|" +
                    @"\bвысоковольт\w*\s*каб\w*\b|\bпереходн\w*\s*пункт\w*\b",

                    @"",
                    @"\bвл\s*\b\d+\b\s*кв\b|\bртп\b|\bкабел\w*\s*лин\w*\b|" +
                    @"\bэлектрич\w*\s*кабел\w*\b",

                    @"",
                    @"\bкомм?ун\w*\s*назнач\w*\b|\bремонт\w*[-\s]*эплуа?тац\w*\b",

                    @"",
                    @"\bхозяйств\w*[-\s]*(быт\w*\s*)?(нужд\w*|деятель\w*)\b",

                    @"",
                    @"\bвузремстройм\w*\b|\bотст\w*\s*убороч\w*\b",

                    @"",
                    @"\bтеплосет\w*\b|\bаварий\w*[-\s]сервис\w*\s*служб\w*\b*",

                    @"",
                    @"\bаэраци\w*\b|\bотсто[яй]\s*.*авто(маш\w*|транспорт\w*)\b",

                    @"",
                    @"\bпротивоголо\w*\s*реаген\w*\b|\bтрансформ\w*\b|\sтп\s.*\bдоу\b",

                    @"мвд",
                    @"\bспецтехник\w*\b|\bспутник\w*\s*антен\w*\b",

                    @"",
                    @"\bпруд\w*[-\s]*отстой\w*\b",

                    @"",
                    @"\bснег\w*(свал\w*|с?плав\w*|а|уборочн\w*)\b",

                    @"",
                    @"\b(контактн\w*|переходн\w*)\s*устройств\w*\b",

                    @"",
                    @"\bфидерн\w*\s*пункт\w*\b|\bцрп\b|\bкнс\b|\bшрп\b|\bгрпш\b|\bвзс\b|\bапс\b",

                    @"",
                    @"\bантен\w*\s*опор\w*\b|\bводоснабжен\w*\b",

                    @"",
                    @"\bтелемех[.]*\w*\s*центр\w*\b",

                    @"",
                    @"\bмосводоканал\w*\b",

                    @"",
                    @"\bавтоном\w*\s*источн\w*\s*теплоснаб\w*\b",

                    @"",
                    @"\bтехнич\w*\s*служб\w*\b|\bкомм?унал\w*[-\s]*эксплуатац\w*\s*баз\w*\b",

                    @"",
                    @"\bаварий\w*[-\s]*регулир\w*\s*резерв\w*\b",

                    @"",
                    @"\b(баз\w*\s*)?аварийн\w*\s*служб\w*\b|\bводонасосн\w*\s*станц\w*\b",

                    @"",
                    @"\bглубин\w*\s*анодн\w*\s*заземлит\w*\b",

                    @"",
                    @"\bдорожно[-\s]*механич\w*\s*баз\w*\b|\bуправлен\w*\s*механизац\w*\b",

                    @"",
                    @"\bавтодормехбаз\w*\b",

                    @"",
                    @"\bраспределит\w*\s*пункт\w*\b",

                    @"",
                    @"\bнасосн\w*\s*канализац\w*\s*станц\w*\b",

                    @"",
                    @"\bрадио[-\s]*релейн\w*\s*мачт\w*\b",

                    @"",
                    @"\bремонт\w*\b.*\bмеханизац\w*\b",

                    @"",
                    @"\bрезерв\w*\b.*регулир\w*\s*уз\w*\b",

                    @"",
                    @"\bочистк\w*\s*сто[кч]\w*\b|\bстанц\w*\s*(катод\w*\s*защит\w*|перекач\w*)\b",

                    @"",
                    @"\bглубок\w*\s*дренаж\w*\b|\bводорегулир\w*\s*уз\w*\b",

                    @"",
                    @"\bоборудован\w*\b.*\bтв\b|\bтв[-\s]*оборудован\w*\b",

                    @"",
                    @"\bремонтн\w*\s*участ\w*\b",

                    @"",
                    @"\bмостерск\w*\s*по\s*ремонт\w*\s*электрооборудован\w*\b",

                    @"",
                    @"\bбктп\b|\bвнутриплощад\w*\s*водохранилищ\w*\b",

                    @"",
                    @"\bобслуживан\w*\s*электролин\w*\b|\bочистн\w*\s*ливнев\w*\b",

                    @"",
                    @"\bбаз\w*\s*дму\b|\bбашн\w*\s*сотов\w*\s*связ\w*\b",

                    @"",
                    @"\bпункт\w*\s*маслоподпитк\w*\b|\bтелекомм?уникац\w*\s*центр\w*\b",

                    @"",
                    @"\bбаз\w*\b.*\b(комм?унальн\w*|дорож\w*[-\s]*строит\w*)\s*техник\w*\b" }
               ));

            nodes.Add(new Community("3.1.2", "3.1", "1004", "100",

               "Размещение помещений и технических устройств общественных туалетов ",

               "Коммунальное обслуживание",
               "",
               "",
               new string[] {
                   @"",
                   @"туалет" }
               ));

            nodes.Add(new Community("3.1.3", "3.1", "1001", "100",

               "Размещение зданий или помещений, предназначенных для " +
               "приема физических и юридических лиц в связи с предоставлением им коммунальных услуг ",

               "Коммунальное обслуживание",
               "",
               "",
               new string[] {
                    @"авиа|дорожно-эксплуатац|отстойно|\bконечн\w{0,2}\s+\w{0,11}?\s*\bстанц\w+|" +
                    @"\sпод\sмагазин|\sнасосной\sстанции|\bпредприят\w{0,3}\s+транспор|антенного|" +
                    @"\bобщественного\s+транспорта|пассажирских\s+терминалов|складских\s+предприятий|" +
                    @"стоянки\s+такси|городск\w{0,3}\s*транспорт|сбербанк|торгово-развл",
                    @"диспетчер",

                    @"",
                    @"диспетчер\w*\b\s*служб\w*\b",

                    @"",
                    @"прием.+лиц.+комм?унал.+услуг",

                    @"",
                    @"\bкомм?унал\w*\s*обслуж\w*\b",

                    @"",
                    @"\bкомм?унль\w*[-\s]*быт\w*|\bодс\b",

                    @"",
                    @"\bобъекты размещения некоммерческих организаций, связанных с проживанием населения\b" }
            ));

            nodes.Add(new Community("3.2.1", "3.2", "1007", "100",

               "Размещение объектов капитального строительства, предназначенных для оказания " +
               "гражданам социальной помощи (службы занятости населения, дома престарелых, " +
               "дома ребенка, детские дома, пункты питания малоимущих граждан, " +
               "пункты ночлега для бездомных граждан) ",

               "Социальное обслуживание",
               "",
               "",
               new string[] {
                    @"",
                    @"\bсоц\w*\s*защит\w*\b",

                    @"",
                    @"\bгеронто\w*\s*центр\w*\b|\bсоц\w*\s*адаптац\w*\b|" +
                    @"\bпсихо\w*[\s\-]*педагог\w*\b|\bдет\w*\s*дом\w*\b|" +
                    @"\bдом\w*\s*реб[её]н\w*\b",

                    @"животн",
                    @"\bпансионат\w*\b.*\bветеран\w*\b|\bсоц\w*\s*гостин\w*\b|" +
                    @"\bноч\w*\s*пребыв\w*\b|\bприют\w*\s*бездомн\w*\b|" +
                    @"\bпункт\w*\s*ночлег\w*\b",

                    @"",
                    @"\bслужб\w*\s*занятост\w*\s*населен\w*\b",

                    @"",
                    @"\bреабилитац\w*\s*инвалид\w*\b|\bреабилитац\w*\b",

                    @"",
                    @"\b(лечеб\w*|социал\w*)[-\s]*реабилитац\w*\b|\bдом\w*[-\s]*интернат\w*\b",

                    @"",
                    @"\bсоциал\w*\s*(приют\w*|отделен\w*)\b" }
               ));

            nodes.Add(new Community("3.2.2", "3.2", "1007", "100",

               "Размещение объектов капитального строительства для служб психологической и " +
               "бесплатной юридической помощи, социальных, пенсионных и иных служб, в которых " +
               "осуществляется прием граждан по вопросам оказания социальной помощи и " +
               "назначения социальных или пенсионных выплат ",

               "Социальное обслуживание",
               "",
               "",
               new string[] {
                    @"",
                    @"\bюридических\sуслуг\sнаселению\b|\bсоц\w*\s*обеспеч\w*\b|" +
                    @"\bсоц\w*[\s\-]*психо\w*\b|\bпсихолого-медико-социального\b",

                    @"",
                    @"\bпенсион\w*\b\s*обеспеч\w*\b|\bсоц\w*\b\s*поддерж\w*\b|" +
                    @"\bпсихо\w*\b\s*поддерж\w*\b|\bсоц\w*\b\s*защит\w*\b",

                    @"",
                    @"\bсоц\w*\b\s*обслуж\w*\b|\bпсих\w*\b\s*помощ\w*\b|\bсоц\w*\b\s*помощ\w*\b",

                    @"",
                    @"\b(комплекс\w*|центр\w*)\s*занятос\w*\b|\bОбслуживан\w*\s*инвалид\w*\b",

                    @"",
                    @"\bгусо мо рц\b|\bмастерск\w*\b.*\bдля\s*дет\w*[-\s]*инвалид\w*\b" }
               ));

            nodes.Add(new Community("3.2.3", "3.2", "1004", "100",

               "Размещение объектов капитального строительства для размещения отделений почты " +
               "и телеграфа ",

               "Социальное обслуживание",
               "",
               "",
               new string[] {
                    @"",
                    @"\bотдел\w*\s*связ\w*\b|\bтелеграф\w*\b",

                    @"\bсортиров\w*\b|\bсклад\w*\b",
                    @"\bпочт\w*\b",

                    @"",
                    @"\bпочт\w*[-\s]*сортиров\w*\b" }
               ));

            nodes.Add(new Community("3.2.4", "3.2", "1001", "100",

               "Размещение объектов капитального строительства для размещения общественных " +
               "некоммерческих организаций: некоммерческих фондов," +
               "благотворительных организаций, клубов по интересам ",

               "Социальное обслуживание",
               "",
               "",
               new string[] {
                   @"",
                   @"благотвор\w*\b",

                   @"спорт|фитнес|культурно-просветит|бильярд|" +
                   @"кафе-бар|клуб-бар|досугов|автоклуба\sс\sмойкой|кафе-клуб",
                   @"клуб",

                   @"",
                   @"\bнекоммерческих организаций\b.*не связанных с проживанием населения\b",

                   @"",
                   @"\bлитератур\w*\s*фонд\w*\s*россии\w*\b" }
               ));

            nodes.Add(new Community("3.3.0", "3.3", "1004", "100",

               "Размещение объектов капитального строительства, предназначенных для оказания " +
               "населению или организациям бытовых услуг (мастерские мелкого ремонта, " +
               "ателье, бани, парикмахерские, прачечные, химчистки, похоронные бюро и т.п.) ",

               "Бытовое обслуживание",
               "",
               "",
               new string[] {
                    @"",
                    @"\bба[н]{1,2}[^к]*\b|\bпрач\w*\b|\bремонт\w*\s*быт\w*\b",

                    @"",
                    @"\bатель\w*\b|\bпарикмахер\w*\b|\s\bпрокат\w*\b\s+\b\w*\b",

                    @"\bторговые\s+центры\b",
                    @"\bбыт\w*\s*(обслуж\w*|услуг\w*)\b|\bпри[её]м\w*\b\sпункт\w*\b|" +
                    @"\bслужб\w*\s*быт\w*\b|\bчист\w*\s*обув\w*\b",

                    @"",
                    @"\bремонт\w*\s+\bтрикотаж\w*\b|\bремонт\w*\s+\bодежд\w*\b|" +
                    @"\bремонт\w*\s+\bобув\w*\b|\bремонт\w*\s+\bшвейн\w*\b|\bремонт\w*\s+\bмехо\w*\b|" +
                    @"\bремонт\w*\s+\bхудоже\w*\b|\bремонт\w*\s+\bювелир\w*\b|\bремонт\w*\s+\bткан\w*\b|" +
                    @"\bремонт\w*\s+\bкож\w*\b|\bремонт\w*\s+\bгалантер\w*\b|\bремонт\w*\s+\bтекстил\w*\b",

                    @"",
                    @"\bметалл?[оа]ремон\w*\b|\bремон\w*\s*час\w*\b|\bдом\w*\s*быта\b",

                    @"",
                    @"\bремонт\w*\s+\bмебел\w*\b|\bреставрац\w*\s+\bмебел\w*\b",

                    @"\bавтомастерск\w*|\bтворческ\w*\s*мастерск\w*\b|" +
                    @"\bавто?(машин\w*|мобил\w*|транспорт\w*)\b|\bавтосервис\w*\b|\bшиномонтаж\w*\b|\bпроизводств\w*\b|\bхудож\w*\b|" +
                    @"\bскульптур\w*\b|\bживописн\w*\b|\bтворческ\w*\b|\bмеханич\w*\b|\bэлектооборудован\w*\b|" +
                    @"\bдет\w*[-\s]*инвалид\w*\b|\bмебельн\w*\b|\bтонировк\w*\b|\bтелемеханическ\w*\b",
                    @"\bмастерск\w*\b",

                    @"\bавто?(машин\w*|мобил\w*|транспорт\w*)\b|\bэлект\w*\s*оборудован\w*\b|" +
                    @"\bтелемеханическ\w*\b",
                    @"\bмастерск\w*\s*по\s*ремонт\w*\b",

                    @"",
                    @"\bфото(мастерск\w*|ателье\w*)|\bфото\w*\b",

                    @"",
                    @"\bремонт\w*\s+\bаппарат\w*\b|\bремонт\w*\s+\bприбор\w*\b|" +
                    @"\bремонт\w*\s+\bоборудов\w*\b|\bхимчист\w*\b|\s\bпрачеч\w*\b",

                    @"",
                    @"\bсалон\w*\s*красот\w*|\bточильн\w*\s*мастерск\w*\b|\bфотоуслуг\w*\b",

                    @"\bчасовни|\bчасовня",
                    @"\bювелирн\w*\b|\bчасов\w*\b|\bфотоатель\w*\b|\bшвейн\w*\s*мастерск\w*\b",

                    @"",
                    @"\bсервис\w*\s*центр\w*\b|кбо|\bблок\w*\s*обслуживан\w*\b|\bкинофотолаб\w*\b",

                    @"",
                    @"\bобществен\w*[-\s]*бытов\w*\s*назнач\w*\b",

                    @"",
                    @"\bкомбин\w*\s*быт\w*\s*обслуж\w*\b",

                    @"",
                    @"\bстуд\w*\s*красот\w*\b|\bпастижер\w*\b",

                    @"",
                    @"\bпункт\w*\s*(видео)?прокат\w*\b|\bломбард",

                    @"",
                    @"\bремонт\w*\s*и\s*пошив\w*\s*обув\w*\b",

                    @"",
                    @"\bуслуг\w*\s*населен\w*\b|\b(санитар\w*)?[-\s]*быт\w*\s*корпус\w*\b",

                    @"",
                    @"\bприем\w*\s*стеклотар\w*\b|\b(спец)?химчистк\w*\b",

                    @"",
                    @"\bсалон\w*\s*причес\w*\b|\bсоляр\w*\b|\bкометическ\w*\s*салон\w*\b",

                    @"",
                    @"\bкомплекс\w*\s*сервис\w*\s*услуг\w*\b",

                    @"",
                    @"\bцентр\w*\s*красот\w*\s*и\s*здоров\w*\b",

                    @"",
                    @"\bдизайнерск\w*\s*центр\w*\b",

                    @"",
                    @"\bпродаж\w*\s*и\s*ремонт\w*\s*оргтехн\w*\b",

                    @"",
                    @"\bобмен\w*\s*газ\w*\s*баллон\w*\b",

                    @"",
                    @"\bкосметич\w*\s*салон\w*\b|\bремонт\w*\b.*\bгаз\w*\s*оборудов\w*\b" }
               ));

            nodes.Add(new HealthCare("3.4.0", "3.4", "1005", "100",

               "Размещение объектов капитального строительства, предназначенных для оказания " +
               "гражданам медицинской помощи. Содержание данного вида разрешенного " +
               "использования включает в себя содержание видов разрешенного " +
               "использования с кодами 3.4.1 - 3.4.2 ",

               "Здравоохранение",
               "",
               "",
               new string[] {
                    @"",
                    @"\bсанитарно[\s\-]*эпидемиолог\w*\b|\bпротивочумн\w*\b|\bдезинфекц\w*\b|" +
                    @"\bгигиен\w*\b\s+и\s+эпидемио\w*",

                    @"санитарно-защит",
                    @"\bморг\w*\b|\bмедицин\w*\s+\bпомо\w+\b|\bздравоо?хранен\w*\b",

                    @"",
                    @"\b(лечебн\w*|медицин\w*)\s*деятел\w*\b",

                    @"",
                    @"\bмедицин\w*\s*(цел\w*|центр\w*|учрежд\w*|услуг\w*)\b",

                    @"",
                    @"\bмедицин\w*\b",

                    @"",
                    @"\bобъект\w*\s*лечебно-оздоровит\w*\b" }
               ));

            nodes.Add(new Clinic("3.4.1.0", "3.4.1", "1005", "100",

               "Размещение объектов капитального строительства, предназначенных для " +
               "оказания гражданам амбулаторно-поликлинической медицинской помощи " +
               "(поликлиники, фельдшерские пункты, пункты здравоохранения, центры матери и " +
               "ребенка, диагностические центры, молочные кухни, станции донорства " +
               "крови, клинические лаборатории) ",

               "Амбулаторно-поликлиническое обслуживание",
               "",
               "",
               new string[] {
                    @"открытой\sгостевой\sавтостоянки|\bветеринарн\w*\b",
                    @"\bполиклин\w*\b|\bдиспансер\w*\b|\bженск\w*\b\s*\bконсульт\w*\b|" +
                    @"\bамбулатор\w*\b|\bмедико-санитар\w*\b",

                    @"\bветеринар\w*\b",
                    @"\bмолоч\w*\s*\bкухн\w*\b|\bперелив\w*\s*кров\w*\b|\bклинич\w*\s*лаборат\w*\b",

                    @"\bинститут\w*|\bветеринарн\w*\b",
                    @"\bцентр\w*\s*матер\w*\b|\bстоматолог\w*\s*\b\w*\b|\bфельдшер\w*\s*пункт\w*\b|" +
                    @"\bцентр\w*\s*диагност\w*\b|\bстанц\w*\s*донор\w*\s*кров\w*\b",

                    @"\bветеринарн\w*\b",
                    @"\bоздоровит\w*\s*центр\w*\b|\bлаборатор\w*[-\s]*диагнос\w*\b|\bклиник\w*\b",

                    @"\bветеринарн\w*\b",
                    @"\bмолоч\w*[-\s]*раздаточ\w*\b",

                    @"",
                    @"\bцентр\w*\s*красот\w*\s*и\s*здоров\w*\b",

                    @"\bветеринарн\w*\b",
                    @"\bдиагностич\w*\s*центр\w*\b",

                    @"",
                    @"\bхимбаклаборатор\w*\b|\bцмсч\b|\bмедсанчаст\w*\b",

                    @"",
                    @"\bлечеб\w*[-\s]*диагностич\w*\b|\bнародн\w*\s*целител\w*\b",

                    @"",
                    @"\bкдц\b|\bфельдшер\w*[-\s]*акушер\w*\b|\bлечебн\w*\b.*\bучрежден\w*\b",

                    @"",
                    @"\bлечебн\w*\s*цел\w*\b|\bнарколог\w*\s*(центр\w*|помощ\w*)\b",

                    @"",
                    @"\bрепродук\w*\s*и\s*планирован\w*\s*сем\w*\b",

                    @"",
                    @"\bвакцинац\w*\s*корп\w*\b" }
               ));

            nodes.Add(new Hospital("3.4.2.0", "3.4.2", "1005", "100",

               "Размещение объектов капитального строительства, предназначенных для " +
               "оказания гражданам медицинской помощи в стационарах " +
               "(больницы, родильные дома, научно-медицинские учреждения и " +
               "прочие объекты, обеспечивающие оказание услуги по лечению в " +
               "стационаре); размещение станций скорой помощи ",

               "Стационарное медицинское обслуживание",
               "",
               "",
               new string[] {
                    @"\bветеринарн\w*\b",
                    @"\bбольниц\w*\b|\bгоспит\w*\b|\bрод[\w\.\s]*\b\s*дом\w*\b|" +
                    @"\bперинат\w*\b\s*\b\w*\b|\bскор\w*\s*(и\s*неотлож\w*\s*)?помощ\w*\b|\bхоспис\w*\b|" +
                    @"\bпсихоневролог\w*\b\s*интерн\w*\b|" +
                    @"\bнауч\w*\b[\s\-]*медицин\w*\b",

                    @"\bветеринарн\w*\b|профилакт\w*\b|оздоровит\w*\b",
                    @"\bбольни[чц]\w*\b|\bлечебн\w*\b|\bлечебн\w*-\w*\b",

                    @"\bветеринарн\w*\b",
                    @"\bгинекологич\w*\s*корпус\w*\b",

                    @"\bветеринарн\w*\b|профилакт\w*\b|оздоровит\w*\b",
                    @"\bлечебн\w*\b.*\bучрежден\w*\b|\bбольнич\w*\s*комплекс\w*\b",

                    @"\bветеринарн\w*\b|профилакт\w*\b|оздоровит\w*\b",
                    @"\bкдц\b|\bфельдшер\w*[-\s]*акушер\w*\b|\bлечебн\w*\b.*\bучрежден\w*\b",

                    @"\bветеринарн\w*\b|\bпрофилакт\w*\b|\bоздоровит\w*\b",
                    @"\bлечебн\w*\s*цел\w*\b|\bнарколог\w*\s*(центр\w*|помощ\w*)\b",

                    @"",
                    @"\bрепродук\w*\s*и\s*планирован\w*\s*сем\w*\b",

                    @"",
                    @"\bонкологич\w*\s*центр\w*\b|\bлечебн\w*\s*корп\w*\b",

                    @"",
                    @"\bклиник\w*[-\s]*хирург\w*\b|\bроддом\w*\b|\bмедвытрезвит\w*\b",

                    @"",
                    @"\bлаборатор\w*[-\s]*клинич\w*\b|\bздан\w*\s*клиник\w*\b",

                    @"",
                    @"\bпаталог\w*[-\s]анатом\w*\b|\bскор\w*\b.*\bнеотлож\w*\s*помощ\w*\b" }
               ));

            nodes.Add(new Education("", "3.5", "", "",

               "Размещение объектов капитального строительства, предназначенных " +
               "для воспитания, образования и просвещения (детские ясли, детские сады, " +
               "школы, лицеи, гимназии, профессиональные технические училища, колледжи, " +
               "художественные, музыкальные школы и училища, образовательные кружки, " +
               "общества знаний, институты, университеты, организации по переподготовке " +
               "и повышению квалификации специалистов и иные организации, " +
               "осуществляющие деятельность по воспитанию, образованию и просвещению). " +
               "Содержание данного вида разрешенного использования включает в себя " +
               "содержание видов разрешенного использования с кодами 3.5.1 - 3.5.2 ",

               "Образование и просвещение",
               "",
               "",
               new string[] {
                    @"",
                    @"\bобразоват\w*\s*деятельност\w*|(\bобразоват\w*|\bучебн\w*)\s*" +
                    @"(цел\w*|здан\w*|центр\w*|комплек\w*|площадк\w*)",

                    @"",
                    @"\bобразова\w*\s*учрежден\w*\b|\bучрежден\w*\s*образова\w*\b",

                    @"",
                    @"\b(объ?ект\w*|центр\w*|цел\w*)\s*образов\w*\b" }
               ));

            nodes.Add(new SchoolEducation("3.5.1.0", "3.5.1", "2003", "100",

               "Размещение объектов капитального строительства, предназначенных для " +
               "просвещения, дошкольного, начального и среднего общего образования " +
               "(детские ясли, детские сады, школы, лицеи, гимназии, художественные, " +
               "музыкальные школы, образовательные кружки и иные организации, " +
               "осуществляющие деятельность по воспитанию, образованию и просвещению) ",

               "Дошкольное, начальное и среднее общее образование",
               "",
               "",
               new string[] {
                    @"православн",
                    @"[-\s]*лице[^н]\w*\b",

                    @"\bказарм\w*\b",
                    @"\bвнешколь\w*\b|\bспорт\w*\s*школ\w*\b",

                    @"\bказарм\w*\b",
                    @"\bкорр?екц\w*\s*школ\w*\b|\bкорр?екц\w*\s*дом\w*\b|\bкорр?екц\w*\b",

                    @"организации\sпитания|\bвоскресн\w*\s*школ\w*\b|\bспорт\w*\b|" +
                    @"здания\sмосковского\sтеатра|территории\sшколы\s№\s1741",
                    @"\bшкол\w*\b",

                    @"\bпрогулочн\w*\b|\bказарм\w*\b",
                    @"\bвоспита\w*\b|\bгимназ\w*\b|\bдошкол\w*\b|\bобщеобраз\w*\b|\bясл\w*\b|" +
                    @"\bдет[.]?[\w]{0,6}[-\s]*сад\w*\b|\bдет[.]?\w*[-\s]*учрежден\w*\b",

                    @"\bказарм\w*\b",
                    @"\bначальн\w*\s*образован\w*\b|\bсредн\w*\s*образован\w*\b",

                    @"\bказарм\w*\b",
                    @"\bхудож\w*\s*школ\w*\b|\bхудож\w*\s*круж\w*\b|\bмузыкал\w*\s*школ\w*\b|" +
                    @"\bмузыкал\w*\s*круж\w*\b|\bдополнит\w*\s*образован\w*\b",

                    @"\bказарм\w*\b",
                    @"\bцентр\sразвит\w*\b|\bцентр\w*\sдетс\w*\b|\bцдт\b",

                    @"\bказарм\w*\b",
                    @"\bсредн\w*\s*обще\w*\s*образован\w*\b|\bувк\b",

                    @"\bказарм\w*\b",
                    @"\bинтернат\w*\s*жил\w*\b",

                    @"",
                    @"\bдетск\w*\s*комбинат\w*\b",

                    @"",
                    @"\bучрежд\w*\s*народ\w*\s*образован\w*\b|\bувк\b",

                    @"",
                    @"\bобразоват\w*\s*процес\w*\s*дет\w*\s*и\s*юнош\w*\b",

                    @"",
                    @"\bпрогимназ\w*\b|\bдетс\w*[-\s]*юнош\w*\b",

                    @"",
                    @"\bдетс\w*\s*балет\w*\s*студ\w*\b",

                    @"",
                    @"\b(дом\w*|центр\w*|двор\w*)\s*дет\w*\b.*\bтворч\w*|\bцдт\b",

                    @"",
                    @"\bцентр\w*\s*творч\w*\s*дет\w*\b",

                    @"",
                    @"\bдюсш\b",

                    @"проф|комбинат|\bказарм\w*\b|\bпрогулочн\w*\b",
                    @"\bучебн\w*[\s\-]*\bвоспитат\w*\b" }
               ));

            nodes.Add(new ProfessionalEducation("3.5.2.0", "3.5.2", "1002", "100",

               "Размещение объектов капитального строительства, предназначенных для " +
               "профессионального образования и просвещения " +
               "(профессиональные технические училища, колледжи, художественные, " +
               "музыкальные училища, общества знаний, институты, университеты, " +
               "организации по переподготовке и повышению квалификации специалистов " +
               "и иные организации, осуществляющие деятельность по образованию и просвещению) ",

               "Среднее и высшее профессиональное образование",
               "",
               "",
               new string[] {
                    @"\bказарм\w*\b",
                    @"\bсредне\w*[\s\-]*профессионально\w*\s*\bобразова\w*\b|" +
                    @"\bвысше\w*[\s\-]*профессионально\w*\s*\bобразова\w*\b|" +
                    @"\bвысше\w*[\s\-]*\bобразова\w*\b",

                    @"\bвоен\w*\b|\bказарм\w*\b|\bнауч\w*[-\s]*исслед\w*\b",
                    @"\bвуз\w{0,5}\b|\b(пед)?колледж\w*\b|\bпту\w*\b|\bтехникум\w*\b|" +
                    @"\bуниверситет\w*\b|\bучилищ[еа]?\w*\b|\bфакультет\w*\b|\bинститут\w*\b",

                    @"",
                    @"\bучеб\w*[-\s]*лаборатор\w*\b|\bметодич\w*\s*центр\w*\b",

                    @"\bсанитарн\w*\s*\bзон\w*\b|\bвоен\w{1,6}\b|\bказарм\w*\b",
                    @"\bучебн\w{1,5}\b\s*\bкорпус\w{0,3}\b|" +
                    @"\bучебн\w*\b[-\s]*\bлабортор\w*\b(\s*корпус\w*\b)?|" +
                    @"\bучебн\w{1,5}\b\s*\bзаведен\w*\b",

                    @"\bнаук\w*\b|\bнародного\w*\b\s*\bхозяйства\w*\b|\bказарм\w*\b",
                    @"\bакадем[ийя]{0,2}\b|\bвысш\w{0,3}\b\s*\bшкол\w{0,3}\b" +
                    @"\bповышен\w*\b\s*\bквалификац\w*\b|\bсредне\w*\b\s*" +
                    @"\bспециальн\w*\b|\bавтошкол\w*\b",

                    @"\bказарм\w*\b",
                    @"\bучебн\w*\s*комбин\w*\b",

                    @"\bказарм\w*\b",
                    @"\bповыш\w*\s*квалиф\w*\b|\bакадем\w*\s*народ\w*\s*хоз\w*\b",

                    @"\bказарм\w*\b",
                    @"\bсред\w*\s*профес\w*\b|\bобразован\w*\s*для\s*взросл\w*\b",

                    @"\bказарм\w*\b",
                    @"\bанатомич\w*\s*корпус\w*\b|\bбиологич\w*\s*станц\w*\b",

                    @"",
                    @"\bучеб\w*[-\s]*(производ\w*\s*)?комбинат\w*\b",

                    @"",
                    @"\bучебн\w*\s*процес\w*\b",

                    @"",
                    @"\bучеб\w*[-\s]*трениров\w*\s*центр\w*\b",

                    @"",
                    @"\bпедагогич\w*\b|\bучебн\w*\s*пункт\w*\b",

                    @"",
                    @"\bцентр\w*\s*подготов\w*\s*специалист\w*\b|\bпросветитель\w*\s*центр\w*\b",

                    @"",
                    @"\bмгу\b.*\bломонос\w*\b",

                    @"",
                    @"\bстуд\w*\s*город\w*\b",

                    @"",
                    @"\bучебн\w*[-\s]*производ\w*\s*баз\w*\b",

                    @"",
                    @"\bплощад\w*\b.*\b(обучен\w*|занят\w*)\s*вожден\w*\s*автомоб\w*\b",

                    @"",
                    @"\bплощад\w*\b.*\bэкзамен\w*\b.*\bвожден\w*\s*автомоб\w*\b",

                    @"",
                    @"\bплощад\w*\b.*\bучебн\w*\s*езд\w*\b",

                    @"",
                    @"\bплощадк\w*\b.*\bобуч\w*\s*(авто)?вожден\w*\b", }
               ));


            nodes.Add(new Community("3.6.1", "3.6", "1003", "100",

               "Размещение объектов капитального строительства, предназначенных для " +
               "размещения в них музеев, выставочных залов, художественных галерей, " +
               "домов культуры, библиотек, кинотеатров и кинозалов, театров, филармоний, " +
               "планетариев, творческих центров и иных культурно-досуговых учреждений ",

               "Культурное развитие",
               "",
               "",
               new string[] {
                    @"\bказарм\w*\b",
                    @"\bобъект\w*\s*культур\w*\b",

                    @"\bказарм\w*\b",
                    @"\bобелиск\w*\b|\bдом\w*\s*творч\w*\b",

                    @"\bказарм\w*\b",
                    @"\bпоклон\w*\s*гор\w*\b|\bотдыха,\sразвлеч\w*\b",

                    @"\bказарм\w*\b",
                    @"\bкультур\w*[-\s]*развлекат\w*\b|\bконференц-зал\b",

                    @"",
                    @"\bкультур\w*[-\s]*торгов\w*\b",

                    @"\bказарм\w*\b",
                    @"\b(худож\w*|скульпт\w*|творческ\w*)\s*мастерск\w*\b|\bобществ\w*[-\s]*культур\w*\b",

                    @"\bказарм\w*\b",
                    @"\bмастерск\w*\s*худож\w*\b",

                    @"\bказарм\w*\b",
                    @"\bдвор\w*\s*(культ\w*|творч\w*)\b|\b(кино)?театр\w*\b",

                    @"\bказарм\w*\b",
                    @"\bкультур\w*\s*отдых\w*\b|\bорганиз\w*\s*просвещ\w*\b",

                    @"\bказарм\w*\b",
                    @"\bвыставоч\w*\s*компл\w*\b|\bофисно-выствоч\w*\b",

                    @"\bказарм\w*\b",
                    @"\bкультур\w*\s*центр\w*\b|\bкультур\w*[-\s]*просветит\w*\b",

                    @"\bнекапитальн\w*\b|\bказарм\w*\b",
                    @"\bбиблиот\w*\b|\bдосуг\w*\b|\bзрелищ\w*\b|" +
                    @"\bконцерт\w*\b|\bмузе\w*\b|\bмультимед\w*\b|" +
                    @"\bпланетар\w*\b|\bрепетиц\w*\b|\bтеатр\w*\b",

                    @"\bмагазин\w*\b|\bпродаж\w*\b|\bмебел\w*\b|\bбеляево\w*\b|" +
                    @"\bритуал\w*\b|\bдемонстрац\w*\b|\bдемонстрац\w*\b",
                    @"\bвыставочн\w*\s*(зал\w*|павльон\w*)\b|\bвыставка\b",

                    @"\bискусствен\w*\b|\b[до]{0,2}?школ\w*\b|\(5\s\.1\)|" +
                    @"\(9\.3\)|\(3\.[45]\)|\(7\.1\)|\(8\.3\)",
                    @"\bискусств\w*\b",

                    @"\bкинолог\w*\b\s|дома\sветеранов",
                    @"\bкино\w*\b\s",

                    @"\b(до)?школ\w*\b|\bколледж\w*\b|\bучилищ\w*\b|" +
                    @"\bучебно\w*\b|\bмагазин\w*\b|\bпроизводст\w*\b",
                    @"\bмузыка\w*\b",

                    @"\bмастерск\w*\b|\bоформление\w*\b|\bобразовани\w*\b|" +
                    @"\bшкол\w*\b|\bзавод\w*\b|\bпроизводст\w*\b",
                    @"\bхудож\w*\b",

                    @"\bказарм\w*\b",
                    @"\bгалере\w*\b|\bдом\w*\b\s*\bкультур\w*\b|\bфилармон\w*\b|" +
                    @"\bтворч\w*\b\s*\bцентр\w*\b",

                    @"\bказарм\w*\b",
                    @"\bкультурно\w*[-\s]*просветит\w*\b",

                    @"",
                    @"\bхудожеств\w*\s*галере\w*\b|\bвыставочн\w*\s*павильон\w*\b",

                    @"",
                    @"\bдизайнерск\w*\s*центр\w*\b",

                    @"",
                    @"\b(дом\w*|центр\w*|двор\w*)\s*дет\w*\b.*\bтворч\w*|\bцдт\b",

                    @"",
                    @"\bцентр\w*\s*творч\w*\s*дет\w*\b",

                    @"",
                    @"\bцентр\w*\s*ремес\w*\b",

                    @"",
                    @"\bдом\w*\s*науки\b|\bучрежден\w*\s*культур\w*\b",

                    @"",
                    @"\bэкспозиц\w*\s*выставоч\w*\b|\bмеждународ\w*\s*выстав\w*\b",

                    @"",
                    @"\bдемострац\w*\s*павиль\w*\b" }
               ));

            nodes.Add(new Community("3.6.2", "3.6", "1003", "100",

               "Устройство площадок для празднеств и гуляний ",

               "Культурное развитиее",
               "",
               "",
               new string[] {
                    @"",
                    @"\bпразднен\w*\b|\bгулян\w*\b"
                }
               ));

            nodes.Add(new Community("3.6.3", "3.6", "1003", "100",

               "Размещение зданий и сооружений для размещения цирков, зверинцев, " +
               "зоопарков, океанариумов; размещение помещений и технических " +
               "устройств парков культуры и отдыха, оранжерей, ботанических садов ",

               "Культурное развитиее",
               "",
               "",
               new string[] {
                    @"\bторгов\w*\b|\bпредприят\w*\b|\bпринадлежностей\w*\b",
                    @"\bцирк\w*\b|\bоранжере\w*\b",

                    @"",
                    @"\bзверин\w*\b|\bзоопарк\w*\b|\bокеанариум\w*\b",

                    @"",
                    @"\bвольер\w*\b.*\bвыгул\w*\s*животн\w*\b"
                 }
               ));

            nodes.Add(new Community("3.7.1", "3.7", "1003", "100",

               "Размещение объектов капитального строительства, предназначенных " +
               "для отправления религиозных обрядов (церкви, соборы, храмы, " +
               "часовни, мечети, молельные дома, синагоги и иные культовые объекты) ",

               "Религиозное использование",
               "",
               "",
               new string[] {
                    @"\bритуал\w*\b|\bна\s+период\w*\b|\bгараж\w*\b",
                    @"\bкульто\w*\b|\bмечет\w*\b|\bмолеб\w*\b|\bсинагог\w*\b|\bхрам\w*\b|\bцерк\w*\b",

                    @"\bфиз\w*\b|\bспорт\w*\b|\bплодов\w*\b|\bягодн\w*\b",
                    @"\bдуховно-культур\w*\b",

                    @"\bздравноохран\w*\b|\bспорт\b|\bпросвящение\b|\bобороны\s*и\s*безопасности|" +
                    @"эксплуатация\sздания\sшколы|правоподрядка|природно-рекреационного|\bритуал\w*\b|" +
                    @"религиозной\sлитературы",
                    @"\bрелиги\w*\b|\bприхода\b",

                    @"",
                    @"\bсобор\w*\b|\bчасовн\w*\b|\bмолель\w*\b|\bхрам\w*\s*комплек\w*\b|\bхрам\w*\b",

                    @"",
                    @"\bмолитвен\w*\s*дом\w*\b|\bмусульман\w*\s*центр\w*\b|\bризниц\w*\b"
                }
               ));

            nodes.Add(new Community("3.7.2", "3.7", "1003", "100",

               "Размещение объектов капитального строительства, предназначенных " +
               "для постоянного местонахождения духовных лиц, паломников и " +
               "послушников в связи с осуществлением ими религиозной службы, " +
               "а также для осуществления благотворительной и религиозной " +
               "образовательной деятельности (монастыри, скиты, воскресные и " +
               "религиозные школы, семинарии, духовные училища, дома притча и т.п.) ",

               "Религиозное использование",
               "",
               "",
               new string[] {
                    @"",
                    @"\bвоскр\w*\s*\bшкол\w*\b|\bдухов\w*[\-]*\w*\b|\bмонастыр\w*\b",

                    @"",
                    @"\bскит\w{0,3}\b|\bсеминари\w*\b|" +
                    @"\bпричт\w{0,3}\b|\bрелигио\w*\b\s+школ\w*\b|\bдуховн\w*\b\s+\bучилищ\w*\b",

                    @"\bмногофункцион\w*\b|\bцентр\w*\s*ремес\w*\b",
                    @"\bлавр[аы]\b|\bподворь\w*\b"
                }
               ));

            nodes.Add(new Community("3.8.1", "3.8", "1001", "100",

               "Размещение объектов капитального строительства, предназначенных " +
               "для размещения органов государственной власти, государственного " +
               "пенсионного фонда, органов местного самоуправления, судов, а " +
               "также организаций, непосредственно обеспечивающих их деятельность; " +
               "размещение иных объектов государственных органов ",

               "Общественное управление",
               "",
               "",
               new string[] {
                   @"мед|судов|двух\sтренировочных\sполей|экспертиз",
                   @"\bсуд\w{0,3}|\b(гос)?арбитраж\w*\b",

                   @"",
                   @"\bсан\w{0,10}\s+надзор|\bэнерго\w{0,10}\s+надзор",

                   @"\sполиклиник\w{0,2}|\bминистерс\w{0,5}\s+оборон|\bбанк\w{0,9}\b|" +
                   @"\bминистерс\w{0,5}\s+внутренн",
                   @"архив|загс|министер|налог|МИД|\bинспекц\w*\b",

                   @"",
                   @"орган\w*\s+государств|государств\w*\s+орган",

                   @"",
                   @"пенсион.+фонд|\bуправы\w*\b",

                   @"",
                   @"местн.+самоупр|\bпрокурат\w*\b",

                   @"",
                   @"\bдвор\w*\s*бракосочетан\w*\b",

                   @"",
                   @"\bадминистра\w*[-\s]*управленч\w*\s*учрежден\w*\b",

                   @"",
                   @"\bпавиль\w*\b.*\bпереоформл\w*\s*автомоб\w*\b",

                   @"",
                   @"\bинспекц\w*\s*рыбохран\w*\b",

                   @"",
                   @"\b(гос)?тех\w*\s*оcмотр\w*\s*(авто)?транспорт\w*\b",

                   @"",
                   @"\bосмотр\w*\s*и\s*регистр\w*\s*(авто)?транспорт\w*\b",

                   @"",
                   @"\bпрефектур\w*\b|\bпаспорт\w*\s*стол\w*\b|\bгоссанэпиднадз\w*\b",

                   @"",
                   @"\bкомитет\w*\b.*\bгос\w*\s*регистрац\w*\b",

                   @"",
                   @"\bнотариал\w*\s*контор\w*\b",

                   @"",
                   @"\bэнергонадзор\w*\b" }
               ));

            nodes.Add(new Community("3.8.2", "3.8", "1001", "100",

               "Размещение объектов капитального строительства, предназначенных " +
               "для размещения органов управления политических партий, " +
               "профессиональных и отраслевых союзов, творческих союзов и " +
               "иных общественных объединений граждан по отраслевому " +
               "или политическому признаку ",

               "Общественное управление",
               "",
               "",
               new string[] {
                   @"обществ.{1,5}\sпитан.+|обеспечение\sобороны\sи\sбезопасности",
                   @"обществ\w{1,5}\b\sорган\w*\b",

                   @"",
                   @"политич.{1,5}\sпартий\w*\b|политич.{1,5}\sсоюз\w*\b|обществ.{1,5}\sобъединен\w*\b",

                   @"",
                   @"\bобществ\w*\s*пункт\w*\s*охран\w*\s*поряд\w*\s" }
               ));

            nodes.Add(new Community("3.8.3", "3.8", "1001", "100",

               "Размещение объектов капитального строительства для дипломатических " +
               "представительств иностранных государств и субъектов РФ, " +
               "консульских учреждений в Российской Федерации ",

               "Общественное управление",
               "",
               "",
               new string[] {
                   @"постановл\w{0,5}\s*правитель|распоряж\w{0,5}\s*правитель|киоск|жилого\sдома|автопарк|" +
                   @"народного\sхозяйства",
                   @"консульств|посольств|правительств",

                   @"",
                   @"\bино\w*\s*представительст\w*\b|\bдом\w*\s*прием\w*\s*официал\w*\s*делегац\w*\b" }
               ));

            nodes.Add(new Sсience("3.9.2", "3.9", "1001", "100",

               "Размещение объектов капитального строительства для " +
               "проведения научных исследований (научно-исследовательские и " +
               "проектные институты, научные центры, инновационные центры, " +
               "государственные академии наук, в том числе отраслевые) ",

               "Обеспечение научной деятельности",
               "",
               "",
               new string[]  {
                    @"пилюгин|научно-богословского|колопроктологии|картофел\w|" +
                    @"древесин\w|\bнауч\w*[-\s]*(производ\w*|лаборат\w*|технич\w*)\b" +
                    @"\bнауч\w*[-\s]исследоват\w*\b.*\bпроизвод\w*\s*баз\w*\b|\bпроизводств\w*\b",
                    @"\bакадем\w*\b\s*?\bнау\w*\b|\bнауч\w*\b[\s\-]*\b\w*\b|\bнауч\w*\b",

                    @"изыскан|испытан|\bмагазин\w*.*\bвычислит\w*\b.*\bтехник\w*\b",
                    @"\bвычислит\w*\b|г?вц",

                    @"документац|исзыскат|-изыскат",
                    @"\bпроектн\w*\b\s*\bинстит\w*\b|\bпроектн\w*[-\s]*изыскат\w*\b",

                    @"",
                    @"исследователь.{1,5}\sинститут.{0, 2}\s",

                    @"",
                    @"исследователь.{1,7}\sцентр.{0, 7}\s",

                    @"",
                    @"инновацион.{1,5}\sинститут.{0, 7}\s",

                    @"",
                    @"инновацион.{1,7}\sцентр.{0, 7}\s",

                    @"",
                    @"\b(объект\w*|учрежден\w*)\s*наук\w*\b|\bнии\b",

                    @"",
                    @"\bконструктор\w*\s*(бюро)\b|\bбиологич\w*\s*станц\w*\b",

                    @"",
                    @"\bниитавтопром\b" }
               ));

            nodes.Add(new Sсience("3.9.3", "3.9", "1300", "300",

               "Размещение объектов капитального строительства для " +
               "проведения изысканий, испытаний опытных промышленных образцов, " +
               "для размещения организаций, осуществляющих научные изыскания, " +
               "исследования и разработки ",

               "Обеспечение научной деятельности",
               "",
               "",
               new string[] {
                    @"",
                    @"\bнауч\w*[-\s]*(производ\w*|лаборат\w*|технич\w*)\b",

                    @"\bветеринарн\w*\b",
                    @"\bэксперимент\w*\s*баз\w*\b|\bлаборатор\w*[-\s]*производ\w*\b" +
                    @"\bлаборатор\w*[-\s]*(конструктор\w*|эксперимент\w*|сбороч\w*)\b|\bинженер\w*[-\s]*лаборатор\w*\b|" +
                    @"\bздан\w*\b.*\bлаборатор\w*(\s*устнов\w*)?\b",

                    @"\bветеринарн\w*\b|\bучеб\w*[-\s]*лаборатор\w*\b",
                    @"\bлаборатор\w*\b|\bинженер\w*[-\s]*технич\w*\s*корпус\w*\b",

                    @"",
                    @"\bздан\w*\b.*\bизготовл\w*\s*опыт\w*\s*образц\w*\b",

                    @"",
                    @"\bконструктор\w*[-\s]*(технологич\w*\s*)?бюро\b",

                    @"",
                    @"\bвакцинац\w*\s*корп\w*\b",

                    @"",
                    @"\bучеб\w*[-\s]*лаборатор\w*\b|\bметодич\w*\s*центр\w*\b",

                    @"",
                    @"\bнауч\w*[-\s]исследоват\w*\b.*\bпроизвод\w*\s*баз\w*\b",

                    @"",
                    @"\bособо\w*\s*экономич\w*\s*зон\w*\b.*\bтехник\w*[-\s]*внедренч\w*\s*тип\w*\b",

                    @"",
                    @"\bнауч\w*[-\s]*\bисследоват\w*\b.*\bпроизвод\w*\b|\bпроизвод\w*\b.*\bнауч\w*[-\s]*\bисследоват\w*\b",

                    @"",
                    @"\bиспытан\w*\s*опытн\w*\s*промышлен\w*\s*образц\w*\b",

                    @"",
                    @"\bунпб\b" }
               ));

            nodes.Add(new Sсience("3.9.4", "3.9", "1001", "300",

               "Размещение технологических парков, технополисов,  бизнес-инкубаторов ",

               "Обеспечение научной деятельносит",
               "",
               "",
               new string[] {
                    @"",
                    @"\bбизнес\w*\b[\s\-]*парк\w*\b|\bтехно\w*[-\s]*парк\w*\b|\bтехнополис\w*\b",

                    @"",
                    @"\bтехнопол\w*\b|\bбизне\w*\s*инкубат\w*\b",

                    @"",
                    @"\bособо\w*\s*экономич\w*\s*зон\w*\b.*\bтехник\w*[-\s]*внедренч\w*\s*тип\w*\b"}
               ));

            nodes.Add(new Sсience("3.9.5", "3.9", "3002", "300",

               "Размещение индустриальных (промышленных) парков ",

               "Обеспечение научной деятельности",
               "",
               "",
               new string[] {
                    @"",
                    @"\bиндустр\w*\s*парк\w*\b|\bпромышл\w*\s*парк\w*\b" }
               ));

            nodes.Add(new WeatherStation("3.9.1.0", "3.9.1", "3003", "300",

               "Размещение объектов капитального строительства, " +
               "предназначенных для наблюдений за физическими и химическими " +
               "процессами, происходящими в окружающей среде, определения " +
               "ее гидрометеорологических, агрометеорологических и " +
               "гелиогеофизических характеристик, уровня загрязнения " +
               "атмосферного воздуха, почв, водных объектов, в том числе " +
               "по гидробиологическим показателям, и околоземного - " +
               "космического пространства, зданий и сооружений, " +
               "используемых в области гидрометеорологии и смежных с " +
               "ней областях (доплеровские метеорологические радиолокаторы, " +
               "гидрологические посты и другие) ",

               "Обеспечение деятельности в области гидрометеорологии и " +
               "смежных с ней областях",
               "",
               "",
               new string[] {
                    @"",
                    @"\bметеороло\w*\b|\bметеостанц\w*\b",

                    @"",
                    @"\bэкологич\w*\s*пост\w*\b",

                    @"",
                    @"\bконтрольно-измерит\w*\s*пункт\w*\b|\bконтрольн\w*\s*пункт\w*\s*№\d*",

                    @"",
                    @"\b(\bмонитор\w*\s*качест\w*|контро\w*\s*загрязне\w*|анализ\w*\s*проб\w*)\s*" +
                    @"(атмосферн\w*)?воздух\w*\b"}
               ));

            nodes.Add(new Veterenary("", "3.10", "", "",

               "Размещение объектов капитального строительства, " +
               "предназначенных для оказания ветеринарных услуг, " +
               "содержания или разведения животных, не являющихся " +
               "сельскохозяйственными, под надзором человека. " +
               "Содержание данного вида разрешенного использования " +
               "включает в себя содержание видов разрешенного " +
               "использования с кодами 3.10.1 - 3.10.2 ",

               "Ветеринарное обслуживание",
               "",
               "",
               new string[] { }));

            nodes.Add(new VetClinic("3.10.1.0", "3.10.1", "1005", "100",

               "Размещение объектов капитального строительства, " +
               "предназначенных для оказания ветеринарных " +
               "услуг без содержания животных ",

               "Амбулаторное ветеринарное обслуживание",
               "",
               "",
               new string[] {
                    @"клиник\sс\sсодержанием",
                    @"\bветеринар\w*\s*клини\w*\b|\bветклин\w*\b|" +
                    @"\bветеринар\w*\s*лаборатор\w*\b|\bветеринар\w*\s*станц\w*\b|" +
                    @"\bветеринар\w*\s*участок\w*\b",

                    @"",
                    @"\bветлечебн\w*\b|\bветеринар\w*\s*(обслуживан\w*|служб\w*|услуг\w*)\b",

                    @"",
                    @"\bбор\w*\s*с\s*болезн\w*\s*животн\w*\b|\bветеринарии\b",

                    @"",
                    @"\bцентр\w*\b.*\bдля\s*домашн\w*\s*животн\w*\b",

                    @"",
                    @"\b(зоо)?ветеринар\w*\s*комплекс\w*\b" }
               ));

            nodes.Add(new AnimalShelter("3.10.2.0", "3.10.2", "1005", "100",

               "Размещение объектов капитального строительства, предназначенных " +
               "для оказания ветеринарных услуг в стационаре; размещение объектов " +
               "капитального строительства, предназначенных для содержания, " +
               "разведения животных, не являющихся сельскохозяйственными, " +
               "под надзором человека, оказания услуг по содержанию и " +
               "лечению бездомных животных; размещение объектов капитального " +
               "строительства, предназначенных для организации гостиниц для животных ",

               "Приюты для животных",
               "",
               "",
               new string[] {
                    @"\bголуб\w*\b|\bрастен\w*\b|декоратив\w*\b",
                    @"\bпитомни\w*\b|\bприют\w*\s*животн\b|\bбездом\w*\s*животн\b|\bприют\w*\b.*животн\w*\b",

                    @"автостоянк",
                    @"\bветеринар\w*\s*лечеб\w*\b|\bцентр\w*\s*кинологич\w*\s*\b\w*\b",

                    @"без\sсодержания",
                    @"\bзоогостиниц\w*\b|\bсодерж\w*\s*\bживотн\w*\b|\bсодерж\w*\s*\bбезнадзорн\w*\b",

                    @"",
                    @"\bветлечебн\w*\b|\b(зоо)?ветеринар\w*\s*комплекс\w*\b",

                    @"",
                    @"\bветеринарн\w*\s*служб\w*\b",

                    @"",
                    @"\bпередерж\w*\s*(безнадзор\w*\s*)?животн\w*\b"}
                ));

            nodes.Add(new BaseBuisness("4.0.0", "4.0", "1000", "100",

               "Размещение объектов капитального строительства в целях извлечения " +
               "прибыли на основании торговой, банковской и иной " +
               "предпринимательской деятельности. Содержание данного вида " +
               "разрешенного использования включает в себя содержание видов " +
               "разрешенного использования с кодами 4.1.0, 4.2.0, 4.3.0, 4.4.0, " +
               "4.5.0, 4.6.0, 4.8.0, 4.9.0, 4.10.0 ",

               "Предпринимательство",
               "",
               "",
               new string[] {
                    @"",
                    @"\bпредприниматель\w{1,6}\s{1,5}\bдеятельност\w{1,5}\b",

                    @"",
                    @"\bкомм?ерч\w*\s*\b(организац\w*|деятель\w*|приедприят\w*)\b",

                    @"",
                    @"\bмногофункц\w*\s*обществ\w*\s*центр\w*\b",

                    @"",
                    @"\bмногофункц\w*\s*(комплекс\w*|здан\w*|\bназнач\w*)\b",

                    @"",
                    @"\bобъект\w*\s*обществен\w*\s*назначен\w*\b",

                    @"",
                    @"\bобъект\w*\s*предпринимат\w*\b",

                    @"",
                    @"\bзем\w*\s*участ\w*\s*смешан\w*\s*размещен\w*\b",

                    @"",
                    @"\bжил\w*[-\s]*комм?ерч\w*\s*комплекс\w*\b",

                    @"",
                    @"\bздан\w*\s*комм?ерч\w*\s*назнач\w*\b",

                    @"",
                    @"\bмногофункц\w*\s*комм?ерч\w*\s*центр\w*\b",

                    @"",
                    @"\bсоцкультбыт\w*\s*\b" }
               ));

            nodes.Add(new Buisness("4.1.0", "4.1", "1001", "100",

               "Размещение объектов капитального строительства с целью: " +
               "размещения объектов управленческой деятельности, не связанной " +
               "с государственным или муниципальным управлением и оказанием услуг, " +
               "а также с целью обеспечения совершения сделок, не требующих " +
               "передачи товара в момент их совершения между организациями, в " +
               "том числе биржевая деятельность (за исключением банковской и " +
               "страховой деятельности) ",

               "Деловое управление",
               "",
               "",
               new string[] {
                    @"\bбанк\w*\b|\bстрахов\w*\b|\bадминистра\w*[-\s]*управленч\w*\s*учрежден\w*\b",
                    @"\bадминистративн\w*\b-*\bделов\w*\b|" +
                    @"\bадминистративн\w*\b\s*\bучрежден\w*\b",

                    @"\bбанк\w*\b|\bстрахов\w*\b|\bадминистра\w*[-\s]*управленч\w*\s*учрежден\w*\b|" +
                    @"\bокруг\w*\b",
                    @"\bофис\w*\b|\bбирж\w*\b|\bадминистратив\w*\b|" +
                    @"\bделов\w*\s*центр\w*\b|\bуправлен\w*\b",

                    @"",
                    @"\bкоммерч\w*\s*фирм\w*\b|\bзаводоуправлен\w*\b",

                    @"",
                    @"\bделов\w*\b|\bадминистратив\w*\s*здан\w*\b",

                    @"",
                    @"\bконтор\w*\s*лесничеств\w*\b",

                    @"",
                    @"\bпавиль\w*\b.*\bпереоформл\w*\s*автомоб\w*\b",

                    @"",
                    @"\b(гос)?тех\w*\s*оcмотр\w*\s*(авто)?транспорт\w*\b",

                    @"",
                    @"\bосмотр\w*\s*и\s*регистр\w*\s*(авто)?транспорт\w*\b",

                    @"",
                    @"\bнотариал\w*\s*контор\w*\b",

                    @"",
                    @"\bэнергонадзор\w*\b|\bдом\w*\s*предпринимат\w*\b",

                    @"",
                    @"\b(здан\w*|помещен\w*)\s*контор\w*\b|\bпод\s*контор\w*\b",

                    @"",
                    @"\bэксплуатац\w*\s*контор\w*|\bконтор\w*\s*помещен\w*\b",

                    @"",
                    @"\bарендн\w*\s*предприят\w*\b|\bздан\w*\s*управлен\w*\b",

                    @"",
                    @"\bконторс\w*\s*цел\w*\b|\bтранспортн\w*\s*агентст\w*\b",

                    @"",
                    @"\bразмещен\w*\b.*(служб\w*|подразделен\w*)\s*управлен\w*\b",

                    @"",
                    @"\bразмещен\w*\b.*\bбух?галтер\w*\b",

                    @"",
                    @"\bпункт\w*\s*(прием\w*\s*и\s*)?оформлен\w*\s*заказ\w*\b",

                    @"",
                    @"\bбизнес\w*[-\s]*центр\w*\b|\bстол\w*\s*заказ\w*\b" }
               ));

            nodes.Add(new Buisness("4.2.0", "4.2", "1004", "100",

               "Размещение объектов капитального строительства общей площадью " +
               "свыше 5000 кв. м с целью размещения одной или нескольких " +
               "организаций, осуществляющих продажу товаров и (или) оказание " +
               "услуг в соответствии с содержанием видов разрешенного " +
               "использования с кодами 4.5.0, 4.6.0, 4.8.0, 4.9.0; размещение " +
               "гаражей и(или) стоянок для автомобилей сотрудников и посетителей ",

               "Объекты торговли (торговые центры, " +
               "торгово-развлекательные центры (комплексы))",
               "",
               "",
               new string[] {
                    @"оптово-складской|оптово-распределитель|мелко-оптовой|складов",
                    @"\bоптов\w*[\s\-]\b\w+\b|\bгипермарк\w*\b|\s\bмолл\w*\b",

                    @"",
                    @"\bторг\w*[\s\-]*\bразвлек\w*\b[\s\-]*\b\w*\b",

                    @"",
                    @"\bторгов\w*(-делов\w*)?\s*(центр\w*|комплекс\w*)\b",

                    @"базар",
                    @"\bмелко\w*[-\s]*оптов\w*\b|\bторгов\w*\s*баз\w*\b",

                    @"\bостановочн\w*\b|\bпавильон\w*\b|\bкиоск\w*\b",
                    @"\bпод\s*торг\w*\b|\bторгов\w*\b",

                    @"",
                    @"\bсвыш\w*\s*5000\s*\b.*\bпродаж\w*\s*товар\w*\b" }
               ));

            nodes.Add(new Buisness("4.3.0", "4.3", "1004", "100",

               "Размещение объектов капитального строительства, сооружений, " +
               "предназначенных для организации постоянной или временной " +
               "торговли (ярмарка, рынок, базар), с учетом того, что каждое " +
               "из торговых мест не располагает торговой площадью более 200 кв. м;" +
               "размещение гаражей и(или) стоянок для автомобилей сотрудников и " +
               "посетителей рынка ",

               "Рынки",
               "",
               "",
               new string[] {
                    @"автостоянки\sрынка\sМедведково|бесплатной\sгостевой\sавтопарковки",
                    @"\bрын\w*\b|\bярмар\w*\b|\bбазар\w*\b",

                    @"",
                    @"\bсадов\w*\s*центр\w*\b" }
               ));

            nodes.Add(new Buisness("4.4.0", "4.4", "1004", "100",

               "Размещение объектов капитального строительства, " +
               "предназначенных для продажи товаров, торговая площадь " +
               "которых составляет до 5000 кв. м ",

               "Магазины",
               "",
               "",
               new string[] {
                    @"вход\w*\b\sв\sмагазин",
                    @"\bгастроном\w*\b|\bсамообслуж\w*\b|\bунивер[см][агм]{2}\w*\b|" +
                    @"\bмагаз\w*\b|\bмагаз\w*[\s\-]{1,4}\bкулинар\w*\b|\bапте[кч]\w*\b|\bоптик\w*\b",

                    @"\bгипермаркет\w*\b|\bкомбинат\w*\b|\bкондитер\w*\b\s*цех|\bмаркетинг\w*\b|" +
                    @"\bпроизводств\w*\s*\bкондитер\w*\b|\bсклад\b\w*\s*\bкондитер\w*\b|" +
                    @"\bпроизводств\w*\s*\bхлеб\w*\b|\bкондитер\w*\b\s*фабри|кафе-кондитерскую|склада\sкондитер",
                    @"\b(мини[-\s]*)?маркет\w*\b|\bбулоч\w*\b|\bкондитер\w*\b|\bпродаж\w*\s*хлеб\w*\b",

                    @"",
                    @"\bобъект\w*\s*торгов\w*\b|\bторгов\w*\s*помещен\w*\b|" +
                    @"\bторг\w*[-\s]*(выставоч\w*\s*)?павиль\w*\b|\bпавиль\w*\s*по\s*реализац\w*\b|" +
                    @"\bпавиль\w*\s*(продук\w*|розничн\w*|морожен\w*|союзпеча\w*|цвет\w*|спортлот\w*)\b|" +
                    @"павиль\w*\sс\sторг\w*\sмодулем*|\bрозничн\w*\s*торго\w*\b|" +
                    @"\bовощи-фрукты\b|\bспорт\w*[-\s]*игров\w*\s*павиль\w*\b",

                    @"",
                    @"\bпродаж\w*\s*(печтн\w*\s*продук\w*|\bхлеб\w*(\s*издел\w*)?)\b",

                    @"",
                    @"\bтов\w*\s*нар\w*\s*прод\w*\s*|\bавтосалон\w*\b",

                    @"",
                    @"\bторг\w*\s*павиль\w*\b|\bпавиль\w*\s*по\s*продаж\w*\b",

                    @"справоч\w*|\bстрелоч\w*\s*пост\w*",
                    @"\bкиоск\w*\b|\bостановоч\w*[-\s]*торгов\w*\b",

                    @"",
                    @"\bлоточ\w*\s*торгов\w*\b",

                    @"",
                    @"\bторгов\w*\s*палат\w*\b|\bпалат\w*\s*(квас\w*|" +
                    @"зоокио\w*|морожен\w*|по\s*(продаж\w*|реализ\w*)|продук\w*|цвет\w*)\b",

                    @"",
                    @"\bавтомагазин\w*\b|\bдемонстрац\w*\s*площадк\w*\s*автомобил\w*\b",

                    @"\bцентр\w*\b|\bкомплекс\w*\b",
                    @"\bпод\s*торг\w*\b|\bторгов\w*\b",

                    @"",
                    @"\bвыставочн\w*\s*площад\w*\b|\bмебел\w*\s*салон\w*\b",

                    @"",
                    @"\bвыстав\w*\s*нов\w*\s*автомаш\w*\b",

                    @"",
                    @"\bпродаж\w*\s*и\s*ремонт\w*\s*оргтехн\w*\b",

                    @"",
                    @"\bобмен\w*\s*газ\w*\s*баллон\w*\b",

                    @"",
                    @"\bбвт?к\b|\bкрыт\w*\s*палатк\w*\b|\bпавильон\w*\s*обои\w*\b",

                    @"",
                    @"\bпавиль\w*\b.*\bбыстровозвод\w*\s*конструк\w*\b",

                    @"",
                    @"\bпродаж\w*\s*(книго)?печат\w*\b(\s*продук\w*\b)?",

                    @"",
                    @"\bтов\w*\s*народ\w*\s*потреб\w*\s*|\bавто\w*\s*салон\w*\b",

                    @"",
                    @"\bсправоч\w*\s*киоск\w*\b|\bинформацион\w*[-\s]*справочн\w*\s*служб\w*\b",

                    @"",
                    @"\bсправоч\w*[-\s]*информац\w*\s*уз\w*\b",

                    @"",
                    @"\bреализац\w*\s*лотере\w*\b(\s*билет\w*\b)?",

                    @"",
                    @"\bавтомоб\w*\s*дизайн\w*[-\s]*салон\w*\b",

                    @"",
                    @"\bавтоцентр\w*\b|\bсалон\w*\s*дет\w*\s*одежд\w*\b",

                    @"",
                    @"\bобмен\w*\s*малолитраж\w*\s*газ\w*\s*балл?он\w*\b",

                    @"",
                    @"\bсупермаркет\w*\b|\bпрод\w*\s*маг\w*\b|\bпродаж\w*\s*продвол\w*\b",

                    @"",
                    @"\bавтозапчас\w*\b|\bмебел\w*\s*выставоч\w*\s*центр\w*\b",

                    @"",
                    @"\bзоомагаз\w*\b|\bсалон\w*\s*штор\w*\b",

                    @"",
                    @"\bреализец\w*\s*цветоч\w*\s*продук\w*\b|\bпункт\w*\s*проезд\w*\s*билет\w*\b",

                    @"",
                    @"\bреализац\w*\s*продукт\w*\s*питан\w*\b",

                    @"",
                    @"\bреализац\w*\s*мебел\w*\b" }
               ));

            nodes.Add(new Buisness("4.5.0", "4.5", "1001", "100",

               "Размещение объектов капитального строительства, предназначенных " +
               "для размещения организаций, оказывающих банковские и страховые ",

               "Банковская и страховая деятельность",
               "",
               "",
               new string[] {
                    @"банкетного",
                    @"\bбанк\w{0,9}\b|\bкредит\w{0,9}\b|\bстрахов\w{0,9}\b|\bфинанс\b",

                    @"",
                    @"\bсбербанк\w*|\bотдел\w*.*\bсб\b|\bломбард\w*\b",

                    @"",
                    @"\bосб\b|\bотд[.].*\bсб\b\s*(россии|рф)\b",

                    @"",
                    @"\bпункт\w*\s*обмен\w*\s*валют\w*\b" }
               ));

            nodes.Add(new Buisness("4.6.0", "4.6", "1004", "100",

               "Размещение объектов капитального строительства в целях " +
               "устройства мест общественного питания (рестораны, " +
               "кафе, столовые, закусочные, бары) ",

               "Общественное питание",
               "",
               "",
               new string[] {
                    @"отдельного\sвхода|бара\sСакура|\bбарж\b|\bкафедр\w*\b",
                    @"\bрестор\w*\b|\bбар\w{0,3}\b|\bкафе\w*\b|\bстолов\w*\b|" +
                    @"\bзакусоч\w*\b|\bбуфет\w*\b|\bкофей\w*\b|\bбыстр\w*\b\s*\bобслуж\w*\b",

                    @"",
                    @"\bобществен\w*\s*питан\w*\b|\bбыстр\w*\s*питан\w*\b|\bбистро\w*\b",

                    @"",
                    @"\bкулинария\b|\bшашлыч\w*\b|\bблинная\w*\b|\bлетне\w*\s*кафе\w*\b",

                    @"",
                    @"\bпредприят\w*\s*масс\w*\s*питан\w*\b" }
               ));

            nodes.Add(new Hotel("4.7.1", "4.7", "1004", "100",

               "Размещение гостиниц, туристических гостиниц, а также иных зданий, " +
               "используемых с целью извлечения предпринимательской выгоды из " +
               "предоставления жилого помещения для временного проживания в них ",

               "Гостиничное обслуживание",
               "",
               "",
               new string[] {
                    @"",
                    @"\bапарт\w*\b|\bгостиниц\w*\b|\bмотел\w*\b|\bс[ьъ]?юит[\s\-]*отел\w*\b",

                    @"",
                    @"\bгостинич\w*\s*(обслуж\w*|комплек\w*)\b",

                    @"",
                    @"\b(мини)?гостини[чц]\w*\b|\bгостев\w*\s*дом\w*\b" }
               ));

            nodes.Add(new Hostel("4.7.2", "4.7", "1004", "100",

               "Размещение хостелов ",

               "Гостиничное обслуживание",
               "",
               "",
               new string[] {
                    @"",
                    @"\bхостел\w*\b" }
               ));

            nodes.Add(new Dormitory("4.7.3", "4.7", "2002", "100",

               "Размещение общежитий ",

               "Гостиничное обслуживание",
               "",
               "",
               new string[] {
                    @"",
                    @"\bобщежит\w*\b",

                    @"",
                    @"\bстуд\w*\s*город\w*\b" }
               ));

            nodes.Add(new Buisness("4.8.0", "4.8", "1003", "100",

               "Размещение объектов капитального строительства, " +
               "предназначенных для размещения: дискотек и танцевальных площадок, " +
               "ночных клубов, аквапарков, боулинга, аттракционов, " +
               "ипподромов и т.п., игровых автоматов (кроме игрового оборудования," +
               " используемого для проведения азартных игр), игровых площадок ",

               "Развлечения",
               "",
               "",
               new string[] {
                  @"",
                  @"\bби[л]{1,2}ьярд\w*\b",

                  @"\bспорт\w*\b|\bплощадок\b|\bигровых\s+форм\b",
                  @"\bигров\w*\b\s*\b\w*\b",

                  @"",
                  @"\bбоулинг\w*\b|\bи[п]{1,2}одром\w*\b",

                  @"",
                  @"\b(спортив\w*[-\s]*)?развлекательн\w*\b",

                  @"",
                  @"\bдискотек\w*\b|\bночн\w*\b\s*\bклуб\w*\b|\bтанц\w*\b\s*\bплощад\w*\b|" +
                  @"\bаквапарк\w*\b|\bа[т]{1,2}ракцион\w*\b",

                  @"\bказарм\w*\b",
                  @"\bбилл?ирд\w*\b|\bбилл?ьярд\w*\b",

                  @"",
                  @"\bкультурн\w*[-\s]*оздоровит\w*\b" }
               ));

            nodes.Add(new Buisness("4.9.0", "4.9", "3004", "300",

               "Размещение постоянных или временных гаражей с несколькими " +
               "стояночными местами, стоянок (парковок), гаражей, в том числе " +
               "многоярусных, не указанных в коде 2.7.1 ",

               "Обслуживание автотранспорта",
               "",
               "",
               new string[] {
                    @"\bличн\w*\b|\bметал\w*\b|\bиндивидуал\w*\b",
                    @"\bгар[ао]?ж\w*\b|\bобслуживан\w*\s*(авто)?транспорт\w*|\bавтогараж\w*\b",

                    @"\bбез\sправа.*\bавтостоя\w*\b",
                    @"\bпарков(к\w*|оч\w*|ок\w*)\b",

                    @"\bаварий\w*\b|\bстоян\w*\s*(строит\w*\s*техн\w*|убороч\w*\s*трактор\w*)\b",
                    @"(\b(?:авто)?стоян\w*[-\s]*)(бокс\w*|открыт\w*)??\b|\2[-\s]*\1",

                    @"",
                    @"\bавтопарков[ок][каие]\w*\b",

                    @"",
                    @"\bгск\b|\bпаркинг\w*\b",

                    @"",
                    @"\bразмещен\w*\s*ракушек\b" }
               ));

            nodes.Add(new GasStation("4.9.1.1", "4.9.1", "3004", "300",

               "Размещение автозаправочных станций (бензиновых, газовых); " +
               "размещение магазинов сопутствующей торговли, зданий для " +
               "организации общественного питания в качестве " +
               "объектов придорожного сервиса ",

               "Объекты придорожного сервиса",
               "",
               "",
               new string[] {
                    @"",
                    @"\b(авто\w*|бенз\w*|газо\w*)[-\s]*(заправ\w*|наполнит\w*|колон\w*)\b|\bАЗС\b",

                    @"",
                    @"\bавто?заправ\w*\b|\bтоплив\w*[-\s]*раздаточ\w*\s*колон\w*\b",

                    @"",
                    @"\bторгов\w*[-\s]*заправочн\w*\b|\bагзс\b|\bавтомоб\w*\s*криоген\w*\s*заправочн\w*\s*станц\w*\b",

                    @"",
                    @"\bакзс\b|\bмноготопливн\w*\s*заправочн\w*\s*станц\w*\b|\bагнкс\b|\bагкнс\b",

                    @"",
                    @"\bтоплив\w*[-\s]*раздаточ\w*\s*пункт\w*\b" }
               ));

            nodes.Add(new Motel("4.9.1.2", "4.9.1", "3004", "300",

               "Размещение зданий для предоставления гостиничных услуг " +
               "в качестве придорожного сервиса ",

               "Объекты придорожного сервиса",
               "",
               "",
               new string[] { }));

            nodes.Add(new CarWashingStation("4.9.1.3", "4.9.1", "3004", "300",

               "Размещение автомобильных моек и прачечных для " +
               "автомобильных принадлежностей ",

               "Объекты придорожного сервиса",
               "",
               "",
               new string[] {
                    @"",
                    @"\b(авто)?мо[ей]к\w*\b|\b(авто)?моеч\w*" }
               ));

            nodes.Add(new BodyShop("4.9.1.4", "4.9.1", "3004", "300",

               "Размещение мастерских, предназначенных для ремонта и " +
               "обслуживания автомобилей, и прочих объектов придорожного сервиса ",

               "Объекты придорожного сервиса",
               "",
               "",
               new string[] {
                    @"",
                    @"\bтех\w*\s*обслуж\w*\b|\bремон\w*\s*(автомобил\w*|машин\w*)\b",

                    @"автостоянки",
                    @"\b(авто\w*|(при)?дорож\w*|мото\w*)[-\s]*(сервис\w*|комплекс\w*)\b",

                    @"",
                    @"\bавто(ремонт\w*|мастерск\w*)\b|\bпавиль\w*\s*шиномонт\w*\b",

                    @"",
                    @"\bшином[оа]нтаж\w*\b|\bавтотехцен\w*\b|\bтех\w*\s*центр\w*\b",

                    @"\bстроит\w*\s*(техник\w*|механизм\w*)\b",
                    @"\bремонт\w*\s*((транспорт\w*|машин\w*)|автомобил\w*)\b", // ((авто)?(транспорт\w*|машин\w*)

                    @"",
                    @"\bпридорож\w*[-\s]*тех\w*\s*комплекс\w*\b",

                    @"",
                    @"\bагзс\b.*\bкомплекс\w*\s*сервис\w*\s*услуг\w*\b",

                    @"",
                    @"\bкомплекс\w*\s*(автоуслуг\w*|дорож\w*\s*обслуж\w*|по\w*\s*обслуживан\w*\s*автомоб\w*|автоцентр\w*)\b",

                    @"",
                    @"\bавтомастерск\w*\b|\bмастерск\w*\s*по\w*\s*ремонт\w*\s*((авто)?машин\w*|.*\bавтомобил\w*\b)\b",

                    @"",
                    @"\bавтотехобслуживан\w*\b",

                    @"",
                    @"\bмастерск\w*\s*тонировк\w*\b",

                    @"",
                    @"\bкомплекс\w*\s*сервис\w*\s*обслуживан\w*\b|\bпункт\w*\s*технич\w*\s*помощ\w*\b",

                    @"",
                    @"\bпереоборудован\w*\s*автомоб\w*\b|\bавтотех[-\s]*центр\w*\b",

                    @"",
                    @"\b(замен\w*\s*и\s*сбор\w*|сбор\w*\s*и\s*замен\w*)\b.*\bотработан\w*\s*(авто\w*\s*)?мас\w*\b" }
               ));

            nodes.Add(new Buisness("4.10.0", "4.10", "1003", "100",

               "Размещение объектов капитального строительства, сооружений, " +
               "предназначенных для осуществления выставочно-ярмарочной и " +
               "конгрессной деятельности, включая деятельность, необходимую " +
               "для обслуживания указанных мероприятий " +
               "(застройка экспозиционной площади, " +
               "организация питания участников мероприятий) ",

               "Выставочно-ярмарочная деятельность",
               "",
               "",
               new string[] {
                   @"",
                   @"\bярмар\w*\b|\bконгрес\w*\b" }
               ));

            nodes.Add(new BaseRecreation("5.0.1", "5.0", "4001", "400",

               "Обустройство мест для занятия спортом, физической культурой, " +
               "пешими или верховыми прогулками, отдыха и туризма, " +
               "наблюдения за природой, пикников, охоты, рыбалки и иной деятельности. " +
               "Содержание данного вида разрешенного использования включает в " +
               "себя содержание видов разрешенного использования с кодами " +
               "5.1.1, 5.1.2, 5.1.3, 5.1.4, 5.1.5, 5.2.1.0, 5.2.0, 5.3.0, 5.5.0, " +
               "5.0.2, 5.4.0 ",

               "Отдых (рекреация)",
               "",
               "",
               new string[] {
                    @"",
                    @"\bрекреацион\w*\s*деятель\w*\b",

                    @"",
                    @"\bобъ?ект\w*\s*рекреац\w*\b",

                    @"организации отдыха, культурного проведения свободного времени, " +
                    @"укрепления здоровья, а так же для выращивания плодовых, ягодных, овощных",
                    @"\bфизкульт\w*[-\s]*озо?доровит\w*\b|\bорганиз\w*\s*отдых\w*\b",

                    @"",
                    @"\bрекреац\w*\b|\bзон\w*\s*отдых\w*\b"}
               ));

            nodes.Add(new Recreation("5.0.2", "5.0", "4001", "400",

               "Создание и уход за парками, городскими лесами, садами и " +
               "скверами, прудами, озерами, водохранилищами, пляжами, " +
               "береговыми полосами водных объектов общего пользования, " +
               "а также обустройство мест отдыха в них ",

               "Отдых (рекреация)",
               "",
               "",
               new string[] {
                    @"филевский|марьинский|метро|временной|троллей\w*\b|" +
                    @"автобус\w*\b|хуамин|на\sпериод|\bтаксомотор\w*\b|" +
                    @"парка\sрезервуаров|мэлз|с\sтерритории\sпарка|технологических|" +
                    @"автомобильных\sдорог|выводимых\sс\sтерритории|" +
                    @"культурно-просветительских\sобъектов",
                    @"\s\bпарк(а|ов|и|ами)?\b|\bпарков(ой|ая|ых)\s*зон\w*\b|\bпаркового\sкомплекса\sи\sотдыха\b",

                    @"\bдетск\w*\b|ясли",
                    @"\bгородск\w*\s*лес\w*\b",

                    @"",
                    @"\bприрод\w*\b.*\bрекреац\w*\b",

                    @"",
                    @"\bмассов\w*\s*отдых\w*\s*населен\w*\b|\bзон\w*\s*отдых\w*\b",

                    @"",
                    @"\bпарк\w*\b\s*\bкультур\w*\b|\bботанич\w*\b\s*\bсад\w*",

                    @"",
                    @"\bземл\w*\s*покрыт\w*\s*лес\w*\b",

                    @"",
                    @"\bдетск\w*\s*город\w*\b" }
               ));

            nodes.Add(new Sport("5.1.1", "5.1", "1006", "100",

               "Размещение спортивных сооружений массового посещения " +
               "(стадионов, дворцов спорта, ледовых дворцов) ",

               "Спорт",
               "",
               "",
               new string[] {
                    @"при\sстадионе\sянтарь",
                    @"\bстадион\w*\b|\bдвор[е]?ц\w*\s*\bспорт\w*\b|\bледов\w*\s*\bдвор[е]?ц\w*\b|" +
                    @"\bспорт[\w\s]{0,7}\bкомплек\b",

                    @"",
                    @"\bспотивн\w*[-\s]*развлекатель\w*\s*(центр\w*|комплекс\w*)\b",

                    @"",
                    @"\bкомплекс\w*\s*массов\w*\s*физкульт\w*\s*",

                    @"",
                    @"\bцентр\w*\s*культур\w*\s*и\s*спорт\w*\b",

                    @"",
                    @"\bстроит\w*\s*спортив\w*\s*сооружен\w*\b",

                    @"",
                    @"\bспортив\w*\s*сооружен\w*\s*масс\w*\s*посещен\w*\b" }
               ));

            nodes.Add(new Sport("5.1.2", "5.1", "1006", "100",

               "Размещение объектов капитального строительства в качестве " +
               "спортивных клубов, спортивных залов, бассейнов, ФОКов, фитнес-центров ",

               "Спорт",
               "",
               "",
               new string[] {
                    @"",
                    @"\bбассейн\w*\b|\bфитнес\w*\b|\bфок\w{0,2}\b",

                    @"",
                    @"\bспорт\w*\s*зал\w*\b|\bспорт\w*\s*клуб\w*\b",

                    @"",
                    @"\b(физкультур\w*|спорт\w*)[-\s]*(оздоровит\w*\s*)?компл\w*\b",

                    @"\bлагер\w*\b",
                    @"\b(физкульт\w*|спорт\w*)[-\s]*озо?доровит\w*\b",

                    @"",
                    @"\bспорт\w*\s*танц\w*\b|\bучебн\w*\s*спортив\w*\b",

                    @"",
                    @"\bспорт\w*\s*соо?руж\w*\s*огранич\w*\b",

                    @"",
                    @"\bучеб\w*[-\s]*спорт\w*\b",

                    @"",
                    @"\bгимнаст\w*\s*зал\w*\b",

                    @"",
                    @"\bкрыты\w*\s*спорт\w*\s*комплекс\w*\b",

                    @"",
                    @"\bдюсш\b",

                    @"",
                    @"\b(учрежден\w*\s*)?физ\w*[-.\s]*культ\w*\b(\s*и\s*спорт\w*\b)?",

                    @"",
                    @"\bобъект\w*\s*спорт\w*\s*назнач\w*\b",

                    @"",
                    @"\bстроит\w*\s*спортив\w*\s*сооружен\w*\b",

                    @"",
                    @"\bсекц\w*\s*(самбо\w*|карат\w*)\b|\bкат\w*\b.*\b(л[её]д\w*|льд\w*)\b",

                    @"",
                    @"\bразмещен\w*\s*спортив\w*\s*объект\w*\b|\bдля\s*спотив\w*[-\s]*тренировоч\w*\b" }
               ));

            nodes.Add(new Sport("5.1.3", "5.1", "1006", "100",

               "Устройство площадок для занятия спортом и физкультурой " +
               "(беговые дорожки, спортивные сооружения, теннисные корты, " +
               "поля для спортивной игры, автодромы, мотодромы, трамплины и т.п., " +
               "трассы и спортивные стрельбища) ",

               "Спорт",
               "",
               "",
               new string[] {
                    @"",
                    @"\bвело[\s\-]*трек\w*\b|\bвело[\s\-]*дром\w*\b|" +
                    @"\bавто[\s\-]*дром\w*\b|\bкарт[\s\-]*дром\w*\b|" +
                    @"\bмото[\s\-]*дром\w*\b",

                    @"",
                    @"\bтренровоч\w*\s*\bпол[яе]\w*\b|\bспорт\w*\b[\s\-]*\bстрельб\w*\b|" +
                    @"\bспорт\w*\b[\s\-]*\bтрасс\w*\b|\bтрамплин\w*\b",

                    @"",
                    @"\bлыж\w*\b|\bкартинг\w*\b|\bтеннис\w*\b|\bкорт\w{0,2}\b",

                    @"",
                    @"\bспорт\w*\s*(площадк\w*\b|город\w*)\b",

                    @"\bмасс\w*\s*посещ\w*\b",
                    @"\bспорт\w*\s*сооружен\w*\b",

                    @"",
                    @"\bстроит\w*\s*спортив\w*\s*сооружен\w*\b",

                    @"",
                    @"\bразмещен\w*\s*спортив\w*\s*объект\w*\b|\bдля\s*спотив\w*[-\s]*тренировоч\w*\b",

                    @"",
                    @"\bплоскостн\w*\s*спортив\w*\s*сооруж\w*\b|\bавтокартодром\w*\b",

                    @"",
                    @"\bоткрыт\w*\s*оздоровит\w*\s*площад\w*\b|\bплощад\w*\s*отдых\w*\s*и\w*\s*спорт\w*\b",

                    @"",
                    @"\bгонолыж\w*\s*трас\w*\b" }
               ));

            nodes.Add(new Sport("5.1.4", "5.1", "1006", "100",

               "Размещение спортивных баз и лагерей, спортивных сооружений " +
               "для занятия водными видами спорта (причалы и сооружения, " +
               "необходимые для водных видов спорта и хранения " +
               "соответствующего инвентаря) ",

               "Спорт",
               "",
               "",
               new string[] {
                     @"дворца\sводного\sспорта|центра\sводного\sспорта|магазина\sводно-спортивной",
                     @"\bгреб\w*\b|\bлодочн\w*\b|\bводн\w*[\s\-]*спорт\w*\b" }
               ));

            nodes.Add(new Sport("5.1.5", "5.1", "1006", "100",

               "Размещение спортивных баз и лагерей ",

               "Спорт",
               "",
               "",
               new string[] {
                    @"водно|конно|рыболовно",
                    @"\bспорт\w*\b[\s\-]*\bбаз\w*\b|\bспорт\w*\b[\s\-]*\bлагер\w*\b",

                    @"",
                    @"\b(физкульт\w*|спорт\w*)[-\s]*озо?доровит\w*\b.*\bлагер\w*\b",

                    @"",
                    @"\bгорнолыжн\w*\s*баз\w*\b" }
               ));

            nodes.Add(new Recreation("5.2.0", "5.2", "1006", "100",

               "Размещение баз и палаточных лагерей для проведения походов и " +
               "экскурсий по ознакомлению с природой, пеших и конных прогулок, " +
               "устройство троп и дорожек, размещение щитов с познавательными " +
               "сведениями об окружающей природной среде; осуществление необходимых " +
               "природоохранных и природовосстановительных мероприятий ",

               "Природно-познавательный туризм",
               "",
               "",
               new string[] {
                    @"товаров|мотеля|агентств|бюро|тропарево",
                    @"\bтуристич\w*\b|\bпоход\w*\b|\bтроп\w*\b",

                    @"",
                    @"\bбаз\w*\s*отдых\w*\b|\bцентр\w*\s*дет[.]?\w*[-\s]*юнош\w*\s*туризм\w*\b" }
               ));

            nodes.Add(new Recreation("5.2.1.0", "5.2.1", "1006", "100",

               "Размещение пансионатов, туристических гостиниц, кемпингов, " +
               "домов отдыха, не оказывающих услуги по лечению, а также иных зданий, " +
               "используемых с целью извлечения предпринимательской выгоды из " +
               "предоставления жилого помещения для временного проживания в них; " +
               "размещение детских лагерей ",

               "Туристическое обслуживание",
               "",
               "",
               new string[] {
                    @"",
                    @"\bдом\w{0,2}[\s\-]*\bотдых\w*\b|\bбаз\w{0,2}[\s\-]*\bотдых\w*\b|" +
                    @"\bлагер\w{0,2}[\s\-]*\bотдых\w*\b",

                    @"ветеранов\sтруда",
                    @"\bпансионат\w*\b|\b(детск\w*|пионер\w*)\s*лагер\w*\b",

                    @"",
                    @"\bцентр\w*\s*семейн\w*\s*отдых\w*\b" }
               ));

            nodes.Add(new Recreation("5.3.0", "5.3", "1006", "100",

               "Обустройство мест охоты и рыбалки, в том числе размещение " +
               "дома охотника или рыболова, сооружений, необходимых для " +
               "восстановления и поддержания поголовья зверей или количества рыбы ",

               "Охота и рыбалка",
               "",
               "",
               new string[] {
                    @"",
                    @"\bрыбал\w*\b",

                    @"",
                    @"\bрыболов\w*[-\s]*(спортив\w*\s*)?баз\w*\b" }
               ));

            nodes.Add(new Recreation("5.4.0", "5.4", "3005", "333",

               "Размещение сооружений, предназначенных для причаливания, " +
               "хранения и обслуживания яхт, катеров, лодок и других маломерных судов ",

               "Причалы для маломерных судов",
               "",
               "",
               new string[] {
                    @"",
                    @"\b(причаливан\w*|хранен\w*|обслуживан\w*)[-\s]*(яхт\w*|катер\w*|лод\w*|маломерн\w*\s*суд\w*)\b",

                    @"",
                    @"\bбаз\w*\s*(маломер\w*\s*флот\w*|сертифика\w*\s*суд\w*)\b" }
               ));

            nodes.Add(new Recreation("5.5.0", "5.5", "1006", "100",

               "Обустройство мест для игры в гольф или осуществления конных прогулок, " +
               "в том числе осуществление необходимых земляных работ и вспомогательных " +
               "сооружений; размещение конноспортивных манежей, не предусматривающих " +
               "устройство трибун ",

               "Поля для гольфа или конных прогулок",
               "",
               "",
               new string[] {
                    @"",
                    @"\bгольф\w*\b|\bконн\w*\s*прогул\w*\b|\bконн\w*\s*манеж\w*\b",

                    @"",
                    @"\bкон\w*[-\s]*спортив\w*\b" }
               ));

            nodes.Add(new BaseIndustry("6.0.0", "6.0", "3002", "300",

               "Размещение объектов капитального строительства в целях добычи недр, " +
               "их переработки, изготовления вещей промышленным способом ",

               "Производственная деятельность",
               "",
               "",
               new string[] {
                    @"\bдревес\w*\b|\bсел\w*\s*хоз\w*\b|\bдревесин\w*\b|\bлесоцех\w*\b|\bдерево\w*\b",
                    @"\bперераб\w*\sприродн\w*\b|\bдобыч\w*\s*природ\w*\s*ресур\w*\b",

                    @"\bдревес\w*\b|\bсел\w*[.]?[-\s]*хоз\w*[.]?\b|\bдревесин\w*\b|\bлесоцех\w*\b|\bдерево\w*\b" +
                    @"\bнауч\w*[-\s]*(производ\w*|лаборат\w*|технич\w*)\b|\bбез\s*прав\w*\s*производств\w*\b|" +
                    @"\bпроизвод\w*\s*землян\w*\s*работ\w*\b|\bавтобаз\w*s*мжд\b",
                    @"\bпроизводств\w*\b|" +
                    @"(\bпроизводств\w\s*)?цех\w*\s*(по)?\s*(производ\w*|изготовл\w*|(об|пере)работ\w*|выпуск\w*|сборк\w*)?" +
                    @"\bпроизводств\w*\s*центр\w*\b",

                    @"\bбытовых\s*услуг\b|\bучебн\w*\b|\bавтокомбина\w*\b|\bсел\w*\s*хоз\w*\b|\bдревесин\w*\b|" +
                    @"\bлесоцех\w*\b|\bдерево\w*\b|\bдет[.]?\w*\b|\bбытов\w*\b",
                    @"\bзавод\w{0,2}\b|\bфабрик\w*\b|\bкомбинат\w*\b",

                    @"\bпромышл\w*\s*товар\w*\b|\bсел\w*\s*хоз\w*\b|\bдревесин\w*\b|\bлесоцех\w*\b|\bдерево\w*\b|" +
                    @"\bиспытан\w*\s*опытн\w*\s*промышлен\w*\s*образц\w*\b",
                    @"\bпромышл\w*\b",

                    @"",
                    @"\bучебн\w*[-\s]*производ\w*\s*баз\w*\b",
                    // Из экселя
                    @"",
                    @"\bдругих\s*предприятий\b|\bбаз\w*\s*испытан\w*\s*электрооборудован\w*\b",

                    @"",
                    @"\b(здан\w*|строен\w*)\s*и\s*сооружен\w*\s*предприят\w*\b|\bмеханическ\w*\s*мастерск\w*\b",

                    @"",
                    @"\bтерритор\w*[,]?\s*необходим\w*\s*для\s*функционирован\w*\s*предприят\w*\b",

                    @"",
                    @"\bзавод\w*\s*здан\w*\s*и\s*сооружен\w*\b" }
               ));

            nodes.Add(new Industry("6.1.0", "6.1", "3002", "300",

               "Осуществление геологических изысканий; добыча недр открытым " +
               "(карьеры, отвалы) и закрытым (шахты, скважины) способами; " +
               "размещение объектов капитального строительства, в том числе " +
               "подземных, в целях добычи недр;размещение объектов " +
               "капитального строительства, необходимых для подготовки " +
               "сырья к транспортировке и(или) промышленной переработке ",

               "Недропользование",
               "",
               "",
               new string[] {
                    @"",
                    @"\bгеолог\w*\s*исыск\w*\b|\bдобыч\w*\s*недр\w*\b",

                    @"",
                    @"\bобработ\w*\s*природ\w*\s*камн\w*\b|\bвермикулит\w*\b",

                    @"",
                    @"\bразработ\w*\s*полезн\w*\s*ископаем\w*\b|\bстроит\w*\s*геодезич\w*\s*шурф\w*\b" }
               ));

            nodes.Add(new Industry("6.2.0", "6.2", "3002", "300",

               "Размещение объектов капитального строительства горно-обогатительной " +
               "и горно-перерабатывающей, металлургической, машиностроительной " +
               "промышленности, а также изготовления и ремонта продукции судостроения, " +
               "авиастроения, вагоностроения, машиностроения, станкостроения, а " +
               "также другие подобные промышленные предприятия, для эксплуатации " +
               "которых предусматривается установление охранных или " +
               "санитарно-защитных зон, за исключением случаев, когда объект " +
               "промышленности отнесен к иному виду разрешенного использования ",

               "Тяжелая промышленность",
               "",
               "",
               new string[] {
                    @"",
                    @"\bгорно\w*\s*промышленн\w*\b|\bметаллург\w*\s*промышленн\w*\b|" +
                    @"\bмашиностроит\w*\s*промышленн\w*\b",

                    @"",
                    @"\bпроизвод\w*\s*оборудов\w*\b|\bпроизвод\w*\s*маш\w*\b",

                    @"",
                    @"\bавиа\w*\s*строен\w*\b|\bсудо\w*\s*вагоно\w*\b|" +
                    @"\bмашино\w*\s*строен\w*\b|\bстанко\w*\s*строен\w*\b",

                    @"",
                    @"\bремонт\w*\b.*\bтехнологич\w*\s*оборудован\w*\b",

                    @"",
                    @"\bмеханич\w*\s*(масерск\w*|баз\w*)\b|\bремонт\w*\s*авиатехник\w*\b",

                    @"",
                    @"\bремонт\w*[-\s]*строит\w*\s*баз\w*\b" }
               ));

            nodes.Add(new Industry("6.2.1.0", "6.2.1", "3002", "300",

               "Размещение объектов капитального строительства, предназначенных " +
               "для производства транспортных средств и оборудования, производства " +
               "автомобилей, производства автомобильных кузовов, производства прицепов, " +
               "полуприцепов и контейнеров, предназначенных для перевозки одним " +
               "или несколькими видами транспорта, производства частей и " +
               "принадлежностей автомобилей и их двигателей ",

               "Автомобилестроительная промышленность",
               "",
               "",
               new string[] {
                    @"",
                    @"\bпроизвод\w*\s\b\w*двигат\w*\b|\bпроизвод*\s*.{0,20}\bавтомоб\w*\b",

                    @"",
                    @"\bэкспер[.]?\w*\b.*\ba/м\b" }
               ));

            nodes.Add(new Industry("6.3.0", "6.3", "3002", "300",

               "Размещение объектов капитального строительства, предназначенных " +
               "для текстильной, фарфоро-фаянсовой, электронной промышленности ",

               "Легкая промышленность",
               "",
               "",
               new string[] {
                    @"",
                    @"\bшвейн\w*\s*производ\w*\b|\bпроизвод\w*\s*швейн\w*\b|\bпроизвод\w*\s*шерст\w*\b|" +
                    @"\bпроизвод\w*\s*войлоч\w*\b|\bпроизвод\w*.*ткани.+кожи.+меха|\bпроизвод\w*\s*трикот\w*\b",

                    @"",
                    @"\bпроизвод\w*\s*ювелир\w*\b|\bпроизвод\w*\s*галантер\w*\b|\bпроизвод\w*\s*текстил\w*\b",

                    @"",
                    @"\bпротез\w*[-\s]*ортопед\w*\s*издел\w*\b",

                    @"",
                    @"\bизготовл\w*\s*неткан\w*\s*матер\w*\b|\bвыпуск\w*\s*трикотаж\w*\s*издел\w*\b" }
               ));

            nodes.Add(new Industry("6.3.1.0", "6.3.1", "3002", "300",

               "Размещение объектов капитального строительства, предназначенных " +
               "для фармацевтического производства, в том числе объектов, в " +
               "отношении которых предусматривается установление охранных или " +
               "санитарно-защитных зон ",

               "Фармацевтическая промышленность",
               "",
               "",
               new string[] {
                    @"",
                    @"\bфармацевт\w*\s*(производ\w*|центр\w*)\b" }
               ));

            nodes.Add(new Industry("6.4.0", "6.4", "3002", "300",

               "Размещение объектов пищевой промышленности, по переработке " +
               "сельскохозяйственной продукции способом, приводящим к их " +
               "переработке в иную продукцию (консервирование, копчение, " +
               "хлебопечение), в том числе для производства напитков, " +
               "алкогольных напитков и табачных изделий ",

               "Пищевая промышленность",
               "",
               "",
               new string[] {
                    @"",
                    @"\bпроизводств\w*.*\b(хлеб\w*|шампан\w*)\b|\bпекарн\w*\b|" +
                    @"\bкондитерско-булочн\w*\b|\bпищев\w*\s*комбин\w*\b",

                    @"",
                    @"\bцех\w*.*\bпродукт\w*\s*питан\w*\b|\bкондитер\w*\sцех\w*\b|" +
                    @"\bцех\w*\s*колбас\w*\b|\bмяс\w*\b.*\bцех\w*\b|\bцех\w*\b.*\bмяс\w*\b",

                    @"",
                    @"\bмясоперераб\w*\b|\bмясокомбинат\w*\b",

                    @"",
                    @"\bмяс\w*\b",

                    @"",
                    @"\bпищев\w*\s*(промышленнос\w*|производст\w*)\b",

                    @"",
                    @"\bпроизвод\w*\s*кондитерск\w*\s*издел\w*\b",

                    @"",
                    @"\bпаточн\w*\s*станц\w*\b|\bхлеб\w*[-\s]*завод\w*\b|\bминипекарн\w*\b",

                    @"",
                    @"\bздан\w*\b.*\bобеспечен\w*\s*питан\w*\s*пассажир\w*\b" }
               ));

            nodes.Add(new Industry("6.5.0", "6.5", "3002", "300",

               "Размещение объектов капитального строительства, " +
               "предназначенных для переработки углеводородного сырья, " +
               "изготовления удобрений, полимеров, химической продукции " +
               "бытового назначения и подобной продукции, а также другие " +
               "подобные промышленные предприятия ",

               "Нефтехимическая промышленность",
               "",
               "",
               new string[] {
                    @"склада\s+минеральных\s+удобрений",
                    @"\bперераб\w*\s*углеводород\w*\s*сырь\w*\b|\bпроизвод\w*\b\s*нефте\w*\b|" +
                    @"\bпроизвод\w*\b\s*кокс\w*\b|\bхим\w*\s*производ\w*\b|" +
                    @"\bполимер\w*\b|\bудобрен\w*\b|\bхимич\w*\s*продук\w*\b",

                    @"",
                    @"\bпроивзодств\w*\s*полиэтилен\w*\b|\bплстмасc\w*\b",

                    @"",
                    @"\bбензохранилищ\w*\b" }
               ));

            nodes.Add(new Industry("6.6.0", "6.6", "3002", "300",

               "Размещение объектов капитального строительства, предназначенных " +
               "для производства: строительных материалов (кирпичей, пиломатериалов, " +
               "цемента, крепежных материалов), бытового и строительного газового и " +
               "сантехнического оборудования, лифтов и подъемников, столярной продукции, " +
               "сборных домов или их частей и тому подобной продукции ",

               "Строительная промышленность",
               "",
               "",
               new string[] {
                    @"",
                    @"\bпроизвод\w*\s*стро\w*[\s\-]матер\w*\b|\bпроизвод\w*\s*стро\w*[\s\-]констр\w*\b|" +
                    @"\bпроизвод\w*\s*и\s*реализ\w*\s*стро\w*[\s\-]матер\w*\b",

                    @"",
                    @"\bпроизвод\w*\s*кирп\w*\b|\bпроизвод\w*\s*пил\w*\b|\bпроизвод\w*\s*цем\w*\b|" +
                    @"\bпроизвод\w*\s*креп[её]ж\w*\b|\bпроизвод\w*\s*газ\w*\b|\bпроизвод\w*\s*лифт\w*\b" +
                    @"|\bпроизвод\w*\s*столяр\w*\b|\bпроизвод\w*\s*сбор\w*\b",

                    @"",
                    @"\bцех\w*\b.*\bмебел\w*\b|\bмебел\w*\b.*\bцех\w*\b|\bмебел\w*\s*мастеск\w*\b",

                    @"",
                    @"\bсборк\w*\b.*\bмебел\w*\b",

                    @"",
                    @"\bремонт\w*[-\s]*строит\w*\s*баз\w*\b", // < ===Excel here 

                    @"",
                    @"\bстроит\w*\s*баз\w*\b|\bизготовлен\w*\s*металлоконструкц\w*\b",

                    @"",
                    @"\b(раствор\w*|асфальт\w*)[-\s]*бетон\w*\b|\bстройдвор\w*\b",

                    @"",
                    @"\bизготовл\w*\s*железобетон\w*\s*издел\w*\b|\bздан\w*\s*по\s*ремонт\w*\s*квартир\w*\b",

                    @"",
                    @"\bкран\w*\s*(площадк\w*\s*)?(№\d+\b)?|\bремонт\w*\s*башен\w*\s*кран\w*\b",

                    @"",
                    @"\bбаз\w*\b.*\bстроит\w*\s*\bмпсм\b|\bздан\w*\s*стройгруппы\b" }
               ));

            nodes.Add(new Industry("6.7.0", "6.7", "3002", "300",

               "Размещение объектов гидроэнергетики, тепловых станций и " +
               "других электростанций, размещение обслуживающих и вспомогательных " +
               "для электростанций сооружений (золоотвалов, " +
               "гидротехнических сооружений); размещение объектов " +
               "электросетевого хозяйства, за исключением объектов энергетики, " +
               "размещение которых предусмотрено содержанием вида разрешенного " +
               "использования с кодом 3.1 ",

               "Энергетика",
               "",
               "",
               new string[] {
                    @"тепломагистра\w*\b",
                    @"\bтэц\w*\b",

                    @"цтп|\bтеплово\w*\s*пунк\w*\b|прокладку\sинженерных\sсетей|\bтеплосет\w*\b|прокладки\sкабел\w*",
                    @"\bртс\w*\b",

                    @"\b(электро)?тягов\w*\s*(под)?станц\w*\b",
                    @"\s\w*электр\w*[\s\-]*(под)?станц\w*\b|\s\w*электро\w*[\s\-]*централ\w*\b",

                    @"",
                    @"\bгидроэнерг\w*\b|\bтеплов\w*\s*станц\w*\b",

                    @"",
                    @"\bобъект\w*\s*энергет\w*\b|\bградир\w*\b",

                    @"",
                    @"\b(сооружен|устро)\w*\s*энер\w*\b",

                    @"\bтягов\w*\b|\bтрансформатор\w*\b|\bтранспорт\w*\b|\bраспределит\w*\b|" +
                    @"\bскор\w*\b.*\bпомощ\w*\b|\bтеплов\w*\b|\bподстанц\w*\s*в\s*комплекс\w*\b" +
                    @"\bводонасосн\w*\b|\bгазораспределит\w*\b|\bзтп\b|\bлэп\b|" +
                    @"\bпонизит\w*\b|\bкабельн\w*\s*колл?ектор\w*\b",
                    @"\bподстанц\w*\b",

                    @"",
                    @"\bфилиал\w*\b.*\b(оао)?мосэнерго\w*\b",

                    @"",
                    @"\bмастерск\w*\s*по\s*ремонт\w*\s*телемеханич\w*\s*оборудован\w*\b",

                    @"",
                    @"\bмастерск\w*\s*по\s*ремонт\w*\s*электр\w*\s*оборудован\w*\b",

                    @"",
                    @"\bградир\w*\b|\bпункт\w\s*распределен\w*\s*электроэнерг\w*\b",

                    @"",
                    @"\bбаз\w*\s*электрослужб\w*\b" }
               ));

            nodes.Add(new Industry("6.7.1", "", "", "",

               "Размещение объектов использования атомной энергии, в том числе " +
               "атомных станций, ядерных установок (за исключением создаваемых в " +
               "научных целях), пунктов хранения ядерных материалов и радиоактивных " +
               "веществ размещение обслуживающих и вспомогательных для электростанций " +
               "сооружений; размещение объектов электросетевого хозяйства, " +
               "обслуживающих атомные электростанции ",

               "Атомная энергетика",
               "",
               "",
               new string[] { }));

            nodes.Add(new Industry("6.8.0", "6.8", "3003", "300",

               "Размещение объектов использования атомной энергии, в том числе " +
               "атомных станций, ядерных установок (за исключением создаваемых в " +
               "научных целях), пунктов хранения ядерных материалов и радиоактивных " +
               "веществ размещение обслуживающих и вспомогательных для электростанций " +
               "сооружений; размещение объектов электросетевого хозяйства, " +
               "обслуживающих атомные электростанции ",

               "Связь",
               "",
               "",
               new string[] {
                    @"бытового\sгородка",
                    @"\bтелевиз\w*\b|\bтелевид\w*\b",

                    @"",
                    @"\bрадио[-\s]*(цен\w*|станц\w*|вещ\w*)|\bуси?лит\w*\b",

                    @"\bотде\w*\s*связ\w*\b|\bпочт\w*\b|\bв\sсвязи\b|теплоснабжения|\bмагазин\w*\b",
                    @"\bсвязи\w*\b",

                    @"",
                    @"\bантен\w*\sпол\w*\b|блок\w*(-станц\w*)?[-\s]проводн\w*\s*вещан\w*\b",

                    @"",
                    @"\bнуп\b|\bдля\s*приемного\s*центра\b|\bбаз\w*\b.*\bррс\b",

                    @"",
                    @"\bрадиолокацион\w*\s*станц\w*\b" }
               ));

            nodes.Add(new Industry("6.9.0", "6.9", "3001", "300",

               "Размещение сооружений, имеющих назначение по временному хранению, " +
               "распределению и перевалке грузов (за исключением хранения стратегических " +
               "запасов), не являющихся частями производственных комплексов, на которых " +
               "был создан груз: промышленные базы, склады, погрузочные терминалы " +
               "и доки, нефтехранилища и нефтеналивные станции, газовые хранилища" +
               " и обслуживающие их газоконденсатные и газоперекачивающие станции, " +
               "элеваторы и продовольственные склады, за исключением железнодорожных " +
               "перевалочных складов ",

               "Склады",
               "",
               "",
               new string[] {
                   @"\bавтотранспорт\w*\b|\bсел\w*\s*хоз\w*\b|\bрастен\w*\b|\bпротиволол[её]\w*\b|"+
                   @"\bсемян\w*\b",
                   @"\bсклад\w*\b",

                   @"пассажирских|москва-сити|платежных\sтерминалов",
                   @"\bтерминал\w*\b|\bдок\w{0,5}\b|\bэлеватор\w*\b|" +
                   @"\bнефт\w*[\s\-]*хранили\w*\b|\bгаз\w*[\s\-]*хранили\w*\b",

                   @"\b(авто)?транспортн\w*\s*цех\w*\b|\b(авто)ремонт\w*\b.*\bцех\w*\b",
                   @"\bтар\w*.*\bцех\w*\b",

                   @"лыжн|базар|автомобильной|\bсклад\w*\b",
                   @"\bпродовольств\w*\s*баз\w{0,2}\b|\b(под)?комплектов\w*\s*баз\w{0,2}\b|" +
                   @"\bразмещен\w*\s*материал\w*\b|\bбаз\w{0,2}\s*(комплектац\w*|по\s*обеспеч\w*)\b",

                   @"\bремонт\w*\b|\bпомещений\sмагазина\sи\sхолодильного\sоборудования",
                   @"\bхолодиль\w*\b",

                   @"",
                   @"\bремонт\w*[-\s]*(строительн\w*\s*)?баз\w*\b",

                   @"",
                   @"\bторг\w*[-\s]*проиводствен\w*\b|\bпроиводствен\w*[-\s]*торг\w*\b",

                   @"",
                   @"\b(материал\w*[-\s]*технич\w*[-\s]*)снабж\w*\b",

                   @"",
                   @"\b(авто)?транспортн\w*\s*цех\w*\b|\b(авто)ремонт\w*\b.*\bцех\w*\b",

                   @"\bнауч\w*\b|\bперераб\w*\s*мяс\w*\b|\bлаборатор\w*\b|\bучебн\w*\b|"+
                   @"\bаварий\w*\b|\bстоян\w*\s*(строит\w*\s*техн\w*|убороч\w*\s*трактор\w*)\b",
                   @"\bпроизводств\w*[-\s]*(технич\w*\s*)?баз\w*\b",

                   @"",
                   @"\bремонт\w*[-\s]*строит\w*\s*баз\w*\b",  // < ===Excel here 

                   @"",
                   @"\bбаз\w*\b.*\bстроит\w*\s*\bмпсм\b|\bздан\w*\s*стройгруппы\b",

                   @"",
                   @"\bстроит\w*\s*баз\w*\b|\bстройдвор\w*\b|\bхозблок\w*\b",

                   @"",
                   @"\bпескобаз\w*\b|\bхозблок\w*\b|\bлогистич\w*\s*центр\w*\b",

                   @"",
                   @"\bматериал\w*[-\s]*техническ\w*\s*баз\w*\b",

                   @"",
                   @"\bнефтебаз\w*\b|\bбаз\w*\s*(сантех\w*|строй\w*)\s*матер\w*\b",

                   @"",
                   @"\bмазут\w*[-\s]*(хранилищ\w*|хозяйств\w*)\b|\bбаз\w*\s*спецорганизац\w*\b",

                   @"",
                   @"^под\s*базу$|\bремотн\w*[-\s]*проктн\w*\s*баз\w*\b",

                   @"",
                   @"\bдля\s*разгрузоч\w*[-\s]*погрузоч\w*\b|\bпромтовар\w*\s*баз\w*\b",

                   @"",
                   @"\bхладокомбинат\w*\b",

                   @"",
                   @"\bбаз\w*\s*механизац\w*\b" }
               ));

            nodes.Add(new Industry("6.10.0", "6.10", "3003", "300",

               "Размещение космодромов, стартовых комплексов и пусковых установок, " +
               "командно-измерительных комплексов, центров и пунктов управления " +
               "полетами космических объектов, пунктов приема, хранения и переработки " +
               "информации, баз хранения космической техники, полигонов приземления " +
               "космических объектов, объектов экспериментальной базы для отработки " +
               "космической техники, центров и оборудования для подготовки космонавтов, " +
               "других сооружений, используемых при " +
               "осуществлении космической деятельности ",

               "Обеспечение космической деятельности",
               "",
               "",
               new string[] {
                   @"",
                   @"\bобъект\w*\b.*\bпрограм\w*\s*космич\w*\s*тематик\w*\b" }
               ));

            nodes.Add(new Industry("6.11.0", "6.11", "3002", "300",

               "Размещение объектов капитального строительства, " +
               "предназначенных для целлюлозно-бумажного производства, " +
               "производства целлюлозы, древесной массы, бумаги, картона и " +
               "изделий из них, издательской и полиграфической деятельности, " +
               "тиражирования записанных носителей информации ",

               "Целлюлозно-бумажная промышленность",
               "",
               "",
               new string[] {
                    @"",
                    @"\bиздат\w*\b|\bтипограф\w*\b",

                    @"",
                    @"\bце[л]{1,2}юл\w*\s*производ\w*\b|\bбума\w*\s*производ\w*\b|\bполиграф\w*\b",

                    @"",
                    @"\bкартонаж\w*\b|\bвыпуск\w*\s*упаковоч\w*\s*тар\w*\b"}
               ));

            nodes.Add(new BaseTransport("", "7.0", "", "600",

               "Размещение различного рода путей сообщения и сооружений, " +
               "используемых для перевозки людей или грузов, либо передачи веществ. " +
               "Содержание данного вида разрешенного использования включает в себя " +
               "содержание видов разрешенного использования с кодами 7.1 -7.5 ",

               "Транспорт",
               "",
               "",
               new string[] { }));

            nodes.Add(new Transport("7.1.1", "7.1", "6000", "600",

               "Размещение железнодорожных путей ",

               "Железнодорожный транспорт",
               "",
               "",
               new string[] {
                    @"\bкасс\w*\b",
                    @"\bжел[.]?\w*[-\s]*дор[.]?\w*\b\s*(пут\w*|веток|ветк\w*)\b",

                    @"",
                    @"\bжел[.]?\w*[-\s]*дор[.]?\w*\b\s*подъ?ездн\w*\s*пут\w*\b",

                    @"",
                    @"\bэксплуатац\w*\s*и\s*(развит\w*|обслуживан\w*)\s*железн\w*\s*дорог\w*\b",

                    @"",
                    @"\bотделен\w*\s*железн\w*\s*дорог\w*\b|\bоктябрьск\w*\s*железн\w*\s*дорог\w*\b",

                    @"",
                    @"\bэксплуатац\w*\s*железн\w*\s*дорог\w*\b|\bполос\w*\s*отвод\w*\s*железн\w*\s*дорог\w*\b",

                    @"",
                    @"\bмжд\b" }
               ));

            nodes.Add(new Transport("7.1.2", "7.1", "3005", "333",

               "Размещение, зданий и сооружений, в том числе железнодорожных вокзалов " +
               "и станций, а также устройств и объектов, необходимых для эксплуатации, " +
               "содержания, строительства, реконструкции, ремонта наземных и " +
               "подземных зданий, сооружений, устройств и других объектов " +
               "железнодорожного транспорта; размещение погрузочно-разгрузочных " +
               "площадок, прирельсовых складов (за исключением складов горюче-смазочных " +
               "материалов и автозаправочных станций любых типов, а также складов, " +
               "предназначенных для хранения опасных веществ и материалов, не " +
               "предназначенных непосредственно для обеспечения железнодорожных " +
               "перевозок) и иных объектов при условии соблюдения требований " +
               "безопасности движения, установленных федеральными законами; " +
               "размещение наземных сооружений метрополитена, в том числе " +
               "посадочных станций, вентиляционных шахт и т.п.; " +
               "размещение наземных сооружений для трамвайного сообщения и иных " +
               "специальных дорог(канатных, монорельсовых, фуникулеров) ",

               "Железнодорожный транспорт",
               "",
               "",
               new string[] {
                    @"остановочно-торгового|\bводн\w*\b|\bпочт\w*\b|речных\sпортов",
                    @"\bвокзал\w*\b",

                    @"\bметров\b",
                    @"\b(метро\w*|железно\w*|трамв\w*)[-\s]*(станц\w*\b)|" +
                    @"\b(станц\w*)[-\s]*(метро\w*|железно\w*|трамв\w*)\b",

                    @"дома\sкультуры|\bпут\w{0,3}\b|железнодор\w*\sвет\w{0,3}|временного",
                    @"\bтягов\w*\s*станц\w*\b",

                    @"",
                    @"\bобъкт\w*\s*железнодорож\w*\s*транспорт\w*|" +
                    @"\bканатн\w*\b|\bмонорел\w*\b|\bфуникул\w*\b",

                    @"",
                    @"\bтпп\s*станц\w*\b",

                    @"",
                    @"\b(электро)?тягов\w*\s*(под)?станц\w*\b",

                    @"\bбез\s*прав\w*\s*строит\w*\s*в\s*технич\w*\s*зон\w*\s*метро\w*\b|\bдо\s*станц\w*\s*метро\b",
                    @"\bметро\w*\b|\bвнешне\w*\s*транспорт\w*\b",

                    @"\bэлектро\w*\b",
                    @"\bэксплуатац\w*\s*опор\w*\s*№",

                    @"",
                    @"\bустройст\w*\s*транспорт\w*\b",

                    @"",
                    @"\bзамоскворец\w*\b|\bэлектродепо\b",

                    @"",
                    @"\bпомещен\w*\s*дежурн\w*\s*по\s*переезд\w*\b|\bстрелочн\w*\s*пост\w*\b",

                    @"",
                    @"\bподъез\w*[-\s]*(пут\w*|дорог\w*)\b" }
               ));

            nodes.Add(new Transport("7.2.1", "7.2", "5000", "500",

               "Размещение автомобильных дорог и технически связанных с " +
               "ними сооружений; размещение зданий и сооружений, предназначенных " +
               "для обслуживания пассажиров, а также обеспечивающих работу " +
               "транспортных средств, размещение объектов, предназначенных для " +
               "размещения постов органов внутренних дел, ответственных за " +
               "безопасность дорожного движения ",

               "Автомобильный транспорт",
               "",
               "",
               new string[] {
                    @"мини-автовокзала",
                    @"\bавтовокз\w*\b|\b(автобус\w*|троллей?б\w*)[-\s]*(станц\w*|останов\w*)\b|" +
                    @"станц\w*[-\s](автобус\w*|троллеб\w*)\b",

                    @"",
                    @"\b(автобус\w*|троллей?б\w*).*\bпарк\w*\b",

                    @"",
                    @"\bобслуживан\w*\s*пассажир\w*\b|\bостановоч\w*\s*(модул\w*|комплекс\w*\b)\b",

                    @"",
                    @"\bдорож\w*[-\s]эксплуатацион\w*\b|\bавтотранспортн\w*\s*перевоз\w*\b",

                    @"",
                    @"\bтранспортн\w*\s*подстанц\w*\s*электро\w*\b",

                    @"",
                    @"\bтранспорт\w*\s*хозяйст\w*\b|\bтранспорт\w*\s*тоннел\w*\b",

                    @"",
                    @"\bавтомобил\w*\s*дорог\w*\b|\bустройств\w*\s*транспорт\w*\b",

                    @"",
                    @"\b(под|над?)земн\w*\s*перех\w*\b|\bавтомагистр\w*\b",

                    @"",
                    @"\bремонт\w*.*\bгородск\w*\s*транспорт\w*\b",

                    @"",
                    @"\bпункт\w*\s*контрол\w*\s*большегруз\w*\s*автотранспорт\w*\b",

                    @"",
                    @"\bмини[-\s]*автовокзал\w*\b|\bплощадк\w*\s*досмотр\w*\s*автотраспорт\w*\b",

                    @"",
                    @"\bплощадк\w*\s*для\s*кратковремен\w*\s*останов\w*\b",

                    @"",
                    @"\bпост\w*\s*эко\w*\sконтрол\w*\s*автотранспорт\w*\b|\bопор\w*\s*мостовог\w*\s*переход\w*\b",

                    @"",
                    @"\bпункт\w*\s*диагностик\w*\s*и\s*регулир\w*\s*автотранспорт\w*\b",

                    @"",
                    @"\bжелезобетон\w*\s*эстакад\w*\b" }
               ));

            nodes.Add(new Transport("7.2.2", "7.2", "3001", "333",

               "Оборудование земельных участков для стоянок автомобильного " +
               "транспорта, а также для размещения депо (устройства мест стоянок " +
               "автомобильного транспорта, осуществляющего перевозки людей по " +
               "установленному маршруту) ",

               "Автомобильный транспорт",
               "",
               "",
               new string[] {
                    @"",
                    @"\bавтобус\w*\s*парк\w*\b|\bтролл[еий]+бус\w*\s*парк\w*\b",

                    @"на\sпериод|разгрузочной\sплощадки\sмагазина|марьинский\sпарк|" +
                    @"скорост\w*\sвнеуличн\w*\sтранспорт\w*",
                    @"\bотсто\w*[\s\-]*разворот\w*\b|\bкон\w*\s*станц\w*\b",

                    @"",
                    @"\bназемн\w*\s*обществ\w*\s*транспорт\w*\b|\bпассажир\w*\s*терминал\w*\b",

                    @"",
                    @"\bавтобус\w*\s*стоян\w*\b|\bтроллейб\w*\s*стоян\w*\b|\bтрамва\w*\s*стоян\w*\b" +
                    @"\bстоян\w*\s*автобус\w*\b|\bстоян\w*\s*троллейб\w*\b|\bстоян\w*\s*трамва\w*\b",

                    @"\bмжд\b",
                    @"\bотстойно-разворотн\w*\b|\bдепо\w*\b|\bавтобаз\w*\b",

                    @"",
                    @"\bотст\w*\s*(авто|троллей\w*s*)?транспор\w*\b|\bавтопарк\w*\b",

                    @"",
                    @"\bотстой\w*\b.*\bтранспорт\w*\b",

                    @"",
                    @"(авто)?транспорт\w*\s*предпр\w*\b|" +
                    @"\b(пункт\w*|площадк\w*)\s*((авто)?транспорт\w*|автоб\w*)\b",

                    @"",
                    @"\bсодерж\w*\b.*\b(авто)?транспорт\w*|\bавтопредприят\w*\b",

                    @"",
                    @"\bбаз\w*\s*спец\w*s*перевоз\w*\b",

                    @"",
                    @"\bпункт\w*\s*прибытия\w*\b",

                    @"",
                    @"\bавтокомбинат\w*\b",

                    @"",
                    @"\bавтобаз\w*\b|\bтаксомото\w*\b",

                    @"",
                    @"\bстоян\w*\s*строит\w*\s*техн\w*\b",

                    @"",
                    @"\bпредприят\w*\s*транспорт\w*\b",

                    @"",
                    @"\bобслуживан\w*\b.*\bстроит\w*[-\s]*дорож\w*\s*техник\w*\b",

                    @"",
                    @"\bремонт\w*\s*автотранспорт\w*\b",

                    @"",
                    @"\bбаз\w*\s*механизац\w*\b" }
               ));

            nodes.Add(new Transport("7.3.0", "7.3", "7000", "333",

               "Размещение искусственно созданных для судоходства внутренних " +
               "водных путей, размещение объектов капитального строительства " +
               "водных путей, в том числе морских и речных портов, причалов, " +
               "пристаней, гидротехнических сооружений, навигационного оборудования " +
               "и других объектов, необходимых для обеспечения судоходства и " +
               "водных перевозок ",

               "Водный транспорт",
               "",
               "",
               new string[] {
                    @"",
                    @"\bречн\w*[-\s]*(порт\w*|причал\w*|пристан\w*)|\bгидротехнич\w*\s*сооруж\w*\b",

                    @"",
                    @"\bпричал\w*\b" }
               ));

            nodes.Add(new Transport("7.4.1", "7.4", "6000", "333",

               "Размещение аэродромов, обустройство мест для приводнения и " +
               "причаливания гидросамолетов, размещение радиотехнического " +
               "обеспечения полетов и прочих объектов, необходимых для взлета и " +
               "приземления (приводнения) воздушных судов, размещение аэропортов " +
               "(аэровокзалов) и иных объектов, необходимых для посадки и высадки " +
               "пассажиров и их сопутствующего обслуживания и обеспечения их " +
               "безопасности, а также размещение объектов, необходимых для " +
               "погрузки, разгрузки и хранения грузов, перемещаемых воздушным путем; " +
               "размещение объектов, предназначенных для технического " +
               "обслуживания и ремонта воздушных судов ",

               "Воздушный транспорт",
               "",
               "",
               new string[] {
                    @"овд|гараж",
                    @"\bаэро(вокзал\w*|порт\w*|дром\w*)\b" }
               ));

            nodes.Add(new Transport("7.4.2", "7.4", "3005", "333",

               "Размещение вертолетных площадок (вертодромов) ",

               "Воздушный транспорт",
               "",
               "",
               new string[] {
                    @"",
                    @"\bверт\w*[-\s]*(дром\w*|площад\w*)\b" }
               ));

            nodes.Add(new Transport("7.5.0", "7.5", "3003", "300",

               "Размещение нефтепроводов, водопроводов, газопроводов и иных " +
               "трубопроводов, а также иных зданий и сооружений, необходимых для " +
               "эксплуатации названных трубопроводов) ",

               "Трубопроводный транспорт",
               "",
               "",
               new string[] {
                    @"\bбытово\w*\s*город\w*\b",
                    @"\b(нефте|трубо\w*|газо\w*)[-\s]*провод\w*\b",

                    @"газорегулятор",
                    @"\bзадвиж\w*" }
               ));

            nodes.Add(new SecurityForces("8.0.1", "8.0", "3003", "300",

               "Размещение объектов капитального строительства, необходимых " +
               "для подготовки и поддержания в боевой готовности " +
               "Вооруженных Сил Российской Федерации, других войск, воинских" +
               " формирований и органов управления ими (размещение военных " +
               "организаций, внутренних войск, учреждений и других объектов, " +
               "дислокация войск и сил флота), проведение воинских учений и" +
               " других мероприятий, направленных на обеспечение боевой " +
               "готовности воинских частей ",

               "Обеспечение обороны и безопасности",
               "",
               "",
               new string[] {
                    @"обелиск|спорт\w*\b|госпит\w*\b|захоронения",
                    @"\bвоенком\w*\b|\bво[еи]н\w*\b|\bвойск\w*\b",

                    @"\bгоспит\w*\b",
                    @"\bоборон\w*\b",

                    @"",
                    @"\bв/ч\s*№\b" }
               ));

            nodes.Add(new SecurityForces("8.0.2", "8.0", "1002", "100",

               "Размещение зданий военных училищ, военных институтов, " +
               "военных университетов, военных академий ",

               "Обеспечение обороны и безопасности",
               "",
               "",
               new string[] {
                    @"",
                    @"\bвоен\w*\b\s*\bучилищ\w*\b|\bвоен\w*\b\s*\bакадем\w*\b|" +
                    @"\bвоен\w*\b\s*\bинститут\w*\b|" +
                    @"\bвоен\w*\b\s*\bуниверситет\w*\b" }
               ));

            nodes.Add(new SecurityForces("8.0.3", "8.0", "3003", "300",

               "Размещение объектов, обеспечивающих осуществление " +
               "таможенной деятельности ",

               "Обеспечение обороны и безопасности",
               "",
               "",
               new string[] {
                    @"",
                    @"\bтаможн\w*\b" }
               ));

            nodes.Add(new SecurityForces("8.1.0", "8.1", "3003", "300",

               "Размещение объектов капитального строительства, предназначенных " +
               "для разработки, испытания, производства ремонта или уничтожения " +
               "вооружения, техники военного назначения и боеприпасов; обустройство " +
               "земельных участков в качестве испытательных полигонов, мест " +
               "уничтожения вооружения и захоронения отходов, возникающих в " +
               "связи с использованием, производством, ремонтом или " +
               "уничтожением вооружений или боеприпасов; размещение " +
               "объектов капитального строительства, необходимых для " +
               "создания и хранения запасов материальных ценностей в " +
               "государственном и мобилизационном резервах (хранилища, " +
               "склады и другие объекты); размещение объектов, для обеспечения " +
               "безопасности которых были созданы закрытые " +
               "административно-территориальные образования ",

               "Обеспечение обороны и безопасности",
               "",
               "",
               new string[] {
                    @"",
                    @"\bвооружен\w[^х]\b|\bвоен\w*\s*техник\w*\b|\bбоеприпас\w*\b|" +
                    @"\bиспытат\w*\s*полиг\w*\b|\bхранен\w*\s*запас\w*\b",

                    @"",
                    @"\b(бомбо|газо)убежищ\w*\b" }
               ));

            nodes.Add(new SecurityForces("", "8.2", "", "",

               "Размещение инженерных сооружений и заграждений, " +
               "пограничных знаков, коммуникаций и других объектов, " +
               "необходимых для обеспечения защиты и охраны " +
               "Государственной границы Российской Федерации, устройство " +
               "пограничных просек и контрольных полос, размещение зданий для " +
               "размещения пограничных воинских частей и органов управления ими, " +
               "а также для размещения пунктов пропуска через " +
               "Государственную границу Российской Федерации ",

               "Охрана Государственной границы Российской Федерации",
               "",
               "",
               new string[] { }));

            nodes.Add(new SecurityForces("8.3.0", "8.3", "3003", "300",

               "Размещение объектов капитального строительства, необходимых " +
               "для подготовки и поддержания в готовности органов внутренних дел " +
               "и спасательных служб, в которых существует военизированная служба; " +
               "размещение объектов гражданской обороны, за исключением объектов " +
               "гражданской обороны, являющихся частями производственных зданий ",

               "Обеспечение внутреннего правопорядка",
               "",
               "",
               new string[] {
                    @"",
                    @"\b(по|ми)лиц\w*\b|\bовд\b|\bо?увд\b|\bвв мвд\b|\bпом\b", // пом - поселковое отделение милиции

                    @"",
                    @"\bпожар\w*\s*депо\w*\b|\b(со+руж\w+|об[ъь]?кт\w*)[-\s]*спец\w*[-\s]*назнач\w*\b",

                    @"оздоровительного",
                    @"\bспасат\w*\b|\b(орган\w*|отдел\w*|управл\w*)\s*внутр\w*\s*дел\w*\b|" +
                    @"\bгражданск\w*\s*оборон\w*\b",

                    @"",
                    @"\b(помещен\w*|будк\w*)\s*охран\w*\b|\bкараульн\w*(\s*помещен\w*)?\b|\bВОХР\b",

                    @"",
                    @"\bконтрольн\w*([-\s]*пропускн\w*)?[-\s]*пункт\w*\b",

                    @"",
                    @"\bстанц\w*\s*обеззараживан\w*\s*техник\w*\b|\bказарм\w*\b",

                    @"",
                    @"\bо?гибдд\b|\bгаи\b",

                    @"",
                    @"\bбюр\w*\s*пропуск\w*\b",

                    @"",
                    @"\bпост\w*\s*\bдпс\b" }
               ));

            nodes.Add(new SecurityForces("8.4.0", "8.4", "3003", "300",

               "Размещение объектов капитального строительства для создания " +
               "мест лишения свободы (следственные изоляторы, тюрьмы, поселения) ",

               "Обеспечение деятельности по исполнению наказаний",
               "",
               "",
               new string[] {
                    @"\bмедицин\w*\b",
                    @"\bисправит\w*\b|\bмест\w+\s+лишен\w*\s*свобод\w*\b|" +
                    @"\bизолятор\w*\b|\bтюрьм\w*\b|\bисправит\w*\s*\bколон\w*\b",

                    @"",
                    @"\bгауптвахт\w*\b" }
               ));

            nodes.Add(new Environment("9.0.0", "9.0", "4002", "400",

               "Сохранение и изучение растительного и животного мира путем " +
               "создания особо охраняемых природных территорий, в границах " +
               "которых хозяйственная деятельность, кроме деятельности, " +
               "связанной с охраной и изучением природы, не допускается " +
               "(государственные природные заповедники, национальные и " +
               "природные парки, памятники природы, дендрологические парки, " +
               "ботанические сады) ",

               "Деятельность по особой охране и изучению природы",
               "",
               "",
               new string[] {
                    @"филевский|марьинский|метро|временной|троллей\w*\b|" +
                    @"автобус\w*\b|хуамин|на\sпериод|\bтаксомотор\w*\b|" +
                    @"парка\sрезервуаров|мэлз|с\sтерритории\sпарка|технологических|" +
                    @"автомобильных\sдорог|выводимых\sс\sтерритории|" +
                    @"культурно-просветительских\sобъектов",
                    @"\s\b(лесо)?парк(а|ов|и|ами)?\b",

                    @"",
                    @"\bприродоо?хран\w*\b|\bособо[-\s]*охран\w*\s*терр\w*\b",

                    @"",
                    @"\bлесные\sземли\b" }
               ));

            nodes.Add(new Environment("9.1.0", "9.1", "4000", "400",

               "Сохранение отдельных естественных качеств окружающей природной " +
               "среды путем ограничения хозяйственной деятельности в данной зоне, " +
               "в частности: создание и уход за запретными полосами, создание и " +
               "уход за защитными лесами, в том числе городскими лесами, " +
               "лесами в лесопарках, и иная хозяйственная деятельность, " +
               "разрешенная в защитных лесах, соблюдение режима использования " +
               "природных ресурсов в заказниках, сохранение свойств земель, " +
               "являющихся особо ценными ",

               "Охрана природных территорий",
               "",
               "",
               new string[] { }
               ));

            nodes.Add(new Environment("9.2.0", "9.2", "4002", "400",

               "Использование, в том числе с их извлечением, для лечения и " +
               "оздоровления человека природных лечебных ресурсов (месторождения " +
               "минеральных вод, лечебные грязи, рапой лиманов и озер, особый климат и " +
               "иные природные факторы и условия, которые используются или могут " +
               "использоваться для профилактики и лечения заболеваний человека), а " +
               "также охрана лечебных ресурсов от истощения и уничтожения в границах " +
               "первой зоны округа горно-санитарной или санитарной охраны " +
               "лечебно-оздоровительных местностей и курорта ",

               "Курортная деятельность",
               "",
               "",
               new string[] { }));

            nodes.Add(new Environment("9.2.1.0", "9.2.1", "1005", "100",

               "Размещение санаториев и профилакториев, обеспечивающих оказание " +
               "услуги по лечению и оздоровлению населения; обустройство " +
               "лечебно-оздоровительных местностей (пляжи, бюветы, места добычи " +
               "целебной грязи); размещение лечебно-оздоровительных лагерей ",

               "Санаторная деятельность",
               "",
               "",
               new string[] {
                    @"",
                    @"\bпрофилактор\w*\b|\bсанатор\w*\b|\bбювет\w*\b|\bлечеб\w*[-\s]*профилак\w*\b|\bпляж\w*\b",

                    @"\bфизкульт\w*\b|спорт",
                    @"\bздравниц\w*\b|\bоздоровит\w*\s*(комплекс\w*|лагер\w*)\b|" +
                    @"\bоздоровит\w*[-\s]*восстановит\w*\s*комплекс\w*\b",

                    @"\bфизкульт\w*\b|спорт",
                    @"\bкультурн\w*[-\s]*оздоровит\w*\b",

                    @"\bфизкульт\w*\b|спорт",
                    @"\bпрофиалкт\w*\s*оздоровит\w*|\bоздоровит\w*\s*цел\w*\b|\bлечебн\w*[-\s]*оздоровит\w*\b" }
               ));

            nodes.Add(new Environment("9.3.0", "9.3", "9000", "900",

               "Сохранение и изучение объектов культурного наследия народов " +
               "Российской Федерации (памятников истории и культуры), " +
               "в том числе: объектов археологического наследия, достопримечательных " +
               "мест, мест бытования исторических промыслов, производств и " +
               "ремесел, недействующих военных и гражданских захоронений, " +
               "объектов культурного наследия, хозяйственная деятельность, " +
               "являющаяся историческим промыслом или ремеслом, а также " +
               "хозяйственная деятельность, обеспечивающая познавательный туризм ",

               "Историко-культурная деятельность",
               "",
               "",
               new string[] {
                    @"природ|\bнадгробн\w*\b",
                    @"\bпамятник\w*\b|\bусадьб\w*\b",

                    @"",
                    @"\bобъект\w*\s*культурн\w*\s*наслед\w*\b",

                    @"",
                    @"\bистор\w*[-\s]*культур\w*\b"}
               ));

            nodes.Add(new BaseForestry("", "10.0", "", "",

               "Деятельность по заготовке, первичной обработке и вывозу " +
               "древесины и недревесных лесных ресурсов, охрана и восстановление " +
               "лесов и иные цели. Содержание данного вида разрешенного " +
               "использования включает в себя содержание видов разрешенного " +
               "использования с кодами 10.1-10.5 ",

               "Использование лесов",
               "",
               "",
               new string[] { }));

            nodes.Add(new Forestry("10.1.0", "10.1", "3002", "300",

               "Рубка лесных насаждений, выросших в природных условиях, в " +
               "том числе гражданами для собственных нужд, частичная переработка, " +
               "хранение и вывоз древесины, создание лесных дорог, размещение " +
               "сооружений, необходимых для обработки и хранения древесины " +
               "(лесных складов, лесопилен), охрана и восстановление лесов ",

               "Заготовка древесины",
               "",
               "",
               new string[] {
                    @"",
                    @"\bцех\w*\b.*\b(пере|об)раб\w*\s*древес\w*\b|\bдеревоо?брабатыв\w*\b|\bлесоцех\w*\b" }
               ));

            nodes.Add(new Forestry("10.2.0", "10.2", "3002", "300",

               "Выращивание и рубка лесных насаждений, выращенных трудом " +
               "человека, частичная переработка, хранение и вывоз древесины, " +
               "создание дорог, размещение сооружений, необходимых для " +
               "обработки и хранения древесины " +
               "(лесных складов, лесопилен), охрана лесов ",

               "Заготовка древесины",
               "",
               "",
               new string[] {
                    @"",
                    @"\bцех\w*\b.*\b(пере|об)раб\w*\s*древес\w*\b|\bдеревоо?брабатыв\w*\b|\bлесоцех\w*\b",

                    @"",
                    @"\bлесн\w*\s*хозяйств\w*"}
               ));

            nodes.Add(new Forestry("10.3.0", "10.3", "3006", "800",

               "Выращивание и рубка лесных насаждений, выращенных трудом " +
               "человека, частичная переработка, хранение и вывоз древесины, " +
               "создание дорог, размещение сооружений, необходимых для " +
               "обработки и хранения древесины " +
               "(лесных складов, лесопилен), охрана лесов "

               , "Заготовка древесины"
               , ""
               , ""
               , new string[] {
                    @"",
                    @"\bзаготов\w*\s*древес\w*\b" }
               ));

            nodes.Add(new Forestry("10.3.0", "10.3", "3006", "800",

               "Заготовка живицы, сбор недревесных лесных ресурсов, в том " +
               "числе гражданами для собственных нужд, заготовка пищевых лесных " +
               "ресурсов и дикорастущих растений, хранение, неглубокая " +
               "переработка и вывоз добытых лесных ресурсов, размещение " +
               "временных сооружений, необходимых для хранения и неглубокой " +
               "переработки лесных ресурсов " +
               "(сушилки, грибоварни, склады), охрана лесов ",

               "Заготовка лесных ресурсов",
               "",
               "",
               new string[] { }));

            nodes.Add(new Forestry("", "10.4", "", "",          ///TODO type 440 kind 4400

               "Деятельность, связанная с охраной лесов ",

               "Резервные леса",
               "",
               "",
               new string[] { }));

            nodes.Add(new WaterObjs("11.0.0", "11.0", "7000", "700",

               "Ледники, снежники, ручьи, реки, озера, болота, территориальные " +
               "моря и другие поверхностные водные объекты ",

               "Водные объекты",
               "",
               "",
               new string[] {
                    @"\bканализац\w*\b|\bканализир\w*\b",
                    @"\bканал\w*\b|\bискусствен\w*\bводн\w*\s*(пут\w*)?\b|\bводоем\w*\b",

                    @"спортивно",
                    @"\bруч(ей|ьи|[её]в)\b|\bрек[иа]?\b|\bозер\b|\bболот\b",

                    @"",
                    @"\bпокрыт\w*\s*вод\w*\b",

                    @"",
                    @"\bземельн\w*\s*участ\w*\b.*\b(водоем\w*|водн\w*\s*объект\w*)\b" }
               ));

            nodes.Add(new WaterObjs("11.1.0", "11.1", "4001", "400",

               "Использование земельных участков, примыкающих к водным объектам " +
               "способами, необходимыми для осуществления общего " +
               "водопользования (водопользования, осуществляемого гражданами " +
               "для личных нужд, а также забор (изъятие) водных ресурсов для " +
               "целей питьевого и хозяйственно-бытового водоснабжения, " +
               "купание, использование маломерных судов, водных мотоциклов и " +
               "других технических средств, предназначенных для отдыха на " +
               "водных объектах, водопой, если соответствующие запреты не " +
               "установлены законодательством) ",

               "Общее пользование водными объектами",
               "",
               "",
               new string[] { }));

            nodes.Add(new WaterObjs("11.2.0", "11.2", "3003", "300",

               "Использование земельных участков, примыкающих к водным " +
               "объектам способами, необходимыми для специального " +
               "водопользования (забор водных ресурсов из поверхностных " +
               "водных объектов, сброс сточных вод и (или) дренажных вод, " +
               "проведение дноуглубительных, взрывных, буровых и других работ, " +
               "связанных с изменением дна и берегов водных объектов) ",

               "Специальное пользование водными объектами",
               "",
               "",
               new string[] { }));

            nodes.Add(new WaterObjs("11.3.0", "11.3", "3003", "300",

               "Размещение гидротехнических сооружений, необходимых для " +
               "эксплуатации водохранилищ (плотин, водосбросов, водозаборных, " +
               "водовыпускных и других гидротехнических сооружений, судопропускных " +
               "сооружений, рыбозащитных и рыбопропускных сооружений, " +
               "берегозащитных сооружений) ",

               "Гидротехнические сооружения",
               "",
               "",
               new string[] {
                    @"",
                    @"\bгидротехнич\w+\s*сооружен\b|\bплотин\w*\b|\bберегозащит\w*\b|\bводохран\w*\b" +
                    @"\bрыбо(защит\w*|пропуск\w*)|\bсудопропуск\w*\b|\bводо(сброс\w*|выпуск\w*)\b" } // был водозабор, мешался с комуналкой
               ));


            nodes.Add(new Infrastructure("12.0.1", "12.0", "4001", "400",

               "Размещение береговых полос водных объектов общего пользования, " +
               "скверов, бульваров, парков, садов, велодорожек и объектов " +
               "велотранспортной инфраструктуры ",

               "Земельные участки (территории) общего пользования",
               "",
               "",
               new string[] {
                    @"",
                    @"(?<=\s)(вело\w*)\s*доро\w*(?=\s)",

                    @"",
                    @"\s\bобъект\w*\s*общег\w*\s*пользован\w*\b",

                    @"",
                    @"\bдетск\w*\s*площад\w*\b|\bпрогулоч\w*\s*площад\w*\b",    // ???

                    @"\bавтомобильн\w*\s*дорог\w*\b",
                    @"\bобще\w*\s*и?с?пользован\w*\b|\bблагоустр\w*\b",

                    @"",
                    @"\b(участ\w*\s*)?озеленен\w*\b|\bозеленен\w*\s*терр?итор\w*\b",

                    @"",
                    @"\bдля\s*посад\w*\s*дерев\w*\b|\bдревес\w*[-\s]*кустарн\w*\s*растит\w*\b",

                    @"\bсретенский\s*бульвар\b",
                    @"\bзем\w*\s*участ\w*\b.*город\w*\s*сквер\w*\b|\sбульвар\w*\b",

                    @"",
                    @"\bдворов\w*\s*терр?итор\w*\b" } // ???
               ));

            nodes.Add(new Infrastructure("12.0.2", "12.0", "5000", "500",

               "Размещение объектов улично-дорожной сети: проездов, площадей,  " +
               "автомобильных дорог и пешеходных тротуаров, пешеходных " +
               "переходов, набережных, искусственных сооружений, велодорожек " +
               "и объектов велотранспортной инфраструктуры ",

               "Земельные участки (территории) общего пользования",
               "",
               "",
               new string[] {
                    @"",
                    @"\bулич\w*[-\s]*дорожн\w*[-\s]*сет\w*\b",

                    @"билет|абонемент",
                    @"\bпроезд\w*\b|\bпешеходн\w*\s*переход\w*\b|\bнабережн\w*\b|\bвелодорож\w*\b",

                    @"\b(электро)?тягов\w*\s*(под)?станц\w*\b",
                    @"\bтранспортн\w*\s*инфраструкт\w*\b",

                    @"\bс\s*улиц\w*\b",
                    @"\bулиц\w*\b|\bпроспект\w*\b",

                    @"",
                    @"\bдорож\w*[-\s]*транспорт\w*\s*(сет\w*)?\b",

                    @"",
                    @"\bазовской\s*ул",

                    @"",
                    @"\b(строит\w*|автомоб\w*|проклад\w*)\s*дорог\w*\b|\bтупик\w*\b",

                    @"",
                    @"\bкольц\w*\s*(авто)?магистр\w*\b|\bавтодорог\w*\b",

                    @"",
                    @"\bпод\s*дороги\b",

                    @"",
                    @"\bмост\w*\b",

                    @"",
                    @"\b(организац\w*|обеспеч\w*|размещен\w*)\s*((в|под)ъезд\w*|(пешеход\w*\s*)?подход\w*)",

                    @"",
                    @"\bразмещен\w*\s*дорожн\w*[-\s]*транспорт\w*\s*сет\w*\b",

                    @"",
                    @"\bстроит\w*\s*пожарн\w*\s*дорог\w*\b" }
               ));

            nodes.Add(new Infrastructure("12.1.0", "12.1", "3003", "300",

               "Размещение кладбищ, крематориев и мест захоронения; " +
               "размещение соответствующих культовых сооружений ",

               "Ритуальная деятельность",
               "",
               "",
               new string[] {
                    @"павильон|автостоянк|продаж|магазин",
                    @"\bкладбищ\w*\b|\bкрематор\w*\b|\bмест\w*\s*захоронен\w*\b|\bколумбар\w*\b|\bритуал\w*\b",

                    @"",
                    @"\bмест\w*\s*погребен\w*\b|\bусыпальниц\w*\b",

                    @"",
                    @"\bгранитн\w*\s*мастерск\w*\b|\bнадгробн\w*\s*(памятн\w*|сооружен\w*)\b" }
               ));

            nodes.Add(new Infrastructure("12.2.0", "12.2", "3003", "300",

               "Размещение, хранение, захоронение, утилизация, накопление, " +
               "обработка, обезвреживание отходов производства и потребления, " +
               "медицинских отходов, биологических отходов, радиоактивных " +
               "отходов, веществ, разрушающих озоновый слой, а также " +
               "размещение объектов размещения отходов, захоронения, " +
               "хранения, обезвреживания таких отходов (скотомогильников, " +
               "мусоросжигательных и мусороперерабатывающих заводов, " +
               "полигонов по захоронению и сортировке бытового мусора и " +
               "отходов, мест сбора вещей для их вторичной переработки ",

               "Специальная деятельность",
               "",
               "",
               new string[] {
                    @"мусоросборников|\b\w*транспорт|контейнер",
                    @"\bмусор\w*(сжигат\w*|перегрузоч\w*|перерабат\w*)\b|" +
                    @"\bпереработ\w*\b.+\bмусор\w*\b",

                    @"",
                    @"\bполигон\w*\s*(\bтбо\b|.*\bотход\w*\b)",

                    @"",
                    @"\bскотомогиль\w*\b",

                    @"",
                    @"(\bпри[её]\w*\b\s*)?\bвтор\w*[-\s]*сырь\w*\b",

                    @"",
                    @"\bвывоз\w*\s*тв[её]рд\w*\s*бытов\w*\s*отход\w*\b",

                    @"",
                    @"\bбрикетиров\w*\b.*\bмусор\w*\b",

                    @"",
                    @"\b(ре)?культивац\w*\s*деградирован\w*\s*зем\w*\b",

                    @"",
                    @"\bнавозоприемн\w*\b|\bстеклобо\w*\b|\bпереработ\w*\s*мукулатур\w*\b",

                    @"",
                    @"\bпереработ\w*\b.*\bразукомплектова\w*\s*(авто)?транспорт\w*\b",

                    @"",
                    @"\bперераб\w*\s*металл?олом\w*\b|\bметалл?обаз\w*\b",

                    @"",
                    @"\bмусоросортировоч\w*\b|\bавторециклинг\w*\b|\bсортивровоч\w*\s*станц\w*\s*ТБО\b",

                    @"",
                    @"\b(замен\w*\s*и\s*сбор\w*|сбор\w*\s*и\s*замен\w*)\b.*\bотработан\w*\s*(авто\w*\s*)?мас\w*\b" }
               ));

            nodes.Add(new OtherFunction("12.3.0", "12.3", "9000", "900",

               "Отсутствие хозяйственной деятельности ",

               "Запас",
               "",
               "",
               new string[] {
                    @"",
                    @"\bотсутств\w*\s*хоз\w*\s*деятнель\w*\b",

                    @"",
                    @"для\sиного\sиспользован",

                    @"",
                    @"не\s*(определ\w*|установл\w*)\b",

                    @"",
                    @"\bгосударствен\w*\s*(нужд\w*|надобно\w*)\b",

                    @"",
                    @"\bслужеб\w*\s*(цел\w*|помещен\w*|здан\w*|корп\w*)\b",

                    @"",
                    @"\bне\s*связан\w*\s*со\s*строит\w*\b",

                    @"",
                    @"\bэксплуатац\w*\s*объект\w*\s*незавершен\w*\s*строит\w*\b",

                    @"",
                    @"\bзем\w*\s*участ\w*\b.*\bограничен\w*\s*в\s*оборот\w*\b" }
               ));

            nodes.Add(new Housing("13.1.0", "13.1", "3006", "800",

               "Осуществление деятельности, связанной с выращиванием " +
               "ягодных, овощных, бахчевых или иных сельскохозяйственных " +
               "культур и картофеля; размещение некапитального жилого " +
               "строения и хозяйственных строений и сооружений, " +
               "предназначенных для хранения сельскохозяйственных орудий " +
               "труда и выращенной сельскохозяйственной продукции ",

               "Ведение огородничества",
               "",
               "",
               new string[] {
                    @"\bжил\w*\s*дом\w*\b|организации отдыха, культурного проведения свободного времени, " +
                    @"укрепления здоровья, а так же для выращивания плодовых, ягодных, овощных и",
                    @"\s\bвыращиван\w*\s.*(\bягод\w*|\bовощ\w*|\bбахчев\w*|\bкартофел\w*)\b|\bогород\w*\b",

                    @"\bжил\w*\s*дом\w*\b",
                    @"\bприусадебн\w*(\s*хоз\w*\b)?",

                    @"\bжил\w*\s*дом\w*\b",
                    @"\bогород\w*\b",

                    @"\bжил\w*\s*дом\w*\b",
                    @"\bприусад[.]?\w*\s*(хозяйств\w*|х-ва|хоз-ва)\b" }
               ));

            nodes.Add(new Housing("13.2.0", "13.2", "2004", "200",

               "Осуществление деятельности, связанной с выращиванием плодовых, " +
               "ягодных, овощных, бахчевых или иных сельскохозяйственных культур " +
               "и картофеля; размещение садового дома, предназначенного для " +
               "отдыха и не подлежащего разделу на квартиры; размещение " +
               "хозяйственных строений и сооружений ",

               "Ведение садоводства",
               "",
               "",
               new string[] {
                    @"\bрынок\w*\b",
                    @"\bс?адовод\w*\b",

                    @"",
                    @"\bсадов\w*[-\s]*огороднич\w*\b|\bсадов\w*\s*(участ\w*|дом\w*)\b",

                    @"",
                    @"\bиндивид\w*\s*сад\w*\b",

                    @"",
                    @"\bдом\w*\s*(облегчен\w*\s*тип\w*|сезон\w*\s*проживан\w*)\b",

                    @"",
                    @"организации отдыха, культурного проведения свободного времени, " +
                    @"укрепления здоровья, а так же для выращивания плодовых, ягодных, овощных" }
               ));

            nodes.Add(new GrowPlanting("13.3.0", "13.3", "2004", "200",

               "Размещение жилого дачного дома (не предназначенного для раздела на " +
               "квартиры, пригодного для отдыха и проживания, высотой не выше трех " +
               "надземных этажей); осуществление деятельности, связанной с " +
               "выращиванием плодовых, ягодных, овощных, бахчевых или иных " +
               "сельскохозяйственных культур и картофеля; размещение хозяйственных " +
               "строений и сооружений ",

               "Ведение дачного хозяйства",
               "",
               "",
               new string[] {
                    @"цех|демонстрационных",
                    @"\bдач\w{0,7}\b",

                    @"",
                    @"\bдом\w*\s*(облегчен\w*\s*тип\w*|сезон\w*\s*проживан\w*)\b",

                    @"",
                    @"\bлетн\w*\s*дом\w*\b" }
               ));
        }
    }

    public class CodesMapping
    {
        public CodesMapping()
        {
            Map = new Dictionary<string, List<string>>
            {
                { "1.0",     new List<string> { "1.0.0" } },
                { "1.1",     new List<string> { "1.1.0" } },
                { "1.2",     new List<string> { "1.2.0" } },
                { "1.3",     new List<string> { "1.3.0" } },
                { "1.4",     new List<string> { "1.4.0" } },
                { "1.5",     new List<string> { "1.5.0" } },
                { "1.6",     new List<string> { "1.6.0" } },
                { "1.7",     new List<string> { "1.7.0" } },
                { "1.8",     new List<string> { "1.8.0" } },
                { "1.9",     new List<string> { "1.9.0" } },
                { "1.10",    new List<string> { "1.10.0" } },
                { "1.11",    new List<string> { "1.11.0" } },
                { "1.12",    new List<string> { "1.12.0" } },
                { "1.13",    new List<string> { "1.13.0" } },
                { "1.14",    new List<string> { "1.14.0" } },
                { "1.15",    new List<string> { "1.15.0" } },
                { "1.16",    new List<string> { "1.16.0" } },
                { "1.17",    new List<string> { "1.17.0" } },
                { "1.18",    new List<string> { "1.18.0" } },
                { "2.0",     new List<string> { "2.0.0" } },
                { "2.1",     new List<string> { "2.1.0" } },
                { "2.1.1",   new List<string> { "2.1.1.0" } },
                { "2.2",     new List<string> { "2.2.0" } },
                { "2.3",     new List<string> { "2.3.0" } },
                { "2.4",     new List<string> { "2.4.0" } },
                { "2.5",     new List<string> { "2.5.0" } },
                { "2.6",     new List<string> { "2.6.0" } },
                { "2.7",     new List<string> { "2.7.0" } },
                { "2.7.1",   new List<string> { "2.7.1.0" } },
                { "3.0",     new List<string> { "3.0.0" } },
                { "3.1",     new List<string> { "3.1.1", "3.1.2", "3.1.3" } }, ///TODO: Приделать механизм для определния типа 100 или 300 
                { "3.2",     new List<string> { "3.2.1", "3.2.2", "3.2.3", "3.2.4" } },
                { "3.3",     new List<string> { "3.3.0" } },
                { "3.4",     new List<string> { "3.4.0" } },
                { "3.4.1",   new List<string> { "3.4.1.0" } },
                { "3.4.2",   new List<string> { "3.4.2.0" } },
                { "3.5",     new List<string> { "3.5.1.0", "3.5.2.0" } },
                { "3.5.1",   new List<string> { "3.5.1.0" } },
                { "3.5.2",   new List<string> { "3.5.2.0" } },
                { "3.6",     new List<string> { "3.6.1", "3.6.2", "3.6.3" } },
                { "3.7",     new List<string> { "3.7.1", "3.7.2" } },
                { "3.8",     new List<string> { "3.8.1", "3.8.2", "3.8.3" } },
                { "3.9",     new List<string> { "3.9.2", "3.9.3", "3.9.4", "3.9.5" } },
                { "3.9.1",   new List<string> { "3.9.1.0" } },
                { "3.10",    new List<string> { "3.10.1.0", "3.10.2.0" } },
                { "3.10.1",    new List<string> { "3.10.1.0" } },
                { "3.10.2",    new List<string> { "3.10.2.0" } },
                { "4.0",     new List<string> { "4.0.0" } },
                { "4.1",     new List<string> { "4.1.0" } },
                { "4.2",     new List<string> { "4.2.0" } },
                { "4.3",     new List<string> { "4.3.0" } },
                { "4.4",     new List<string> { "4.4.0" } },
                { "4.5",     new List<string> { "4.5.0" } },
                { "4.6",     new List<string> { "4.6.0" } },
                { "4.7",     new List<string> { "4.7.1", "4.7.2", "4.7.3" } },
                { "4.8",     new List<string> { "4.8.0" } },
                { "4.9",     new List<string> { "4.9.0" } },
                { "4.9.1",   new List<string> { "4.9.1.1", "4.9.1.2", "4.9.1.3", "4.9.1.4" } },
                { "4.10",    new List<string> { "4.10.0" } },
                { "5.0",     new List<string> { "5.0.1", "5.0.2" } },
                { "5.1",     new List<string> { "5.1.1", "5.1.2", "5.1.3", "5.1.4", "5.1.5" } },
                { "5.2",     new List<string> { "5.2.0" } },
                { "5.2.1",   new List<string> { "5.2.1.0" } },
                { "5.3",     new List<string> { "5.3.0" } },
                { "5.4",     new List<string> { "5.4.0" } },
                { "5.5",     new List<string> { "5.5.0" } },
                { "6.0",     new List<string> { "6.0.0" } },
                { "6.1",     new List<string> { "6.1.0" } },
                { "6.2",     new List<string> { "6.2.0" } },
                { "6.2.1",   new List<string> { "6.2.1.0" } },
                { "6.3",     new List<string> { "6.3.0" } },
                { "6.3.1",   new List<string> { "6.3.1.0" } },
                { "6.4",     new List<string> { "6.4.0" } },
                { "6.5",     new List<string> { "6.5.0" } },
                { "6.6",     new List<string> { "6.6.0" } },
                { "6.7",     new List<string> { "6.7.0" } },
                { "6.7.1",   new List<string> { "6.7.0" } },
                { "6.8",     new List<string> { "6.8.0" } },
                { "6.9",     new List<string> { "6.9.0" } },
                { "6.10",    new List<string> { "6.10.0" } },
                { "6.11",    new List<string> { "6.11.0" } },
                { "7.0",     new List<string> { "7.1.1", "7.1.2", "7.2.1", "7.2.2", "7.3.0", "7.4.1", "7.4.2", "7.5.0"} },                         ///TODO: придумать как проставить тип 777                
                { "7.1",     new List<string> { "7.1.1" } },
                { "7.2",     new List<string> { "7.2.1" } },
                { "7.3",     new List<string> { "7.3.0" } },
                { "7.4",     new List<string> { "7.4.1", "7.4.2" } },
                { "7.5",     new List<string> { "7.5.0" } },
                { "8.0",     new List<string> { "8.0.1", "8.0.2", "8.0.3" } },  ///TODO: Тоже, что и с 3.1.1. Либо 300 либо 100              
                { "8.1",     new List<string> { "8.1.0" } },
                { "8.2",     new List<string> {  } },                           ///TODO: Что-то надо бы придумать.                
                { "8.3",     new List<string> { "8.3.0" } },
                { "8.4",     new List<string> { "8.4.0" } },
                { "9.0",     new List<string> { "9.0.0" } },
                { "9.1",     new List<string> { "9.1.0" } },
                { "9.2",     new List<string> { "9.2.0" } },
                { "9.2.1",   new List<string> { "9.2.1.0" } },
                { "9.3",     new List<string> { "9.3.0" } },
                { "10.0",    new List<string> { "10.1.0", "10.2.0" } },
                { "10.1",    new List<string> { "10.1.0" } },
                { "10.2",    new List<string> { "10.2.0" } },
                { "10.3",    new List<string> { } },                            ///TODO: Index or type or somestnig else               
                { "10.4",    new List<string> { } },                            ///TODO: Same shit.
                { "11.0",    new List<string> { "11.1.0" } },
                { "11.2",    new List<string> { "11.2.0" } },
                { "12.0",    new List<string> { "12.0.1", "12.0.2" } },         ///TODO: like 3.1.1.                 
                { "12.1",    new List<string> { "12.1.0" } },
                { "12.2",    new List<string> { "12.2.0" } },
                { "12.3",    new List<string> { "12.3.0" } },
                { "13.1",    new List<string> { "13.1.0" } },
                { "13.2",    new List<string> { "13.2.0" } },
                { "13.3",    new List<string> { "13.3.0" } }
            };
        }

        public Dictionary<string, List<string>> Map { get; }

        /// <summary>
        /// TODO: AAAAAAAAAAAAAAAAAAAAAAAAAAAAAA
        /// </summary>
        /// <param name="values"></param>
        /// <returns></returns>
        public List<Node> CreateNodes(string[] values)
        {
            var mf = new NodeFeed();
            var result = new List<Node>();
            foreach (var value in values)
            {
                if (value.Equals("7.0"))
                {
                    var node = new BaseTransport("7.0", "", "", "777", "", "", "", "", new string[] { });
                    result.Add(node);
                }
                else if (value.Equals("10.3"))
                {
                    var node = new BaseTransport("10.3", "", "", "300", "", "", "", "", new string[] { });
                    result.Add(node);
                }
                else if (value.Equals("10.4"))
                {
                    var node = new BaseTransport("10.4", "", "", "300", "", "", "", "", new string[] { });
                    result.Add(node);
                }
                else
                    result.Add(mf.getM(value));
            }
            return result;
        }
    }


    /// <summary>
    /// Компоратор для сортировки ВРИ_листа_2 
    /// </summary>
    /// TODO: Какая-то шляпа. Убрать или переделать.
    class VRI_Comparer : IComparer<string>
    {
        int IComparer<string>.Compare(string x, string y)
        {
            var mf = new NodeFeed().GetNodes();
            var intA = mf.FindIndex(p => p.vri.Equals(x));
            var intB = mf.FindIndex(p => p.vri.Equals(y));

            if (intA > intB) return 1;
            if (intA < intB) return -1;
            else return 0;

            throw new NotImplementedException();
        }
    }
}


