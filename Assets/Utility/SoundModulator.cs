using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundModulator : MonoBehaviour {

    private new AudioSource audio;

	// Use this for initialization
	void Awake () {
        audio = gameObject.AddComponent<AudioSource>();
	}

    public void PlayModulatedClip(AudioClip clip, float minPitch = .9f, float maxPitch = 1.1f) {
        audio.pitch = Random.Range(minPitch, maxPitch);
        audio.PlayOneShot(clip);
    }
}
