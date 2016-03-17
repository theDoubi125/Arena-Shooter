using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.GameTree.Operators
{
    public class First : GameTreeOperator
    {

        public First(GameTreeEngine _engine) : base(_engine)
        { }

        // Activate all childs on activation
        public override void Activate()
        {
            base.Activate();

            foreach (var child in m_Childs)
            {
                child.Activate();
            }
        }

        public override void Update()
        {
            base.Update();

            if(Childs == null || Childs.Count() == 0)
            {
                State = GameOperatorState.SUCCESS;
                return;
            }

            foreach(var child in Childs)
            {
                child.Update();

                if(child.State != GameOperatorState.INDETERMINATE)
                {
                    State = child.State;
                    return;
                }
            }
        }
    }
}
