using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetCone : MonoBehaviour {

    public const float CONE_ANGLE = 30;
    public const float SLOW_RATIO = 2f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerStay2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            other.GetComponent<Bullet>().InfluenceTrajectory(transform.parent.position - other.transform.position);
            //other.transform.parent = this.transform;
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.CompareTag("Bullet")) {
            other.GetComponent<Bullet>().InfluenceTrajectory(transform.parent.position - other.transform.position);
            //other.transform.parent = GameObject.FindGameObjectWithTag("Dynamic").transform;
        }
    }

    public void ReleaseChildren() {
        for (int i = transform.childCount - 1; i >= 0; i--) {
            transform.GetChild(i).parent = GameObject.FindGameObjectWithTag("Dynamic").transform;
        }
    }
}
