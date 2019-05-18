using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Los : Przeciwnik {

    
    private Vector3 nowaPozycja;
    private bool maCel;
    private Vector2 kierunek;
    private float szybkosc = 3f;
    bool widziGracza = false;
    Player gracz;
    Vector3 OstatniaPozycja;
    GameObject asd;

    // Use this for initialization
    new void Start () {
        base.Start();
        animator = GetComponent<Animator>();
        gracz = FindObjectOfType<Player>();
        horyzontalnyCollider = transform.Find("HorizontalBodyCollider");
        wertykalnyCollider = transform.Find("VerticalBodyCollider");
        transform.Find("Canvas").GetComponent<Canvas>().worldCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {

        Poruszanie();
        PasekZdrowiaPodPrzeciwnikiem(0,-1);
        if (widziGracza)
        {
            UciekajOdGracza(gracz.transform.position);
        }
        WylaczCollider();




    }
    public void UciekajOdGracza(Vector3 pozycjaGracza)
    {
        //GetComponent<Animator>().SetBool("widziCel", true);

        //kierunek = (transform.position - pozycjaGracza).normalized;
        kierunek= ((Vector2)transform.position - (Vector2)pozycjaGracza).normalized;
        transform.Translate(kierunek * szybkosc * Time.deltaTime);
        animator.SetFloat("X", kierunek.x);
        animator.SetFloat("Y", kierunek.y);


    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            
            widziGracza = true;
            //GetComponentInParent<Los>().UciekajOdGracza(collision.transform.position);

            



        }

    }
  
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            animator.Play("Idle");
            animator.SetBool("widziCel", false);
            widziGracza = false;

        }
    }
  
}
