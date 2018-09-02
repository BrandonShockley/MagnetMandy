using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private int maxHealth;
    private int health;

    [SerializeField]
    private Slider healthBar;

    [SerializeField]
    private Sprite hitSprite;
    private Sprite mainSprite;

    private SpriteRenderer sr;
    private SoundModulator soundMod;

    [SerializeField]
    private AudioClip hitSound;
    [SerializeField]
    private AudioClip deathSound;

    private IEnumerator playHitAnimation;

    public delegate void DeathAction();
    public event DeathAction OnDeath;

    void Start() {
        sr = GetComponent<SpriteRenderer>();
        soundMod = GetComponent<SoundModulator>();
        health = maxHealth;
        mainSprite = sr.sprite;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            if (playHitAnimation == null) {
                health--;
                healthBar.value = health;
                if (health <= 0) {
                    if (OnDeath != null)
                        OnDeath();
                    soundMod.PlayModClipLate(deathSound);
                    gameObject.SetActive(false);
                } else {
                    StartCoroutine(playHitAnimation = PlayHitAnimation());
                    soundMod.PlayModClip(hitSound);
                }
            }
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
            yield return new WaitForSeconds(.04f);
        }
        playHitAnimation = null;
    }
}
