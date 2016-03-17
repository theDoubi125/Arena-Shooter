using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameTree.Operators
{
    public class SequentialAnd : GameTreeOperator
    {
        int m_CurrentlyActive = 0;

        public SequentialAnd(GameTreeEngine _engine) : base(_engine)
        {}

        public override void Activate()
        {
            base.Activate();

            if(m_CurrentlyActive < Childs.Count())
                Childs.ElementAt(m_CurrentlyActive).Activate();
        }

        public override void Update()
        {
            base.Update();

            if(Childs == null)
            {
                State = GameOperatorState.SUCCESS;
                return;
            }

            if(Childs.ElementAt(m_CurrentlyActive).State == GameOperatorState.SUCCESS)
            {
                ++m_CurrentlyActive;
                if(m_CurrentlyActive < Childs.Count()) // if there is still something to activate
                {
                    Childs.ElementAt(m_CurrentlyActive).Activate();
                }
                else // Everything is a success
                {
                    State = GameOperatorState.SUCCESS;
                }
            }
            else if(Childs.ElementAt(m_CurrentlyActive).State == GameOperatorState.INDETERMINATE)
            {
                Childs.ElementAt(m_CurrentlyActive).Update();
            }
            else // Current object is a failure
            {
                State = GameOperatorState.FAILURE;
            }
        }
    }
}
