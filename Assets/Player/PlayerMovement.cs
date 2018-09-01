using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    const float ACCEL_RATIO = 10f;
    const float DECCEL_RATIO = 15f;

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private Vector2 accel;

    private Rigidbody2D rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

        //WASD Control
        accel = (Vector2.right * ((Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f: 0f))
               + Vector2.up * ((Input.GetKey(KeyCode.W) ? 1f : 0f) - (Input.GetKey(KeyCode.S) ? 1f : 0f))).normalized * walkSpeed * ACCEL_RATIO;

        //Slow down if key not held
        bool xDeccelerating = false;
        bool yDeccelerating = false;
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
            accel.x = -rb.velocity.x * DECCEL_RATIO;
            xDeccelerating = true;
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
            accel.y = -rb.velocity.y * DECCEL_RATIO;
            yDeccelerating = true;
        }

        Vector2 prevVelocity = rb.velocity;
        rb.velocity += accel * Time.deltaTime;
        //Ensure no rebounding decceleration
        /*if (xDeccelerating && Mathf.Sign(prevVelocity.x) != Mathf.Sign(rb.velocity.x))
            rb.velocity.Set(0, rb.velocity.y);
        if (yDeccelerating && Mathf.Sign(prevVelocity.y) != Mathf.Sign(rb.velocity.y))
            rb.velocity.Set(rb.velocity.x, 0);*/


        if (rb.velocity.magnitude > walkSpeed)
            rb.velocity = rb.velocity.normalized * walkSpeed;

        //Mouse tracking
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerToMouse = mousePosition - (Vector2)transform.position;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(playerToMouse.y, playerToMouse.x) - 90f);
	}
}
