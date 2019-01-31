using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using Microsoft.Office.Interop.Excel;




namespace DBMananger
{
    class Program
    {
        static void Main(string[] args)
        {
            var db = new DBAdapter(@"D:\work\Mapinfo\карты в работе\Классификатор\DB\ПланФакт_сущпол_СВОД.accdb", "ПланФакт_сущпол_СВОД");
            db.DbModify();
            db.DbRead("VRI_DOC");
            Console.Read();
            
        }

        static void intrface1()
        {
            string dbPath = @"D:\work\Mapinfo\карты в работе\Классификатор\DB\Доп_ЗУ.accdb";
            string connetionString = "Provider=Microsoft.ACE.OLEDB.12.0; Mode = 16; Data Source=" + dbPath;
            OleDbConnection dbase;
            OleDbCommand dbcom;

            dbase = new OleDbConnection(connetionString);
            dbase.Open();

            string query = "SELECT bydoc, FID FROM Доп_ЗУ";
            dbcom = new OleDbCommand(query);
            dbcom.Connection = dbase;
            OleDbDataReader reader = dbcom.ExecuteReader();

            int counter = 2;



            var list = new List<forSorter>();



            while (reader.Read())
            {

                string bti_func = "";
                int bti_count = 0;
                bool lowLevelHousing = false;
                bool midLevelHousing = false;
                bool highLevelHousing = false;
                bool individualHousing = false;

                string vri_doc = reader[0].ToString();

                double uniqueID = Single.Parse(reader[1].ToString());

                //var bti = new Classifier.Bti(bti_func, bti_count, lowLevelHousing, midLevelHousing, highLevelHousing, individualHousing);

                //var Sample = new Classifier.Sorter(vri_doc, 600, bti);

                //Sample.GetVRI_FullSearh();

                list.Add(new forSorter(counter, bti_func, bti_count, lowLevelHousing, midLevelHousing, highLevelHousing, individualHousing, vri_doc, uniqueID));



                if (counter % 1000 == 0) Console.WriteLine(counter);

                counter++;
            }

            multyTread(list);

            int cntr = 1;
            //foreach (var val in list)
            //{
            //    workSheet.Cells[cntr, "A"] = val.Input;
            //    workSheet.Cells[cntr, "B"] = val.Area.ToString();

            //    if (cntr % 1000 == 0) Console.WriteLine(cntr);

            //    cntr++;

            //}
            Console.Read();
        }

        static void inrface2()
        {
            string dbPath = @"D:\work\Mapinfo\карты в работе\Классификатор\DB\БТИ.accdb";
            string connetionString = "Provider=Microsoft.ACE.OLEDB.12.0; Mode = 16; Data Source=" + dbPath;
            OleDbConnection dbase;
            OleDbCommand dbcom;

            dbase = new OleDbConnection(connetionString);
            dbase.Open();

            string query = "SELECT Назначение_строения, Общая_площадь_здания, MAPINFO_ID, UNOM FROM БТИ";
            dbcom = new OleDbCommand(query);
            dbcom.Connection = dbase;
            OleDbDataReader reader = dbcom.ExecuteReader();

            int counter = 2;

            var list = new List<forSorter2>();

            while (reader.Read())
            {

                string bydoc = reader[0].ToString();
                double area = (double)reader[1];
                int FID = (int)reader[2];
                int UNOM = (int)reader[3];

                if (bydoc != "")
                    list.Add(new forSorter2(bydoc, FID, area, UNOM));

                if (counter % 1000 == 0) Console.WriteLine(counter);

                counter++;
            }

            //multyTread(list);
        }

        static void multyTread(List<forSorter> list)
        {
            var excelApp = new Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();
            Worksheet workSheet = excelApp.ActiveSheet;
            var cells = workSheet.Cells;
            cells.NumberFormat = "@";
                       
           
            try
            {
                var result = Parallel.ForEach(list, item => work(item, workSheet));
                
            }
            catch(Exception e)
            {
                Console.WriteLine("FUCKUP DETECTED");
                Console.WriteLine(e.InnerException.Message);
            }

        }

        static void work2(forSorter2 fors, Worksheet workSheet)
        {
            try
            {
                Classifier.Sorter Sample = new Classifier.Sorter(fors.bydoc, (int)fors.area);

                Sample.Voronezh_search();

                workSheet.Cells[fors.FID, "A"] = fors.UNOM;
                workSheet.Cells[fors.FID, "D"] = Sample.Results[1];
                workSheet.Cells[fors.FID, "G"] = Sample.Results[4];
                workSheet.Cells[fors.FID, "H"] = Sample.Results[5];
                workSheet.Cells[fors.FID, "I"] = Sample.Results[6];
                workSheet.Cells[fors.FID, "J"] = Sample.Results[9];
                workSheet.Cells[fors.FID, "M"] = fors.FID;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
                Console.WriteLine(e.Message);
            }
        }

        static void work(forSorter fors, Worksheet workSheet)
        {
            Classifier.Sorter Sample = new Classifier.Sorter(fors.vri_doc, 600, 
                new Classifier.Bti(fors.bti_func, fors.bti_count, fors.lowLevelHousing, 
                    fors.midLevelHousing, fors.highLevelHousing, fors.individualHousing));

            Sample.TestBehaviorSearchWithoutBti();

            workSheet.Cells[fors.excel_row, "C"] = Sample.Results[0];
            workSheet.Cells[fors.excel_row, "D"] = Sample.Results[1];
            workSheet.Cells[fors.excel_row, "E"] = Sample.Results[2];
            workSheet.Cells[fors.excel_row, "F"] = Sample.Results[3];
            workSheet.Cells[fors.excel_row, "G"] = Sample.Results[4];
            workSheet.Cells[fors.excel_row, "H"] = Sample.Results[5];
            workSheet.Cells[fors.excel_row, "I"] = Sample.Results[6];
            workSheet.Cells[fors.excel_row, "J"] = Sample.Results[9];
            workSheet.Cells[fors.excel_row, "M"] = fors.vri_doc;
            workSheet.Cells[fors.excel_row, "B"] = fors.uniqueID;
            workSheet.Cells[fors.excel_row, "A"] = fors.excel_row;


            
        }

    }


    struct forSorter
    {
        public int excel_row;
        public string bti_func;
        public int bti_count;
        public bool lowLevelHousing;
        public bool midLevelHousing;
        public bool highLevelHousing;
        public bool individualHousing;
        public string vri_doc;
        public double uniqueID;

        public forSorter(int excel_row, string bti_func, int bti_count, bool lowLevelHousing, bool midLevelHousing, 
                            bool highLevelHousing, bool individualHousing, string vri_doc, double uniqueID)
        {
            this.excel_row = excel_row;
            this.bti_func = bti_func;
            this.bti_count = bti_count;
            this.lowLevelHousing = lowLevelHousing;
            this.midLevelHousing = midLevelHousing;
            this.highLevelHousing = highLevelHousing;
            this.individualHousing = individualHousing;
            this.vri_doc = vri_doc;
            this.uniqueID = uniqueID;
        }
    }

    struct forSorter2
    {
        public string bydoc;
        public int FID;
        public double area;
        public int UNOM;

        public forSorter2(string bydoc, int FID, double area, int UNOM)
        {
            this.bydoc = bydoc;
            this.FID = FID;
            this.area = area;
            this.UNOM = UNOM;
        }
    }
}
