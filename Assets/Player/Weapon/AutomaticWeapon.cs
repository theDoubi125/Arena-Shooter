using UnityEngine;
using System.Collections;

public class AutomaticWeapon : Weapon {
    public float fireRate;
    private float fireTime;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        fireTime -= Time.deltaTime;
        if(Input.GetButton("Fire1") && fireTime <= 0)
        {
            fireTime = fireRate;
            Fire();
        }
	}
}
