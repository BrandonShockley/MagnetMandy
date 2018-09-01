﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private Rigidbody2D rb;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    //When created from BulletShooter
    public void Shoot(Vector2 direction, float speed) {
        rb.velocity = direction.normalized * speed;
        UpdateHeading();
	}

	// Move Bullet
	void Update () {

	}

	public void InfluenceTrajectory(Vector2 direction) {
        rb.AddForce(direction, ForceMode2D.Force);
        UpdateHeading();
    }

    private void UpdateHeading() {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(rb.velocity.y, rb.velocity.x) - 90f);
    }
}
