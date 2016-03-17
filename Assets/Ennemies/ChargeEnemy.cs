using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]

public class ChargeEnemy : MonoBehaviour {
    protected GameObject target;
    protected Rigidbody2D body;
    public float maxSpeed, acceleration, friction;

    [SerializeField]
    private int contactDamage;
    private float timeContact = 0;

	void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        body = GetComponent<Rigidbody2D>();
	}
	
	public void Update ()
    {
        if (target != null && target.transform.position != transform.position)
        {
            body.AddForce((target.transform.position - transform.position).normalized * acceleration);
            body.AddForce(-body.velocity * friction);
            if (body.velocity.sqrMagnitude > maxSpeed * maxSpeed)
                body.velocity = body.velocity.normalized * maxSpeed;
        }
	}

    void OnCollisionStay2D(Collision2D Collision)
    {
        if (Collision.collider.tag == "Player")
        {
            timeContact += Time.fixedDeltaTime;
            if (timeContact > 0.7f)
            {
                timeContact = 0;
                Collision.collider.GetComponent<LivingEntity>().Damage(contactDamage);
            }
        }
    }
}
