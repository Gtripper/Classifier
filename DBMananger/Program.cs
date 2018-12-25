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

            string dbPath = @"D:\work\Mapinfo\карты в работе\Классификатор\DB\oldDB.accdb";
            string connetionString = "Provider=Microsoft.ACE.OLEDB.12.0; Mode = 16; Data Source=" + dbPath;
            OleDbConnection dbase;
            OleDbCommand dbcom;

            dbase = new OleDbConnection(connetionString);
            dbase.Open();

            string query = "SELECT bti_func, кол_во_объектов_БТИ, Признак_2_1_1, Признак_2_5, Признак_2_6, признак_ИЖС, VRI_DOC, OBJECTID FROM ПланФакт_сущпол";
            dbcom = new OleDbCommand(query);
            dbcom.Connection = dbase;
            OleDbDataReader reader = dbcom.ExecuteReader();

            int counter = 2;

            

            var list = new List<forSorter>();

            

            while (reader.Read())
            {

                string bti_func = reader[0].ToString();
                int bti_count = (int)reader[1];
                bool lowLevelHousing = (bool)reader[2];
                bool midLevelHousing = (bool)reader[3];
                bool highLevelHousing = (bool)reader[4];
                bool individualHousing = (bool)reader[5];

                string vri_doc = reader[6].ToString();

                double uniqueID = (double)reader[7];

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
                var result = Parallel.ForEach<forSorter>(list, item => work(item, workSheet));
            }
            catch
            {
                Console.WriteLine("FUCKUP DETECTED");
            }

        }

        static void work(forSorter fors, Worksheet workSheet)
        {
            Classifier.Sorter Sample = new Classifier.Sorter(fors.vri_doc, 600, 
                new Classifier.Bti(fors.bti_func, fors.bti_count, fors.lowLevelHousing, 
                    fors.midLevelHousing, fors.highLevelHousing, fors.individualHousing));

            Sample.GetVRI_FullSearh();

            workSheet.Cells[fors.excel_row, "C"] = Sample.CodesVri;
            workSheet.Cells[fors.excel_row, "D"] = Sample.Mathes;
            workSheet.Cells[fors.excel_row, "E"] = Sample.Input;
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
}
