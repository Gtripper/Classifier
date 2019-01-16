using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;

namespace DBMananger
{
    /// <summary>
    /// Подключение к БД Access
    /// </summary>
    class DBAdapter
    {
        /// <summary>
        /// Путь к базе данных
        /// </summary>
        internal string Path { get; private set; }
        internal string DbName { get; private set; }
        internal OleDbConnection DbConn { get; private set; }
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
            string cmd = "ALTER TABLE " + DbName + " ADD COLUMN TEST Text(254)";
            OleDbCommand com = new OleDbCommand(cmd, DbConn);
            com.ExecuteNonQuery();
        }

        public void DbRead(string vri_doc)
        {
            string query = "SELECT * FROM " + DbName;

            OleDbDataAdapter adapter = new OleDbDataAdapter(query, DbConn);
            DataSet ds = new DataSet();
            adapter.Fill(ds);

            Data = ds.Tables[0];


            Parallel.ForEach(Data.Rows.Cast<DataRow>(), item =>
            {
                op(item);
                Console.WriteLine(item["TEST"]);
            });




            adapter.Update(ds);


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
