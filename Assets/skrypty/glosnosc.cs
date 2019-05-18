using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class glosnosc : MonoBehaviour {
	public AudioMixer audioMixer;

	public void zmienGlosnosc (float glosnosc) {
		audioMixer.SetFloat("glosnosc", glosnosc);
	}
}
