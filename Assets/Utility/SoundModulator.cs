using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundModulator : MonoBehaviour {

    private new AudioSource audio;

	// Use this for initialization
	void Awake () {
        audio = gameObject.AddComponent<AudioSource>();
	}

    public void PlayModClip(AudioClip clip, float minPitch = .9f, float maxPitch = 1.1f) {
        audio.pitch = Random.Range(minPitch, maxPitch);
        audio.PlayOneShot(clip);
    }

    public void PlayModClipLate(AudioClip clip, float minPitch = .9f, float maxPitch = 1.1f) {
        AudioSource audio = GameObject.Find("AudioPlayer").GetComponent<AudioSource>();
        audio.pitch = Random.Range(minPitch, maxPitch);
        audio.PlayOneShot(clip);
    }
}
