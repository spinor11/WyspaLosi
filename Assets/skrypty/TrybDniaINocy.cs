using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TrybDniaINocy : MonoBehaviour {
    int dzien = 1;
    int godzinaWGrze=12;
    [SerializeField]
    float szybkoscUplywuCzasu = 4f;
    Image noc;
    Color a;
    public Color color = Color.white;
    [SerializeField]
    Transform wskazowkaZegara;
    public static bool jestNoc;

    // Use this for initialization
    void Start()
    {
        noc = GetComponent<Image>();
        StartCoroutine(Licz());


    }

    // Update is called once per frame
    void Update()
    {
        ss();
        //Debug.Log(dzien + "/" + godzinaWGrze);
        
    }


    IEnumerator Licz()
    {
        while (true)
        {
            RuszajWskazowka();
            if (godzinaWGrze == 24)
            {
                godzinaWGrze = 0;
                dzien += 1;
            }
            yield return new WaitForSeconds(szybkoscUplywuCzasu);
            godzinaWGrze += 1;
            

        }

    }
    public void ss()
    {
        if (godzinaWGrze == 17||godzinaWGrze==8)
        {
            noc.color = new Color32(0, 0, 0, 40);
        }
        
        else if (godzinaWGrze == 19||godzinaWGrze==6)
        {
            noc.color = new Color32(0, 0, 0, 80);
        }
        else if(godzinaWGrze == 21||godzinaWGrze==4)
        {
            jestNoc = false;

            noc.color = new Color32(0, 0, 0, 120);
        }
        else if(godzinaWGrze == 23)
        {
            jestNoc = true;
            noc.color = new Color32(0, 0, 0, 160);
        }
        else if(godzinaWGrze>=9 && godzinaWGrze<17)
        {
            noc.color = new Color32(0, 0, 0, 0);
        }
    }

    public void RuszajWskazowka()
    {
        
        wskazowkaZegara.rotation = Quaternion.Euler(0, 0, godzinaWGrze*-15);
        
    }
}
