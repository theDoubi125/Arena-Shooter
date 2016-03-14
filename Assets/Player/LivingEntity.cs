using UnityEngine;
using System.Collections;

public class LivingEntity : MonoBehaviour {
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
            Destroy(gameObject);
    }

    public void Heal(int heal)
    {
        health += heal;
        if (health > maxHealth)
            health = maxHealth;
    }
}
