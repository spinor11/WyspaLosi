using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class STrzalaLosia : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update () {
        ZastrzelGracza();
	}
    public void ZastrzelGracza()
    {

        transform.position = Vector2.MoveTowards(transform.position, LosGracz.pozycja.position, Time.deltaTime * 5);
    }
}
