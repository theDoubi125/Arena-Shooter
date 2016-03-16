using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameTree.Functions
{
    public class TimerFunction : GameTreeOperator
    {
        private float m_limit = 5.0f;
        private float m_time = 0.0f;

        public TimerFunction(GameTreeEngine _engine) : base(_engine)
        { }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}
