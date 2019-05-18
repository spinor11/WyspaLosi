using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrzesuwanieNapisowKoncowych : MonoBehaviour {
    float szybkoscPrzesuwania = 25f;
    RectTransform rt;
	// Use this for initialization
	void Start ()
    {
        rt = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        
        rt.Translate(Vector2.up*szybkoscPrzesuwania*Time.deltaTime);

	}
}
