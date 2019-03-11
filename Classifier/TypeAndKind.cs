using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    public interface ITypeAndKind
    {
        int Type { get; }
        int Kind { get; }
    }


    class TypeAndKind : ITypeAndKind
    {
        private ICodesTypes codes;
        public TypeAndKind(ICodesTypes codes)
        {
            this.codes = codes;
        }

        public int Type
        {
            get
            {
                if (codes.Count > 0)
                    return getType();
                else
                    return 0;
            }
        }
        public int Kind
        {
            get
            {
                if (codes.Count > 0)
                    return getKind();
                else
                    return 0;
            }
        }

        /// TODO: Посмотреть и доработать метод (рассмотреть варианты и возможности)
        private int getType()
        {
            var whiteList = new List<int> {100, 200, 300,
                120, 130, 230, 140, 240, 340, 124, 134, 234,
                    123, 500, 600, 700, 800, 900 };

            var set = codes.GetTypes();

            if (set.Count == 1)
            {                
                return int.Parse(set[0]);
            }
            else
            {
                return IsCodeCorrect(whiteList, MixedTypeKind(set, 3));
            }
        }
        private int MixedTypeKind(List<string> set, int N)
        {
            set.Sort();
            set.ForEach(delegate(string elem)
            {
                elem.Remove(1);
            });
            var result = "";
            foreach (var val in set)
            {
                result += val;
            }
            while (result.Length < N)
            {
                result += "0";
            }
            return int.Parse(result);
        }

        /// TODO: Посмотреть и доработать метод (рассмотреть варианты и возможности)
        private int getKind()
        {
            List<int> whiteList = new List<int> { 1001, 1002, 1003, 1004, 1005, 1006, 1007,
                1000, 2001, 2002, 2003, 2004, 2000, 3001, 3002, 3003, 3004, 3005, 3006, 3000,
                    4001, 4002, 4000, 1200, 1300, 2300, 1230, 5000, 6000, 7000, 8000, 9000 };

            var set = codes.GetKinds();

            if (set.Count == 1)
            {
                return int.Parse(set[0]);
            }
            else
            {
                return IsCodeCorrect(whiteList, MixedTypeKind(set, 4));
            }
        }

        private int IsCodeCorrect(List<int> whiteList, int value)
        {
            if (whiteList.Exists(p => p == value))
                return value;
            else
                return 999;
        }
    }
}
