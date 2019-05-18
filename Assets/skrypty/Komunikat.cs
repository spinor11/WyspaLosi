using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Komunikat : MonoBehaviour {

    Text komunikat;
    int czasWyswietlaniaKomunikatu = 2;
    float timer;
    [SerializeField]
    Transform potwierdzenie;
    bool komunikatWyswietlany = false;
    // Use this for initialization
    void Start () {
        komunikat = GetComponentInChildren<Text>();
        gameObject.SetActive(false);
        potwierdzenie.gameObject.SetActive(false);
        WyswieltKomunikatZPotwierdzeniem("Ostatnie co pąmietam to sztorm na morzu i krzyk załogi naszego statku. Ta wyspa nie wygląda na przyjazną, muszę jak najszybciej znaleść sposób aby się stąd wydostać.");
        GetComponentInParent<Canvas>().sortingOrder = 1000;

    }
    private void Update()
    {
        timer = timer + Time.deltaTime;
    }


    public IEnumerator WyswieltKomunikat(string tekstKomunikatu)
    {
        if (komunikatWyswietlany == false)
        {
            komunikatWyswietlany = true;
            komunikat.text = tekstKomunikatu;
            gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(czasWyswietlaniaKomunikatu);
            komunikatWyswietlany = false;
            gameObject.SetActive(false);
        }
        
    }

    public void WyswieltKomunikatZPotwierdzeniem(string tekstKomunikatu)
    {
        komunikat.text = tekstKomunikatu;
        gameObject.SetActive(true);
        potwierdzenie.gameObject.SetActive(true);
        Time.timeScale = 0;

    }


    public void SetActive()
    {
        potwierdzenie.gameObject.SetActive(false);
        gameObject.SetActive(false);
        Time.timeScale = 1;

    }

}
