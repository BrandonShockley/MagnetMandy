using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    const float ACCEL_RATIO = 10f;
    const float DECCEL_RATIO = 15f;

    const float MAG_NOISE_GAIN = 1f;
    const float MAG_NOISE_FADE = 2f;

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private Vector2 accel;

    private Rigidbody2D rb;
    private new AudioSource audio;
    private Transform magnetCone;

    private IEnumerator startMagnet;
    private IEnumerator stopMagnet;
    
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        audio = GetComponent<AudioSource>();
        magnetCone = transform.GetChild(0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Move();
        TrackMouse();

        //Mag
        if (Input.GetMouseButton(0) && !magnetCone.gameObject.activeInHierarchy)
            StartCoroutine(startMagnet = StartMagnet());
        if (!Input.GetMouseButton(0) && magnetCone.gameObject.activeInHierarchy)
            StartCoroutine(stopMagnet = StopMagnet());
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
        rb.velocity += accel * Time.fixedDeltaTime;


        if (rb.velocity.magnitude > walkSpeed)
            rb.velocity = rb.velocity.normalized * walkSpeed;
    }

    private void TrackMouse() {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 playerToMouse = mousePosition - (Vector2)transform.position;

        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(playerToMouse.y, playerToMouse.x) - 90f);
    }

    private IEnumerator StartMagnet() {
        if (stopMagnet != null)
            StopCoroutine(stopMagnet);
        magnetCone.gameObject.SetActive(true);
        while (audio.volume < 1f) {
            audio.volume += Time.deltaTime;
            yield return 0;
        }
        audio.volume = 1f;
    }

    private IEnumerator StopMagnet() {
        if (startMagnet != null)
            StopCoroutine(startMagnet);
        magnetCone.gameObject.SetActive(false);
        while (audio.volume > 0f) {
            audio.volume -= Time.deltaTime * 2;
            yield return 0;
        }
        audio.volume = 0f;
    }
}
