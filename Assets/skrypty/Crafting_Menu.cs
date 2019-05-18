using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Crafting_Menu : MonoBehaviour {


    public Ekwipunek ekwipunekGracza;
    [SerializeField]
    private Komunikat komunikat;
    AudioSource dzwiekTworzenia;
    public static string choosenItem = "Miecz";


    public Sprite axe;
    public Sprite sword;
    public Sprite pickaxe;
    public Sprite bow;
    public Sprite sprite;
    public Sprite strzaly;
	public Sprite plecak;
	public Sprite butelka;
	public Sprite ognisko;




    public static Canvas craft_menu;
    public static Canvas[] othersCanvases;


    Przedmiot ToporPrefab;
    Przedmiot mieczPrefab;
    Przedmiot lukPrefab;
    Przedmiot kilofPrefab;
    Przedmiot strzalyPrefab;
    Przedmiot doubleAxePrefab;
    Przedmiot hammerPrefab;
    Przedmiot drewnianyMieczPrefab;


    private void Awake()
    {
        craft_menu = GetComponentInParent<Canvas>();
        othersCanvases = GetComponentsInChildren<Canvas>();     // wpisujemy pozostałe Canvasy do tablicy aby je wyłączyć, bez tego gracz wybiera itemy klikając po mapie nawet gdy menu główne jest zamknięte

        dzwiekTworzenia = GetComponent<AudioSource>();

        ToporPrefab =  (Resources.Load<Przedmiot>("Topor"));
        mieczPrefab = (Resources.Load<Przedmiot>("Miecz"));
        lukPrefab = (Resources.Load<Przedmiot>("Luk"));
        kilofPrefab = (Resources.Load<Przedmiot>("Kilof"));
        strzalyPrefab = (Resources.Load<Przedmiot>("Strzaly"));
        doubleAxePrefab = (Resources.Load<Przedmiot>("DoubleAxe"));
        hammerPrefab = (Resources.Load<Przedmiot>("Hammer"));
        drewnianyMieczPrefab = (Resources.Load<Przedmiot>("DrewnianyMiecz"));

    }


    public void GoBack_Btn()
    {
        craft_menu.enabled = false;
        othersCanvases[1].enabled = false;
        othersCanvases[0].enabled = false;
        Time.timeScale = 1;
    }

    public void Create_Btn()
    {
        if (Ekwipunek.PusteSloty == 0)
        {
            StartCoroutine(komunikat.WyswieltKomunikat("Masz pełny ekwipunek!"));
            return;
        }

        
        if (choosenItem == "Topór")
        {
            //Przedmiot temp= Instantiate(Resources.Load<Przedmiot>("Topor"));
            //temp.nazwa = ("Topór");
            if(SprawdzSurowce("Siekiera",1, 1, 1))
            {
                
                Przedmiot topor = Instantiate(ToporPrefab);
                topor.name = "Topor";
                ekwipunekGracza.DodajPrzedmiot(topor);
                Destroy(topor.gameObject);
            }
            
            
            
           
        }
        else if (choosenItem == "Miecz")
        {
            if (SprawdzSurowce("Miecz",3,0,2))
            {
                Przedmiot miecz = Instantiate(mieczPrefab);
                miecz.name = "Miecz";
                ekwipunekGracza.DodajPrzedmiot(miecz);
                Destroy(miecz.gameObject);
            }
            

            // ekwipunekGracza.DodajPrzedmiot(Resources.Load<Przedmiot>("Miecz"));
        }
        else if (choosenItem == "Łuk")
        {
            if (Player.poziomBohatera > 2)
            { 
                if (SprawdzSurowce("Łuk",2,2))
                {
                
                    Przedmiot luk = Instantiate(lukPrefab);
                    luk.name = "Luk";
                    ekwipunekGracza.DodajPrzedmiot(luk);
                    Destroy(luk.gameObject);
                }
            }
            else
            {
                StartCoroutine(komunikat.WyswieltKomunikat("Masz za niski poziom aby stworzyć ten przedmiot"));
            }
                

        }
        else if (choosenItem == "Kilof")
        {
            if (SprawdzSurowce("Kilof",1, 1,1)) //ffff
            {
                Przedmiot kilof = Instantiate(kilofPrefab);
                kilof.name = "Kilof";
                ekwipunekGracza.DodajPrzedmiot(kilof);
                Destroy(kilof.gameObject);
            }
            

        }
        else if (choosenItem == "Strzały")
        {
            if (SprawdzSurowce("Strzały",2,1,1))
            {
                ekwipunekGracza.DodajPrzedmiot(Resources.Load<Przedmiot>("Strzaly"));
                ekwipunekGracza.DodajPrzedmiot(Resources.Load<Przedmiot>("Strzaly"));
                ekwipunekGracza.DodajPrzedmiot(Resources.Load<Przedmiot>("Strzaly"));
                ekwipunekGracza.DodajPrzedmiot(Resources.Load<Przedmiot>("Strzaly"));
                ekwipunekGracza.DodajPrzedmiot(Resources.Load<Przedmiot>("Strzaly"));
            }
                
            //Przedmiot strzaly = Instantiate(strzalyPrefab);
            //strzaly.name = "Kilof";
            //ekwipunekGracza.DodajPrzedmiot(strzaly);
            //Destroy(strzaly.gameObject);

        }
		else if (choosenItem == "Plecak")
		{
            if (!Ekwipunek.ekwipunekUlepszony)
            {
                if (SprawdzSurowce("Plecak", 5, 1, 1, 4))
                {
                    ekwipunekGracza.ZwiekszEkwipunek();
                }
            }
            else if (Ekwipunek.ekwipunekUlepszony)
            {
                StartCoroutine(komunikat.WyswieltKomunikat("Masz już ulepszony ekwipunek"));
            }
                

		}
		else if (choosenItem == "Butelka")
		{
			Debug.Log ("Stworzyłeś butelke");

		}
		else if (choosenItem == "Ognisko")
		{
            if (!Ekwipunek.ekwipunekUlepszony && SprawdzSurowce("Ognisko",1, 2))
            {
                Vector2 pozycja = GameObject.FindWithTag("Player").transform.position;

                //Vector2 pozycja = new Vector2(5,5);

                GameObject ogniskoObj = GameObject.FindWithTag("Ognisko");

                ogniskoObj.transform.position = new Vector2(pozycja.x + 1, pozycja.y);
            }
             
           
		}
        else if (choosenItem == "DoubleAxe")
        {
            if (SprawdzSurowce("Topór",5, 1, 2))
            {
                Przedmiot doubleAxe = Instantiate(doubleAxePrefab);
                doubleAxe.name = "DoubleAxe";
                ekwipunekGracza.DodajPrzedmiot(doubleAxe);
                Destroy(doubleAxe.gameObject);
            }
             

        }
        else if (choosenItem == "Hammer")
        {
            if (SprawdzSurowce("Młot",6, 1, 2))
            {
                Przedmiot hammer = Instantiate(hammerPrefab);
                hammer.name = "DoubleAxe";
                ekwipunekGracza.DodajPrzedmiot(hammer);
                Destroy(hammer.gameObject);
            }
            

        }
        else if (choosenItem == "DrewnianyMiecz")
        {
            if (SprawdzSurowce("DrewnianyMiecz",1, 2))
            {
                Przedmiot drewnianyMiecz = Instantiate(drewnianyMieczPrefab);
                drewnianyMiecz.name = "DrewnianyMiecz";
                ekwipunekGracza.DodajPrzedmiot(drewnianyMiecz);
                Destroy(drewnianyMiecz.gameObject);
            }
            

        }
        else
        {
            Debug.Log("nie działa");
        }


       

        
      
    }

    public void ChooseItem(Button button)
    {



        //TO DO:
        // po wciśnięciu przycisku danego  itemu
        // button jest wciśnięty dopóki nie wybierze się innego
        string buttonName = button.name;

        choosenItem = buttonName;
       
        Debug.Log("wybrano " + buttonName);

    }

	public void chooseItemType(Button button){



       

        int drewno = ekwipunekGracza.LiczIlosc("Drewno");
        int zelazo = ekwipunekGracza.LiczIlosc("Zelazo");
        int skora = ekwipunekGracza.LiczIlosc("Skóra");


        GameObject[] neededWood = new GameObject[11];
        neededWood = GameObject.FindGameObjectsWithTag("neededWood");

        GameObject[] neededIron = new GameObject[11];
        neededIron = GameObject.FindGameObjectsWithTag("neededIron");

        GameObject[] neededLeather = new GameObject[11];
        neededLeather = GameObject.FindGameObjectsWithTag("neededLeather");

        //Text[] neededWoodText = new Text[neededWood.Length];
        //Text[] neededIronText = new Text[neededIron.Length];
        //Text[] neededLeatherText = new Text[neededLeather.Length];

        checkResources(neededWood, drewno);
        checkResources(neededIron, zelazo);
        checkResources(neededLeather, skora);




        string buttonName = button.name;

		if (buttonName == "Weapons") {
			othersCanvases[1].enabled = false;
            othersCanvases[0].enabled = true;
		} else if (buttonName == "Others") {
            othersCanvases[1].enabled = true;
            othersCanvases[0].enabled = false;
		} else {

			Debug.Log ("crafting nie dziala");
		}
	
	}
    

    public static void disableCanvases()
    {
        othersCanvases[0].enabled = false;
        othersCanvases[1].enabled = false;

    }


    public static string getChoosenItem()
    {
        return Crafting_Menu.choosenItem;

    }

    public void checkResources(GameObject[] neededResource, int surowiec)
    {
        Text[] neededResourceText = new Text[11];

        for (int i = 0; i < neededResource.Length - 1; i++)
        {
            neededResourceText[i] = neededResource[i].GetComponent<Text>();

            if (int.Parse(neededResourceText[i].text) >surowiec)
            {
                neededResourceText[i].color = Color.red;
            }
            else
            {
                neededResourceText[i].color = Color.green;
            }

        }


    }

    public bool SprawdzSurowce(string nazPrzed,int wymaganyPoziom, int ilDr = 0, int ilZel = 0, int ilSkr = 0)
    {
        if (wymaganyPoziom <= Player.poziomBohatera)
        {
            if (ekwipunekGracza.LiczIlosc("Drewno") >= ilDr && ekwipunekGracza.LiczIlosc("zelazo") >= ilZel && ekwipunekGracza.LiczIlosc("Skóra") >= ilSkr)
            {
                dzwiekTworzenia.Play();
                Slot dr = ekwipunekGracza.ZnajdzPrzedmiot("Drewno");
                ZabierzSurowce(dr, ilDr);
                Slot ze = ekwipunekGracza.ZnajdzPrzedmiot("zelazo");
                ZabierzSurowce(ze, ilZel);
                Slot sk = ekwipunekGracza.ZnajdzPrzedmiot("Skora");
                ZabierzSurowce(sk, ilSkr);
                return true;
            }
            else
            {
                StartCoroutine(komunikat.WyswieltKomunikat("Nie masz surowców aby stworzyć " + nazPrzed));
                return false;
            }
        }
        else
        {
            StartCoroutine(komunikat.WyswieltKomunikat("Nie masz wymaganego poziomu bohatera aby stworzyć " + nazPrzed));
            return false;
        }





    
    }

    public void ZabierzSurowce(Slot slotZsurowcami, int ilosc)
    {
        
        for (int i = 0; i < ilosc; i++)
        {
            slotZsurowcami.UzyjPrzedmiotu();
        }
    }



}
