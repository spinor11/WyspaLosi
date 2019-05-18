using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocisk : KrolLosi {

    // Use this for initialization
    bool wraca;
    GameObject pozycjaKrolaLosi;
    private void Awake()
    {
        miejsceWystrzalu = GetComponentInParent<Transform>();
        ostatniaPozycja = transform.position;
        pozycjaKrolaLosi = GameObject.Find("Król Łosi");
    }
    private void Update()
    {
        if(!wraca)
        transform.position = Vector3.MoveTowards(miejsceWystrzalu.position, dostepDoSkryptuGracza.transform.position, Time.deltaTime * 2f);
        if (wraca)
        {
            Debug.Log("wraca");
            transform.localPosition = Vector3.MoveTowards(transform.position, pozycjaKrolaLosi.transform.position, Time.deltaTime * 2f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("zamieniam w Losia");
            dostepDoSkryptuGracza.ZamienWLosia();
            wraca = true;
        }
        if (wraca == true && collision.name == "Król Łosi")
        {
            KrolLosi.ropzocznijzamianeWGracza = true;
            Debug.Log("zamieniam w Gracza");
            Destroy(gameObject);

        }
    }





}
