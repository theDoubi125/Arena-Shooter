using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rigidbody;
    public float acceleration, deceleration;
    private Weapon weapon;

	// Use this for initialization
	void Start ()
    {
        this.rigidbody = GetComponent<Rigidbody2D>();
        this.weapon = GetComponent<Weapon>();
	}

    void Update()
    {
        Vector3 rot = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rot.z = 0;
        transform.rotation = Quaternion.LookRotation(rot);
    }
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        rigidbody.AddForce(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")) * acceleration);
        rigidbody.AddForce(-rigidbody.velocity * deceleration);
	}

    public void ChangeWeapon(Weapon weapon)
    {
        this.weapon = weapon;
    }
}
