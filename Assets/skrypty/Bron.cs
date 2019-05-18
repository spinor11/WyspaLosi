using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bron:MonoBehaviour {

    [SerializeField]
    private int obrazeniaZwierzentom;
    [SerializeField]
    private int obrazeniaRoslinom;
    [SerializeField]
    private int obrazeniaMineralom;
    //[SerializeField]
    //int aktualnaWytrzymalosc;
    //[SerializeField]
    //int maxWytrzymalosc;
    private Player player;

    public int ObrazeniaZwierzentom
    {
        get
        {
            return obrazeniaZwierzentom;
        }

        set
        {
            obrazeniaZwierzentom = value;
        }
    }

    public int ObrazeniaRoslinom
    {
        get
        {
            return obrazeniaRoslinom;
        }

        set
        {
            obrazeniaRoslinom = value;
        }
    }

    public int ObrazeniaMineralom
    {
        get
        {
            return obrazeniaMineralom;
        }

        set
        {
            obrazeniaMineralom = value;
        }
    }

 

    public void asd1()
    {
        Debug.Log("...");
    }

  
}
