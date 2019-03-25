using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapInfoWrap;
using Classifier;
using DBMananger;
using MapInfo;
using System.Data;
using System.Data.OleDb;

namespace Core
{
    public class Fabric
    {
        public void Do()
        {
//            IMapInfoApp app = new MapinfoCurrentApp();
//            MapInfoAppControls controls = new MapInfoAppControls(app);
            //controls.CreateBufferDataBase(@"M:\Mapinfo Files\План фактического использования АКТУАЛЬНЫЙ\АНАЛИЗ_01_2019\темп\Рабочая_необработанные_участки.TAB");

//           controls.GetTableList();
//            controls.TablesShow();
//            var _table = controls.ChooseTable();
            

//            MapinfoTable table = new MapinfoTable(app.CreateInstance(), _table);
//            table.UniqueID();
//            table.CreateBufferDataBase();


            var savePath = @"D:\work\Mapinfo\программы\Classifier\tempBD\bufferDB.accdb";
            DBAdapter adapter = new DBAdapter(savePath, "bufferDB");
            adapter.DbRead();

            var data = adapter.Data;
            AsincRead(data);

            adapter.Update();
            //adapter.RunSorter();

            //controls.save();
            Console.ReadKey();
        }

        public void AsincRead(DataTable data)
        {
            var columns = data.Columns;

            foreach (DataColumn val in columns)
            {
                Console.WriteLine(val.ColumnName);
                Console.WriteLine(val.DataType);
            }

            //var item = data.Rows[5];
            //IFactory qwe = new Factory(CreateInput(item));
            //qwe.Execute();
            //WriteOutputData(qwe.outputData, item);

            //var inst = CreateInput(item);


            Parallel.ForEach(data.Rows.Cast<DataRow>(), itemX =>
            {
                Console.WriteLine(itemX[1]);
                var res = CreateInput(itemX);
                IFactory factory = new Factory(res);
                factory.Execute();
                WriteOutputData(factory.outputData, itemX);
            });

           
        }

        public InputData CreateInput(DataRow item)
        {
            var vri_doc = item["VRI_DOC"] is DBNull ? "" : (string)item["VRI_DOC"];
            var area = item["_COL14"] is DBNull ? 0 : (int)item["_COL14"];
            var bti_codes = item["BTICodes"] is DBNull ? "" : (string)item["BTICodes"];
            var lo = item["lo_lvl"] is DBNull ? false : (bool)item["lo_lvl"];
            var mid = item["mid_lvl"] is DBNull ? false : (bool)item["mid_lvl"];
            var hi = item["hi_lvl"] is DBNull ? false : (bool)item["hi_lvl"];


            return new InputData(vri_doc, area, bti_codes, lo, mid, hi);
        }

        public void WriteOutputData(IOutputData outputData, DataRow item)
        {
            item.BeginEdit();
            item["VRI"] = outputData.VRI_List;
            item["Matches"] = outputData.Matches;
            item["Type"] = outputData.Type;
            item["Kind"] = 666;
            item["Maintenance"] = outputData.IsMaintenance;
            item["Landscape"] = outputData.IsLandscape;
            item["FedSearch"] = outputData.IsFederalSearch;
            item.EndEdit();
        }
    }
}
