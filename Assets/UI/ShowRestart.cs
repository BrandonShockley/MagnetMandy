using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRestart : MonoBehaviour {

    private Button button;
    private Image image;

	// Use this for initialization
	void Start () {
        button = GetComponent<Button>();
        image = GetComponent<Image>();
        FindObjectOfType<PlayerHealth>().OnDeath += EnableRestart;
	}

    private void EnableRestart() {
        button.enabled = true;
        image.enabled = true;
    }
}
