using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCone : MonoBehaviour {

    public const float CONE_ANGLE = 30;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            other.GetComponent<Bullet>().InfluenceTrajectory(transform.parent.position - other.transform.position);
        }
    }
}
