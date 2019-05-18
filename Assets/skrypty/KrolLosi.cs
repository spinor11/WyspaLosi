using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrolLosi : Przeciwnik {

    GameObject pocisk;
    [SerializeField]
    public Transform miejsceWystrzalu;
    public static bool ropzocznijzamianeWGracza;
    public bool idzieDoLodzi;
    [SerializeField]
    Sprite spriteGracz;
    Transform cel1;
    Transform cel2;
    GameObject asd;
    bool osiagnalcel1 = false;
    public static bool strzelil;
    [SerializeField]
    Transform luk1;
    [SerializeField]
    Transform miejsceWystrzaluStrzaly;
    bool strzelilZluku;
    GameObject Strzala;
    // Use this for initialization
    new void Start () {

        base.Start();
        cel1 = GameObject.Find("cel1").GetComponent<Transform>();
        cel2 = GameObject.Find("cel2").GetComponent<Transform>();
        pocisk = Resources.Load("KrolLosistrzal") as GameObject;
        animator = GetComponent<Animator>();
        //StartCoroutine(dostepDoSkryptuGracza.PKomunikat.WyswieltKomunikat("Nie tak szybko! Zabiłeś wielu moich braci, myślisz, że tak po prostu pozwole ci opuścić wyspę? Moją wyspę?"));




    }
	
	// Update is called once per frame
	void Update ()
    {
        Poruszanie();
        if (strzelil)
        {
            Strzel();
            strzelil = false;
        }
        if (ropzocznijzamianeWGracza)
        {
            ZamienWGracza();
        }
        if (idzieDoLodzi)
        {
            IdzDoLodzi();
        }
        //if (strzelilZluku)
        //{
        //    ZastrzelGracza();
        //}
	}

    public void Strzel()
    {
    
         asd = Instantiate(pocisk, miejsceWystrzalu.position,miejsceWystrzalu.rotation);
        //strzelil = false;
        //asd.transform.position = Vector2.MoveTowards(miejsceWystrzalu.position,dostepDoSkryptuGracza.PozycjaGracza,Time.deltaTime*2);
    }

    public void ZamienWGracza()
    {
        GetComponent<Animator>().runtimeAnimatorController= (RuntimeAnimatorController) Resources.Load("LosioGracz");
        GetComponent<SpriteRenderer>().sprite = (spriteGracz);
        transform.Find("Collider").GetComponent<BoxCollider2D>().enabled = false;
        Destroy(transform.Find("korona").gameObject);
        transform.localScale = new Vector2(1f, 1f);
        KrolLosi.ropzocznijzamianeWGracza = false;
        idzieDoLodzi = true;
    }

    public void IdzDoLodzi()
    {   /*if(true/*(Vector2)transform.position != cel1*/

        if (transform.position == cel2.position)
        {
            

            Debug.Log("strzelam do gracza");
            idzieDoLodzi = false;

            StartCoroutine(StrzelDoGracza());

        }
        else if (osiagnalcel1)
        {
            transform.localPosition = Vector2.MoveTowards(transform.position, cel2.position, Time.deltaTime * 4);
        }
        else if (osiagnalcel1==false && transform.position != cel1.position)
        {
            transform.localPosition = Vector2.MoveTowards(transform.position, cel1.position, Time.deltaTime * 4);
        }
        else if (transform.position == cel1.position)
        {
            osiagnalcel1 = true;
        }
        
        
        
    
    }

    public  IEnumerator StrzelDoGracza()
    {
        luk1.GetComponent<SpriteRenderer>().enabled = true;
        luk1.GetComponent<Animator>().SetBool("Naciaganie", true);
        StartCoroutine(dostepDoSkryptuGracza.PKomunikat.WyswieltKomunikat("Przed moją strzałą nigdy nie uciekniesz!"));
        yield return new WaitForSeconds(2);
        Strzala = Instantiate(Resources.Load("StrzalaKrola"), miejsceWystrzaluStrzaly.position, Quaternion.Euler(new Vector3(0, 0, 270))) as GameObject;
        Strzala.name = "StrzalaKrola";
        luk1.GetComponent<SpriteRenderer>().enabled = false;
        gameObject.transform.parent = GameObject.Find("NaprawionaLodz(Clone)").GetComponent<Transform>();
        NaprawionaLodz.plynie = true;
        strzelilZluku = true;
        
    }


    
}
