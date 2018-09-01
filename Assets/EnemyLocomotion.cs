using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLocomotion : MonoBehaviour {

	[Range(0,1)]
	public float accelerationSpeed = 0.2f;

	[Range(0, 1)]
	public float decelerationSpeed = 0.2f;

	[Range(0, 5)]
	public float maxSpeed;

	Rigidbody2D rigid;

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


}
