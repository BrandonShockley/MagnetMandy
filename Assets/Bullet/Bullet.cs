using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    //When created from BulletShooter
    public void Shoot(Vector2 direction, float speed)
	{
        rb.velocity = direction.normalized * speed;
	}

	// Move Bullet
	void Update () {
	}

	
}
