using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.GameTree.Functions
{
    public class SpawnEnemyFunction : GameTreeOperator
    {
		private SpawnManager spawnManager;

        string name;
        string value;
		
        public SpawnEnemyFunction(GameTreeEngine _engine) : base(_engine)
        { 
			spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		}

        public override void Activate()
        {
            base.Activate();
        }

        public override void Setup()
        {
            base.Setup();

            if (name == "Type" && spawnManager)
                spawnManager.AddToQueue(value, this);
        }

        public override void Update()
        {
            base.Update();
        }

        public override void ParseAttribute(string _name, string _value)
        {
            if(_name == "Type")
			{
                name = _name;
                value = _value;
// 				if(spawnManager != null)
// 					spawnManager.AddToQueue(_value, this);
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
