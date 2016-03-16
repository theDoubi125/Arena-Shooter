using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameTree
{
    public class GameTree
    {
        GameTreeElement m_root;

        public GameTree()
        {
            
        }

        public void Update()
        {
            m_root.Operator.Update();
        }

        public void AddOperator(string _parentID, GameTreeOperator _operator)
        {
            var parent = GetNode(_parentID);
            if(parent != null)
            {
                parent.Add(_operator);
            }
        }

        public void AddElement(string _parentID, GameTreeElement _elem)
        {
            var parent = GetNode(_parentID);
            if(parent != null)
            {
                parent.Add(_elem);
            }
        }

        public GameTreeElement GetNode(string _id)
        {
            foreach(var node in GetNodes())
            {
                if (node.Operator.Name == _id)
                    return node;
            }

            return null;
        }

        public IEnumerable<GameTreeElement> GetNodes()
        {
            Stack<GameTreeElement> stack = new Stack<GameTreeElement>();
            stack.Push(m_root);
            while(stack.Count > 0)
            {
                GameTreeElement elem = stack.Pop();
                yield return elem;
                foreach(var child in elem.Childs)
                {
                    stack.Push(child);
                    yield return child;
                }
            }
        }
    }
}
