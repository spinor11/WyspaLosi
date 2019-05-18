using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;
[Serializable]
public class Statystyka {
    [SerializeField]
    PasekStatystyk1 Pasek;
    [SerializeField]
    private float maksymalnaWartosc;
    [SerializeField]
    private float aktualnaWartosc;

    public float AktualnaWartosc
    {
        get
        {
            return aktualnaWartosc;
        }

        set
        {
            //zapewnia, że aktualna wartosc statystyki nie bedzie mniejsza od 0 i nie wieksza od maks.wart.
            aktualnaWartosc = (Mathf.Clamp(value, 0, MaksymalnaWartosc));
            
            if (Pasek != null)
            {
               
                Pasek.aktualnaWartosc = (int)aktualnaWartosc;
            }
            

            
        }
    }

    public float MaksymalnaWartosc
    {
        get
        {
            return maksymalnaWartosc;
        }

        set
        {
            
            maksymalnaWartosc = value;
            if (Pasek != null)
            {
                Pasek.maxWartosc = (int)value;
            }

        }
    }


    public void RozpocznijStat()
    {
        this.MaksymalnaWartosc = maksymalnaWartosc;
        this.AktualnaWartosc = aktualnaWartosc;
    }

    public void WyswietlObrazenia(int obrazenia, Vector2 pozycja)
    {
        new Rect(pozycja,pozycja);
    }
}
