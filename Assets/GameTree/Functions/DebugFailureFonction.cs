using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameTree.Functions
{
    public class DebugFailureFonction : GameTreeOperator
    {
        float m_timer = 0;

        public DebugFailureFonction(GameTreeEngine _engine) : base(_engine)
        { }

        public DebugFailureFonction(DebugFailureFonction _operator) : base(_operator)
        { }

        public override void Setup()
        {
            base.Setup();

            m_timer = 0;
        }

        public override void Update()
        {
            base.Update();

            m_timer += Time.deltaTime;
            if (m_timer > 2.0f)
                State = GameOperatorState.FAILURE;
        }
    }
}
