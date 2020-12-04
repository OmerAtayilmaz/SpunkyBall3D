using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoreScript : MonoBehaviour
{

    [Header("fiyat")]
    [SerializeField]
    private int materyal_fiyat;
    [Header("durum")]
    [SerializeField]
    private int materyal_durum;

    Button satinAlbtn;

    GameManagerScr gameManagerScr;
    
    void Start()
    {
        gameManagerScr = GameObject.Find("GameManager").GetComponent<GameManagerScr>();
        satinAlbtn = GameObject.Find("BuyButton").GetComponent<Button>();
        veriGonder();
    }


    void Update()
    {
        
    }

    public void veriGonder()
    {
        Text textBuy = GameObject.Find("BuyText").GetComponent<Text>();
        textBuy.text = materyal_fiyat.ToString();
        if(PlayerPrefs.GetInt("alindi:"+textBuy.text)==1)
        {
            satinAlbtn.interactable = false;
            gameObject.GetComponent<Image>().color = Color.green;

            PlayerPrefs.SetInt("giyilen", Convert.ToInt32(textBuy.text.ToString()));
            GameObject.Find("GameManager").GetComponent<GameManagerScr>().SaveColor();
            textBuy.text = "Changed";
        }
        else
        {
            satinAlbtn.interactable = true;

        }
    }

    public void satinAl()
    {
        Text textBuy = GameObject.Find("BuyText").GetComponent<Text>();
        int id = Convert.ToInt32(textBuy.text);
        
        //satın alınmamışsa satın al
        if(PlayerPrefs.GetInt("alindi:"+id,0)==0)
        {

                 if (PlayerPrefs.GetInt("diamond")>=id)
                  {
                     PlayerPrefs.SetInt("diamond", (PlayerPrefs.GetInt("diamond") - id));
                     PlayerPrefs.SetInt("alindi:"+id, 1);
                     satinAlbtn.interactable = false;
                     gameManagerScr.ElmasYazdir();
                  }
                 else
                  {
                     StartCoroutine(gameManagerScr.motText("Yetersiz Altın!"));
                  }

        }
        //zaten satın alınmışsa giy
        else if(PlayerPrefs.GetInt("alindi:" + id)==1)
        {
            gameManagerScr.SaveColor();
            PlayerPrefs.SetInt("durum:" + id, 1);
        }
    
    }

    
}
