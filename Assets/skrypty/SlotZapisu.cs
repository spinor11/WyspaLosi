using System.Globalization;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SlotZapisu : MonoBehaviour {
	public int ktory;

	// Use this for initialization
	void OnEnable () {
		if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Menu")) foreach (Text text in GetComponentsInChildren<Text>()) text.fontSize = 24;

		if (File.Exists(Application.dataPath + "/saves/" + ktory + ".dat")) {
			string czasModyfikacji =  File.GetLastWriteTime(Application.dataPath + "/saves/" + ktory + ".dat").ToString("G", CultureInfo.CreateSpecificCulture("pl-PL"));
			GetComponentsInChildren<Text>()[1].text = czasModyfikacji.Substring(11) + "\n" + czasModyfikacji.Substring(0, 11);
		}
	}
}
