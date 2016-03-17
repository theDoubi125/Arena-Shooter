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
        int healthBoss;

        System.Random rnd = new System.Random();
		
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

            if (Name == "Type" && spawnManager)
            {
                if (value == "BossEnemy")
                    spawnManager.AddToQueue(value, this, healthBoss);
                else
                    spawnManager.AddToQueue(value, this, 0);
            }
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
                Name = _name;
                value = _value;
			}
            else if (_name == "Health")
            {
                healthBoss = Int32.Parse(_value);
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
                GameTreeElement father = m_Engine.GetTreeElementOfOperator(this).Father;
                if (m_Engine.nbShooterEnemy > 1 && father != null)
                {
                    father.Remove(this);
                    --m_Engine.nbShooterEnemy;
                }
            }
            else if (value == "BasicEnemy")
            {
                GameTreeElement father = m_Engine.GetTreeElementOfOperator(this).Father;
                if (m_Engine.nbBasicEnemy > 1 && father != null)
                {
                    father.Remove(this);
                    --m_Engine.nbBasicEnemy;
                }
            }
            else if (value == "BossEnemy")
            {
                GameTreeElement father = m_Engine.GetTreeElementOfOperator(this).Father;
                if (father != null)
                {
                    --healthBoss;
                }
            }
        }

        protected override void Harder()
        {
            base.Harder();

            GameTreeElement father = m_Engine.GetTreeElementOfOperator(this).Father;
            if (father != null && Name != "Root")
            {
                if (value == "BossEnemy")
                {
                    ++healthBoss;
                }
                else
                {
                    int nb = rnd.Next(0, 2);
                    if (nb == 0)
                    {
                        father.Add(new GameTreeElement(m_Engine.GetTreeElementOfOperator(this)));

                        if (value == "BasicEnemy")
                        {
                            ++m_Engine.nbBasicEnemy;
                        }
                        else if (value == "ShooterEnemy")
                        {
                            ++m_Engine.nbShooterEnemy;
                        }
                    }
                    else if (father.Father != null && Name != "Root")
                    {
                        father.Father.Add(new GameTreeElement(father));
                    }
                }
            }
        }
    }
}
