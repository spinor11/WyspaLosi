using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NieruchomyObiekt : Obiekt {

    private int punktyDoswiadczenia;




    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update () {
		
	}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bron")
        {
            if (this.tag == "Roslina")
            {
                OtrzymajObrazenia(collision.GetComponent<Bron>().ObrazeniaRoslinom);
            }

            else if(tag=="Ruda")
            {
                Debug.Log(name + "uderzony");
                OtrzymajObrazenia(collision.GetComponent<Bron>().ObrazeniaMineralom);
            }
        }
    }


}
