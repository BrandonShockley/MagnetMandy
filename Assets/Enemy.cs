using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLocomotion))]
public class Enemy : MonoBehaviour {

	public delegate void DeathAction();
	public static event DeathAction OnDeath;

	public int health = 1;

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
        movement.MoveTowardsPlayer();
		//movement.ApplyDeceleration();
	}

	private void Update()
	{
		Shoot();
	}

	void Shoot()
	{
		bulletTimer += Time.deltaTime;
		if (bulletTimer > fireRate)
		{
			shooter.ShootBullet(movement.DirectionToPlayer);
			bulletTimer = 0;
		}
	}

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
}
