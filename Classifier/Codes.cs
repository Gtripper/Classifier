using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Classifier
{
    public interface ICodes
    {
        List<Node> Nodes { get; }

        /// <summary>
        /// Возвращает количество элементов коллекции Nodes
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Добавляет node в коллекцию Nodes
        /// </summary>
        /// <param name="node"></param>
        void Add(Node node);

        /// <summary>
        /// Добавляет элементы коллекции nodes к коллекции Nodes
        /// </summary>
        /// <param name="nodes"></param>
        void AddNodes(IEnumerable<Node> nodes);

        /// <summary>
        /// Добавляет элементы к коллекции Nodes из коллекции с кодами ПЗЗ
        /// </summary>
        /// <param name="vri"></param>
        void AddNodes(IEnumerable<string> vri);

        /// <summary>
        /// Добавляет элементы к коллекции Nodes из строки с кодами ПЗЗ
        /// </summary>
        /// <param name="vri"></param>
        void AddNodes(string vri);

        /// <summary>
        /// Удаляет все элементы из коллекции Nodes
        /// </summary>
        void Clear();

        /// <summary>
        /// Определяет, содержит ли Nodes элементы, переданные в аргументе
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        bool Exists(IEnumerable<string> codes);

        /// <summary>
        /// Определяет, содержит ли Nodes элементы, переданные в аргументе
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        bool Exists(string codes);

        /// <summary>
        /// Сортирует элементы в коллекции Nodes с использованием копмпоратора CodeComparer
        /// </summary>
        void Sort();

        /// <summary>
        /// Удаляет все элемениы коллекции Nodes, переданные в аргументе
        /// </summary>
        /// <param name="codes"></param>
        void RemoveAll(IEnumerable<string> codes);

        /// <summary>
        /// Удаляет все элемениы коллекции Nodes, переданные в аргументе
        /// </summary>
        /// <param name="codes"></param>
        void RemoveAll(string codes);
    }


    class Codes : ICodes
    {
        NodeFeed mf;
        public List<Node> Nodes { get; private set; }

        public Codes(NodeFeed mf)
        {
            this.mf = mf;
            Nodes = new List<Node>();
        }

        public int Count {get { return Nodes?.Count ?? 0; }}

        public void Add(Node node)
        {
            Nodes.Add(node);
        }

        public void AddNodes(IEnumerable<Node> nodes)
        {
            Nodes.AddRange(nodes);
        }

        public void AddNodes(IEnumerable<string> vri)
        {
            var result = mf.GetNodes().Where(p => vri.Contains(p.vri));
            AddNodes(result);
        }

        public void AddNodes(string vri)
        {
            var pattern = @"\d+[.]\d+[.]\d+([.]\d+)?";
            var result = Regex.Matches(vri, pattern).Cast<Match>().Select(p => p.Value);

            AddNodes(result);
        }

        public void Clear()
        {
            Nodes.Clear();
        }

        public bool Exists(IEnumerable<string> codes)
        {
            return Nodes.Exists(p => codes.Contains(p.vri));
        }

        public bool Exists(string codes)
        {
            var pattern = @"\d+[.]\d+[.]\d+([.]\d+)?";
            var result = Regex.Matches(codes, pattern).Cast<Match>().Select(p => p.Value);

            return Exists(result);
        }

        public void RemoveAll(IEnumerable<string> codes)
        {
            Nodes.RemoveAll(p => codes.Contains(p.vri));
        }

        public void RemoveAll(string codes)
        {
            var pattern = @"\d+[.]\d+[.]\d+([.]\d+)?";
            var result = Regex.Matches(codes, pattern).Cast<Match>().Select(p => p.Value);

            RemoveAll(result);
        }

        public override string ToString()
        {
            string result = "";
            foreach (var node in Nodes)
            {
                result += (result.Length == 0) ? node.vri : ", " + node.vri;
            }
            return result;
        }

        public void Sort()
        {
            IComparer<Node> comparer = new CodeComparer();
            Nodes.Sort(comparer);
        }
    }
}
