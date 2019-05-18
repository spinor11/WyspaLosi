using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Ekwipunek : MonoBehaviour, IPointerClickHandler
{

    private int liczbaSlotow = 14;
    private static int pusteSloty = 14;
    //private List<GameObject> allSlots;
    public  List<SlotSzybkiegoDostepu> SlotySzybkiegoDostępu;
    public static Canvas ekwipunekCanvas;
    private Image ognisko;
    public static bool ekwipunekUlepszony;
    public AudioSource As;


    private void Awake()
    {
        ekwipunekCanvas = GetComponentInParent<Canvas>();
        ognisko = transform.Find("Ognisko").GetComponent<Image>();
        

    }
    private void Start()
    {

    }
    public static int PusteSloty
    {
        get
        {
            return pusteSloty;
        }

        set
        {
            pusteSloty = value;
        }
    }
    

    public bool DodajPrzedmiot(Przedmiot przedmiot)
    {
        przedmiot.Start();
        if (przedmiot.maksymalnaIlosc == 1)
        {
            Znajdzwolne(przedmiot);
            return true;
        }
        else
        {
            for (int i = 0; i < liczbaSlotow; i++)
            {
                string tempString = "Slot" + i;
                Transform tempTransform = transform.Find(tempString);
                Slot temp = tempTransform.GetComponent<Slot>();
                if (!temp.SlotJestPusty)
                {
                    if (temp.aktualnyPrzemdmiot.nazwa == przedmiot.nazwa && przedmiot.maksymalnaIlosc > temp.Przedmioty.Count)
                    {
                        temp.DodajPrzemiot(przedmiot);
                        return true;
                    }
                }
            }
            if (PusteSloty > 0)
            {
                Znajdzwolne(przedmiot);
            }
        }
        
        return false;
    }
    public bool Znajdzwolne(Przedmiot przedmiot)
    {
        if (PusteSloty > 0)
        {
            for (int i = 0; i < liczbaSlotow; i++)
            {
                string tempString = "Slot" + i;
                //obrazPrzedmiotuTransform = transform.Find("ObrazPrzedmiotu");
                //obrazPrzedmiotu = obrazPrzedmiotuTransform.GetComponent<Image>().sprite;
                Transform tempTransform = transform.Find(tempString);

                Slot temp = tempTransform.GetComponent<Slot>();
                if (temp.SlotJestPusty)
                {
                    temp.DodajPrzemiot(przedmiot);
                    PusteSloty--;
                    return true;
                }
               

                
            }
            
       }
        Debug.Log("pelny");
        return false;
    }

    public Slot ZnajdzPrzedmiot(string nazwaSzukanegoPrzedmiotu)
    {
        for (int i = 0; i < liczbaSlotow; i++)
        {
            string tempString = "Slot" + i;
            Transform tempTransform = transform.Find(tempString);
            Slot temp = tempTransform.GetComponent<Slot>();
            if (!temp.SlotJestPusty)
            {
                if (temp.aktualnyPrzemdmiot.nazwa == nazwaSzukanegoPrzedmiotu)
                {
                    Debug.Log(temp.aktualnyPrzemdmiot.nazwa);
                    return temp;

                    
                }
            }
        }
        return null;
    }

    //liczy ilosc przedmiotow o danej nazwie w ekwipunku
    public int LiczIlosc(string nazwaPrzedmiotu)
    {
        int ilosc = 0;
        for (int i = 0; i < liczbaSlotow; i++)
        {
            string tempString = "Slot" + i;
            Transform tempTransform = transform.Find(tempString);
            Slot temp = tempTransform.GetComponent<Slot>();
            if (!temp.SlotJestPusty)
            {
                
                if (temp.aktualnyPrzemdmiot.nazwa == nazwaPrzedmiotu)
                {
                    
                    ilosc +=temp.Przedmioty.Count;
                    continue;


                }
            }
        }
        return ilosc;
    }
    public void ZwiekszEkwipunek()
    {
        
        for (int i = liczbaSlotow-1; i < liczbaSlotow+7; i++)
        {
            string tempString = "Slot" + i;
            Button tempButton = transform.Find(tempString).GetComponent<Button>();
            tempButton.interactable = true;
            Image tempImage = transform.Find(tempString).GetComponent<Image>();
            tempImage.raycastTarget = true;

        }
        ekwipunekUlepszony = true;
        liczbaSlotow = 21;
        pusteSloty = pusteSloty + 7;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerEnter.name == "PrzyciskWyjscia")
        {
            transform.parent.GetComponent<Canvas>().enabled = false;
            Time.timeScale = 1;
        }
    }
    public  void AktualizujSlorySzybkiegoDostepu()
    {
        foreach (SlotSzybkiegoDostepu slot in SlotySzybkiegoDostępu)
        {
            slot.sprawdz();
        }
    }

    public void PrzygasOgnisko(bool czyJestPrzyOgnisku)
    {
        if (czyJestPrzyOgnisku)
        {
            ognisko.color = new Color32(255, 255, 255, 255);
        }
        else
        {
            ognisko.color = new Color32(255, 255, 255, 95);
        }

    }

    
}
