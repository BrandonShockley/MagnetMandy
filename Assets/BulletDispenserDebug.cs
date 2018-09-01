using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BulletShooter))]
public class BulletDispenserDebug : MonoBehaviour {

	BulletShooter shooter;
	public Vector2 direction;

	void Start () {
		shooter = GetComponent<BulletShooter>();
	}
	
	void Update () {
		if (Input.GetKeyDown(KeyCode.C))
		{
			shooter.ShootBullet(direction);
		}
	}
}
