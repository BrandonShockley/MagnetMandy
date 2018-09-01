using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLocomotion))]
public class Enemy : MonoBehaviour {

	public delegate void DeathAction();
	public static event DeathAction OnDeath;

	public int health = 1;
	public float distToShoot = 10f;

	public float fireRate = 0.2f;
	float bulletTimer = 0;

	EnemyLocomotion movement;
	BulletShooter shooter;

	void Start () {
		movement = GetComponent<EnemyLocomotion>();
		shooter = GetComponentInChildren<BulletShooter>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        movement.RotateTowardsPlayer();
		if (WithinDistToPlayer())
			movement.ApplyDeceleration();
		else
			movement.MoveTowardsPlayer();
	}

	private void Update()
	{
		Shoot();
	}

	//Collision
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Bullet")
		{
			health--;
			if (health <= 0)
			{
				if (OnDeath != null)
					OnDeath();
				Destroy(gameObject);
			}
		}
	}

	//Helpers
	void Shoot()
	{
		bulletTimer += Time.deltaTime;
		if (bulletTimer > fireRate)
		{
			shooter.ShootBullet(movement.DirectionToPlayer);
			bulletTimer = 0;
		}
	}

	bool WithinDistToPlayer()
	{
		return movement.DistanceToPlayer < distToShoot;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, distToShoot);
	}


}
