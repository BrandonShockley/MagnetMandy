using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private int maxHealth;
    private int health;

    [SerializeField]
    private Sprite hitSprite;
    private Sprite mainSprite;

    private SpriteRenderer sr;
    [SerializeField]
    private new AudioSource audio;

    [SerializeField]
    private AudioClip hitSound;

    private IEnumerator playHitAnimation;

    void Start() {
        sr = GetComponent<SpriteRenderer>();

        health = maxHealth;
        mainSprite = sr.sprite;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            health--;
            StartCoroutine(PlayHitAnimation());
            audio.PlayOneShot(hitSound);
        }
    }

    private IEnumerator PlayHitAnimation() {
        bool isRed = false;
        for (int i = 0; i < 6; i++) {
            if (isRed) {
                sr.sprite = mainSprite;
            } else {
                sr.sprite = hitSprite;
            }
            isRed = !isRed;
            yield return new WaitForSeconds(.03f);
        }
    }
}
