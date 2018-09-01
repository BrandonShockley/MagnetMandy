using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	[SerializeField] Vector2 direction;
	[SerializeField] float speed;

	//When created from BulletShooter
	public void Shoot(Vector2 direction, float speed)
	{
		this.direction = direction.normalized;
		this.speed = speed;
	}

	// Move Bullet
	void Update () {
		transform.position = transform.position + (Vector3)(direction * speed * Time.deltaTime);
	}

	
}
