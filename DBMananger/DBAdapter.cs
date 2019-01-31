using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;
using IronPython.Hosting;
using Microsoft.Scripting.Hosting;

namespace DBMananger
{
    /// <summary>
    /// Подключение к БД Access
    /// </summary>
    internal class DBAdapter
    {
        /// <summary>
        /// Путь к базе данных
        /// </summary>
        internal string Path { get; private set; }
        internal string DbName { get; private set; }
        internal OleDbConnection DbConn { get; private set; }
        internal OleDbDataAdapter adapter { get; private set; }
        internal DataTable Data { get; private set; }

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

        public void DbRead(string vri_doc)
        {
            string query = "SELECT * FROM " + DbName;

            adapter = new OleDbDataAdapter(query, DbConn);
            
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            Data = ds.Tables[0];            
        }

        /// <summary>
        /// 
        /// </summary>
        /// TODO: Допилить обрезатель строк под 254 символа.
        public void RunSorter()
        {
            try
            {
                Parallel.ForEach(Data.Rows.Cast<DataRow>(), item =>
                {
                    var res = new Classifier.Sorter(item["VRI_DOC"].ToString(), 0,
                    new Classifier.BtiMock().Bti);

                    res.TestBehaviorSearchWithoutBti();
                    item.BeginEdit();
                    item["new_vri"] = res.Results[4];
                    item["new_matches"] = res.Results[1];
                    item["new_tip"] = res.Results[5];
                    item["new_vid"] = res.Results[6];
                    item.EndEdit();
                    Console.WriteLine(item["new_vri"] + "   " + item["new_tip"]);
                });
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

        private void op(DataRow row)
        {
            var res = new Classifier.Sorter(row["bydoc"].ToString(), 0,
                new Classifier.Bti("жилой дом", 1, false, false, false, true));
            res.TestBehaviorSearchWithoutBti();

            

            row["TEST"] = res.Results[4];
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
