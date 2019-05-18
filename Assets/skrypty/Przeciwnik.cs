using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Przeciwnik : Obiekt {

    public int zadawaneObrazenia;
    protected Animator animator;
    public Transform pasekZdrowia;
    public int szybkoscPoruszania;
    protected Vector3 ostatniaPozycja;
    protected bool wZasieguAtaku;
    protected int numerCeluPatrolu = 0;
    public bool WZasieguAtaku
    {
        get
        {
            return wZasieguAtaku;
        }

        set
        {
            wZasieguAtaku = value;
        }
    }


    protected Transform horyzontalnyCollider;
    protected Transform wertykalnyCollider;


    //new protected void OtrzymajObrazenia(Collider2D kolizja, Statystyka punktyZdrowia)
    //{
    //    if (kolizja.tag == "Bron")
    //    {
    //        punktyZdrowia.AktualnaWartosc -= 20;
    //    }
    //    if (kolizja.name == "Strzala")
    //    {
    //        Destroy(kolizja.gameObject);
    //        punktyZdrowia.AktualnaWartosc -= 20;

    //    }
        
    //}
    public void PasekZdrowiaPodPrzeciwnikiem(float offsetX, float offsetY)
    {
        pasekZdrowia.transform.position = new Vector2(transform.position.x+offsetX, transform.position.y+offsetY);
    }

    //STANY
    public void Poruszanie()
    {
        animator.SetBool("IdzieWPrawo", false);
        animator.SetBool("IdzieWGore", false);
        animator.SetBool("IdzieWDol", false);
        animator.SetBool("IdzieWLewo", false);

        Vector3 temp = transform.position - ostatniaPozycja;
        temp = temp.normalized;
        ostatniaPozycja = transform.position;

        //if(temp.x == 0 && temp.y == 0)
        //{
        //    return;
        //}
        if (temp.x >= -1.0 && temp.x <= -0.4)
        {
            animator.SetBool("IdzieWLewo", true);
        }
        else if (temp.x > -0.4 && temp.x < 0.4)
        {
            if (temp.y > 0)
            {

                animator.SetBool("IdzieWGore", true);
            }
            if (temp.y < 0)
            {
                animator.SetBool("IdzieWDol", true);
            }

        }
        if (temp.x >= 0.4)
        {
            animator.SetBool("IdzieWPrawo", true);
        }
        

    }

    public virtual void IdzDoGracza()
    {

        transform.position = Vector2.MoveTowards(transform.position, dostepDoSkryptuGracza.transform.position, szybkoscPoruszania * Time.deltaTime);


    }


    public void Patrol1(List<Vector2> listaCelowPatrolu)
    {
        //do napisania pętla

        //for (int i = 0; listaCelowPatrolu[i] == (Vector2)transform.position;)
        //{
        //    Debug.Log(i);
        //    transform.position = Vector2.MoveTowards(transform.position, listaCelowPatrolu[i], szybkoscPoruszania * Time.deltaTime);
        //    if (listaCelowPatrolu[i] == (Vector2)transform.position)
        //    {
        //        i = i + 1;
        //    }

        //}
        if (numerCeluPatrolu == 0 || numerCeluPatrolu == listaCelowPatrolu.Count)
        {
            transform.position = Vector2.MoveTowards(transform.position, listaCelowPatrolu[0], szybkoscPoruszania * Time.deltaTime);
            if ((Vector2)transform.position == listaCelowPatrolu[0])
            {
                numerCeluPatrolu = 1;
            }

        }


        else if (numerCeluPatrolu == 1)
        {
            transform.position = Vector2.MoveTowards(transform.position, listaCelowPatrolu[1], szybkoscPoruszania * Time.deltaTime);
            if ((Vector2)transform.position == listaCelowPatrolu[1])
            {
                numerCeluPatrolu = 2;
            }
        }
        else if (numerCeluPatrolu == 2)
        {
            transform.position = Vector2.MoveTowards(transform.position, listaCelowPatrolu[2], szybkoscPoruszania * Time.deltaTime);
            if ((Vector2)transform.position == listaCelowPatrolu[2])
            {
                numerCeluPatrolu = 3;
            }
        }
        else if (numerCeluPatrolu == 3)
        {
            transform.position = Vector2.MoveTowards(transform.position, listaCelowPatrolu[3], szybkoscPoruszania * Time.deltaTime);
            if ((Vector2)transform.position == listaCelowPatrolu[3])
            {
                numerCeluPatrolu = 4;
            }
        }
        else if (numerCeluPatrolu == 4)
        {
            transform.position = Vector2.MoveTowards(transform.position, listaCelowPatrolu[4], szybkoscPoruszania * Time.deltaTime);
            if ((Vector2)transform.position == listaCelowPatrolu[4])
            {
                numerCeluPatrolu = 5;
            }
        }

    }

    public void WylaczCollider()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("horyzontalnie"))
        {
            horyzontalnyCollider.gameObject.SetActive(true);
            wertykalnyCollider.gameObject.SetActive(false);
        }
        else
        {
            horyzontalnyCollider.gameObject.SetActive(false);
            wertykalnyCollider.gameObject.SetActive(true);
        }
    }
}
