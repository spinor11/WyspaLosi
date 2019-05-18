using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class getCurrentResolution : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = Screen.width + "x" + Screen.height;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
