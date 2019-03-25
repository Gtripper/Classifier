using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MapInfo;

namespace MapInfoWrap
{
    public interface IMapTable
    {

    }

    public class MapinfoTable : IMapTable
    {
        DMapInfo instance;
        string tabName;
        Dictionary<string, string> columns;

        public MapinfoTable()
        {

        }

        public MapinfoTable(DMapInfo _instance, string _tabName)
        {
            instance = _instance;
            tabName = _tabName;
            columns = GetColumnsList();
        }

        public void CreateBufferDataBase(string tempFolder = @"D:\work\Mapinfo\программы\Classifier\tempBD\")
        {
            //instance.Do(@"Open Table """ + tablePath + @""" As currentTable");

            var savePath1 = tempFolder + "bufferDB.TAB";
            var savePath2 = tempFolder + "bufferDB.accdb";

            var sqlCom = @"SELECT UId, VRI_DOC, BTICodes, lo_lvl, mid_lvl, hi_lvl, VRI, Matches, Type, Kind, Maintenance, Landscape, FedSearch, Int(Area(obj, ""sq m"")) FROM " + tabName + " Into rrr";
            instance.Do(sqlCom);

            var saveToDBCommand = @"Commit Table rrr As """ + savePath1 + @""" Type ACCESS Database """ + savePath2 + @""" Table ""bufferDB""";
            instance.Do(saveToDBCommand);
        }

        public void save()
        {
            instance.Do(@"Register Table ""D:\work\Mapinfo\программы\Classifier\tempBD\bufferDB.accdb""  Type ACCESS Table ""bufferDB"" Into ""D:\work\Mapinfo\программы\Classifier\tempBD\bufferDB.TAB""");
            instance.Do(@"Open Table ""D:\work\Mapinfo\программы\Classifier\tempBD\bufferDB.TAB"" As Modify");
        }

        public void UniqueID()
        {
            if (!columns.ContainsKey("UId")) instance.Do("Alter Table " + tabName + "( Add UId Integer)");
            instance.Do("Update " + tabName + " Set UId = RowID");
            instance.Do("Commit Table " + tabName);
        }

        public Dictionary<string, string> GetColumnsList()
        {
            var _cols = new Dictionary<string, string>();
            if (int.TryParse(instance.Eval("TableInfo(" + tabName + ", 4)"), out var amount))
            {
                var it = 1;
                while (it <= amount)
                {
                    var columnName = instance.Eval("ColumnInfo(" + tabName + @", COL" + it.ToString() + ", 1)");
                    var columnType = "";
                    switch (instance.Eval("ColumnInfo(" + tabName + @", COL" + it.ToString() + ", 3)"))
                    {
                        case "1":
                            columnType = "CHAR";
                            break;
                        case "2":
                            columnType = "DECIMAL";
                            break;
                        case "3":
                            columnType = "INTEGER";
                            break;
                        case "4":
                            columnType = "SMALLINT";
                            break;
                        case "5":
                            columnType = "DATE";
                            break;
                        case "6":
                            columnType = "LOGICAL";
                            break;
                        case "7":
                            columnType = "GRAPHIC";
                            break;
                        case "8":
                            columnType = "FLOAT";
                            break;
                        case "37":
                            columnType = "TIME";
                            break;
                        case "38":
                            columnType = "DATETIME";
                            break;
                    }
                    _cols.Add(columnName, columnType);
                    it++;
                }
            }
            return _cols;
        }
    }
}
