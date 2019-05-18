using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Szeregowaniewarstw : MonoBehaviour {
    private SpriteRenderer sprite;

    // ta klasa zapewnia prawidłowe wyswietlanie warstw obiektow w grze, tak aby zachowany był widok izometryczny
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();

    }
    private void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            sprite.sortingOrder = 5;

        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            sprite.sortingOrder = 3;

        }

    }
}
