using UnityEngine;
using System.Collections;

public class ShooterEnemy : ChargeEnemy
{
    public float avoidDistance, maxChangeDirTime, minChangeDirTime;
    public Transform projectilePrefab;

    public int avoidDirection;
    public float minFireRate = 0.2f, maxFireRate = 2;
    private float changeDirectionTime = 0;
    private float time = 0, reloadTime = 0;
    public float projectileSpeed;

    void Start ()
    {
        avoidDirection = Random.value > 0.5f ? -1 : 1;
        changeDirectionTime = minChangeDirTime + (maxChangeDirTime - minChangeDirTime) * Random.value;
        reloadTime = minFireRate + (maxFireRate - minFireRate) * Random.value;
        body = GetComponent<Rigidbody2D>();
        target = GameObject.FindGameObjectWithTag("Player");
    }
	
	void Update ()
    {
        if(Vector3.Distance(target.transform.position, transform.position) < avoidDistance)
        {
            Vector2 dir = target.transform.position - transform.position;
            Vector2 norm = new Vector2(dir.y, -dir.x).normalized;
            body.AddForce(norm * acceleration * avoidDirection);
            body.AddForce(-body.velocity * friction);
            if (body.velocity.sqrMagnitude > maxSpeed * maxSpeed)
                body.velocity = body.velocity.normalized * maxSpeed;
        }
        else base.Update();

        time += Time.deltaTime;
        if (changeDirectionTime < time)
        {
            time -= changeDirectionTime;
            avoidDirection *= -1;
        }

        reloadTime -= Time.deltaTime;
        if(reloadTime < 0)
        {
            Fire();
            reloadTime = minFireRate + (maxFireRate - minFireRate) * Random.value;
        }
    }

    public void Fire()
    {
        Transform projectile = Instantiate<Transform>(projectilePrefab);
        projectile.transform.position = transform.position;
        projectile.GetComponent<Projectile>().Init((target.transform.position - transform.position).normalized * projectileSpeed, gameObject, 1);
    }
}
