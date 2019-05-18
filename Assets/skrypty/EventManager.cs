using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class EventManager: MonoBehaviour
{
    [SerializeField]
    Image czarnyEkran;
    [SerializeField]
    Text koniecGry;
   public void WlaczMape()
    {
        Mapa.WlaczMape();
    }
    public void WlaczEkwipunek()
    {
        Player.WlaczEkwipunek();
    }
    public void WlaczCrafting()
    {
        Player.WlaczCrafting();
    }
    public void PrzyciemnijEkran()
    {
        czarnyEkran.enabled = true;
        czarnyEkran.color = new Color32(0, 0, 0, 1);
        czarnyEkran.CrossFadeAlpha(255, 6f, false);
        koniecGry.color = new Color32(255, 255, 255, 1);
        koniecGry.CrossFadeAlpha(255, 4f, false);
        koniecGry.text = "Koniec gry :)";
    }



}
