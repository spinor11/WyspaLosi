using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    [SerializeField]
    private float speed;
    [SerializeField]
    private GameObject bron;
    [SerializeField]
    private GameObject prawaDlon;
    private Vector2 kierunek;
    private GameObject pochodnia;
    private Animator animator;
    [SerializeField]
    private Image czerwonyEkran;
    private float czasWyswietlaniaCzerwonegoEkranu = 0.2f;
    public GameObject Luk;
    private bool atakuje;
    private bool jestPrzyOgnisku;
    public static Vector2 pozycjaGracza;
    public float czasOczekiwaniaNaAtak;
    private float czasAtaku;
    [SerializeField]
    private Statystyka punktyZdrowia;
    [SerializeField]
    private Statystyka glod;
    [SerializeField]
    private Statystyka pragnienie;
    [SerializeField]
    Statystyka doswiadczenie;
    Slot slotSkrypt;
    [SerializeField]
    ZadanieNaprawyLodzi za;
    [SerializeField]
    Text poziomBoahteraNaPasku;
    public Camera mapaCamera;
    public Camera mainCamera;
    public static bool trybMapy = false;
    public static int poziomBohatera = 1;
    //public static Canvas ekwipunek;
    public Ekwipunek ekwipunekGracza;
    bool komunikatWyswietlony = false;
    public Komunikat PKomunikat;
    public static Przedmiot aktywnaBron;
    public static AudioSource PlayerAuSour;
    public List<SlotSzybkiegoDostepu> slotySzybkiegoDostepu;
    //private string aktywnaBron;
	public GameObject menu;


    public  float AktualnePunktyZdrowia
    {
        set
        {
            
            punktyZdrowia.AktualnaWartosc = value;
            if (AktualnePunktyZdrowia <= 20)
            {
                mainCamera.GetComponent<AudioSource>().UnPause();
            }
            else
                mainCamera.GetComponent<AudioSource>().Pause();
            Smierc();
        }
        get
        {
            return punktyZdrowia.AktualnaWartosc;
        }
    }
    public float AktualnePunktyGlodu
    {
        set
        {
            glod.AktualnaWartosc = value;

        }
        get
        {
            return glod.AktualnaWartosc;
        }
    }
    public float AktualnePunktyPragnienia
    {
        set
        {
            pragnienie.AktualnaWartosc = value;
        }
        get
        {
            return pragnienie.AktualnaWartosc;
        }
    }
    public float AktualnePunktyDoswiadczenia
    {
        set
        {
            doswiadczenie.AktualnaWartosc = value;
            

        }
        get
        {
            return doswiadczenie.AktualnaWartosc;
        }
    }

    public Vector2 PozycjaGracza
    {
        get
        {
            return transform.position;
        }

        set
        {
            pozycjaGracza = value;
        }
    }

    public bool TrybMapy
    {
        get
        {
            return trybMapy;
        }
    }

    public bool Atakuje
    {
        get
        {
            return atakuje;
        }

        set
        {
            atakuje = value;
            
        }
    }

    public bool JestPrzyOgnisku
    {
        get
        {
            return jestPrzyOgnisku;
        }

        set
        {
            jestPrzyOgnisku = value;
            ekwipunekGracza.PrzygasOgnisko(value);
        }
    }

    //public string AktywnaBron
    //{
    //    get
    //    {
    //        return aktywnaBron;
    //    }
    //}

    //public Statystyka PunktyZdrowia
    //{
    //    get
    //    {
    //        return punktyZdrowia;
    //    }

    //    set
    //    {
    //        punktyZdrowia = value;
    //    }
    //}

    // Use this for initialization
    private void Awake()
    {
        slotSkrypt = FindObjectOfType<Slot>();
        punktyZdrowia.RozpocznijStat();
        glod.RozpocznijStat();
        pragnienie.RozpocznijStat();
        doswiadczenie.RozpocznijStat();
        

        //punktyZdrowia.RozpocznijStat();
        //pragnienie.RozpocznijStat();
    }
    void Start () {
		aktywnaBron = new Przedmiot("temp", "temp", null, 1);
        
		PlayerAuSour = GetComponent<AudioSource>();
        JestPrzyOgnisku = false;
        pochodnia = transform.Find("Pochodnia").gameObject;
        animator = GetComponent<Animator>();
        Atakuje = false;
        poziomBoahteraNaPasku.text = "Poz. " + poziomBohatera;
        mainCamera.GetComponent<AudioSource>().Play();
        mainCamera.GetComponent<AudioSource>().Pause();
        Debug.Log("W końcu porządek w logach xD");
		Debug.Log("Aż miło popatrzec :D");
        
    }
   
	
	// Update is called once per frame
	void Update ()
    {
        Debug.Log(Ekwipunek.PusteSloty);
        Move();
        czyAtakuje();
        GetInput();
        ZwiekszGlodiPragnienie();
        ObrazeniaOdNocy();
        if (Input.GetKeyDown(KeyCode.T))
        {
            ZamienWLosia();
        }
        
        
        
    }
    private void Move()
    {

        
        transform.Translate(kierunek * speed * Time.deltaTime);

        AnimujRuch(kierunek);
    }
    

    
    private void GetInput()
    {
        kierunek = Vector2.zero;
        if (Input.GetKeyDown(KeyCode.M))
        {
            if (mapaCamera.isActiveAndEnabled)
            {
                cameraScript.mainCamera.gameObject.SetActive(true);
                mapaCamera.gameObject.SetActive(false);
                trybMapy = false;
                Time.timeScale = 1;
            }
            else
            {
                mapaCamera.gameObject.SetActive(true);
                cameraScript.mainCamera.gameObject.SetActive(false);
                Time.timeScale = 0;
                trybMapy = true;
            }
                
            
            
        }

		if (Input.GetKeyDown(KeyCode.Escape)&&!(Ekwipunek.ekwipunekCanvas.enabled||trybMapy||Crafting_Menu.craft_menu.enabled))
        {
			if (menu.activeSelf)
            {
				menu.SetActive(false);
				Time.timeScale = 1;
			} else {
				menu.SetActive(true);
				Time.timeScale = 0;
			}
		}


        if (!animator.GetBool("CzyStrzela") && LosGracz.GraczJestLosiem==false)
        {
             if (Input.GetKey(KeyCode.W))
            {
                Debug.Log(ekwipunekGracza.LiczIlosc("Strzaly"));
                kierunek += Vector2.up;
                
            }
            else if (Input.GetKey(KeyCode.S))
            {
                kierunek += Vector2.down;
            }
            else if (Input.GetKey(KeyCode.A))
            {
                kierunek += Vector2.left;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                kierunek += Vector2.right;
                ekwipunekGracza.ZnajdzPrzedmiot("Strzały");
            }
        }
        
        if (Input.GetKeyDown(KeyCode.B))
        {
            WlaczEkwipunek();

        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            
            WlaczCrafting();

        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {

			if (Ekwipunek.ekwipunekCanvas.enabled||Crafting_Menu.craft_menu.enabled||trybMapy)
            {
                Ekwipunek.ekwipunekCanvas.enabled = false;
				Crafting_Menu.craft_menu.enabled = false;
                cameraScript.mainCamera.gameObject.SetActive(true);
                mapaCamera.gameObject.SetActive(false);
                trybMapy = false;
                Time.timeScale = 1;
            }
//			else if (Crafting_Menu.craft_menu.enabled)
//			{
//				Crafting_Menu.craft_menu.enabled = false;
//				cameraScript.mainCamera.gameObject.SetActive(true);
//				mapaCamera.gameObject.SetActive(false);
//				trybMapy = false;
//				Time.timeScale = 1;
//			}
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Slot temp;
            SlotSzybkiegoDostepu.powiazaniaZeSlotami.TryGetValue(slotySzybkiegoDostepu[0], out temp);
            if (temp != null)
                temp.UzyjPrzedmiotu();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Slot temp;
            SlotSzybkiegoDostepu.powiazaniaZeSlotami.TryGetValue(slotySzybkiegoDostepu[1], out temp);
            if (temp != null)
                temp.UzyjPrzedmiotu();

        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {

            Slot temp;
            SlotSzybkiegoDostepu.powiazaniaZeSlotami.TryGetValue(slotySzybkiegoDostepu[2], out temp);
            if(temp != null)
            temp.UzyjPrzedmiotu();

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Slot temp;
            SlotSzybkiegoDostepu.powiazaniaZeSlotami.TryGetValue(slotySzybkiegoDostepu[3], out temp);
            if (temp != null)
                temp.UzyjPrzedmiotu();

        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Slot temp;
            SlotSzybkiegoDostepu.powiazaniaZeSlotami.TryGetValue(slotySzybkiegoDostepu[4], out temp);
            if (temp != null)
                temp.UzyjPrzedmiotu();

        }

        if (Input.GetMouseButtonDown(0) && kierunek == Vector2.zero && aktywnaBron.nazwa == "Luk") // TUTAJ
        {
                Luk.GetComponent<SpriteRenderer>().enabled = true;
                animator.SetBool("CzyStrzela", true);
                Luk.GetComponent<Animator>().SetBool("Naciaganie", true);


        }

        if (Input.GetMouseButtonDown(0) && kierunek == Vector2.zero && aktywnaBron.nazwa != "Luk" && trybMapy == false && LosGracz.GraczJestLosiem == false) //TUTAJ
        {
                Vector2 pozycjaMyszki = Camera.main.ScreenToWorldPoint(Input.mousePosition);


            if ((pozycjaMyszki.y > PozycjaGracza.y) && (pozycjaMyszki.x > PozycjaGracza.x - 0.45f && pozycjaMyszki.x < PozycjaGracza.x + 0.45f) && (Time.time > czasAtaku))
            {
                
                animator.SetTrigger("AtakWGore");
                //Slot.slotZAkwaytnaBronia.Przedmioty.Pop();
                Atakuje = true;
                aktywnaBron.ZmniejszWytrzymalosc();
                czasAtaku = czasOczekiwaniaNaAtak + Time.time;
            }
            else if (pozycjaMyszki.x < PozycjaGracza.x && (pozycjaMyszki.y > PozycjaGracza.y - 0.5f && pozycjaMyszki.y < PozycjaGracza.y + 0.4f) && (Time.time > czasAtaku))
            {
                Atakuje = true;
                animator.SetTrigger("AtakWLewo");
                aktywnaBron.ZmniejszWytrzymalosc();
                czasAtaku = czasOczekiwaniaNaAtak + Time.time;

            }
            else if (pozycjaMyszki.x > PozycjaGracza.x && (pozycjaMyszki.y > PozycjaGracza.y - 0.5f && pozycjaMyszki.y < PozycjaGracza.y + 0.5f) && (Time.time > czasAtaku))
            {
                Atakuje = true;
                animator.SetTrigger("AtakWPrawo");
                aktywnaBron.ZmniejszWytrzymalosc();
                czasAtaku = czasOczekiwaniaNaAtak + Time.time;

            }
            else if (pozycjaMyszki.y < PozycjaGracza.y && (pozycjaMyszki.x > PozycjaGracza.x - 0.45f && pozycjaMyszki.x < PozycjaGracza.x + 0.45f) && (Time.time > czasAtaku))
            {
                Atakuje = true;
                animator.SetTrigger("AtakWDol");
                aktywnaBron.ZmniejszWytrzymalosc();
                czasAtaku = czasOczekiwaniaNaAtak + Time.time;

            }
            else
                Atakuje = false;
            
        }
        
        if (Input.GetMouseButtonUp(0) && animator.GetBool("CzyStrzela"))
        {
            Luk.GetComponent<Animator>().SetBool("Naciaganie", false);
            Luk.GetComponent<Luk>().Strzal();
            Luk.GetComponent<Animator>().SetTrigger("Strzal");
            animator.SetBool("CzyStrzela", false);
            Luk.GetComponent<SpriteRenderer>().enabled = false;
        }

        
    }
    public void AnimujRuch(Vector2 direction)
    {
        animator.SetInteger("xx", (int)direction.x);
        animator.SetInteger("yy", (int)direction.y);
    }
    public void czyAtakuje()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsTag("Atak"))
        {
            Atakuje = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Przedmiot")
        {
            if (Ekwipunek.PusteSloty == 0 && !ekwipunekGracza.ZnajdzPrzedmiot(collision.name))
            {
                StartCoroutine(PKomunikat.WyswieltKomunikat("Masz pełny ekwipunek!"));
                return;
            }
            ekwipunekGracza.DodajPrzedmiot(collision.GetComponent<Przedmiot>());
            Debug.Log("Dodano " + collision.gameObject.name + " do plecaka");
            Destroy(collision.gameObject);
           
        }
        else if (collision.name == "Światło")
        {
            JestPrzyOgnisku = true;

        }
        else if(collision.name == "roslinaKolec")
        {
            ZadajObrazenia(30);
        }
        else if(collision.name == "RozwalonaLodka")
        {
            if(ZadanieNaprawyLodzi.czyRozpoczetoZadanie == false)
            {
                za.PokazPotrzebneSurowce();
                ZadanieNaprawyLodzi.czyRozpoczetoZadanie = true;
                PKomunikat.WyswieltKomunikatZPotwierdzeniem("Łódź!!! Ciekawe skąd się tutaj wzięła?! Niestety, jest ona w fatalnym stanie... Lecz jeśli dobrze poszukam to z pewnością znajdę potrzebne materiały na jej odbudowę! Czas brać się do roboty!(Przedmioty potrzebne do realizacji zadania znajdziesz w ekwipunku)");
            }
            else if (ZadanieNaprawyLodzi.zadanieSkonczone)
            {
                PKomunikat.WyswieltKomunikatZPotwierdzeniem("Nareszcie! Zebrałem wszystkie materiały potrzebne do naprawy łodzi! Mogę wracać do domu!");
                    StartCoroutine(za.NaprawLodz());
            }
          
        }
    

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Światło")
        {
            JestPrzyOgnisku = false;;
            Debug.Log("niejestPrzyuOgnisku");
            komunikatWyswietlony = false;

        }
        //else if (collision.name == "Rzeka")
        //{
        //    CancelInvoke();
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Rzeka")
        {
            AktualnePunktyPragnienia = -10;
        }
    }

    public void Leczenie(int LiczbaUleczonychPktZdrowia)
    {
        AktualnePunktyZdrowia += LiczbaUleczonychPktZdrowia;
    }
    public void ZwiekszGlodiPragnienie()
    {
        glod.AktualnaWartosc = Time.deltaTime*0.7f + glod.AktualnaWartosc;
        pragnienie.AktualnaWartosc = Time.deltaTime*0.7f + pragnienie.AktualnaWartosc;
        if (AktualnePunktyGlodu == glod.MaksymalnaWartosc)
        {
            AktualnePunktyZdrowia = AktualnePunktyZdrowia - Time.deltaTime * 0.3f;
            StartCoroutine(PKomunikat.WyswieltKomunikat("Otrzymujesz obrażenia od głodu! Zjedz coś!"));
        }
        if(pragnienie.AktualnaWartosc == pragnienie.MaksymalnaWartosc)
        {
            AktualnePunktyZdrowia = AktualnePunktyZdrowia - Time.deltaTime * 0.3f;
            StartCoroutine(PKomunikat.WyswieltKomunikat("Otrzymujesz obrażenia od pragnienia! Podejdz do rzeki aby się napić!"));
        }

    }
    public void Smierc()
    {
        if (AktualnePunktyZdrowia == 0)
        {
            AktualnePunktyZdrowia = punktyZdrowia.MaksymalnaWartosc;
            AktualnePunktyGlodu = 0;
            AktualnePunktyPragnienia = 0;
            doswiadczenie.AktualnaWartosc = doswiadczenie.AktualnaWartosc - 300;
            try
            {
                GameObject temp = GameObject.Find("Ognisko");
            }
            catch
            {
                transform.position = new Vector2(0, 0);
                return;
            }
            GameObject ognisko = GameObject.Find("Ognisko");
            transform.position = new Vector2(ognisko.transform.position.x,ognisko.transform.position.y);
            StartCoroutine(PKomunikat.WyswieltKomunikat("Zginąłeś!"));
        }
    }
    public void PrzyznajDoswiadczenie(int przyznanePunktyDoswiadczenia)
    {
        doswiadczenie.AktualnaWartosc += przyznanePunktyDoswiadczenia;
        AwansNaWyzszyPoziom();
    }
    public void AwansNaWyzszyPoziom()
    {
        if (doswiadczenie.AktualnaWartosc == doswiadczenie.MaksymalnaWartosc)
        {
            poziomBohatera += 1;
            poziomBoahteraNaPasku.text = "Poz. " + poziomBohatera;
            doswiadczenie.AktualnaWartosc = 0;
            punktyZdrowia.MaksymalnaWartosc += 20;
            doswiadczenie.MaksymalnaWartosc = doswiadczenie.MaksymalnaWartosc + 300 * poziomBohatera;
            StartCoroutine(PKomunikat.WyswieltKomunikat(string.Format("Gratulacje,osiągnałeś poziom {0}!",poziomBohatera)));
        }
    }
    public void ZadajObrazenia(float iloscObrazen)
    {
        AktualnePunktyZdrowia -= iloscObrazen;
        if (!czerwonyEkran.IsActive())
        StartCoroutine("CzerwonyEkran");
        
        
    }
    public void ObrazeniaOdNocy()
    {
        if (TrybDniaINocy.jestNoc == true && !JestPrzyOgnisku)
        {
            if (LosGracz.GraczJestLosiem==false)
            {
                ZadajObrazenia(4 * Time.deltaTime);
                if (!komunikatWyswietlony)
                {
                    StartCoroutine(PKomunikat.WyswieltKomunikat("Otrzmujesz obrazenia od zimna, wróć do ogniska"));
                    komunikatWyswietlony = true;
                }
            }
            
            
            
        }
    }
    public void ZalozPochodnie()
    {
        pochodnia.SetActive(pochodnia.activeSelf ? false : true);


    }
    public  IEnumerator CzerwonyEkran()
    {
        czerwonyEkran.gameObject.SetActive(true);
        yield return new WaitForSeconds(czasWyswietlaniaCzerwonegoEkranu);
        czerwonyEkran.gameObject.SetActive(false);
    }
    public static void WlaczEkwipunek()
    {
        

            if (Ekwipunek.ekwipunekCanvas.enabled == true)
            {
            Ekwipunek.ekwipunekCanvas.enabled = false;
                Time.timeScale = 1;
            }
            else
            {
            Ekwipunek.ekwipunekCanvas.enabled = true;
            Time.timeScale = 0;
        }
        }

    public static void WlaczCrafting()
    {
        

        if (Crafting_Menu.craft_menu.enabled == true)
        {
            Crafting_Menu.craft_menu.enabled = false;
            Crafting_Menu.disableCanvases();
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
            Crafting_Menu.craft_menu.enabled = true;
        }
    }
    public void UstawBroń()
    {
        //Debug.Log(nazwaBroni);
        //if (aktywnaBron.nazwa==nazwaBroni)
        //{
        //    foreach (var slot in slotySzybkiegoDostepu)
        //    {
        //        if (slot.nazwaPrzedmiotuWSlocie == aktywnaBron.nazwa) //Aktywna bron, jeszcze przed aktualizacja zmiennej, wiec usuwamy z niej obramowke
        //        {
        //            slot.ustawObramowke(nazwaBroni);
        //        }


        //    }
        //    aktywnaBron.nazwa = nazwaBroni;
        //    SkryptBroni.AktywnaBron = aktywnaBron.nazwa;
        //}

        foreach (var slot in slotySzybkiegoDostepu)
        {
            if (slot.nazwaPrzedmiotuWSlocie == aktywnaBron.nazwa)
            {
                slot.ustawObramowke();
            }
            else slot.UsunObramowke();

        }
    }


    public void KomunikatZnisczonaBron(string nazwaBroni)
    {
        StartCoroutine(PKomunikat.WyswieltKomunikat(string.Format("Twoja broń {0} uległa zniszczeniu!", Player.aktywnaBron.nazwa)));
        UsunAktywnaBron();
    }
    public void UsunAktywnaBron()
    {
        aktywnaBron = new Przedmiot("temp", "temp", null, 1);
    }

    public void ZamienWLosia()
    {
        
        GameObject LosGracza = Instantiate(Resources.Load("LosGracza"), transform) as GameObject;
        LosGracza.transform.parent = null;
        LosGracza.name = "LosGracza";
        LosGracz.GraczJestLosiem = true;
        UkryjGracza();
        
        
    }

    private void UkryjGracza()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<BoxCollider2D>().enabled = false;
        PlayerAuSour.enabled = false;
        punktyZdrowia.MaksymalnaWartosc = 10000;
        AktualnePunktyZdrowia = 10000;
    }



}



