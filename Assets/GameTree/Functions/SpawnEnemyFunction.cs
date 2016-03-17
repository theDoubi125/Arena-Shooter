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

        public string value;
		
        public SpawnEnemyFunction(GameTreeEngine _engine) : base(_engine)
        { 
			spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
		}

        public SpawnEnemyFunction(SpawnEnemyFunction _operator) : base(_operator)
        {
            spawnManager = _operator.spawnManager;
            Name = _operator.Name;
            value = _operator.value;
        }

        public override void Activate()
        {
            base.Activate();
        }

        public override void Setup()
        {
            base.Setup();

            if (Name == "Type" && spawnManager)
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
                Name = _name;
                value = _value;
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

        protected override void Easier()
        {
            base.Easier();

            if (value == "ShooterEnemy")
            {
                if (m_Engine.nbShooterEnemy > 1)
                {
                    m_Engine.GetTreeElementOfOperator(this).Father.Remove(this);
                    --m_Engine.nbShooterEnemy;
                }
            }
            else if (value == "BasicEnemy")
            {
                if (m_Engine.nbBasicEnemy > 1)
                {
                    m_Engine.GetTreeElementOfOperator(this).Father.Remove(this);
                    --m_Engine.nbBasicEnemy;
                }
            }
        }

        protected override void Harder()
        {
            base.Harder();

            m_Engine.GetTreeElementOfOperator(this).Father.Add(new GameTreeElement(m_Engine.GetTreeElementOfOperator(this)));
            
            if (value == "BasicEnemy")
            {
                ++m_Engine.nbBasicEnemy;
            }
            else if (value == "ShooterEnemy")
            {
                ++m_Engine.nbShooterEnemy;
            }
        }
    }
}
