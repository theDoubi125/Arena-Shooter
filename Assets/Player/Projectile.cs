using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour {
    private Rigidbody2D body;
    private GameObject launcher;
    private int damage;

	// Use this for initialization
	void Start ()
    {
        body = GetComponent<Rigidbody2D>();
    }

    public void Init(Vector2 velocity, GameObject launcher, int damage)
    {
        GetComponent<Rigidbody2D>().velocity = velocity;
        this.launcher = launcher;
        this.damage = damage;
    }
	
	// Update is called once per frame
	void Update ()
    {
        float angle = Vector2.Angle(Vector2.right, body.velocity);
        if (body.velocity.y < 0)
            angle = -angle;
		/* dafuq ??*/		
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
		
		/* If it moves out of the screen we destroy it */
		Vector3 vpPosition = Camera.main.WorldToViewportPoint(this.transform.position);
		if(vpPosition.x < 0.001 || vpPosition.x > 1.001 || vpPosition.y < 0.001 || vpPosition.y > 1.001)
            Destroy(gameObject);
			
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //print(other);
        if(other.gameObject != launcher)
        {
            LivingEntity entity = other.GetComponent<LivingEntity>();
            if (entity != null)
                entity.Damage(damage);
            Destroy(gameObject);
        }
    }
}
