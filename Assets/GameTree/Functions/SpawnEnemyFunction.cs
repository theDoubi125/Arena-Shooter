using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameTree.Functions
{
    public class SpawnEnemyFunction : GameTreeOperator
    {
		private SpawnManager spanwManager;
		
        public SpawnEnemyFunction(GameTreeEngine _engine) : base(_engine)
        { 
			spanwManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		}

        public override void Activate()
        {
            base.Activate();
        }

        public override void Setup()
        {
            base.Setup();
        }

        public override void Update()
        {
            base.Update();
        }

        public override void ParseAttribute(string _name, string _value)
        {
            if(_name == "Type")
			{
				if(spanwManager != null)
					spanwManager.AddToQueue(_value, this);
			}
        }
		
		public void HasPlayerWon(int success)
		{
			if(State == GameOperatorState.INDETERMINATE)
			{
				if(success == 1)
					State = GameOperatorState.SUCCESS;		
				else if(success == -1)
					State = GameOperatorState.FAILURE;					
			}				
		}
    }
}
