using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace Assets.GameTree
{
    public class GameTree
    {		
        GameTreeElement m_root;
        public string Name { get; private set; }
        public int DifficultyRating { get; set; }

        public GameTree(string _name, int _difficulty)
        {
            Name = _name;
            DifficultyRating = _difficulty;
        }

        public void Reset()
        {
            m_root.Operator.Setup();
        }

        public void Start()
        {
            m_root.Operator.Activate();
        }

        public void Update()
        {
            if(m_root.Operator.State == GameTreeOperator.GameOperatorState.INDETERMINATE)
                m_root.Operator.Update();
        }

        public void SetRoot(GameTreeOperator _operator)
        {
            m_root = new GameTreeElement(_operator);
        }

        public void SetRoot(GameTreeElement _root)
        {
            m_root = _root;
        }

        public GameTreeOperator GetRootOperator()
        {
            return m_root.Operator;
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
