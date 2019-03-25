using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using Microsoft.Scripting.Hosting;
using Classifier;

namespace DBMananger
{
    /// <summary>
    /// Подключение к БД Access
    /// </summary>
    public class DBAdapter
    {
        /// <summary>
        /// Путь к базе данных
        /// </summary>
        internal string Path { get; private set; }
        internal string DbName { get; private set; }
        internal OleDbConnection DbConn { get; private set; }
        internal OleDbDataAdapter adapter { get; private set; }
        public DataTable Data { get; private set; }

        public DBAdapter(string path, string dbName)
        {
            Path = path;
            DbName = dbName;
            DbConnection();
        }

        /// <summary>
        /// Открытие соединения с БД
        /// </summary>
        private void DbConnection()
        {
            string connetionString = "Provider=Microsoft.ACE.OLEDB.12.0; Mode = 16; Data Source=" + Path;

            DbConn = new OleDbConnection(connetionString);
            DbConn.Open();
        }

        /// <summary>
        /// Закрытие БД
        /// </summary>
        private void DbClose()
        {
            DbConn.Close();
        }

        public void DbModify()
        {
            try
            {
                string addCols = "ALTER TABLE " + DbName + " ADD COLUMN new_vri Text(254), " +
                    "               new_matches Text(254), new_tip Text(10), new_vid Text(10)";
                OleDbCommand com = new OleDbCommand(addCols, DbConn);
                com.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("COLUMNS ARE EXIST");
            }
        }

        public void DbRead(string vri_doc = "*")
        {
            string query = "SELECT " + vri_doc + " FROM " + DbName;

            adapter = new OleDbDataAdapter(query, DbConn);
            
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            adapter.Fill(ds);

            Data = ds.Tables[0];
            Console.WriteLine("here");
        }

        public InputData CreateInputData(DataRow dataRow)
        {
            return new InputData((string) dataRow["VRI_DOC"], (int) dataRow["_COL13"], (string) dataRow["BTICodes"], (bool)dataRow["lo_lvl"], (bool)dataRow["mid_lvl"], (bool)dataRow["hi_lvl"]);
        }

        /// <summary>
        /// 
        /// </summary>
        /// TODO: Допилить обрезатель строк под 254 символа.
        public void RunSorter()
        {
            try
            {
                for (int j = 0; j < 5; j++)
                {
                    DataRow item = Data.Rows[j];

                    //var _lo = (int)item["lo_lvl"] == 0;
                    //var _mid = (int)item["mid_lvl"] == 0;
                    //var _hi = (int)item["hi_lvl"] == 0;

                    var inpt = new InputData((string)item["VRI_DOC"], 0, "", false, false, false);
                    IFactory factory = new Factory(inpt);
                    factory.Execute();

                    Console.WriteLine(item["VRI_DOC"].ToString() + "  " + factory.outputData.VRI_List);

                    item["VRI"] = factory.outputData.VRI_List;
                    item["Matches"] = factory.outputData.Matches;
                    item["Type"] = factory.outputData.Type;
                    item["Kind"] = factory.outputData.Kind;
                    item["Maintenance"] = factory.outputData.IsMaintenance;
                    item["Landscape"] = factory.outputData.IsLandscape;
                    item["FedSearch"] = factory.outputData.IsFederalSearch;
                }

                //Parallel.ForEach(Data.AsEnumerable(), item =>
                //{
                //    var inpt = new InputData((string)item["VRI_DOC"], (int)item["_COL13"],
                //        (string)item["BTICodes"], (bool)item["lo_lvl"], (bool)item["mid_lvl"], (bool)item["hi_lvl"]);
                //    IFactory factory = new Factory(inpt);

                //    Console.WriteLine((string)item["VRI_DOC"] + "  " + factory.outputData.VRI_List);

                //    item["VRI"] = factory.outputData.VRI_List;
                //    item["Matches"] = factory.outputData.Matches;
                //    item["Type"] = factory.outputData.Type;
                //    item["Kind"] = factory.outputData.Kind;
                //    item["Maintenance"] = factory.outputData.IsMaintenance;
                //    item["Landscape"] = factory.outputData.IsLandscape;
                //    item["FedSearch"] = factory.outputData.IsFederalSearch;

                //    //var res = new Classifier.Sorter(item["VRI_DOC"].ToString(), 0,
                //    //new Classifier.BtiMock().Bti);

                //    //res.TestBehaviorSearchWithoutBti();
                //    //item.BeginEdit();
                //    //item["new_vri"] = res.Results[4];
                //    //item["new_matches"] = res.Results[1];
                //    //item["new_tip"] = res.Results[5];
                //    //item["new_vid"] = res.Results[6];
                //    //item.EndEdit();
                //    //Console.WriteLine(item["new_vri"] + "   " + item["new_tip"]);
                //});
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException.Message);
            }
            finally
            {
                Console.WriteLine("ЭТО ПИШЕТСЯ ПОСЛЕ ЦИКЛА");

                OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                adapter.UpdateCommand = builder.GetUpdateCommand();

                adapter.Update(Data);

                Console.Read();
            }
        }

        public void Update()
        {
            OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
            adapter.UpdateCommand = builder.GetUpdateCommand();
            adapter.Update(Data);
        }

        private void op(DataRow row)
        {
            //var res = new Classifier.Sorter(row["bydoc"].ToString(), 0,
            //    new Classifier.Bti("жилой дом", 1, false, false, false, true));
            //res.TestBehaviorSearchWithoutBti();

            

            //row["TEST"] = res.Results[4];
        }

        public void DbRead(string vri_doc, string area)
        {
            string query = "SELECT " + vri_doc + ", OBJECTID FROM " + DbName;

            //DBCom = new OleDbCommand(query);
            //DBCom.Connection = DbConn;
            //OleDbDataReader reader = DBCom.ExecuteReader();
        }


    }
}
