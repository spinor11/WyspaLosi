using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZasiegAtaku : MonoBehaviour {
    string parentName;
    Przeciwnik skryptPrzeciwnik;
    private void Start()
    {
        skryptPrzeciwnik = GetComponentInParent<Przeciwnik>();
    }
    private void Update()
    {
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            skryptPrzeciwnik.WZasieguAtaku = true;
            //GetComponentInParent<Wilk>().WZasieguAtaku = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            skryptPrzeciwnik.WZasieguAtaku = false;
            //GetComponentInParent<Wilk>().WZasieguAtaku = false;
            
        }
    }

    
}
