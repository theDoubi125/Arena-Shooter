using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public Transform projectilePrefab;

    public float bulletSpeed = 5;
    public int damage = 1;

	// Use this for initialization
	void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            Fire();
        }
	}

    public void Fire()
    {
        Transform instance = Instantiate<Transform>(projectilePrefab);
        instance.transform.position = transform.position;
        Vector3 rot = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        rot.z = 0;

        instance.GetComponent<Projectile>().Init(rot.normalized * bulletSpeed, gameObject, damage);
    }
}
