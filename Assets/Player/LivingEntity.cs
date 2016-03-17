using UnityEngine;
using System.Collections;

using Assets.GameTree.Functions;

public class LivingEntity : MonoBehaviour 
{
    public int maxHealth;
    private int health;

    public int Health { get { return health; } set { health = value; } }
	
	private SpawnEnemyFunction spawnEnemyFunction = null;



	// Use this for initialization
	void Start ()
    {
        Reset();
	}

    public void Reset()
    {
        health = maxHealth;
    }

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
		{
			
			if(this.tag.Equals("Player"))
			{
				/* If the player is killed, the enemies have won */
				GameObject[] enemiesObjects = GameObject.FindGameObjectsWithTag("Enemy");
        
				foreach (GameObject enemyObject in enemiesObjects) 
				{
					LivingEntity enemyEntity = enemyObject.GetComponent<LivingEntity>();			
					if(enemyEntity != null)
						enemyEntity.SetHasWon(-1);
				}
				
				PlayerDied();
			}
			else
			{
				/* If the enemy is killed, the player has won */
				this.SetHasWon(1);				
				Destroy(gameObject);				
			}		
		}
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
    }
	
	private void PlayerDied()
	{		
	}
	
	public void SetSpawnEnemyFunction(SpawnEnemyFunction function)
	{
		this.spawnEnemyFunction = function;
	}
	
	public void SetHasWon(int success)
	{
		if(this.spawnEnemyFunction != null)
			this.spawnEnemyFunction.HasPlayerWon(success);		
	}
}
