using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameMenu : MonoBehaviour {

	public GameObject menu;

	void Start () {
		
	}

	public void Podswietl (GameObject przysisk)  {
		przysisk.GetComponent<Text> ().color = new Color32 (255, 0, 0, 255);
	}

	public void PrzywrocKolor (GameObject przysisk)  {
		przysisk.GetComponent<Text> ().color = new Color32 (255, 255, 255, 255);
	}

	public void ZapiszGre(GameObject subMenu) {
		subMenu.GetComponentInChildren<Text>().text = "Zapisz grę";
	}

	public void WczytajGre(GameObject subMenu) {
		subMenu.GetComponentInChildren<Text>().text = "Wczytaj grę";
	}

	public void Opcje(GameObject subMenu) {
		subMenu.GetComponentInChildren<Text>().text = "Opcje";
	}

	public void Wznow () {
		menu.SetActive(false);
		Time.timeScale = 1;
	}

	public void Powrot () {
		SceneManager.LoadScene (0,LoadSceneMode.Single);
	}

	public void PelnyEkran(Toggle toggle) {
		Screen.fullScreen = toggle.isOn;
	}

	public void ZmianaRozdzielczosci(Dropdown dropdown) {
		string[] rozdzielczosc = dropdown.options[dropdown.value].text.Split('x');
		Screen.SetResolution(int.Parse(rozdzielczosc[0]), int.Parse(rozdzielczosc[1]), Screen.fullScreen);
	}
}
