using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSFX : MonoBehaviour {


    public AudioSource source;
    public AudioClip music;

	void Start () {
        source.PlayOneShot(music);
	}
	

}
