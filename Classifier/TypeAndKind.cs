using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    public interface ITypeAndKind
    {   
        /// <summary>
        /// Тип
        /// </summary>
        int Type { get; }
        /// <summary>
        /// Вид
        /// </summary>
        int Kind { get; }
        /// <summary>
        /// Event's observer
        /// </summary>
        /// <param name="msg"></param>
        void IsFederal(bool state, string msg);
        /// <summary>
        /// Event's observer
        /// </summary>
        /// <param name="state"></param>
        void CodesAreCuting(bool state);
    }


    class TypeAndKind : ITypeAndKind
    {
        private ICodes codes;
        public TypeAndKind(ICodes codes)
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

        #region Events
        #region SearchCodes event
        string node = "";
        bool federal = false;
        /// <summary>
        /// Подписчик на событие: Найден федеральный код.
        /// </summary>
        /// <param name="msg">Передает федеральный код</param>
        public void IsFederal(bool state, string msg)
        {
            node = msg;
            federal = state;
        }
        #endregion

        #region CodeProcessing event
        bool cut = false;
        /// <summary>
        /// Подписчик на событие: Есть уточняющий код БТИ
        /// для федерального кода. 
        /// <remark>Вместо россыпи кодов из федерального
        /// придет один код, из БТИ</remark>
        /// </summary>
        /// <param name="state"></param>
        public void CodesAreCuting(bool state)
        {
            cut = state;
        }
        #endregion
        #endregion

        private int getType()
        {
            var whiteList = new List<int> {100, 200, 300,
                120, 130, 230, 140, 240, 340, 124, 134, 234,
                    123, 500, 600, 700, 800, 900 };

            var set = new List<string>();
            if (federal && !cut)
            {
                if (Equals(node, "3.1"))
                    set = codes.GetTypes("3.1.2, 3.1.3");
                else
                    set = codes.GetTypes();
            }
            else
            {
                set = codes.GetTypes();
            }

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
            var cutSet = new List<string>();
            foreach (var item in set)
            {
                cutSet.Add(item.Remove(1));
            }
            var result = "";
            foreach (var val in cutSet.Distinct())
            {
                result += val;
            }
            while (result.Length < N)
            {
                result += "0";
            }
            return int.Parse(result);
        }

       
        private int getKind()
        {
            List<int> whiteList = new List<int> { 1001, 1002, 1003, 1004, 1005, 1006, 1007,
                1000, 2001, 2002, 2003, 2004, 2000, 3001, 3002, 3003, 3004, 3005, 3006, 3000,
                    4001, 4002, 4000, 1200, 1300, 2300, 1230, 5000, 6000, 7000, 8000, 9000 };

            var set = new List<string>();
            if (federal && !cut)
            {
                if (Equals(node, "3.1"))
                    set = codes.GetKinds("3.1.2, 3.1.3");
                else
                    set = codes.GetKinds();
            }
            else
            {
                set = codes.GetKinds();
            }

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
