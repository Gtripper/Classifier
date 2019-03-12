using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    public interface IOutputData
    {
        string VRI_List { get; }
        string Matches { get; }

        bool IsMainSearch { get; }
        bool IsPZZSearch { get; }
        bool IsFederalSearch { get; }

        bool IsLandscape { get; }
        bool IsMaintenance { get; }

        int Type { get; }
        int Kind { get; }
    }

    class OutputData : IOutputData
    {
        public string VRI_List { get; private set; }
        public string Matches { get; private set; }

        public bool IsMainSearch { get; private set; }
        public bool IsPZZSearch { get; private set; }
        public bool IsFederalSearch { get; private set; }

        public bool IsLandscape { get; private set; }
        public bool IsMaintenance { get; private set; }

        public int Type { get; private set; }
        public int Kind { get; private set; }

        public OutputData(string _VRI_List, string _Matches, bool 
            _IsMainSearch, bool _IsPZZSearch, bool _IsFederalSearch, 
                bool _IsLandscape, bool _IsMaintenance, int _Type, int _Kind)
        {
            VRI_List = _VRI_List;
            Matches = _Matches;
            IsMainSearch = _IsMainSearch;
            IsPZZSearch = _IsPZZSearch;
            IsFederalSearch = _IsFederalSearch;
            IsLandscape = _IsLandscape;
            IsMaintenance = _IsMaintenance;
            Type = _Type;
            Kind = _Kind;
        }
    }
}
