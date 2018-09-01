using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLocomotion))]
public class Enemy : MonoBehaviour {

	EnemyLocomotion movement;

	void Start () {
		movement = GetComponent<EnemyLocomotion>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        movement.RotateTowardsPlayer();
        movement.MoveTowardsPlayer();
		//movement.ApplyDeceleration();
	}
}
