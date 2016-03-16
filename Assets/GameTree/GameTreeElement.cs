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

        public GameTreeElement()
        {
            m_operator = null;
        }

        public GameTreeElement(GameTreeOperator _operator)
        {
            m_operator = _operator;
        }

        public void ParseXml(XmlNode _node)
        {
            string type = _node.Attributes["type"].Value;
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
