using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameTree.Operators
{
    public class ParallelAnd : GameTreeOperator
    {
        public ParallelAnd(GameTreeEngine _engine) : base(_engine)
        {
        }

        public ParallelAnd(GameTreeOperator _operator) : base(_operator)
        {
        }

        // Activate all childs on activation
        public override void Activate()
        {
            base.Activate();

            foreach(var child in m_Childs)
            {
                child.Activate();
            }
        }

        // Update childs and check for success state, stop if one failure
        public override void Update()
        {
            base.Update();

            if (Childs == null || Childs.Count() == 0)
            {
                State = GameOperatorState.SUCCESS;
                return;
            }

            bool allSuccess = true;
            foreach(var child in m_Childs)
            {
                if(child.State == GameOperatorState.INDETERMINATE)
                {
                    child.Update();
                    allSuccess = false;
                }
                else if(child.State == GameOperatorState.FAILURE)
                {
                    State = GameOperatorState.FAILURE;
                    allSuccess = false;
                }
            }

            if(allSuccess)
            {
                State = GameOperatorState.SUCCESS;
            }
        }

    }
}
