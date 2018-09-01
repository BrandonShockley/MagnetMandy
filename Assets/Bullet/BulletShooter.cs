using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletShooter : MonoBehaviour {

	public GameObject bullet;
	[Range(0, 5)]
	public float defaultSpeed = 1f;

	[Range(0, 90)]
	public float angleNoise = 0f; //noise in angle offset

	public void ShootBullet(Vector2 direction, Vector2 startPosition, float speed)
	{
		GameObject bulletInst = Instantiate(bullet, startPosition, Quaternion.FromToRotation(Vector3.up, direction));
		bulletInst.GetComponent<Bullet>().Shoot(direction, speed);
	}

	public void ShootBullet(Vector2 direction, Vector2 startPosition)
	{
		ShootBullet(direction, startPosition, defaultSpeed);
	}

	public void ShootBullet(Vector2 direction)
	{
		ShootBullet(direction, transform.position);
	}
}
