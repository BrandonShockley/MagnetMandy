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
	GameObject player;

	public Vector2 DirectionToPlayer
	{
		get
		{
			if (player != null)
			{
				return (player.transform.position - transform.position).normalized;
			}
			return Vector2.zero;
		}
	}

	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
	}

	#region Movement
	public bool MoveTowardsPlayer()
	{
		if (player == null)
		{
			return false;
		}

		Vector2 direction = DirectionToPlayer;
		if (direction == Vector2.zero)
		{
			return false;
		}

		rigid.velocity += direction * accelerationSpeed;

		if (rigid.velocity.sqrMagnitude > maxSpeed * maxSpeed) //optimization
		{
			rigid.velocity = rigid.velocity.normalized * maxSpeed;
		}

		return true;
	}

	public void ApplyDeceleration()
	{
		if (rigid.velocity.sqrMagnitude > decelerationSpeed * decelerationSpeed) {
			rigid.velocity -= rigid.velocity.normalized * decelerationSpeed;
		} else
		{
			rigid.velocity = Vector2.zero;
		}

	}
	#endregion

	public bool RotateTowardsPlayer()
	{
		if (player == null)
		{
			return false;
		}
		Vector2 dir = DirectionToPlayer;
		if (dir == Vector2.zero)
		{
			return false;
		}

		Debug.Log(dir);
		//transform.rotation.SetLookRotation(new Vector2(1,0));//Quaternion.FromToRotation(transform.forward, dir);

		transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(dir.y, dir.x) - 90f);

		return true;
	}
}
