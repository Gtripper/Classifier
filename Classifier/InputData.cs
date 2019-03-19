using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    public interface IInputData
    {
        string Vri_doc { get; }
        int Area { get; }
        string BtiVri { get; }
        bool Lo_lvl { get; }
        bool Mid_lvl { get; }
        bool Hi_lvl { get; }
    }


    public class InputData : IInputData
    {
        public string Vri_doc { get; }
        public int Area { get; }

        public string BtiVri { get; }
        public bool Lo_lvl { get; }
        public bool Mid_lvl { get; }
        public bool Hi_lvl { get; }

        public InputData(string _vri_doc, int _area)
        {
            Vri_doc = _vri_doc;
            Area = _area;
            BtiVri = "";
            Lo_lvl = false;
            Mid_lvl = false;
            Hi_lvl = false;
        }

        public InputData(string _vri_doc, int _area, string _btiVri, bool _lo, bool _mid, bool _hi)
        {
            Vri_doc = _vri_doc;
            Area = _area;
            BtiVri = _btiVri;
            Lo_lvl = _lo;
            Mid_lvl = _mid;
            Hi_lvl = _hi;
        }
    }
}
