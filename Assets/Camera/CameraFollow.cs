using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private GameObject target;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 partWayPoint = Vector2.Lerp(target.transform.position, mousePosition, Input.GetKey(KeyCode.LeftShift) ? .2f : 0f);
        transform.position = Vector3.Lerp(transform.position, new Vector3(partWayPoint.x, partWayPoint.y, transform.position.z), .3f);
	}
}
