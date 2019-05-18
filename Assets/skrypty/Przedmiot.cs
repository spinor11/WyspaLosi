using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType { Jedzenie,Surowiec,Broń,Strzały};
public class Przedmiot : MonoBehaviour {

    public ItemType Typ;
    public string nazwa;
    [SerializeField]
    public string opis;
    public Sprite sprite;
    public int maksymalnaIlosc;
    private Player skryptGracza;
    public bool moznaUpiec;
    [SerializeField]
    int maxWytrzymalosc;
    int aktualnaWytrzymalosc;
    string wytrzymaloscText;
    private string opisCzesc1;
    bool start = false;

    public Player SkryptGracza
    {
        get
        {
            return skryptGracza;
        }

        set
        {
            skryptGracza = value;
        }
    }

    //private Przedmiot aktywnaBron;

    //public Przedmiot AktywnaBron
    //{
    //    get
    //    {
    //       // Debug.Log(aktywnaBron.nazwa);
    //        return aktywnaBron;
    //    }

    //    set
    //    {
    //        aktywnaBron = value;
    //    }
    //}

    public void Start()
    {
        
        nazwa = gameObject.name;
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
        GameObject temp = GameObject.Find("Player");
        SkryptGracza = temp.GetComponent<Player>();
        aktualnaWytrzymalosc = maxWytrzymalosc;
        opisCzesc1 = opis;
        //ZmienOpisWytrzymalosc();
        start = true;
        

        
    }

    public void Uzyj()
    {
        
        //if (skryptGracza == null)
        //{
        //    GameObject temp = GameObject.Find("Player");
        //    skryptGracza = temp.GetComponent<Player>();
        //}
        switch (nazwa)
        {
            
            case "UpieczoneMieso":
                
                SkryptGracza.Leczenie(20);
                SkryptGracza.AktualnePunktyGlodu -= 10;
                break;
            case "CzerwonyGrzyb":
                SkryptGracza.Leczenie(20);
                SkryptGracza.AktualnePunktyGlodu -= 10;
                Debug.Log("uzyto grzyba");
                break;
            case "BrazowyGrzyb":
                SkryptGracza.AktualnePunktyGlodu -= 50;
                SkryptGracza.ZadajObrazenia(20);
                break;
            case ("Pomidor"):
                SkryptGracza.AktualnePunktyGlodu -= 10;
                SkryptGracza.AktualnePunktyPragnienia -= 10;
                break;
            case "Mięso":
                SkryptGracza.AktualnePunktyGlodu -= 50;
                SkryptGracza.Leczenie(40);
                break;
            case "Pochodnia":
                break;
            case "Topor":
                Player.aktywnaBron = this;
                SkryptGracza.UstawBroń();

                break;
            case "Kilof":
                Player.aktywnaBron = this;
                SkryptGracza.UstawBroń();

                break;
            case "Luk":
                Player.aktywnaBron = this;
                SkryptGracza.UstawBroń();

                break;
            case "Miecz":
                Player.aktywnaBron = this;
                SkryptGracza.UstawBroń();    
                break;
            case "DrewnianyMiecz":
                Player.aktywnaBron = this;
                SkryptGracza.UstawBroń();
                break;
            case "DoubleAxe":
                Player.aktywnaBron = this;
                SkryptGracza.UstawBroń();
                break;
            case "Hammer":
                Player.aktywnaBron = this;
                SkryptGracza.UstawBroń();
                break;
            case "SuroweJajko":
                SkryptGracza.AktualnePunktyGlodu -= 20;
                break;
            case "JajkoNaMiekko":
                SkryptGracza.AktualnePunktyGlodu -= 20;
                SkryptGracza.Leczenie(30);
                break;
            case "JajkoNaTwardo":
                SkryptGracza.AktualnePunktyGlodu -= 20;
                SkryptGracza.Leczenie(30);
                SkryptGracza.PrzyznajDoswiadczenie(200);
                break;

        }
        Debug.Log("Użyto " + nazwa);
    }

    public void ZmniejszWytrzymalosc()
    {


        if (Player.aktywnaBron.nazwa != "temp")
        {
            aktualnaWytrzymalosc -= 1;
            //ZmienOpisWytrzymalosc();
            if (aktualnaWytrzymalosc <= 0)
            {

                SkryptGracza.KomunikatZnisczonaBron(Player.aktywnaBron.nazwa);
                SkryptGracza.ekwipunekGracza.AktualizujSlorySzybkiegoDostepu();
                if (Slot.slotZAkwaytnaBronia.powiazany != null)
                {
                    Slot.slotZAkwaytnaBronia.powiazany.WyczyscSlot();
                }


                Slot.slotZAkwaytnaBronia.Przedmioty.Pop();
                Slot.slotZAkwaytnaBronia.obrazPrzedmiotu.enabled = false;

                Slot.slotZAkwaytnaBronia.ilosc.text = "";

                Ekwipunek.PusteSloty += 1;
            }
            Debug.Log(aktualnaWytrzymalosc);
        }
    }

    //private void ZmienOpisWytrzymalosc()
    //{
    //    wytrzymaloscText = aktualnaWytrzymalosc + "/" + maxWytrzymalosc;

    //    opis = string.Format("{0} {1}", opisCzesc1, wytrzymaloscText);
    //}


    public Przedmiot(string nazwa, string opis, Sprite sprite, int maksymalnaIlosc)
    {


        ItemType Typ = ItemType.Broń;


        this.Typ = Typ;
        this.nazwa = nazwa;
        this.opis = opis;
        this.sprite = sprite;
        this.maksymalnaIlosc = maksymalnaIlosc;

    }

}
