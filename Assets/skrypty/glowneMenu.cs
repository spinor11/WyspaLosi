using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class glowneMenu : MonoBehaviour {

	public void NowaGra (GameObject subMenu) {
		subMenu.GetComponentInChildren<Text> ().text = "Nowa gra";
		Time.timeScale = 1;
		SceneManager.LoadScene(1,LoadSceneMode.Single);
	}

	public void WczytajGre (GameObject subMenu) {
		subMenu.GetComponentInChildren<Text> ().text = "Wczytaj grę";
	}

	public void Opcje (GameObject subMenu) {
		subMenu.GetComponentInChildren<Text> ().text = "Opcje";
	}

	public void Wyjscie () {
		Application.Quit();
	}

	public void Podswietl (GameObject przysisk)  {
		przysisk.GetComponent<Text> ().color = new Color32 (255, 0, 0, 255);
	}

	public void PrzywrocKolor (GameObject przysisk)  {
		przysisk.GetComponent<Text> ().color = new Color32 (255, 255, 255, 255);
	}

	public void PelnyEkran (Toggle toggle) {
		Screen.fullScreen = toggle.isOn;
	}

	public void ZmianaRozdzielczosci (Dropdown dropdown) {
		string[] rozdzielczosc = dropdown.options[dropdown.value].text.Split('x');
		Screen.SetResolution(int.Parse(rozdzielczosc[0]), int.Parse(rozdzielczosc[1]), Screen.fullScreen);
	}
}
