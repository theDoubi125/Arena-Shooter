using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameTree.Functions
{
    public class FailOnPlayerDieFunction : GameTreeOperator
    {
        private LivingEntity m_player;

        public FailOnPlayerDieFunction(GameTreeEngine _engine) : base(_engine)
        { }

        public override void Setup()
        {
            base.Setup();

            foreach(var obj in GameObject.FindObjectsOfType<LivingEntity>())
            {
                if (obj.name == "Player")
                    m_player = obj;
            }
        }

        public override void Update()
        {
            base.Update();

            if (m_player.Health <= 0)
                State = GameOperatorState.FAILURE;
        }

    }
}
