using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour,IPointerClickHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{

    private Stack<Przedmiot> przedmioty;
    public Text ilosc;
    public Sprite pustySlot;
    public Sprite podswietlenieSlot;
    public Transform obrazPrzedmiotuTransform;
    public Image obrazPrzedmiotu;
    private Image asd;
    public Text tooltip;
    private string nazwaPrzenoszonegoPrzedmiotu;
    private Przedmiot przenoszonyPrzedmiot;
    public Image obrazPrzenoszenia;
    private string nazwaPrzedmiotuNaPasku;
    private Stack<Przedmiot> przenoszonyStos;
    private Player gracz;
    public static Slot slotZAkwaytnaBronia;
    public SlotSzybkiegoDostepu powiazany;
    

    public bool SlotJestPusty
    {
        get
        {
            return przedmioty.Count == 0;
        }
    }
    public bool asdd
    {
        get { return aktualnyPrzemdmiot.maksymalnaIlosc > przedmioty.Count; }
    }
    public Przedmiot aktualnyPrzemdmiot
    {
        
        get {
            return przedmioty.Peek(); }
    }

    public Stack<Przedmiot> Przedmioty
    {
        get
        {
            return przedmioty;
        }

        set
        {
            przedmioty = value;
        }
    }

    void Start () {

        gracz = FindObjectOfType<Player>();
        obrazPrzedmiotu = gameObject.transform.Find("ObrazPrzedmiotu").GetComponent<Image>();
        przedmioty = new Stack<Przedmiot>();
        obrazPrzedmiotu.enabled = false;
        
        


        ////obrazPrzedmiotu = GetComponentInChildren<Image>().sprite;
        //obrazPrzedmiotuTransform = transform.Find("ObrazPrzedmiotu");
        //obrazPrzedmiotu = obrazPrzedmiotuTransform.GetComponent<Image>().sprite;
        ////////Debug.Log(obrazPrzedmiotuTransform);
        ////obrazPrzedmiotu = obrazPrzedmiotuTransform.GetComponent<Image>().sprite;
        ////obrazPrzedmiotu = obrazPrzedmiotuTransform.Find("ObrazPrzedmiotu").imag;
        //Debug.Log(obrazPrzedmiotu);
        //asd = GetComponentInChildren<Image>();
        //gameObject.transform.GetChild(0).GetComponent<Image>().overrideSprite = obrazPrzedmiotu;
        //asd=get

    }

    // Update is called once per frame
   

    public void DodajPrzemiot(Przedmiot przedmiot)
    {
        obrazPrzedmiotu.enabled = true;
        przedmioty.Push(przedmiot);
        
        ilosc.text = przedmioty.Count.ToString();
        obrazPrzedmiotu.sprite = przedmiot.sprite;      
    }
    public void UzyjPrzedmiotu()
    {
        //if (!SlotJestPusty)
        //{
        //    if (przedmioty.Peek().Typ.ToString() == "Surowiec")
        //    {
        //        przedmioty.Pop();
        //    }
        //    if (przedmioty.Peek().Typ.ToString() == "Jedzenie" || przedmioty.Peek().Typ.ToString() == "Strzały")
        //    {
        //        przedmioty.Pop().Uzyj();
        //        Debug.Log("Użyto przedmiotu");
        //    }


        //    if (przedmioty.Count > 1)
        //    {
        //        ilosc.text = przedmioty.Count.ToString();
        //    }
        //    else
        //    {
        //        ilosc.text = "";
        //    }
        //}
        //else if (przedmioty.Peek().Typ.ToString() == "Broń")
        //{
        //    slotZAkwaytnaBronia = this;
        //    przedmioty.Peek().Uzyj();
        //}





        {
            if (przedmioty.Peek().Typ.ToString() == "Jedzenie" || przedmioty.Peek().Typ.ToString() == "Strzały" || przedmioty.Peek().Typ.ToString() == "Surowiec")
            {
                przedmioty.Pop().Uzyj();
                Debug.Log("Użyto przedmiotu");
                if (przedmioty.Count > 1)
                {
                    ilosc.text = przedmioty.Count.ToString();
                }
                else
                {
                    ilosc.text = "";
                }
            }
            else if (przedmioty.Peek().Typ.ToString() == "Broń")
            {
                slotZAkwaytnaBronia = this;
                przedmioty.Peek().Uzyj();
            }
        }

        if (SlotJestPusty)
            {


                //Ekwipunek.AktualizujSlorySzybkiegoDostepu();
                //foreach (SlotSzybkiegoDostepu slot in transform.GetComponentInParent<Ekwipunek>().SlotySzybkiegoDostępu)
              //  {
               if(powiazany != null)
                {
                    powiazany.WyczyscSlot();
                }
                        
              //  }


                //Debug.Log(przedmioty.Peek().nazwa);
                obrazPrzedmiotu.enabled = false;
                Ekwipunek.PusteSloty++;
            }

        }
    

    public void OnPointerClick(PointerEventData eventData)
    {
        
        
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            if (eventData.pointerEnter.tag == "SlotSzybkiegoDostepu")
            {
                string nazwa = eventData.pointerEnter.GetComponent<SlotSzybkiegoDostepu>().nazwaPrzedmiotuWSlocie;

                Debug.Log(nazwaPrzedmiotuNaPasku);
                Slot temp;
                //Slot temp =FindObjectOfType<Player>().ekwipunekGracza.ZnajdzPrzedmiot(nazwa);
                // SlotSzybkiegoDostepu.powiazaniaZeSlotami
                // Slot temp = eventData.pointerEnter.GetComponent<SlotSzybkiegoDostepu>().gracz.ekwipunekGracza.ZnajdzPrzedmiot(nazwa);
                SlotSzybkiegoDostepu.powiazaniaZeSlotami.TryGetValue(eventData.pointerEnter.GetComponent<SlotSzybkiegoDostepu>(), out temp);
                temp.UzyjPrzedmiotu();
                if (temp = null)
                {
                    obrazPrzedmiotu.GetComponent<Image>().enabled = false;
                    SlotSzybkiegoDostepu.powiazaniaZeSlotami.Remove(eventData.pointerEnter.GetComponent<SlotSzybkiegoDostepu>());
                }

                //if(temp != null)
                //{
                //    temp.UzyjPrzedmiotu();
                //    if (temp==null)
                //    {
                //        obrazPrzedmiotu.GetComponent<Image>().enabled = false;
                //    }

                //}
                //else
                //    obrazPrzedmiotu.GetComponent<Image>().enabled = false;

                

            }
            if(przedmioty.Peek().Typ.ToString() != "Surowiec")
            {
                UzyjPrzedmiotu();
            }
            
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!SlotJestPusty)
        {

            tooltip.text = aktualnyPrzemdmiot.opis;
            Vector2 pozycjaMyszki = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            tooltip.transform.position = new Vector2(pozycjaMyszki.x, pozycjaMyszki.y - 0.7f);



        }
    }
    public  void OnPointerExit(PointerEventData data)
    {
        tooltip.text = "";

    }

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {

    }
    public void OnDrag(PointerEventData aa)
    {
        
        if (!SlotJestPusty)
        {
            obrazPrzenoszenia.GetComponent<Image>().enabled = true;
            przenoszonyStos = new Stack<Przedmiot>(przedmioty);
            Vector2 pozycjaMyszki = (Camera.main.ScreenToWorldPoint(Input.mousePosition));
            obrazPrzenoszenia.overrideSprite = obrazPrzedmiotu.sprite;
            obrazPrzenoszenia.rectTransform.position = new Vector2(pozycjaMyszki.x, pozycjaMyszki.y);

        }
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
       
        {
            
            
            if (!SlotJestPusty)
            {

                przenoszonyPrzedmiot = przenoszonyStos.Peek();
                //ta zmienna poinfromuje czy trzeba usuwać wyczyscic pierwotny slot
                bool czyPrzenoszenieDwustronne = false;
                //zapisujemy slot do ktorego gracz cche przeniesc przedmioty
                //sprawdzamy czy kursor zostal zwolniony w obszarze slotow ekwipunku    
                try
                {

                    if (eventData.pointerEnter.name == "Kosz")
                    {
                        slotZAkwaytnaBronia = null;
                        przedmioty.Clear();
                        obrazPrzenoszenia.enabled = false;
                        obrazPrzedmiotu.enabled = false;
                        ilosc.text = "";
                        Ekwipunek.PusteSloty++;
                        return;
                    }
                    //Slot temp = (eventData.pointerEnter.transform.GetComponent<Slot>());
                }
                
                catch
                {
                    obrazPrzenoszenia.GetComponent<Image>().enabled = false;
                    return;
                }

                if (eventData.pointerEnter.name == "Ognisko")
                {

                    UpieczPrzedmiot();
                    


                }
                if (powiazany != null)
                {
                    Debug.Log("zmianiam powizania");
                    Slot.slotZAkwaytnaBronia = eventData.pointerEnter.GetComponent<Slot>();
                    Slot.slotZAkwaytnaBronia.powiazany = powiazany;
                    powiazany = null;
                }
                
                //Debug.Log(przenoszonyPrzedmiot.nazwa);
                
                Slot gdziePrzeniesc = (eventData.pointerEnter.transform.GetComponent<Slot>());
                if (eventData.pointerEnter.tag == "SlotSzybkiegoDostepu")
                {
                    gdziePrzeniesc.obrazPrzedmiotu.GetComponent<Image>().enabled = true;
                    gdziePrzeniesc.obrazPrzedmiotu.sprite = przenoszonyStos.Peek().sprite;

                    SlotSzybkiegoDostepu temp = eventData.pointerEnter.GetComponent<SlotSzybkiegoDostepu>();
                    nazwaPrzedmiotuNaPasku = przedmioty.Peek().nazwa;

                    if(SlotSzybkiegoDostepu.powiazaniaZeSlotami.ContainsKey(temp))
                    {
                        SlotSzybkiegoDostepu.powiazaniaZeSlotami.Remove(temp);
                    }
                    SlotSzybkiegoDostepu.powiazaniaZeSlotami.Add(temp, this);
                    powiazany = temp;
                    eventData.pointerEnter.GetComponent<SlotSzybkiegoDostepu>().nazwaPrzedmiotuWSlocie = przedmioty.Peek().nazwa;
                    //  eventData.pointerEnter.GetComponent<SlotSzybkiegoDostepu>().powiazanyZ=this.GetComponent<Slot>();
                  //  powiazany = eventData.pointerEnter.GetComponent<SlotSzybkiegoDostepu>();



                    obrazPrzenoszenia.GetComponent<Image>().enabled = false;

                    return;
                    
                    

                }
                //Jesli slot do ktorego gracz cche przeniesc przedmioty nie jest pusty
                try
                {
                    if (!gdziePrzeniesc.SlotJestPusty /*&&/* eventData.pointerEnter.tag != "SlotSzybkiegoDostepu"*/)
                    {
                        przedmioty = gdziePrzeniesc.przedmioty;
                        obrazPrzedmiotu.sprite = gdziePrzeniesc.obrazPrzedmiotu.sprite;
                        czyPrzenoszenieDwustronne = true;
                        ilosc.text = przedmioty.Count.ToString();

                    }
                }
                catch
                {

                    obrazPrzenoszenia.GetComponent<Image>().enabled = false;
                    return;
                }
                
                ////dopisujemy przenoszone przedmioty do wybranego przez gracza slotu
                gdziePrzeniesc.przedmioty = przenoszonyStos;
                gdziePrzeniesc.ilosc.text = gdziePrzeniesc.przedmioty.Count.ToString();

                //usuwamy przedmioty z prewotnego slotu
                if (!czyPrzenoszenieDwustronne/*&& eventData.pointerEnter.tag != "SlotSzybkiegoDostepu"*/)
                {
                    przedmioty.Clear();
                    ilosc.text = "";
                    obrazPrzedmiotu.enabled = false;
                }

                //uaktywniamy obraz przedmiotu w danym slocie w ekwipunku
                gdziePrzeniesc.obrazPrzedmiotu.GetComponent<Image>().enabled = true;
                gdziePrzeniesc.obrazPrzedmiotu.sprite = gdziePrzeniesc.przedmioty.Peek().sprite;
                obrazPrzenoszenia.GetComponent<Image>().enabled = false;
                Debug.Log(this.gameObject.name);
            }
            
       

        }


    }
    public void SprawdzPrzedmiotWSlocie(SlotSzybkiegoDostepu slots)
    {
        string nazwa = slots.nazwaPrzedmiotuWSlocie;
        Slot temp = slots.gracz.ekwipunekGracza.ZnajdzPrzedmiot(nazwa);
        if (temp != null)
        {
            Debug.Log(nazwa);
            temp.UzyjPrzedmiotu();
            if (slots.gracz.ekwipunekGracza.ZnajdzPrzedmiot(nazwa) == null)
            {
                obrazPrzedmiotu.GetComponent<Image>().enabled = false;
            }

        }
    }

    public void UpieczPrzedmiot()
    {

        //&& 
        if (gracz.JestPrzyOgnisku == false)
        {
            StartCoroutine(gracz.PKomunikat.WyswieltKomunikat("Musisz być przy ognisku aby to zrobić!"));
            obrazPrzenoszenia.GetComponent<Image>().enabled = false;
            return;
        }
        
        if (przenoszonyPrzedmiot.moznaUpiec)
        {
            przedmioty.Clear();
            obrazPrzedmiotu.enabled = false;
            ilosc.text = "";
            int liczbaPrzedmiotówWSlocie = przenoszonyStos.Count;
            if (przenoszonyPrzedmiot.nazwa == "Mięso")
            {
                for (int i = 1; liczbaPrzedmiotówWSlocie >= i; i++)
                {
                   
                    GetComponentInParent<Ekwipunek>().DodajPrzedmiot(Resources.Load<Przedmiot>("UpieczoneMieso"));
                    StartCoroutine(gracz.PKomunikat.WyswieltKomunikat(string.Format("Dodano do ekwipunku <{0}>UpieczoneMięso",liczbaPrzedmiotówWSlocie)));
                }
            }
            else if (przenoszonyPrzedmiot.nazwa == "SuroweJajko")
            {
                for (int i = 1; liczbaPrzedmiotówWSlocie >= i; i++)
                {
                    GetComponentInParent<Ekwipunek>().DodajPrzedmiot(Resources.Load<Przedmiot>("JajkoNaMiekko"));
                    StartCoroutine(gracz.PKomunikat.WyswieltKomunikat(string.Format("Dodano do ekwipunku <{0}>JajkoNaMiękko", liczbaPrzedmiotówWSlocie)));
                }
            }
            else if (przenoszonyPrzedmiot.nazwa == "JajkoNaMiekko")
            {
                for (int i = 1; liczbaPrzedmiotówWSlocie >= i; i++)
                {
                    GetComponentInParent<Ekwipunek>().DodajPrzedmiot(Resources.Load<Przedmiot>("JajkoNaTwardo"));
                    StartCoroutine(gracz.PKomunikat.WyswieltKomunikat(string.Format("Dodano do ekwipunku <{0}>JajkoNaTwardo", liczbaPrzedmiotówWSlocie)));
                }
            }
            
        }
        else
        {
            StartCoroutine(gracz.PKomunikat.WyswieltKomunikat("Nie można upiec tego przedmiotu!"));
        }



        //Debug.Log("pieke mieso");

        //Debug.Log(liczbaPrzedmiotówWSlocie);
        //Debug.Log(przenoszonyPrzedmiot.nazwa);
        //przedmioty.Clear();


    }
    }






