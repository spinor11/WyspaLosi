using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasekStatystyk1 : MonoBehaviour {

    float fillAmount;
    Image pasekImg;
    public float maxWartosc;
    public float aktualnaWartosc;

    [SerializeField]
    Text wartosc;



    // Use this for initialization
    void Start () {
        pasekImg = gameObject.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(maxWartosc);
        dd();
	}
    public void dd()
    {
        
        pasekImg.fillAmount = aktualnaWartosc / maxWartosc;
        try
        {
            wartosc.text = aktualnaWartosc + "/" + maxWartosc;
        }
        catch (System.Exception)
        {

            return;
        }
        
    }

}
