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

            string dbPath = @"D:\work\Mapinfo\карты в работе\Классификатор\DB\shortList100k.accdb";
            string connetionString = "Provider=Microsoft.ACE.OLEDB.12.0; Mode = 16; Data Source=" + dbPath;
            OleDbConnection dbase;
            OleDbCommand dbcom;

            dbase = new OleDbConnection(connetionString);
            dbase.Open();

            string query = "SELECT bti_func, кол_во_объектов_БТИ, Признак_2_1_1, Признак_2_5, Признак_2_6, признак_ИЖС, VRI_DOC FROM shortList100k";
            dbcom = new OleDbCommand(query);
            dbcom.Connection = dbase;
            OleDbDataReader reader = dbcom.ExecuteReader();

            int counter = 2;

            var excelApp = new Application();
            excelApp.Visible = true;
            excelApp.Workbooks.Add();

            Worksheet workSheet = excelApp.ActiveSheet;

            var list = new List<Classifier.Sorter>();

            while (reader.Read())
            {
                string bti_func = reader[0].ToString();
                int bti_count = (int)reader[1];
                bool lowLevelHousing = (bool)reader[2];
                bool midLevelHousing = (bool)reader[3];
                bool highLevelHousing = (bool)reader[4];
                bool individualHousing = (bool)reader[5];

                string vri_doc = reader[6].ToString();

                var bti = new Classifier.Bti(bti_func, bti_count, lowLevelHousing, midLevelHousing, highLevelHousing, individualHousing);

                var Sample = new Classifier.Sorter(vri_doc, 600, bti);

                //Sample.GetVRI_FullSearh();

                list.Add(Sample);

                

                if (counter % 1000 == 0) Console.WriteLine(counter);

                counter++;
            }

            int cntr = 1;
            foreach (var val in list)
            {
                workSheet.Cells[cntr, "A"] = val.Input;
                workSheet.Cells[cntr, "B"] = val.Area.ToString();

                if (cntr % 1000 == 0) Console.WriteLine(cntr);

                cntr++;

            }




            Console.Read();


        }       
    }
}
