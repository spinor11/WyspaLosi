using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ZapisILadowanieGry : MonoBehaviour {
	public GameObject wczytywanie;
	public Slider slider;
	public GameObject menu;
	public Komunikat PKomunikat;
	public GameObject komunikat;

	private GameObject wilkPrefab;
	private GameObject losPrefab;
	private GameObject orzelPrefab;
	private GameObject rudaPrefab;
	private GameObject kamienPrefab;
	private GameObject pomidorPrefab;
	private GameObject ogniskoPrefab;
	private GameObject grzybCzerwonyPrefab;
	private GameObject grzybBrazowyPrefab;
	private GameObject drzewo1Prefab;
	private GameObject drzewo2Prefab;
	private GameObject drzewo3Prefab;

	private GameObject[] rosliny;
	private GameObject[] rudy;
	private GameObject[] przeciwnicy;
	private GameObject[] ognisko;
	private GameObject[] przedmioty;

	private GameObject gracz;

	private GameObject[] ekwipunek;

	private static DaneZapisu daneZapisu = new DaneZapisu();

	void Start() {
		wilkPrefab = (GameObject)Resources.Load("LoadingElements/Wilk");
		losPrefab = (GameObject)Resources.Load("LoadingElements/Los");
		orzelPrefab = (GameObject)Resources.Load("LoadingElements/Orzel");
		rudaPrefab = (GameObject)Resources.Load("LoadingElements/Rudazelaza");
		kamienPrefab = (GameObject)Resources.Load("LoadingElements/Kamien");
		pomidorPrefab = (GameObject)Resources.Load("LoadingElements/RoslinaPomidor");
		ogniskoPrefab = (GameObject)Resources.Load("LoadingElements/Ognisko");
		grzybCzerwonyPrefab = (GameObject)Resources.Load("LoadingElements/CzerwonyGrzyb");
		grzybBrazowyPrefab = (GameObject)Resources.Load("LoadingElements/BrazowyGrzyb");
		drzewo1Prefab = (GameObject)Resources.Load("LoadingElements/Drzewo_1 1");
		drzewo2Prefab = (GameObject)Resources.Load("LoadingElements/Drzewo2");
		drzewo3Prefab = (GameObject)Resources.Load("LoadingElements/drzewo3");

		DontDestroyOnLoad(this);
	}

	private void PobierzElementy() {
		rosliny = GameObject.FindGameObjectsWithTag("Roslina");
		rudy = GameObject.FindGameObjectsWithTag("Ruda");
		ognisko = GameObject.FindGameObjectsWithTag("Ognisko");
		przeciwnicy = GameObject.FindGameObjectsWithTag("Przeciwnik");
		przedmioty = GameObject.FindGameObjectsWithTag("Przedmiot");

		gracz = GameObject.FindGameObjectWithTag("Player");

		ekwipunek = GameObject.FindGameObjectsWithTag("Slot");
	}

	private void PobierzDane() {
		PobierzElementy();

		daneZapisu = new DaneZapisu();
		//gracz

		daneZapisu.gracz.pozycja = Vector2ToSer(gracz.transform.position);
		daneZapisu.gracz.aktualneZdrowie = gracz.GetComponent<Player>().AktualnePunktyZdrowia;
		daneZapisu.gracz.glod = gracz.GetComponent<Player>().AktualnePunktyGlodu;
		daneZapisu.gracz.doswiadczenie = gracz.GetComponent<Player>().AktualnePunktyDoswiadczenia;
		daneZapisu.gracz.pragnienie = gracz.GetComponent<Player>().AktualnePunktyPragnienia;

		//rosliny
		foreach (GameObject roslina in rosliny) {
			string[] prefabName = roslina.name.Split(' ');
			prefabName = prefabName[0].Split('(');

			if (prefabName[0] != null&& prefabName[0]!= "Pien"&& (roslina.GetComponentInChildren<NieruchomyObiekt>() != null|| roslina.GetComponent<NieruchomyObiekt>() != null)) {
				SerObjektZZyciem item = new SerObjektZZyciem {
					aktualneZdrowie = roslina.GetComponentInChildren<NieruchomyObiekt>()!=null?roslina.GetComponentInChildren<NieruchomyObiekt>().punktyZdrowia.AktualnaWartosc:roslina.GetComponent<NieruchomyObiekt>().punktyZdrowia.AktualnaWartosc,
					pozycja = Vector2ToSer(roslina.transform.position)
				};

				if (prefabName[0] == "Drzewo_1") daneZapisu.drzewa1.Add(item);
				else if (prefabName[0] == "Drzewo2") daneZapisu.drzewa2.Add(item);
				else if (prefabName[0] == "drzewo3") daneZapisu.drzewa3.Add(item);
				else if (prefabName[0] == "RoslinaPomidor") daneZapisu.pomidory.Add(item);
				else if (prefabName[0] == "CzerwonyGrzyb") daneZapisu.grzybyCzerwone.Add(item);
				else if (prefabName[0] == "BrazowyGrzyb") daneZapisu.grzybyBrazowe.Add(item);
			}
		}

		//rudy
		foreach (GameObject ruda in rudy) {
			string[] prefabName = ruda.name.Split(' ');
			prefabName = prefabName[0].Split('(');


			if (prefabName[0] != null) {
				SerObjektZZyciem item = new SerObjektZZyciem {
					aktualneZdrowie = ruda.GetComponent<NieruchomyObiekt>().punktyZdrowia.AktualnaWartosc,
					pozycja = Vector2ToSer(ruda.transform.position)
				};

				if (prefabName[0] == "RudaZelaza") daneZapisu.rudy.Add(item);
				else if (prefabName[0] == "Kamien") daneZapisu.kamienie.Add(item);
			}
		}

		//ognisko
		foreach (GameObject ognisko in ognisko) {
			string[] prefabName = ognisko.name.Split(' ');
			prefabName = prefabName[0].Split('(');

			if (prefabName[0] != null) {
				SerObjekt item = new SerObjekt {
					pozycja = Vector2ToSer(ognisko.transform.position)
				};

				if (prefabName[0] == "Ognisko") daneZapisu.ognisko.Add(item);
			}
		}

		//przedmioty
		foreach (GameObject przedmiot in przedmioty) {
			string[] prefabName = przedmiot.name.Split(' ');
			prefabName = prefabName[0].Split('(');

			if (prefabName[0] != null) {
				SerPrzedmiot item = new SerPrzedmiot {
					pozycja = Vector2ToSer(przedmiot.transform.position),
					nazwa = prefabName[0]
				};

				daneZapisu.przedmioty.Add(item);
			}
		}

		//przeciwnicy
		foreach (GameObject przeciwnik in przeciwnicy) {
			string[] prefabName = przeciwnik.name.Split(' ');
			prefabName = prefabName[0].Split('(');

			if (prefabName[0] != null) {
				//wilki
				if (prefabName[0] == "Wilk") {
					SerPrzeciwnik item = new SerPrzeciwnik {
						pozycja = Vector2ToSer(przeciwnik.transform.position),
						aktualneZdrowie = przeciwnik.GetComponent<Wilk>().punktyZdrowia.AktualnaWartosc
					};
					foreach (Vector2 v2 in przeciwnik.GetComponent<Wilk>().listaCelow) {
						print(item.listaCelow);
						item.listaCelow.Add(Vector2ToSer(v2));
					}

					daneZapisu.wilki.Add(item);
					//orly
				} else if (prefabName[0] == "Orzel") {
					SerPrzeciwnik item = new SerPrzeciwnik {
						pozycja = Vector2ToSer(przeciwnik.transform.position),
						aktualneZdrowie = przeciwnik.GetComponent<Orzel>().punktyZdrowia.AktualnaWartosc
					};
					foreach (Vector2 v2 in przeciwnik.GetComponent<Orzel>().listaCelow) {
						item.listaCelow.Add(Vector2ToSer(v2));
					}

					daneZapisu.orly.Add(item);
					//łosie
				} else if (prefabName[0] == "Łoś") {
					SerObjektZZyciem item = new SerObjektZZyciem {
						pozycja = Vector2ToSer(przeciwnik.transform.position),
						aktualneZdrowie = przeciwnik.GetComponent<Los>().punktyZdrowia.AktualnaWartosc
					};

					daneZapisu.losie.Add(item);
				}
			}
		}
		//ekwipunek
		foreach (GameObject slot in ekwipunek) {
			SerSlot item = new SerSlot();
			item.ktory = int.Parse(slot.name.Substring(4));

			if(slot.GetComponent<Slot>().Przedmioty.Count>0) {
				item.nazwa = slot.GetComponent<Slot>().aktualnyPrzemdmiot.nazwa;
				item.ilosc = slot.GetComponent<Slot>().Przedmioty.Count;
			}

			daneZapisu.ekwipunek.Add(item);
		}
	}

	private void PrzywrocDane() {
		//SceneManager.SetActiveScene(SceneManager.GetSceneByName("scenaDoWczytywania"));
		gracz = GameObject.FindGameObjectWithTag("Player");
		GameObject wczytywaneDane = new GameObject("Wczytywane dane");

		//gracz
		gracz.transform.position = SerToVector2(daneZapisu.gracz.pozycja);
		gracz.GetComponent<Player>().AktualnePunktyZdrowia = daneZapisu.gracz.aktualneZdrowie;
		gracz.GetComponent<Player>().AktualnePunktyGlodu = daneZapisu.gracz.glod;
		gracz.GetComponent<Player>().AktualnePunktyDoswiadczenia = daneZapisu.gracz.doswiadczenie;
		gracz.GetComponent<Player>().AktualnePunktyPragnienia = daneZapisu.gracz.pragnienie;

		//tworzenie obiektów
		foreach (SerObjektZZyciem obiekt in daneZapisu.drzewa1) {
			GameObject d = Instantiate(drzewo1Prefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
			d.GetComponentInChildren<NieruchomyObiekt>().punktyZdrowia.AktualnaWartosc = obiekt.aktualneZdrowie;
		}
		foreach (SerObjektZZyciem obiekt in daneZapisu.drzewa2) {
			GameObject d = Instantiate(drzewo2Prefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
			d.GetComponentInChildren<NieruchomyObiekt>().punktyZdrowia.AktualnaWartosc = obiekt.aktualneZdrowie;
		}
		foreach (SerObjektZZyciem obiekt in daneZapisu.drzewa3) {
			GameObject d = Instantiate(drzewo3Prefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
			d.GetComponentInChildren<NieruchomyObiekt>().punktyZdrowia.AktualnaWartosc = obiekt.aktualneZdrowie;
		}
		foreach (SerObjektZZyciem obiekt in daneZapisu.pomidory) {
			GameObject d = Instantiate(pomidorPrefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
			d.GetComponentInChildren<NieruchomyObiekt>().punktyZdrowia.AktualnaWartosc = obiekt.aktualneZdrowie;
		}
		foreach (SerObjektZZyciem obiekt in daneZapisu.grzybyBrazowe) {
			GameObject d = Instantiate(grzybBrazowyPrefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
			d.GetComponentInChildren<NieruchomyObiekt>().punktyZdrowia.AktualnaWartosc = obiekt.aktualneZdrowie;
		}
		foreach (SerObjektZZyciem obiekt in daneZapisu.grzybyCzerwone) {
			GameObject d = Instantiate(grzybCzerwonyPrefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
			d.GetComponentInChildren<NieruchomyObiekt>().punktyZdrowia.AktualnaWartosc = obiekt.aktualneZdrowie;
		}
		foreach (SerObjektZZyciem obiekt in daneZapisu.rudy) {
			GameObject d = Instantiate(rudaPrefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
			d.GetComponentInChildren<NieruchomyObiekt>().punktyZdrowia.AktualnaWartosc = obiekt.aktualneZdrowie;
		}
		foreach (SerObjektZZyciem obiekt in daneZapisu.kamienie) {
			GameObject d = Instantiate(kamienPrefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
			d.GetComponentInChildren<NieruchomyObiekt>().punktyZdrowia.AktualnaWartosc = obiekt.aktualneZdrowie;
		}
		foreach (SerObjektZZyciem obiekt in daneZapisu.losie) {
			GameObject d = Instantiate(losPrefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
			d.GetComponent<Los>().punktyZdrowia.AktualnaWartosc = obiekt.aktualneZdrowie;
		}
		foreach (SerPrzeciwnik obiekt in daneZapisu.wilki) {
			GameObject d = Instantiate(wilkPrefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
			d.GetComponent<Wilk>().punktyZdrowia.AktualnaWartosc = obiekt.aktualneZdrowie;
			d.GetComponent<Wilk>().listaCelow.Clear();
			foreach (SerVector2 sv2 in obiekt.listaCelow) {
				d.GetComponent<Wilk>().listaCelow.Add(SerToVector2(sv2));
			}
		}
		foreach (SerPrzeciwnik obiekt in daneZapisu.orly) {
			GameObject d = Instantiate(orzelPrefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
			d.GetComponent<Orzel>().punktyZdrowia.AktualnaWartosc = obiekt.aktualneZdrowie;
			d.GetComponent<Orzel>().listaCelow.Clear();
			foreach (SerVector2 sv2 in obiekt.listaCelow) {
				d.GetComponent<Orzel>().listaCelow.Add(SerToVector2(sv2));
			}
		}
		foreach (SerObjekt obiekt in daneZapisu.ognisko) {
			GameObject d = Instantiate(ogniskoPrefab, wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
		}
		foreach (SerPrzedmiot obiekt in daneZapisu.przedmioty) {
			GameObject d = Instantiate((GameObject)Resources.Load(obiekt.nazwa), wczytywaneDane.transform);
			d.transform.position = SerToVector2(obiekt.pozycja);
		}

		foreach (SerSlot slot in daneZapisu.ekwipunek) {
			Przedmiot przedmiot = Resources.Load<Przedmiot>(slot.nazwa);
			for (int i = 0; i < slot.ilosc; i++)
				GameObject.Find("Slot" + slot.ktory).GetComponent<Slot>().DodajPrzemiot(przedmiot);
		}

		Time.timeScale = 1;
	}

	public void Wczytaj(int ktory) {
		if (!Directory.Exists(Application.dataPath + "/saves"))
			Directory.CreateDirectory(Application.dataPath + "/saves");

		BinaryFormatter binaryFormatter = new BinaryFormatter();

		if (File.Exists(Application.dataPath + "/saves/" + ktory + ".dat")) {
			FileStream fileStream = File.OpenRead(Application.dataPath + "/saves/" + ktory + ".dat");

			daneZapisu = (DaneZapisu)binaryFormatter.Deserialize(fileStream);

			fileStream.Close();

			StartCoroutine(EkranLadowania());
		}
	}

	public void NowaGra() {
		StartCoroutine(EkranLadowania(true));
	}

	private IEnumerator EkranLadowania(bool nowaGra = false) {
		menu.SetActive(false);
		wczytywanie.SetActive(true);
		AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(nowaGra?"Map":"scenaDoWczytywania");
		asyncOperation.allowSceneActivation = false;
		bool zaladowano = false;

		while (!zaladowano) {
			if (!asyncOperation.isDone) {
				slider.value = asyncOperation.progress;
				if (asyncOperation.progress == 0.9f) {
					slider.value = 1f;
					asyncOperation.allowSceneActivation = true;
				}
			} else {
				if (!nowaGra) PrzywrocDane();
				zaladowano = true;
			}
			yield return null;
		}
	}

	public void Zapisz (int ktory) {
		PobierzDane();

		if (!Directory.Exists(Application.dataPath + "/saves")) 
			Directory.CreateDirectory(Application.dataPath + "/saves");

		BinaryFormatter binaryFormatter = new BinaryFormatter();

		FileStream fileStream = File.Create(Application.dataPath + "/saves/"+ ktory + ".dat");
		print("Zapisano w pliku: " + Application.dataPath + "/saves/" + ktory + ".dat");
		PKomunikat.WyswieltKomunikatZPotwierdzeniem("Zapisano");
		Time.timeScale = 1;

		binaryFormatter.Serialize(fileStream, daneZapisu);

		fileStream.Close();
	}

	public static SerVector2 Vector2ToSer (Vector2 v2) {
		SerVector2 sv2 = new SerVector2 {
			x = v2.x,
			y = v2.y
		};
		return sv2;
	}

	public static Vector2 SerToVector2 (SerVector2 sv2) {
		Vector2 v2 = new Vector2 {
			x = sv2.x,
			y = sv2.y
		};
		return v2;
	}
}

[System.Serializable]
public class DaneZapisu {
	public SerGracz gracz = new SerGracz();

	public List<SerPrzeciwnik> wilki = new List<SerPrzeciwnik>();
	public List<SerPrzeciwnik> orly = new List<SerPrzeciwnik>();
	public List<SerObjektZZyciem> losie = new List<SerObjektZZyciem>();
	public List<SerObjektZZyciem> rudy = new List<SerObjektZZyciem>();
	public List<SerObjektZZyciem> kamienie = new List<SerObjektZZyciem>();
	public List<SerObjektZZyciem> pomidory = new List<SerObjektZZyciem>();
	public List<SerObjekt> ognisko = new List<SerObjekt>();
	public List<SerObjektZZyciem> grzybyCzerwone = new List<SerObjektZZyciem>();
	public List<SerObjektZZyciem> grzybyBrazowe = new List<SerObjektZZyciem>();
	public List<SerObjektZZyciem> drzewa1 = new List<SerObjektZZyciem>();
	public List<SerObjektZZyciem> drzewa2 = new List<SerObjektZZyciem>();
	public List<SerObjektZZyciem> drzewa3 = new List<SerObjektZZyciem>();
	public List<SerPrzedmiot> przedmioty = new List<SerPrzedmiot>();

	public List<SerSlot> ekwipunek = new List<SerSlot>();
}

[System.Serializable]
public class SerVector2 {
	public float x;
	public float y;
}

[System.Serializable]
public class SerObjekt {
	public SerVector2 pozycja;
}

[System.Serializable]
public class SerObjektZZyciem : SerObjekt {
	public float aktualneZdrowie;
}

	[System.Serializable]
public class SerPrzeciwnik : SerObjektZZyciem {
	public List<SerVector2> listaCelow = new List<SerVector2>();
}

[System.Serializable]
public class SerGracz : SerObjektZZyciem {
	public float glod;
	public float pragnienie;
	public float doswiadczenie;
}

[System.Serializable]
public class SerSlot {
	public string nazwa;
	public int ilosc;
	public int ktory;
}

[System.Serializable]
public class SerPrzedmiot : SerObjekt{
	public string nazwa;
}