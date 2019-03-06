using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    class CodeComparer : IComparer<string>
    {
        int IComparer<string>.Compare(string x, string y)
        {
            var mf = new NodeFeed().GetNodes();
            var intA = mf.FindIndex(p => p.vri.Equals(x));
            var intB = mf.FindIndex(p => p.vri.Equals(y));

            if (intA > intB) return 1;
            if (intA < intB) return -1;
            else return 0;

            throw new NotImplementedException();
        }
    }
}
