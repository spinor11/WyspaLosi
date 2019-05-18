using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaprawionaLodz : MonoBehaviour {

    public static bool plynie;
    int szybkoscObrotu=10;
    int szybkosLodzi = 1;
	// Use this for initialization
	
	
	// Update is called once per frame
	void Update ()
    {
        if (plynie)
        {
            transform.Translate(Vector2.up * Time.deltaTime *szybkosLodzi);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0,0,90), szybkoscObrotu*Time.deltaTime);
        }
	}
}
