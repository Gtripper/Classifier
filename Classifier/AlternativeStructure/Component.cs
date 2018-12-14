using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classifier
{
    public abstract class Component
    {
        protected dynamic name;
        //protected Monster mnstr;
        

        public Component(dynamic name)
        {
            this.name = name;
            //this.mnstr = mnstr;
        }
                       

        public abstract void Operation();
        public abstract void Add(Component component);
        public abstract void Remove(Component component);
        public abstract Component GetChild(int index);
        public abstract string[] GetPattern();
        public abstract Monster GetMonster();
    }

    class Leaf : Component
    {
        public Monster monster;
        public Leaf(int name, Monster mnstr) : base(name)
        {
            monster = mnstr;
        }

        public Leaf(string name, Monster mnstr) : base(name)
        {
            monster = mnstr;
        }

        public Leaf(int name)
        : base(name)
        { }

        public Leaf(string name) : base(name)
        {
        }

        public override void Operation()
        {
           // Console.WriteLine(GetType().Name + "     {0}    {1}", name, monster.GetVri);
        }
        public override void Add(Component component)
        {
            throw new InvalidOperationException();
        }
        public override void Remove(Component component)
        {
            throw new InvalidOperationException();
        }
        public override Component GetChild(int index)
        {
            throw new InvalidOperationException();
        }
        public override string[] GetPattern()
        {
            return monster.GetPatterns();
        }
        public override Monster GetMonster()
        {
            return monster;
        }

    }

    class Composite : Component
    {
        ArrayList nodes = new ArrayList();
        public Monster monster;

        public Composite(int name, Monster mnstr) : base(name)
        {
            monster = mnstr;
        }

        public Composite(string name, Monster mnstr) : base(name)
        {
            monster = mnstr;
        }

        public Composite(int name) : base(name)
        {
        }

        public Composite(string name) : base(name)
        {
        }

        public override void Operation()
        {
            if (!name.Equals("ROOT"))
            Console.WriteLine(GetType().Name + "     " + name + "      " + monster.GetVri540);
            foreach (Component component in nodes)
                component.Operation();
        }
        public override void Add(Component component)
        {
            nodes.Add(component);
        }
        public override void Remove(Component component)
        {
            nodes.Remove(component);
        }
        public override Component GetChild(int index)
        {
            return nodes[index] as Component;
        }
        public override string[] GetPattern()
        {
            if (!name.Equals("ROOT"))
            {
                
                foreach (Component component in nodes)
                {
                    return component.GetPattern();
                }
                return GetPattern();
            }
            else return null;
        }
        public override Monster GetMonster()
        {
            if (!name.Equals("ROOT"))
            {

                foreach (Component component in nodes)
                {
                    return component.GetMonster();
                }
                return GetMonster();
            }
            else return null;
        }


    }
}
