using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SlotSzybkiegoDostepu : MonoBehaviour {

    public Slot powiazanyZ;
    public string nazwaPrzedmiotuWSlocie;
    public  Player gracz;
    public GameObject obramowka;
    bool maObramowke;
    public static Dictionary<SlotSzybkiegoDostepu, Slot> powiazaniaZeSlotami;
    // Use this for initialization
    void Start ()
    {
        gracz = FindObjectOfType<Player>();
        powiazaniaZeSlotami = new Dictionary<SlotSzybkiegoDostepu, Slot>();
       // powiazaniaZeSlotami.Add(this, null);
    }
	
	// Update is called once per frame
	void Update ()
    {

	}

    public  void sprawdz()
    {
        if (gracz.ekwipunekGracza.ZnajdzPrzedmiot(nazwaPrzedmiotuWSlocie) == null)
        {

            transform.Find("ObrazPrzedmiotu").GetComponent<Image>().enabled = false;
        }
    }

    public void ustawObramowke()
    {
        if ((nazwaPrzedmiotuWSlocie == Player.aktywnaBron.nazwa) && !maObramowke)
        {
            obramowka = Instantiate(Resources.Load("Obramowka"), transform.position, Quaternion.identity) as GameObject;
            obramowka.transform.SetParent(gameObject.transform);
            maObramowke = true;
        }
    }
    public void UsunObramowke()
    {
        Destroy(obramowka);
        maObramowke = false;
    }

    public void WyczyscSlot()
    {
        nazwaPrzedmiotuWSlocie = "";
        Debug.Log("Czyszcze" + name);
        transform.Find("ObrazPrzedmiotu").GetComponent<Image>().enabled = false;
        UsunObramowke();
        //GetComponent<Slot>().Przedmioty.Clear();
        GetComponent<Slot>().obrazPrzedmiotu.enabled = false;
    }




}
