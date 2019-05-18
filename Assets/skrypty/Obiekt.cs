using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Obiekt : MonoBehaviour {

    //private static   Statystyka punktyZdrowia;
    string nazwaDropu;
    
    [SerializeField]
    public Statystyka punktyZdrowia;
    public int giveExp;
    GameObject playerObiekt;
    protected Player dostepDoSkryptuGracza;
    public List<string> upusczczanePrzedmioty;
    



    protected void Start()
    {
        punktyZdrowia.RozpocznijStat();
        GameObject playerObiekt = GameObject.Find("Player");
        dostepDoSkryptuGracza = playerObiekt.GetComponent<Player>();
        
    }
    protected void Zniszcz(Statystyka punktyZdrowia)
    {
        if (punktyZdrowia.AktualnaWartosc == 0)
        {

            UpuscPrzedmioty();
            dostepDoSkryptuGracza.PrzyznajDoswiadczenie(giveExp);
            
            //playerObiekt.GetComponent<Player>().PrzyznajDoswiadczenie(przyznawanePunktyDoswiadczenia);
            if (gameObject.name.Contains("Pien"))
            {
                Destroy(transform.parent.gameObject);
            }
            else
            Destroy(gameObject);
            punktyZdrowia.WyswietlObrazenia(30, transform.position);
        }
    }
    protected void UpuscPrzedmioty()
    {
        nazwaDropu = gameObject.name;
        foreach(string przedmiot in upusczczanePrzedmioty)
        {
            GameObject drop = Instantiate(Resources.Load(przedmiot), transform.position, Quaternion.identity) as GameObject;
            drop.name = przedmiot;
            drop.GetComponent<Rigidbody2D>().AddForce(new Vector2((Random.Range(-30, 20)), (Random.Range(-30, 30))));
            drop.GetComponent<Rigidbody2D>().AddTorque(Random.Range(150, 320));
        }
       
        
        
        
    }
    public void OtrzymajObrazenia(int liczbaObrazen)
    {
        //if (asd.tag == "Bron")
        //{
        //    punktyZdrowia.AktualnaWartosc -= 20;
        //}
        //if (asd.name == "Strzala")
        //{
        //    punktyZdrowia.AktualnaWartosc -= 20;

        //}
        punktyZdrowia.AktualnaWartosc -= liczbaObrazen;
        Zniszcz(punktyZdrowia);
    }

    

    
}
