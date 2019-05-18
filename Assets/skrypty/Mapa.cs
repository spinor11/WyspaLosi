using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Mapa : MonoBehaviour
{

    Button przyciskWroc;
    float predkoscprzesuwaniaMapy = 0.5f;
    public static Camera mapaCamera;
    float szybkoscPrzesuwaniaMapy = 0.5f;
    // Use this for initialization
    private void Awake()
    {
        mapaCamera = GetComponent<Camera>();
        gameObject.SetActive(false);
    }
    void Start ()
    {
        przyciskWroc = GetComponent<Button>();
        
        przyciskWroc.onClick.AddListener(WlaczMape);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.Translate(Vector2.up);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(Vector2.down);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left);
        }
    }
    private void OnEnable()
    {

        transform.position = GameObject.Find("Player").GetComponent<Transform>().position;

    }
    public static void WlaczMape()
    {

        if (Player.trybMapy)
        {
            cameraScript.mainCamera.gameObject.SetActive(true);
            mapaCamera.gameObject.SetActive(false);
            Player.trybMapy = false;
            Time.timeScale = 1;
        }
        else
        {
            mapaCamera.gameObject.SetActive(true);
            cameraScript.mainCamera.gameObject.SetActive(false);
            Time.timeScale = 0;
            Player.trybMapy = true;
        }

    }


}
