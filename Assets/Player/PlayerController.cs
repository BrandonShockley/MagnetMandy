using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

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
        Move();
        TrackMouse();


	}

    private void Move() {
        //WASD Control
        accel = (Vector2.right * ((Input.GetKey(KeyCode.D) ? 1f : 0f) - (Input.GetKey(KeyCode.A) ? 1f : 0f))
               + Vector2.up * ((Input.GetKey(KeyCode.W) ? 1f : 0f) - (Input.GetKey(KeyCode.S) ? 1f : 0f))).normalized * walkSpeed * ACCEL_RATIO;

        //Slow down if key not held
        if (!Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.A)) {
            accel.x = -rb.velocity.x * DECCEL_RATIO;
        }
        if (!Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) {
            accel.y = -rb.velocity.y * DECCEL_RATIO;
        }
        rb.velocity += accel * Time.deltaTime;


        if (rb.velocity.magnitude > walkSpeed)
            rb.velocity = rb.velocity.normalized * walkSpeed;
    }

    private void TrackMouse() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerToMouse = mousePosition - (Vector2)transform.position;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(playerToMouse.y, playerToMouse.x) - 90f);
    }
}
