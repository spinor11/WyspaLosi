using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orzel : Przeciwnik {

    Vector3 OstatniaPozycja;
    public List<Vector2> listaCelow;
    Vector2 pozycjaStartowa;
    bool maCel;
    bool czyAtakuje;
    bool oddalaSie;
    float czasOczekiwaniaNaAtak = 2;
    // Use this for initialization
    new void Start () {
        base.Start();
        szybkoscPoruszania = 3;
        animator = GetComponent<Animator>();
        pozycjaStartowa = transform.position;
        listaCelow.Add(pozycjaStartowa);
        listaCelow.Add(new Vector2(transform.position.x, transform.position.y - 10));
        listaCelow.Add(new Vector2(listaCelow[1].x - 10, listaCelow[1].y));
        listaCelow.Add(new Vector2(listaCelow[2].x, listaCelow[2].y + 10));
        listaCelow.Add(new Vector2(listaCelow[3].x + 10, listaCelow[3].y));
        transform.Find("Canvas").GetComponent<Canvas>().worldCamera = Camera.main;
        
    }
	
	// Update is called once per frame
	void Update () {
        Poruszanie();
        PasekZdrowiaPodPrzeciwnikiem(0.5f, 0);
        zarzadzajStanami();
    }

    public void zarzadzajStanami()
    {
        
        if(!maCel&&!oddalaSie)
            Patrol1(listaCelow);
        if (maCel)
        {
            if (!oddalaSie)
            {
                Szarzuj();
            }
            
            if (WZasieguAtaku&&!czyAtakuje)
            {
                StartCoroutine(StanAtakuje());
                //Atakuj();
                

            }
            //if(oddalaSie)
            //    StartCoroutine(OddalSiee());



        }
        
    }


    public void Szarzuj()
    {
        transform.position = Vector2.MoveTowards(transform.position, dostepDoSkryptuGracza.PozycjaGracza, szybkoscPoruszania * 2 * Time.deltaTime);


    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }
    private void OnTriggerEnter2D(Collider2D kolizja)
    {

        if (kolizja.tag == "Bron")
        {
            int liczbaObrazen = kolizja.GetComponent<Bron>().ObrazeniaZwierzentom;
            OtrzymajObrazenia(liczbaObrazen);
        }
        if (kolizja.tag == "Strzala")
        {
            OtrzymajObrazenia(30);
            Destroy(kolizja.gameObject);
        }
    }

    public void Atakuj()
    {
        
        czyAtakuje = true;
        
        WZasieguAtaku = false;
        oddalaSie = true;
    }
    IEnumerator StanAtakuje()
    {
        Debug.Log("atakuje");
        czyAtakuje = true;
        
        maCel = false;
        dostepDoSkryptuGracza.ZadajObrazenia(zadawaneObrazenia);
        StartCoroutine(OddalSiee());
        //OddalSie();
        yield return new WaitForSeconds(0f);
        czyAtakuje = false;
    }

    public void OddalSie()
    {
        oddalaSie = true;
        Vector2 pozycjaDoOddalenia = new Vector2(transform.position.x + Random.Range(-5.0f, 5.0f), transform.position.y + Random.Range(-5.0f, 5.0f));
        transform.position=Vector2.MoveTowards(transform.position,pozycjaDoOddalenia , szybkoscPoruszania * 2 * Time.deltaTime);
        if ((Vector2)transform.position == pozycjaDoOddalenia)
        {
            oddalaSie = false;
        }
        
    }
    IEnumerator OddalSiee()
    {

        Vector2 pozycjaDoOddalenia = new Vector2(transform.position.x + Random.Range(-5.0f, 5.0f), transform.position.y + Random.Range(-5.0f, 5.0f));
        transform.position = Vector2.MoveTowards(transform.position, pozycjaDoOddalenia, szybkoscPoruszania * 2 * Time.deltaTime);
        Debug.Log(pozycjaDoOddalenia);
        yield return new WaitForSeconds(1f);
        //oddalaSie = false;
    }
}
