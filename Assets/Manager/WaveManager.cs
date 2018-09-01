using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour {

    private List<List<Transform>> waves;

    private int currentWave = 0;

	// Use this for initialization
	void Start () {
        //Init list of waves
		for (int i = 0; i < transform.childCount; i++) {
            waves.Add(new List<Transform>());
            for (int j = 0; j < transform.GetChild(i).childCount; j++) {
                waves[i].Add(transform.GetChild(i).GetChild(j));
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnWave() {
        foreach (Transform enemy in waves[currentWave]) {
            enemy.gameObject.SetActive(true);
        }
    }
}
