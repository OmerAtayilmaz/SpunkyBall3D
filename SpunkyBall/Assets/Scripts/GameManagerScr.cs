using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScr : MonoBehaviour
{

    [Space]
    [Header("Karakter")]
    [Description("Karakterin eklendiği bölümdür.")]
    public GameObject character;

    [Space]
    [Header("Karakter Hız")]
    [Description("Karakterin Oyun içindeki ilerleyiş hızı")]
    [SerializeField]
    [Range(1.0f, 800.0f)]
    private float cSpeed;



    [Header("Karakter Kontrolleri")]
    [Description("Karakterin sağ-sol kontrollerini yapıldğıı bölümdür")]
    [SerializeField]
    [Range(1.0f, 200.0f)]
    private float hassasiyet;

    [Header("Componentler")]
    Rigidbody rb;


    [Space]
    [Header("Slider Kontrol")]
    [Description("Slider kontrollerinin yapıldığı bölüm")]
    [SerializeField]
    private Image imageofSlider;


    [Space]
    [Description("buttonManager Scriptine Erişim Sağladık")]
    buttonManager btnManager;



    [Space]
    [Description("Değişkenler")]
    [Header("Değişkenler")]
    [Range(1.0f,3.0f)]
    public int OyunDurum;
    //1- oyun başladı ve devam ediyor
    //2-oyun durdu
    //3-oyun bitti



    [Space]
    [Description("Motivasyon metinleri")]
    [Header("Text Yazıları")]
    [SerializeField]
    private string[] TextYazisi;

    [Space]
    [Description("Yazılacak TextMeshPro")]
    [Header("mot TextMeshPro")]
    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    [Space]
    [Description("METİN GÖSTERİCİ")]
    [Header("METİN GÖSTERİCİ")]
    [SerializeField]
    private GameObject textGosterici;
 
    //0-Oyun daha başlamadı
    //1- oyun başladı ve devam ediyor
    //2-oyun çarptı veya düştü - kaybedildi -
    //3-oyun bitti - kazandıız
    //4-oyun durduruldu.



    [Space]
    [Description("efekt")]
    [Header("Arka efekt")]
    [SerializeField]
    private GameObject arkaEfekt;


    [Space]
    [Description("Diamond Text")]
    [Header("TEXT TO COUNT DİAMOND")]
    [SerializeField]
    private Text textDiamond;

    [Space]
    [Description("Slider Asagi")]
    [SerializeField]
    private GameObject SliderTutucu;


    [Space]
    [Description("Trail Renderer Control")]
    [SerializeField]
    private TrailRenderer trailRendererControl;

    [Space]
    [Description("PathBuilder Erişim")]
    PathBuilderScr pathBuilderScr;

    [Space]
    [Description("Color of Character")]
    [Header("Renkler")]
    [SerializeField]
    private Material characterMaterial;


    [Space]
    [Description("Textler")]
    [Header("Level Textleri")]
    [SerializeField]
    Text[] textLevels;


    [Header("PATH COLOR")]
    [SerializeField]
    private Material pathColor;


    public float hizlan;
    public float hassasiyetArtir;


    [Space]
    [Header("TEXTSKOR")]
    [SerializeField]
    private Text textSkor;

    [Space]
    [Header("TEXTSKORWIN")] 
    public Text textSkorWin;

    public void TextLevelBilgiVer()
    {

        textLevels[0].text = PlayerPrefs.GetInt("level").ToString();
        textLevels[1].text = (PlayerPrefs.GetInt("level") + 1).ToString();

    }
    private void Awake()
    {

        OyunDurum = 0;
        hizlan = 0;
        hassasiyetArtir = 0;
        trailRendererControl = GameObject.Find("arkaEfekt").GetComponent<TrailRenderer>();
        trailRendererControl.startColor = RandmColors();
        trailRendererControl.endColor = RandmColors();
        SaveColor();


    }

    void Start()
    {
        rb = character.GetComponent<Rigidbody>();
        btnManager = GameObject.Find("UIManager").GetComponent<buttonManager>();
        pathBuilderScr = GameObject.Find("pathBuilder").GetComponent<PathBuilderScr>();
        ElmasYazdir();
        SaveColor();
    }

    void FixedUpdate()
    {

        switch (OyunDurum)
        {


            case 1:
                //oyunda
                arkaEfekt.transform.position = character.transform.position;
                characterControl();
                SliderKontrol();
                SliderTutucu.SetActive(true);
                TextLevelBilgiVer();
                characterFalling();
                break;


            case 3:
                //oyun bitti
       
                btnManager.PanelNextLevel();
                break;

        }
   
        
    }

    void SliderKontrol()
    {
        float GercekMesafe =   character.transform.position.z / pathBuilderScr.mesafeOlc;
        if (imageofSlider.fillAmount !=GercekMesafe)
        {
            imageofSlider.fillAmount =GercekMesafe;
        }
        else
        {
            imageofSlider.fillAmount = 0;
          //müzik çalma      FindObjectOfType<AudioManager>().Play("PlayerDeath");
        }

    }

    void characterControl()
    {


        if (Input.touchCount > 0) 
        {
          

            Touch dokunma = Input.GetTouch(0);

            if (dokunma.deltaPosition.x > 3)
            {
                hizlan += Time.deltaTime * 5;
                hassasiyetArtir += Time.deltaTime * 2;
                rb.velocity = new Vector3((hassasiyet+hassasiyetArtir) * Time.deltaTime, 0, (cSpeed + hizlan) * Time.deltaTime);
               

            }
            if (dokunma.deltaPosition.x < -3)
            {
                hizlan += Time.deltaTime * 5;
                hassasiyetArtir += Time.deltaTime * 2;
                rb.velocity = new Vector3(-(hassasiyet + hassasiyetArtir) * Time.deltaTime, 0, (cSpeed + hizlan) * Time.deltaTime);
          

            }
            else if(dokunma.phase==TouchPhase.Stationary)
            {

                hizlan += Time.deltaTime * 2;
           
                rb.velocity = new Vector3(0, 0, (cSpeed + hizlan) * Time.deltaTime);

            }
        }
        else
        {
            
            rb.velocity = new Vector3(0, 0, 0);
            rb.velocity = Vector3.zero;
            hizlan = 0;
            hassasiyetArtir = 0;

        }

    }

    public void LevelAtlama()
    {
        imageofSlider.fillAmount = 0;
        if(PlayerPrefs.GetInt("level",1)<=1)
        {
            PlayerPrefs.SetInt("level", 2);
        }
        else
        {
            PlayerPrefs.SetInt("level", PlayerPrefs.GetInt("level") + 1);
            pathColor.color = RandmColors();
            
        }
    }

  public  IEnumerator motText(string text)
    {
        textMeshPro.text = text;
        textGosterici.SetActive(true);
        yield return new WaitForSeconds(1.0f);

        textGosterici.SetActive(false);
        StopCoroutine(motText(""));

  
    }

   public void characterFalling()
    {


        float deger = character.transform.position.y;
        if(deger<=0.1f)
        {
            textSkor.text = "+ " + PlayerPrefs.GetInt("toplanan").ToString();
            PlayerPrefs.SetInt("toplanan", 0);
            btnManager.panelGameOverControl();
            OyunDurum = 2;
          

        }
    }
   

    public void ElmasYazdir()
    {

        textDiamond.text = PlayerPrefs.GetInt("diamond").ToString();
      
    }

    private Color RandmColors()
    {
        Color[] renkler = new Color[9];

        renkler[0] = Color.Lerp(Color.blue, Color.black, 0.5f);
        renkler[1] = Color.red;
        renkler[2] = Color.Lerp(Color.magenta, Color.black, 0.5f); ;
        renkler[3] = Color.cyan;
        renkler[4] = Color.black;
        renkler[5] = Color.Lerp(Color.gray, Color.black, 0.5f);
        renkler[6] = Color.Lerp(Color.grey, Color.black, 0.5f);
        renkler[7] = Color.Lerp(Color.yellow, Color.black, 0.5f);
        renkler[8] = Color.magenta;

        Color secilen = renkler[UnityEngine.Random.Range(0, 8)];
         return secilen;
    }

   public IEnumerator ContinueDirilme()
    {
        yield return new WaitForSeconds(1.0f);
        character.transform.position = new Vector3(0, 0.5f, character.transform.position.z - 3.0f);
        OyunDurum = 1;
    }

    public void SaveColor()
    {
        int deger = PlayerPrefs.GetInt("giyilen", -1);

        switch (deger)
        {
            case 0:
                characterMaterial.color = Color.Lerp(Color.blue, Color.red, 0.5f);
                break;
            case 50:
                characterMaterial.color = Color.blue;
                break;
            case 150:
                characterMaterial.color = Color.yellow;
                break;
            case 300: //imagelerini degistirmeyi unutma, bir tane beyaz yuvarlak koysan hallolur aslında image rengini değiştirirz
                characterMaterial.color = Color.blue;
                break;
            case 500:
                characterMaterial.color = Color.green;
                break;
            case 1000:
                characterMaterial.color = Color.red;
                break;
        }
    }


    public void Kaybettiniz()
    {
        CancelInvoke("characterFalling");
        btnManager.panelGameOverControl();
        textSkor.text = "+ " + PlayerPrefs.GetInt("toplanan").ToString();
        PlayerPrefs.SetInt("toplanan", 0);
    
    }
    
   
}
