using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZadanieNaprawyLodzi : MonoBehaviour {

    public static bool czyRozpoczetoZadanie = false;
    Text drewno;
    Text zelazo;
    Text mieso;
    Text skora;
    int potrzebnaIloscDrewna = 18;
    int potrzebnaIloscMiesa = 15;
    int potrzebnaIloscZelaza = 9;
    int potrzebnaIloscSkory = 9;

    int aktualnaIloscDrewna;
    int aktualnaIloscMiesa;
    int aktualnaIloscZelaza;
    int aktualnaIloscSkory;

    public static bool zadanieSkonczone;
    private bool komunikatWyswietlony;
    Player gracz;

    [SerializeField]
    Transform rozwalonaLodz;
    
    [SerializeField]
    Ekwipunek ekwipunek;

    [SerializeField]
    Image ekranNaprawy;

    [SerializeField]
    Text tekstNaprawy;

    [SerializeField]
    Transform pozycjaSpawnu;


    // Use this for initialization
    void Start ()
    {
        GetComponent<RectTransform>().localScale = new Vector2(0, 0);
        gracz = FindObjectOfType<Player>();
        drewno = transform.Find("Drewno").GetComponent<Text>();
        zelazo = transform.Find("Zelazo").GetComponent<Text>();
        mieso = transform.Find("Mieso").GetComponent<Text>();
        skora = transform.Find("Skora").GetComponent<Text>();
    }
	
	// Update is called once per frame
	void Update () {
        //  ekranNaprawy.CrossFadeAlpha(0, 2f, false);

            LiczIloscPrzedmiotow();

        
            
        
	}

    public void LiczIloscPrzedmiotow()
    {
        aktualnaIloscDrewna= ekwipunek.LiczIlosc("Drewno");
        aktualnaIloscZelaza = ekwipunek.LiczIlosc("zelazo");
        aktualnaIloscSkory = ekwipunek.LiczIlosc("Skóra");
        aktualnaIloscMiesa = ekwipunek.LiczIlosc("Mięso");
        UstawIloscPrzedmiotow();
        ZakonczZadanie();

    }

    public void UstawIloscPrzedmiotow()
    {
        if (Ekwipunek.ekwipunekCanvas.isActiveAndEnabled)
        {
            drewno.text = "Drewno: " + aktualnaIloscDrewna + "/" + potrzebnaIloscDrewna;
            zelazo.text = "Żelazo: " + aktualnaIloscZelaza + "/" + potrzebnaIloscZelaza;
            mieso.text = "Mieso: " + aktualnaIloscMiesa + "/" + potrzebnaIloscMiesa;
            skora.text = "Skóra: " + aktualnaIloscSkory + "/" + potrzebnaIloscSkory;
        }
        
    }

    public void ZakonczZadanie()
    {
        if (aktualnaIloscDrewna >= potrzebnaIloscDrewna && aktualnaIloscSkory >= potrzebnaIloscSkory && aktualnaIloscMiesa >= potrzebnaIloscMiesa && aktualnaIloscZelaza >= potrzebnaIloscZelaza)
        {
            zadanieSkonczone = true;
            if (!komunikatWyswietlony)
            {
                komunikatWyswietlony = true;
                
                StartCoroutine(gracz.PKomunikat.WyswieltKomunikat("Czas wrócić do łodzi i ją naprawić"));
                
            }

        }
        else
        {
            zadanieSkonczone = false;
            komunikatWyswietlony = false;
        }
            
    }

    public  IEnumerator NaprawLodz()
    {
        
        ekranNaprawy.enabled = true;
        ekranNaprawy.CrossFadeAlpha(0, 4f, false);
        tekstNaprawy.enabled = true;
        tekstNaprawy.CrossFadeAlpha(0, 4f, false);
        Instantiate(Resources.Load("NaprawionaLodz"), rozwalonaLodz.position, Quaternion.identity);
        Destroy(rozwalonaLodz.gameObject);
        yield return new WaitForSeconds(4f);
        StartCoroutine(gracz.PKomunikat.WyswieltKomunikat("Robota skończona, czas wracać do domu!"));
        
        yield return new WaitForSeconds(2f);
        
        gracz.PKomunikat.WyswieltKomunikatZPotwierdzeniem("Nie tak szybko! Zabiłeś wielu moich braci, myślisz, że tak po prostu pozwole ci opuścić wyspę? Moją wyspę?");
        GameObject temp = Instantiate(Resources.Load("Król Łosi"), pozycjaSpawnu) as GameObject;
        temp.name = "Król Łosi";
        temp.transform.parent = null; 
        KrolLosi.strzelil = true;

    }

    public  void PokazPotrzebneSurowce()
    {
         GetComponent<RectTransform>().localScale = new Vector2(1, 1);
    }



    
    
}
