using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosGracz : Przeciwnik {

    private static bool graczJestLosiem;
    public static Transform pozycja;
    private bool nieZyje;
    public static bool GraczJestLosiem
    {
        get
        {
            return graczJestLosiem;
        }

        set
        {
            graczJestLosiem = value;
            cameraScript.ZmnienTarget();
        }
    }

    // Use this for initialization
    new void Start () {
        animator = GetComponent<Animator>();
        pozycja = transform;
        horyzontalnyCollider = transform.Find("HorizontalBodyCollider");
        wertykalnyCollider = transform.Find("VerticalBodyCollider");

    }
	
	// Update is called once per frame
	void Update () {
        Poruszanie();
        WylaczCollider();
        if (!nieZyje)
        {
            if (Input.GetKey(KeyCode.W))
            {
                transform.Translate(Vector2.up * Time.deltaTime * szybkoscPoruszania);
            }
            else if (Input.GetKey(KeyCode.S))
            {
                transform.Translate(Vector2.down * Time.deltaTime * szybkoscPoruszania);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                transform.Translate(Vector2.right * Time.deltaTime * szybkoscPoruszania);
            }
            else if (Input.GetKey(KeyCode.A))
            {
                transform.Translate(Vector2.left * Time.deltaTime * szybkoscPoruszania);
            }
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "StrzalaKrola")
        {
          //  FindObjectOfType<EventManager>().PrzyciemnijEkran();
            Destroy(collision.gameObject);
            GetComponent<SpriteRenderer>().enabled = false;
            nieZyje = true;
            StartCoroutine(KoniecGry());
        }
    }
   
    IEnumerator KoniecGry()
    {
        FindObjectOfType<EventManager>().PrzyciemnijEkran();
        yield return new WaitForSeconds(6f);
        SceneManager.LoadScene("NapisyKoncowe", LoadSceneMode.Single);
    }
}
