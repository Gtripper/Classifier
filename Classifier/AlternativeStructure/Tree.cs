using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    public class Tree
    {

        public Tree() { }
        /// <summary>
        /// This is a tree of codes elements.
        /// </summary>


        public Component CreateTree()
        {
            var mf = new MonsterFeed().getMonster();


            Component root = new Composite("ROOT");
            Component Agriculture = new Composite("Agriculture", mf[0]);
            Component GrowPlanting1 = new Composite("GrowPlanting1", mf[1]);
            Component GrowPlanting2 = new Leaf("GrowPlanting2", mf[2]);
            Component GrowPlanting3 = new Leaf("GrowPlanting3", mf[3]);
            Component GrowPlanting4 = new Leaf("GrowPlanting4", mf[4]);
            Component GrowPlanting5 = new Leaf("GrowPlanting5", mf[5]);
            Component GrowPlanting6 = new Leaf("GrowPlanting6", mf[6]);
            Component LiveStocking1 = new Composite("LiveStocking1", mf[7]);
            Component LiveStocking2 = new Leaf("LiveStocking2", mf[8]);
            Component LiveStocking3 = new Leaf("LiveStocking3", mf[9]);
            Component LiveStocking4 = new Leaf("LiveStocking4", mf[10]);
            Component LiveStocking5 = new Leaf("LiveStocking5", mf[11]);
            Component Agronomy1 = new Leaf("Agronomy1", mf[12]);
            Component Agronomy2 = new Leaf("Agronomy2", mf[13]);
            Component Agronomy3 = new Leaf("Agronomy3", mf[14]);
            Component Agronomy4 = new Leaf("Agronomy4", mf[15]);
            Component Agronomy5 = new Leaf("Agronomy5", mf[16]);
            Component Agronomy6 = new Leaf("Agronomy6", mf[17]);
            Component Agronomy7 = new Leaf("Agronomy7", mf[18]);
            Component Dwelling = new Composite("Dwelling", mf[19]);
            Component Housing1 = new Leaf("Housing1", mf[20]);
            Component Housing2= new Leaf("Housing2", mf[21]);
            Component Housing3 = new Leaf("Housing3", mf[22]);
            Component Housing4 = new Leaf("Housing4", mf[23]);
            Component Housing5 = new Leaf("Housing5", mf[24]);
            Component Housing6 = new Leaf("Housing6", mf[25]);
            Component Housing7 = new Leaf("Housing7", mf[26]);
            Component Housing8 = new Leaf("Housing8", mf[27]);
            Component Housing9 = new Leaf("Housing9", mf[028]);
            Component BaseCommunity = new Composite("BaseCommunity", mf[29]);
            Component Community1 = new Leaf("Community1", mf[30]);
            Component Community2= new Leaf("Community2", mf[31]);
            Component Community3 = new Leaf("Community3", mf[32]);
            Component Community4 = new Leaf("Community4", mf[33]);
            Component Community5 = new Leaf("Community5", mf[34]);
            Component Community6 = new Leaf("Community6", mf[35]);
            Component Community7 = new Leaf("Community7", mf[36]);
            Component Community8 = new Leaf("Community8", mf[37]);
            Component HealthCare = new Composite("HealthCare", mf[38]);
            Component Clinic = new Leaf("Clinic", mf[39]);
            Component Hospital = new Leaf("Hospital", mf[40]);
            Component Education = new Composite("Education", mf[41]);
            Component SchoolEducation = new Leaf("SchoolEducation", mf[42]);
            Component ProfessionalEducation = new Leaf("ProfessionalEducation", mf[43]);
            Component Community9 = new Leaf("Community15", mf[44]);
            Component Community10 = new Leaf("Community16", mf[45]);
            Component Community11 = new Leaf("Community17", mf[46]);
            Component Community12 = new Leaf("Community18", mf[47]);
            Component Community13 = new Leaf("Community19", mf[48]);
            Component Community14 = new Leaf("Community20", mf[49]);
            Component Community15 = new Leaf("Community21", mf[50]);
            Component Community16 = new Leaf("Community22", mf[51]);
            Component Sсience1 = new Leaf("Sсience1", mf[52]);
            Component Sсience2 = new Leaf("Sсience2", mf[53]);
            Component Sсience3 = new Leaf("Sсience3", mf[54]);
            Component Sсience4 = new Leaf("Sсience4", mf[55]);
            Component WeatherStation = new Leaf("WeatherStation", mf[56]);
            Component Veterenary = new Composite("Veterenary", mf[57]);
            Component VetClinic = new Leaf("VetClinic", mf[58]);
            Component AnimalShelter = new Leaf("AnimalShelter", mf[59]);
            Component BaseBuisness = new Composite("BaseBuisness", mf[60]); //!!!!
            Component Buisness1 = new Leaf("Buisness1", mf[61]);
            Component Buisness2 = new Leaf("Buisness2", mf[62]);
            Component Buisness3 = new Leaf("Buisness3", mf[63]);
            Component Buisness4 = new Leaf("Buisness4", mf[64]);
            Component Buisness5 = new Leaf("Buisness5", mf[65]);
            Component Buisness6 = new Leaf("Buisness6", mf[66]);
            Component Hotel = new Composite("Hotel", mf[67]);
            Component Hostel = new Leaf("Hostel", mf[68]);
            Component Dormitory = new Leaf("Dormitory", mf[69]);
            Component Buisness7 = new Leaf("Buisness7", mf[70]);
            Component Buisness8 = new Leaf("Buisness8", mf[71]);
            Component GasStation = new Leaf("GasStation", mf[72]);
            Component Motel = new Leaf("Motel", mf[73]);
            Component CarWashingStation = new Leaf("CarWashingStation", mf[74]);
            Component BodyShop = new Leaf("BodyShop", mf[75]);
            Component Buisness9 = new Leaf("Buisness9", mf[76]);
            Component BaseRecreation = new Composite("BaseRecreation", mf[77]);
            Component Recreation1 = new Leaf("Recreation1", mf[78]);
            Component Sport1 = new Leaf("Sport1", mf[79]);
            Component Sport2 = new Leaf("Sport2", mf[80]);
            Component Sport3 = new Leaf("Sport3", mf[81]);
            Component Sport4 = new Leaf("Sport4", mf[82]);
            Component Sport5 = new Leaf("Sport5", mf[83]);
            Component Recreation2 = new Leaf("Recreation2", mf[84]);
            Component Recreation3 = new Leaf("Recreation3", mf[85]);
            Component Recreation4 = new Leaf("Recreation4", mf[86]);
            Component Recreation5 = new Leaf("Recreation5", mf[87]);
            Component Recreation6 = new Leaf("Recreation6", mf[88]);
            Component BaseIndustry = new Composite("BaseIndustry", mf[89]);
            Component Industry1 = new Leaf("Industry1", mf[90]);
            Component Industry2 = new Leaf("Industry2", mf[91]);
            Component Industry3 = new Leaf("Industry3", mf[92]);
            Component Industry4 = new Leaf("Industry4", mf[93]);
            Component Industry5 = new Leaf("Industry5", mf[94]);
            Component Industry6 = new Leaf("Industry6", mf[95]);
            Component Industry7 = new Leaf("Industry7", mf[96]);
            Component Industry8 = new Leaf("Industry8", mf[97]);
            Component Industry9 = new Leaf("Industry9", mf[98]);
            Component Industry10 = new Leaf("Industry10", mf[99]);
            Component Industry11 = new Leaf("Industry11", mf[100]);
            Component Industry12 = new Leaf("Industry12", mf[101]);
            Component Industry13 = new Leaf("Industry13", mf[102]);
            Component Industry14 = new Leaf("Industry14", mf[103]);
            Component BaseTransport = new Composite("BaseTransport", mf[104]);
            Component Transport1 = new Leaf("Transport1", mf[105]);
            Component Transport2 = new Leaf("Transport2", mf[106]);
            Component Transport3 = new Leaf("Transport3", mf[107]);
            Component Transport4 = new Leaf("Transport4", mf[108]);
            Component Transport5 = new Leaf("Transport5", mf[109]);
            Component Transport6 = new Leaf("Transport6", mf[110]);
            Component Transport7 = new Leaf("Transport7", mf[111]);
            Component Transport8 = new Leaf("Transport8", mf[112]);
            Component SecurityForces1 = new Leaf("SecurityForces1", mf[113]);
            Component SecurityForces2 = new Leaf("SecurityForces2", mf[114]);
            Component SecurityForces3 = new Leaf("SecurityForces3", mf[115]);
            Component SecurityForces4 = new Leaf("SecurityForces4", mf[116]);
            Component SecurityForces5 = new Leaf("SecurityForces5", mf[117]);
            Component SecurityForces6 = new Leaf("SecurityForces6", mf[118]);
            Component SecurityForces7 = new Leaf("SecurityForces7", mf[119]);
            Component Environment1 = new Leaf("Environment1", mf[120]);
            Component Environment2 = new Leaf("Environment2", mf[121]);
            Component Environment3 = new Leaf("Environment3", mf[122]);
            Component Environment4 = new Leaf("Environment4", mf[123]);
            Component Environment5 = new Leaf("Environment5", mf[124]);
            Component BaseForestry = new Composite("BaseForestry", mf[125]);
            Component Forestry1 = new Leaf("Forestry1", mf[126]);
            Component Forestry2 = new Leaf("Forestry2", mf[127]);
            Component Forestry3 = new Leaf("Forestry3", mf[128]);
            Component Forestry4 = new Leaf("Forestry4", mf[129]);
            Component Forestry5 = new Leaf("Forestry5", mf[130]);
            Component WaterObjs1 = new Leaf("WaterObjs1", mf[131]);
            Component WaterObjs2 = new Leaf("WaterObjs2", mf[132]);
            Component WaterObjs3 = new Leaf("WaterObjs3", mf[133]);
            Component WaterObjs4 = new Leaf("WaterObjs4", mf[134]);
            Component Infrastructure1 = new Leaf("Infrastructure1", mf[135]);
            Component Infrastructure2 = new Leaf("Infrastructure2", mf[136]);
            Component Infrastructure3 = new Leaf("Infrastructure3", mf[137]);
            Component Infrastructure4 = new Leaf("Infrastructure4", mf[138]);
            Component OtherFunction = new Leaf("OtherFunction", mf[139]);
            Component Housing10 = new Leaf("Housing10", mf[140]);
            Component Housing11 = new Leaf("Housing11", mf[141]);
            Component GrowPlanting7 = new Leaf("GrowPlanting7", mf[142]);





            root.Add(Agriculture);
            Agriculture.Add(GrowPlanting1);
            GrowPlanting1.Add(GrowPlanting2);
            GrowPlanting1.Add(GrowPlanting3);
            GrowPlanting1.Add(GrowPlanting4);
            GrowPlanting1.Add(GrowPlanting5);
            GrowPlanting1.Add(GrowPlanting6);
            Agriculture.Add(LiveStocking1);
            LiveStocking1.Add(LiveStocking2);
            LiveStocking1.Add(LiveStocking3);
            LiveStocking1.Add(LiveStocking4);
            LiveStocking1.Add(LiveStocking5);
            Agriculture.Add(Agronomy1);
            Agriculture.Add(Agronomy2);
            Agriculture.Add(Agronomy3);
            Agriculture.Add(Agronomy4);
            Agriculture.Add(Agronomy5);
            Agriculture.Add(Agronomy6);
            Agriculture.Add(Agronomy7);
            root.Add(Dwelling);
            Dwelling.Add(Housing1);
            Dwelling.Add(Housing2);
            Dwelling.Add(Housing3);
            Dwelling.Add(Housing4);
            Dwelling.Add(Housing5);
            Dwelling.Add(Housing6);
            Dwelling.Add(Housing7);
            Dwelling.Add(Housing8);
            Dwelling.Add(Housing9);
            root.Add(BaseCommunity);
            BaseCommunity.Add(Community1);
            BaseCommunity.Add(Community2);
            BaseCommunity.Add(Community3);
            BaseCommunity.Add(Community4);
            BaseCommunity.Add(Community5);
            BaseCommunity.Add(Community6);
            BaseCommunity.Add(Community7);
            BaseCommunity.Add(Community8);
            BaseCommunity.Add(HealthCare);
            HealthCare.Add(Clinic);
            HealthCare.Add(Hospital);
            BaseCommunity.Add(Education);
            Education.Add(SchoolEducation);
            Education.Add(ProfessionalEducation);
            BaseCommunity.Add(Community9);
            BaseCommunity.Add(Community10);
            BaseCommunity.Add(Community11);
            BaseCommunity.Add(Community12);
            BaseCommunity.Add(Community13);
            BaseCommunity.Add(Community14);
            BaseCommunity.Add(Community15);
            BaseCommunity.Add(Community16);
            BaseCommunity.Add(Sсience1);
            BaseCommunity.Add(Sсience2);
            BaseCommunity.Add(Sсience3);
            BaseCommunity.Add(Sсience4);
            BaseCommunity.Add(WeatherStation);
            BaseCommunity.Add(Veterenary);
            Veterenary.Add(VetClinic);
            Veterenary.Add(AnimalShelter);
            root.Add(BaseBuisness);
            BaseBuisness.Add(Buisness1);
            BaseBuisness.Add(Buisness2);
            BaseBuisness.Add(Buisness3);
            BaseBuisness.Add(Buisness4);
            BaseBuisness.Add(Buisness5);
            BaseBuisness.Add(Buisness6);
            BaseBuisness.Add(Hotel);
            BaseBuisness.Add(Hostel);
            BaseBuisness.Add(Dormitory);
            BaseBuisness.Add(Buisness7);
            BaseBuisness.Add(Buisness8);
            BaseBuisness.Add(GasStation);
            BaseBuisness.Add(Motel);
            BaseBuisness.Add(CarWashingStation);
            BaseBuisness.Add(BodyShop);
            BaseBuisness.Add(Buisness9);
            root.Add(BaseRecreation);
            BaseRecreation.Add(Recreation1);
            BaseRecreation.Add(Sport1);
            BaseRecreation.Add(Sport2);
            BaseRecreation.Add(Sport3);
            BaseRecreation.Add(Sport4);
            BaseRecreation.Add(Sport5);
            BaseRecreation.Add(Recreation2);
            BaseRecreation.Add(Recreation3);
            BaseRecreation.Add(Recreation4);
            BaseRecreation.Add(Recreation5);
            BaseRecreation.Add(Recreation6);
            root.Add(BaseIndustry);
            BaseIndustry.Add(Industry1);
            BaseIndustry.Add(Industry2);
            BaseIndustry.Add(Industry3);
            BaseIndustry.Add(Industry4);
            BaseIndustry.Add(Industry5);
            BaseIndustry.Add(Industry6);
            BaseIndustry.Add(Industry7);
            BaseIndustry.Add(Industry8);
            BaseIndustry.Add(Industry9);
            BaseIndustry.Add(Industry10);
            BaseIndustry.Add(Industry11);
            BaseIndustry.Add(Industry12);
            BaseIndustry.Add(Industry13);
            BaseIndustry.Add(Industry14);
            root.Add(BaseTransport);
            BaseTransport.Add(Transport1);
            BaseTransport.Add(Transport2);
            BaseTransport.Add(Transport3);
            BaseTransport.Add(Transport4);
            BaseTransport.Add(Transport5);
            BaseTransport.Add(Transport6);
            BaseTransport.Add(Transport7);
            BaseTransport.Add(Transport8);
            root.Add(SecurityForces1);
            root.Add(SecurityForces2);
            root.Add(SecurityForces3);
            root.Add(SecurityForces4);
            root.Add(SecurityForces5);
            root.Add(SecurityForces6);
            root.Add(SecurityForces7);
            root.Add(Environment1);
            root.Add(Environment2);
            root.Add(Environment3);
            root.Add(Environment4);
            root.Add(Environment5);
            root.Add(BaseForestry);
            BaseForestry.Add(Forestry1);
            BaseForestry.Add(Forestry2);
            BaseForestry.Add(Forestry3);
            BaseForestry.Add(Forestry4);
            BaseForestry.Add(Forestry5);
            root.Add(Forestry1);
            root.Add(Forestry2);
            root.Add(Forestry3);
            root.Add(Forestry4);
            root.Add(Infrastructure1);
            root.Add(Infrastructure2);
            root.Add(Infrastructure3);
            root.Add(Infrastructure4);
            Dwelling.Add(Housing10);
            Dwelling.Add(Housing11);
            Agriculture.Add(GrowPlanting7);

            //root.Operation();


            return root;
        }

        public void Operation()
        {
            var root = CreateTree();
            root.Operation();
        }

        public void ListOfMonster()
        {
            var root = CreateTree();
            var list = new List<Monster>();

            list.Add(root.GetMonster());

        }
        
    }
}
