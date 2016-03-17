using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameTree.Functions
{
    public class TimerFunction : GameTreeOperator
    {
        private float m_time = 0.0f;
        private float m_timer = 0.0f;

        public TimerFunction(GameTreeEngine _engine) : base(_engine)
        { }

        public TimerFunction(GameTreeOperator _operator) : base(_operator)
        { }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Setup()
        {
            base.Setup();

            m_timer = 0;
        }

        public override void Update()
        {
            base.Update();
            m_timer += Time.deltaTime;
            if(m_timer > m_time)
            {
                State = GameOperatorState.SUCCESS;
            }
        }

        public override void ParseAttribute(string _name, string _value)
        {
            if(_name == "Time")
            {
                m_time = float.Parse(_value);
            }
        }
    }
}
