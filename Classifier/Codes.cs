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
        /// Определяет, содержит ли Nodes типы, переданные в аргументе
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        bool ExistsType(IEnumerable<string> codes);

        /// <summary>
        /// Определяет, содержит ли Nodes типы, переданные в аргументе
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        bool ExistsType(string codes);

        /// <summary>
        /// Возвращает строку, содержащую все индексы ВРИ, кроме
        /// переданных в аргументе
        /// </summary>
        /// <param name="vri"></param>
        /// <returns></returns>
        string Except(string vri);

        /// <summary>
        /// Возвращает коллекцию уникальных типов Nodes
        /// </summary>
        /// <returns></returns>
        List<string> GetTypes();

        /// <summary>
        /// Возвращает коллекцию уникальных типов Nodes, за исключением кодов
        /// указанных в аргументе функции
        /// </summary>
        /// <returns></returns>
        List<string> GetTypes(string except);

        /// <summary>
        /// Возвращает коллекцию уникальных видов Nodes
        /// </summary>
        /// <returns></returns>
        List<string> GetKinds();

        /// <summary>
        /// Возвращает коллекцию уникальных видов Nodes, за исключением кодов
        /// указанных в аргументе функции
        /// </summary>
        /// <returns></returns>
        List<string> GetKinds(string except);

        /// <summary>
        /// Сортирует элементы в коллекции Nodes с использованием копмпоратора CodeComparer
        /// </summary>
        void Sort();

        /// <summary>
        /// Выводит ворматированную строку кодов ВРИ
        /// </summary>
        /// <returns></returns>
        string Show { get; }

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

        IEnumerator<Node> GetEnumerator();
    }

    public interface ICodesTypes
    {
        /// <summary>
        /// Возвращает количество элементов коллекции Nodes
        /// </summary>
        int Count { get; }

        /// <summary>
        /// Определяет, содержит ли Nodes типы, переданные в аргументе
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        bool ExistsType(IEnumerable<string> codes);

        /// <summary>
        /// Определяет, содержит ли Nodes типы, переданные в аргументе
        /// </summary>
        /// <param name="codes"></param>
        /// <returns></returns>
        bool ExistsType(string codes);

        /// <summary>
        /// Возвращает строку, содержащую все индексы ВРИ, кроме
        /// переданных в аргументе
        /// </summary>
        /// <param name="vri"></param>
        /// <returns></returns>
        string Except(string vri);

        /// <summary>
        /// Возвращает коллекцию уникальных типов Nodes
        /// </summary>
        /// <returns></returns>
        List<string> GetTypes();

        /// <summary>
        /// Возвращает коллекцию уникальных видов Nodes
        /// </summary>
        /// <returns></returns>
        List<string> GetKinds();
    }

    class Codes : ICodes, ICodesTypes, IEnumerable<Node>
    {
        NodeFeed mf;
        public List<Node> Nodes { get; private set; }

        public Codes(NodeFeed mf)
        {
            this.mf = mf;
            Nodes = new List<Node>();
        }

        public int Count { get { return Nodes?.Count ?? 0; } }

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

        public bool ExistsType(IEnumerable<string> types)
        {
            return Nodes.Exists(p => types.Contains(p.typeCode));
        }

        public bool ExistsType(string types)
        {
            var pattern = @"\d+";
            var result = Regex.Matches(types, pattern).Cast<Match>().Select(p => p.Value);

            return ExistsType(result);
        }

        public string Except(string vri)
        {
            string result = "";
            foreach (var node in this.Where(p => !vri.Contains(p.vri)))
            {
                result += (result.Length == 0) ? node.vri : ", " + node.vri;
            }
            return result;
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

        public string Show { get { return ToString(); } }

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

        public List<string> GetTypes()
        {
            return Nodes.Select(p => p.typeCode).Distinct().ToList();
        }

        public List<string> GetTypes(string except)
        {
            var pattern = @"\d+[.]\d+[.]\d+([.]\d+)?";
            var result = Regex.Matches(except, pattern).Cast<Match>().Select(p => p.Value).ToList();

            return Nodes.Where(p => !result.Contains(p.vri)).Select(p => p.typeCode).Distinct().ToList();
        }

        public List<string> GetKinds()
        {
            return Nodes.Select(p => p.kindCode).Distinct().ToList();
        }

        public List<string> GetKinds(string except)
        {
            var pattern = @"\d+[.]\d+[.]\d+([.]\d+)?";
            var result = Regex.Matches(except, pattern).Cast<Match>().Select(p => p.Value).ToList();

            return Nodes.Where(p => !result.Contains(p.vri)).Select(p => p.kindCode).Distinct().ToList();
        }

        public IEnumerator<Node> GetEnumerator()
        {
            return Nodes.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return Nodes.GetEnumerator();
        }
    }
}
