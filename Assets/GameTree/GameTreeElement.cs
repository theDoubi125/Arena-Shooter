using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.GameTree
{
    public class GameTreeElement
    {
        GameTreeOperator m_operator;
        public GameTreeOperator Operator
        {
            get { return m_operator; }
            private set { m_operator = value; }
        }

        LinkedList<GameTreeElement> m_childs = new LinkedList<GameTreeElement>();
        
        public LinkedList<GameTreeElement> Childs
        {
            get { return m_childs; }
        }

        GameTreeElement m_father;

        public GameTreeElement Father
        {
            get { return m_father; }
        }

        public GameTreeElement()
        {
            m_operator = null;
        }

        public GameTreeElement(GameTreeOperator _operator)
        {
            m_operator = _operator;
        }

        public GameTreeElement(GameTreeElement _elem)
        {
            Type type = _elem.Operator.GetType();
            System.Reflection.ConstructorInfo constructor = type.GetConstructor(new Type[] { type });
            m_operator = constructor.Invoke(new object[] { _elem.Operator }) as GameTreeOperator;
            foreach (GameTreeOperator op in _elem.Operator.Childs)
            {
                type = op.GetType();
                constructor = type.GetConstructor(new Type[] { type });
                Add(constructor.Invoke(new object[] { op }) as GameTreeOperator);
            }
            m_father = _elem.Father;
        }

        public void ParseXml(XmlNode _node)
        {
            string type = _node.Attributes["type"].Value;
        }

        public void SetFather(GameTreeElement _elem)
        {
            m_father = _elem;
        }

        public void Add(GameTreeOperator _operator)
        {
            Add(new GameTreeElement(_operator));
        }

        public void Add(GameTreeElement _elem)
        {
            m_childs.AddLast(_elem);
        }

        public void Remove(GameTreeOperator _operator)
        {
            Remove(m_childs.Where((elem) => elem.Operator == _operator).First());
        }

        public void Remove(GameTreeElement _elem)
        {
            m_childs.Remove(_elem);
        }
    }
}
