using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muzyka : MonoBehaviour {
    AudioSource muzykaAS;
    [SerializeField]
    List<AudioClip> muzycznaLista;
    int ClipNR;

    // Use this for initialization
    void Start () {
        muzykaAS = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()

    {
        if (!muzykaAS.isPlaying)
        {
            ClipNR += 1;
            muzykaAS.clip = muzycznaLista[ClipNR];
            muzykaAS.Play();
            if (ClipNR == muzycznaLista.Count-1)
            {
                ClipNR=0;
            }
        }
    }
}
