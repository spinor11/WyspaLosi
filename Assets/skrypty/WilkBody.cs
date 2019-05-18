using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WilkBody : MonoBehaviour {
    Wilk skryptWilk;

    private void Start()
    {
        skryptWilk = transform.GetComponentInParent<Wilk>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        {
            if (collision.tag == "Bron")
            {
                skryptWilk.OtrzymajObrazenia(collision.GetComponent<Bron>().ObrazeniaZwierzentom);
            }
            if (collision.tag == "Strzala")
            {
                skryptWilk.OtrzymajObrazenia(40);
                Destroy(collision.gameObject);
            }
        }
        
    }
}
