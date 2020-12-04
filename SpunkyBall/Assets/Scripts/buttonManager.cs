using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class buttonManager : MonoBehaviour
{
    [Space]
    [Header("Panels")]
    [Description("All of the panels in the game")]
    [SerializeField]
    private GameObject panelNextLvl, PanelStore, PanelPause, PanelMenu, PanelAch,panelGameOver;


   
    [Description("Diğer Scriptlere Erişim Sağlama bölümü")]
    private GameManagerScr managerScript;
   
    [Space]
    [Description("Panel açılma animasyon klibi")]
    [Header("Animation clips")]
    [SerializeField]
    private GameObject[] clipPanelPlay;


    AudioManager sesKontrol;

    [Space]
    [Header("Ses spriteleri")]
    [SerializeField]
    private Sprite[] gorseller;

    public Button btnSes;
    public void Seskontrol()
    {
        if(sesKontrol.gameObject.activeSelf==true)
        {
            sesKontrol.gameObject.SetActive(false);
            btnSes.image.sprite = gorseller[0];
        }
        else
        {
            sesKontrol.gameObject.SetActive(true);
            btnSes.image.sprite = gorseller[1];


        }
    }

    public GameObject gameObjectGecis;

    private void Awake()
    {
        gameObjectGecis.GetComponent<Animator>().SetTrigger("AnimAc");
    }
    private void Start()
    {
        managerScript = GameObject.Find("GameManager").GetComponent<GameManagerScr>();
        sesKontrol = GameObject.Find("AudioManager").GetComponent<AudioManager>();

    }

    public void PanelNextLevel()
    {
        sesKontrol.Play("click");
        if (panelNextLvl.activeSelf==false)
        {  

           panelNextLvl.SetActive(true);
           managerScript.LevelAtlama();
        }
  

    }

    public void PanelOpenStore()
    {
        sesKontrol.Play("click");

        if (PanelStore.activeSelf==false)
        {
            PanelStore.SetActive(true);
        }
        else
        {
            PanelStore.SetActive(false);
        }
    }

    public void PanelOpenMenu()
    {
        sesKontrol.Play("click");

        PanelMenu.SetActive(true);
    }

    public void PanelOpenAch()
    {
        sesKontrol.Play("click");


        if (PanelAch.activeSelf==false)
        {
            PanelAch.SetActive(true);
        }
        else
        {
            PanelAch.SetActive(false);
        }

    }



    public void PanelOpenPause()
    {
        sesKontrol.Play("click");
        PanelPause.SetActive(true);
    }

    public void GoToNextLevel()
    {
       
        if(panelNextLvl.activeSelf==true)
        {
            gameObjectGecis.SetActive(true);
            gameObjectGecis.GetComponent<Animator>().SetTrigger("AnimKapa");
            
        }
    }

    public void StartGame()
    {
        if (PanelMenu.activeSelf == true)
        {
            clipPanelPlay[0].GetComponent<Animator>().SetBool("panelPlayOynat", true);
            StartCoroutine(work());
     

        }
    }
  public  IEnumerator work()
    {
        yield return new WaitForSeconds(2.0f);
        PanelMenu.SetActive(false);
        managerScript.OyunDurum = 1;
        StopCoroutine(work());
    }

    public void PauseToCountinue()
    {
        if (PanelPause.activeSelf == true)
        {
            PanelPause.SetActive(false);
            managerScript.OyunDurum = 1;

        }
    }

    public void DestroyObjects(string ObjectTag)
    {
        GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

        for (int i = 0; i < GameObjects.Length; i++)

        {
            if (GameObjects[i].tag == ObjectTag)
            {
                GameObjects[i].gameObject.SetActive(false);
                Debug.Log("Panel kapatıldı");
            }
        } 
    
    }

    public void panelGameOverControl()
    {

        if(panelGameOver.activeSelf==false)
        {
            panelGameOver.SetActive(true);
        }
       
    }

     public void PlayAgain()
    {
        gameObjectGecis.SetActive(true);

        gameObjectGecis.GetComponent<Animator>().SetTrigger("AnimKapa");

    }

    public void ReBornAfterAds()
    {
       StartCoroutine( managerScript.ContinueDirilme());
        panelGameOver.SetActive(false);
    }

}
