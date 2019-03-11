using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    class CodeComparer : IComparer<Node>
    {
        int IComparer<Node>.Compare(Node x, Node y)
        {
            var mf = new NodeFeed().GetNodes();
            var intA = mf.FindIndex(p => p.Equals(x));
            var intB = mf.FindIndex(p => p.Equals(y));

            if (intA > intB) return 1;
            if (intA < intB) return -1;
            else return 0;

            throw new NotImplementedException();
        }
    }
}
