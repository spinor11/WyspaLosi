using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SkryptBroni : MonoBehaviour {
    Player gracz;
    [SerializeField]
    List<GameObject> listaBroni;
    bool script;
    SpriteRenderer sr;
    bool temp = false;
    private static AudioSource dzwiekBroni;
    private static string aktywnaBron;
    public static string AktywnaBron
    {
        get
        {
            return aktywnaBron;
        }

        set
        {
            aktywnaBron = value;
            //ObramowkaNaDolnymPasku.ustawObramowke = true;
        }
    }

    // Use this for initialization
    void Start () {
        dzwiekBroni = GetComponentInParent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        gracz = GetComponentInParent<Player>();




    }
	
	// Update is called once per frame
	void Update () {

        Atakuj();
        
        //SprawdzWytrzymalosc();


    }

    //public void SprawdzWytrzymalosc()
    //{
    //    if (wytrzymałość == 0)
    //    {
    //        Slot temp = gracz.ekwipunekGracza.ZnajdzPrzedmiot(name);
    //        StartCoroutine(gracz.PKomunikat.WyswieltKomunikat("Twoja bron {0} uległa zniszczeniu!"));
    //        temp.Przedmioty.Pop();

            
    //    }
    //}

    public void Atakuj()
    {
        
        if (gracz.Atakuje && Player.aktywnaBron.nazwa == name)
        {
            dzwiekBroni.Play();
            GetComponent<Renderer>().enabled = true;
            GetComponent<BoxCollider2D>().enabled = true;
            //Debug.Log(name + "" + wytrzymałość);

        }
        else
        {

            GetComponent<Renderer>().enabled = false;
            GetComponent<BoxCollider2D>().enabled = false;
            sr.sortingOrder = 5;

        }
    }



    

}
