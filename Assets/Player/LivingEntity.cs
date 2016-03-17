using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour 
{
    public int maxHealth;
    private int health;

	// Use this for initialization
	void Start ()
    {
        health = maxHealth;
	}

    public void Damage(int damage)
    {
        health -= damage;
        if (health <= 0)
		{
			if(this.tag.Equals("Player"))
				GameOver();
			else
				Destroy(gameObject);			
		}
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
    }
	
	private void GameOver()
	{
		GameObject gameManagerGO = GameObject.Find("GameManager");
		
		if(gameManagerGO != null)
		{
			GameManager gameManager = gameManagerGO.GetComponent<GameManager>();
			if (gameManager != null)
				gameManager.GameOver();
		}
	}
}
