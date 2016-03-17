using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameTree.Operators
{
    public class ParallelOr : GameTreeOperator
    {
        public ParallelOr(GameTreeEngine _engine) : base(_engine)
        {
        }

        public ParallelOr(GameTreeOperator _operator) : base(_operator)
        {
        }

        public override void Activate()
        {
            base.Activate();

            foreach(var child in m_Childs)
            {
                child.Activate();
            }
        }

        // failure if all childs are failure, stops when one child is successful
        public override void Update()
        {
            base.Update();

            if (Childs == null || Childs.Count() == 0)
            {
                State = GameOperatorState.SUCCESS;
                return;
            }

            bool allFailure = true;
            foreach(var child in m_Childs)
            {
                if(child.State == GameOperatorState.SUCCESS)
                {
                    State = GameOperatorState.SUCCESS;
                    allFailure = false;
                }
                else if(child.State == GameOperatorState.INDETERMINATE)
                {
                    child.Update();
                    allFailure = false;
                }
            }

            if(allFailure)
            {
                State = GameOperatorState.FAILURE;
            }
        }
    }
}
