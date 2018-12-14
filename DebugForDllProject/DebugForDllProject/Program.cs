using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




namespace DebugForDllProject
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Halo");
            


            Classifier.MonsterFeed mstr = new Classifier.MonsterFeed();

           

            //foreach (Classifier.Monster it in mstr.getMonster()) {
            //    Console.WriteLine(it.GetVri());
            //}

            Classifier.Class clss = new Classifier.Class();

            string test;

            Classifier.Class MyClass = new Classifier.Class();
            test = MyClass.SubStringSearchSimpleDescript("Ското");

            Console.WriteLine(test);

            Console.ReadKey();
        }
    }
}
