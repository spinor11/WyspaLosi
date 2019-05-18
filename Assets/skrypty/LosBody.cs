using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LosBody : MonoBehaviour {

    Los losSkrypt;
    private void Start()
    {
        losSkrypt = transform.GetComponentInParent<Los>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (!LosGracz.GraczJestLosiem)
            {
                if (collision.tag == "Bron")
                {
                    int obrazenia = collision.GetComponent<Bron>().ObrazeniaZwierzentom;
                    losSkrypt.OtrzymajObrazenia(obrazenia);

                }
                if (collision.tag == "Strzala")
                {
                    losSkrypt.OtrzymajObrazenia(40);
                    Destroy(collision.gameObject);
                }
            }
            
        }
        
    }
}
