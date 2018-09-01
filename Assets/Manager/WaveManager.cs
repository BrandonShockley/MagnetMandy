using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class WaveManager : MonoBehaviour {

    private List<List<Transform>> waves;

    private int currentWave = -1;
    public int CurrentWave {
        set {
            currentWave = value;
            waveCounter.text = currentWave.ToString();
        }
        get {
            return currentWave;
        }
    }

    private int currentEnemies;

    [SerializeField]
    private TextMeshProUGUI waveCounter;

	// Use this for initialization
	void Start () {
        //Init list of waves
        waves = new List<List<Transform>>();
		for (int i = 0; i < transform.childCount; i++) {
            waves.Add(new List<Transform>());
            for (int j = 0; j < transform.GetChild(i).childCount; j++) {
                waves[i].Add(transform.GetChild(i).GetChild(j));
            }
        }
        Enemy.OnDeath += OnEnemyDeath;
        NextWave();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnEnemyDeath() {
        currentEnemies--;
        if (currentEnemies <= 0) {
            if (currentWave - 1 == waves.Count) {
                NextWave();
            } else {
                waveCounter.text = "You win!";
                Debug.Log("You win!");
            }
        }
    }

    void NextWave() {
        CurrentWave++;
        currentEnemies = 0;
        foreach (Transform enemy in waves[CurrentWave]) {
            enemy.gameObject.SetActive(true);
            currentEnemies++;
        }
    }
}
