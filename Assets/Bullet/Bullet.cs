using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private const float FADE_SPEED = 10f;

    private float speed;

    private Rigidbody2D rb;
    private SpriteRenderer childSr;
    private LineRenderer lr;
    private IEnumerator fadeLine;

    private Transform player;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        childSr = transform.GetChild(0).GetComponent<SpriteRenderer>();
        lr = GetComponent<LineRenderer>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //When created from BulletShooter
    public void Shoot(Vector2 direction, float speed) {
        this.speed = speed;
        rb.velocity = direction.normalized * speed;
        UpdateHeading();
	}

	// Move Bullet
	void Update () {
        //Update line positioning
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, player.position);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (!other.CompareTag("MagCone")) {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("MagCone")) {
            StartFade();
        }
    }

    public void InfluenceTrajectory(Vector2 direction) {
        rb.AddForce(direction, ForceMode2D.Force);
        UpdateHeading();
        MaintainSpeed();
    }

    private void UpdateHeading() {
        transform.rotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(rb.velocity.y, rb.velocity.x) - 90f);
    }

    private void MaintainSpeed() {
        rb.velocity = rb.velocity.normalized * speed;
    }

    private void StartFade() {
        if (fadeLine != null) {
            StopCoroutine(fadeLine);
        }
        StartCoroutine(fadeLine = FadeLine());
    }
    private IEnumerator FadeLine() {
        lr.enabled = true;
        //Setup initial alpha
        lr.startColor = new Color(lr.startColor.r, lr.startColor.g, lr.startColor.b, 1f);
        lr.endColor = lr.startColor;
        childSr.color = new Color(childSr.color.r, childSr.color.g, childSr.color.b, 1f);
        yield return 0;
        
        while (lr.startColor.a > 0f) {
            //Fade effect
            float alpha = lr.startColor.a - Time.deltaTime * FADE_SPEED;
            lr.startColor = new Color(lr.startColor.r, lr.startColor.g, lr.startColor.b, alpha);
            lr.endColor = lr.startColor;
            childSr.color = new Color(childSr.color.r, childSr.color.g, childSr.color.b, alpha);
            yield return 0;
        }
        lr.enabled = false;
    }
}
