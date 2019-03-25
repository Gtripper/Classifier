using MapInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MapInfoWrap
{
    public class MapInfoAppControls
    {
        public DMapInfo instance;
        public IMapInfoApp MapinfoApp;
        public List<string> tables;

        public MapInfoAppControls(IMapInfoApp _MapinfoApp)
        {
            MapinfoApp = _MapinfoApp;
            instance = MapinfoApp.CreateInstance();
            tables = new List<string>();
        }

        public void GetTableList()
        {             

            if (int.TryParse(instance.Eval("NumTables()"), out var amount))
            {
                var it = 1;
                while (it <= amount)
                {
                    var table = instance.Eval(string.Format("TableInfo({0}, 1)", it));
                    if (!String.IsNullOrEmpty(table)) tables.Add(table);
                    it++;
                }
            }
        }

        public void TablesShow()
        {
            var it = 0;
            foreach (var table in tables)
            {
                Console.WriteLine("{0} {1}", it, table);
                it++;
            }
        }

        public string ChooseTable()
        {
            Console.WriteLine("Введите номер выбранной таблицы: ");
            var key = Console.ReadLine();
            if (int.TryParse(key, out var n))
                return tables[n];
            else
                return tables[0];
        }
    }
}
