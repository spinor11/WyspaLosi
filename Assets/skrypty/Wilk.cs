using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Wilk : Przeciwnik {

    bool maCel;

    float temp;
    Vector3 kierunekPoruszania;
    
    
    bool jestWidoczny;
    bool wraca = false;
    bool widziGracza;
    bool zmierzaDoCelu1 = true;
    bool zmierzaDoCelu2 = false;
    private bool czyAtakuje;
    
    
    public List<Vector2> listaCelow;
    float czasOczekiwaniaNaAtak = 1;
    Vector2 pozycjaStartowa;
    [SerializeField]
    float kierunekPatroluX = -3;
    [SerializeField]
    float kierunekPatroluY = -3;


    // Use this for initialization
    new void Start () {
        base.Start();
        pozycjaStartowa = transform.position;
        animator = GetComponent<Animator>();
        szybkoscPoruszania = 2;
        horyzontalnyCollider = transform.Find("HorizontalBodyCollider");
        wertykalnyCollider = transform.Find("VerticalBodyCollider");
        listaCelow.Add(pozycjaStartowa);
        listaCelow.Add(new Vector2(transform.position.x+kierunekPatroluX, transform.position.y + kierunekPatroluY));
        transform.Find("Canvas").GetComponent<Canvas>().worldCamera = Camera.main;

        //StartCoroutine("Patrol");
        //transform.position = Vector2.MoveTowards(transform.position, Vector2.right*2, szybkoscPoruszania * Time.deltaTime);
        //transform.Translate(Vector2.right * (szybkoscPoruszania * Time.deltaTime));

    }
	
	// Update is called once per frame
	void Update () {
        //
        
        Poruszanie();
        ZarzadzajStanami();
        PasekZdrowiaPodPrzeciwnikiem(0 ,-0.8f);
        WylaczCollider();

        //transform.Translate(Vector2.right * (szybkoscPoruszania * Time.deltaTime));


    }


    private void OnBecameVisible()
    {
        jestWidoczny = true;
    }
    private void OnBecameInvisible()
    {
         jestWidoczny = false;
        
        
    }
    
   
    //IEnumerator Patrol(List<Vector2> listaCelowPatrolu)
    //{

    //    transform.position = Vector2.MoveTowards(transform.position, listaCelowPatrolu[0], szybkoscPoruszania * Time.deltaTime);
    //    for (int i = 1; listaCelowPatrolu[i] == (Vector2)transform.position;i++)
    //    {
    //        Debug.Log("Cel numer"+i);
    //        transform.position = Vector2.MoveTowards(transform.position, listaCelowPatrolu[i], szybkoscPoruszania * Time.deltaTime);
            

    //    }
    //    yield return new WaitForSeconds(0.0f);
    //}

    //public void OtrzymajObrazeniaWilk(Collider2D kolizja)
    //{
    //    OtrzymajObrazenia(kolizja, punktyZdrowia);
    //    Zniszcz(punktyZdrowia);
    //}
    public void ZarzadzajStanami()
    {
        if (widziGracza&&!WZasieguAtaku&&!czyAtakuje)
        {
            IdzDoGracza();
        }

        else if (WZasieguAtaku&&!czyAtakuje)
        {
            
            StartCoroutine("StanAtakuje");
            

        }
        else if (!widziGracza)
        {

            Patrol1(listaCelow);

        }

    }
    
    IEnumerator StanAtakuje()
    {
        czyAtakuje = true;
        dostepDoSkryptuGracza.ZadajObrazenia(zadawaneObrazenia);
        yield return new WaitForSeconds(czasOczekiwaniaNaAtak);
        czyAtakuje = false;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" || collision.name == "Strzala")
        {

            Debug.Log("widze gracza");
            widziGracza = true;






        }

    }

}
