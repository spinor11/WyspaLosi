using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luk : MonoBehaviour {
    [SerializeField]
    private Player gracz;
    public Vector2 pozycjaMyszki;
    private Transform miejsceWystrzalu;
    private float szybkoscStrzaly = 10;
    private bool czyLukNaciagniety;
    private bool czyStrzela;
    Slot slotZeStrzalami;
    bool posiadaStrzaly;
    int i;


    public Transform MiejsceWystrzalu
    {
        get
        {
            return transform.Find("MiejsceWystrzalu");
        }

    }




    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (!gracz.TrybMapy)
        {
            pozycjaMyszki = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            pozycjaMyszki.Normalize();
            float rotZ = Mathf.Atan2(pozycjaMyszki.y, pozycjaMyszki.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ);
        }
        
        UstawPozycjeMyszki();
        UstawZaBohaterem();

        
        
    }

    public void Strzal()
    {
        
        if (czyLukNaciagniety)
        {
            ZnajdzStrzale();
            if (posiadaStrzaly)
            {
                Player.aktywnaBron.ZmniejszWytrzymalosc();
                GameObject Strzala = Instantiate(Resources.Load("Strzala"), MiejsceWystrzalu.position, gameObject.transform.rotation) as GameObject;
                Player.PlayerAuSour.clip = Resources.Load("Sounds/Luk") as AudioClip;
                //Player.PlayerAuSour.clip = aa;
                Player.PlayerAuSour.Play();
                slotZeStrzalami.UzyjPrzedmiotu();
                Strzala.name = "Strzala";
                Strzala.transform.Rotate(Vector2.left);
                //Strzala.GetComponent<Rigidbody2D>().AddForce(pozycjaMyszki);
                Strzala.GetComponent<Rigidbody2D>().velocity = pozycjaMyszki * szybkoscStrzaly;
                czyLukNaciagniety = false;
                Destroy(Strzala, 3);
            }
            else
            {
                
            }
            
        }     
    }
    public void NaciagnietyLuk()
    {
        czyLukNaciagniety = true;
    }


    public void UstawPozycjeMyszki()
    {
        gracz.GetComponent<Animator>().SetFloat("PozycjaMyszkiX",pozycjaMyszki.x);
        gracz.GetComponent<Animator>().SetFloat("PozycjaMyszkiY",pozycjaMyszki.y);
    }
    public void UstawZaBohaterem()
    {
        if(gracz.GetComponent<Animator>().GetBool("CzyStrzela")&&(pozycjaMyszki.x<0.713&&pozycjaMyszki.x>-0.7552)&&pozycjaMyszki.y<0  )
        {
            GetComponent<Renderer>().sortingOrder = 5;
        }
        else
            GetComponent<Renderer>().sortingOrder = 3;
    }


    public void ZnajdzStrzale()
    {
        i = i + 1;
        slotZeStrzalami = gracz.ekwipunekGracza.ZnajdzPrzedmiot("Strzaly");
        if (slotZeStrzalami == null)
        {
            posiadaStrzaly = false;
            StartCoroutine(gracz.PKomunikat.WyswieltKomunikat("Nie posiadasz strzał!"));
        }
        else
        {

            posiadaStrzaly = true;
        }
        
    }



}
