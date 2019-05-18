using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PelnyEkran : MonoBehaviour {
	[SerializeField]
	Toggle toggle;


	// Use this for initialization
	void Start () {
		toggle.isOn = Screen.fullScreen;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
