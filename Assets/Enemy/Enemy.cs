using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(EnemyLocomotion))]
public class Enemy : MonoBehaviour {

	public delegate void DeathAction();
	public static event DeathAction OnDeath;

	public int health = 1;
	public float distToShoot = 10f;

    public enum FireMode {
        SINGLE,
        BURST
    }

    public FireMode fireMode;

	public float fireRate = 0.2f;
	float bulletTimer = 0;

	EnemyLocomotion movement;
	BulletShooter shooter;
    SpriteRenderer sr;

    Sprite mainSprite;
    [SerializeField]
    Sprite hitSprite;

    SoundModulator soundMod;
    [SerializeField]
    AudioClip hitSound;
    [SerializeField]
    AudioClip fireSound;
    [SerializeField]
    AudioClip deathSound;

    public bool isDead = false;

	void Start () {
		movement = GetComponent<EnemyLocomotion>();
		shooter = GetComponentInChildren<BulletShooter>();
        sr = GetComponent<SpriteRenderer>();
        soundMod = GetComponent<SoundModulator>();

        mainSprite = sr.sprite;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        movement.RotateTowardsPlayer();
		if (WithinDistToPlayer())
		{
			Collider2D closeEnemy = FindCloseEnemy();
			if (closeEnemy != null)
			{
				movement.MoveAwayFromPoint(closeEnemy.transform.position);
			} else
			{
				movement.ApplyDeceleration();
			}
		}
		else
			movement.MoveTowardsPlayer();
	}

	private void Update()
	{
		Shoot();
	}

	//Collision
	private void OnTriggerEnter2D(Collider2D collider)
	{
		if (collider.tag == "Bullet")
		{
			health--;
            
            if (health <= 0 && !isDead) {
                isDead = true;
                if (OnDeath != null)
                    OnDeath();
                Debug.Log("Death1");
                soundMod.PlayModClipLate(deathSound);
                Destroy(gameObject);
                Debug.Log("Death2");
            } else {
                StartCoroutine(PlayHitAnimation());
                soundMod.PlayModClipLate(hitSound);
            }
        }
	}

	//Helpers
	void Shoot()
	{
		bulletTimer += Time.deltaTime;
		if (bulletTimer > fireRate && CanSeePlayer() && FindCloseEnemy() == null)
		{
            StartCoroutine(Fire());
			
		}
	}

	bool WithinDistToPlayer()
	{
		return movement.DistanceToPlayer < distToShoot;
	}

	bool CanSeePlayer()
	{
		Vector2 dir = movement.DirectionToPlayer;
		RaycastHit2D hit = Physics2D.BoxCast((Vector2)transform.position + dir,new Vector2(.5f, .5f), 0f, dir, movement.DistanceToPlayer);
		Debug.DrawLine(transform.position, transform.position + (Vector3)(dir * movement.DistanceToPlayer));

		if (hit.collider != null)
		{
			if (hit.collider.tag == "Obstacle" || hit.collider.tag == "Enemy")
			{
				return false;
			}
		}
		return true;
	}

	Collider2D FindCloseEnemy()
	{
		float dist = 0.5f;
		Debug.DrawLine(transform.position, transform.position + Vector3.right * dist);
		RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, dist, Vector2.zero);

		foreach (RaycastHit2D hit in hits)
		{
			if (hit.collider.tag == "Enemy" && hit.collider.transform != transform)
			{
				return hit.collider;
			}
		}
		return null;
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawWireSphere(transform.position, distToShoot);
	}

    private IEnumerator Fire() {
        for (int i = 0; i < (fireMode == FireMode.BURST ? 3 : 1); i++) {
            shooter.ShootBullet(movement.DirectionToPlayer);
            soundMod.PlayModClip(fireSound);
            bulletTimer = Random.Range(-.5f, .5f);
            yield return new WaitForSeconds(.1f);
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
    }

}
