using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameTree
{
    public class GameTreeEngine : MonoBehaviour
    {
        GameTree m_GameTree;

        void Update()
        {
            m_GameTree.Update();
        }

        public IEnumerable<GameTreeOperator> GetChildsOfOperator(GameTreeOperator _operator)
        {
            foreach(var elem in m_GameTree.GetNodes())
            {
                if(elem.Operator == _operator)
                {
                    return elem.Childs.Select<GameTreeElement, GameTreeOperator>(e => e.Operator);
                }
            }

            return null;
        }
    }
}
