using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rozdzielczosci : MonoBehaviour {
	[SerializeField]
	Dropdown dropdown;

	// Use this for initialization
	void Start () {
		Resolution[] resolutions = Screen.resolutions;
		List<string> opcjeListy = new List<string> { };
		foreach (Resolution res in resolutions)
		{
			opcjeListy.Add(res.width + "x" + res.height);
		}
		dropdown.AddOptions(opcjeListy);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
