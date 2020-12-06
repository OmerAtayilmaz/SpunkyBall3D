using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class AchScript : MonoBehaviour
{
    [Space]
    [Description("Materyal Ve durumlar")]
    [Header("id")]
    [SerializeField]
    private int id;

  
    [Space]
    [Description("Materyal Ve durumlar")]
    [Header("odul")]
    [SerializeField]
    private int odul;

    [Space]
    [Description("Materyal Ve durumlar")]
    [Header("odul")]
    [SerializeField]
    private int kosulLevel;
    GameManagerScr yoneticiScr;
    void Start()
    {
        yoneticiScr = GameObject.Find("GameManager").GetComponent<GameManagerScr>();

        durumKontrol();
    }

   public void satinAl()
    {
        //daha önce satın alınmışmı
       if (PlayerPrefs.GetInt("id:" + id, 0)== 0)
        {
        
            //level koşulunu karşılıyormu
            if(PlayerPrefs.GetInt("level")>=kosulLevel)
            {
                Debug.Log("Önceki Elmas: " + PlayerPrefs.GetInt("diamond"));
                PlayerPrefs.SetInt("diamond", PlayerPrefs.GetInt("diamond") + odul);
                Debug.Log("Sonraki Elmas: " + PlayerPrefs.GetInt("diamond"));
                PlayerPrefs.SetInt("id:"+id, 1);
                gameObject.GetComponent<Button>().interactable = false;
            }
            else
            {
                gameObject.GetComponent<Button>().interactable = false;

            }
        }
       else
        {
            gameObject.GetComponent<Button>().interactable = false;
        }

 
    }
  public  void durumKontrol()
    {
        if ((PlayerPrefs.GetInt("id:" + id, 0) == 1) )
        {
            gameObject.GetComponent<Button>().interactable = false;
            gameObject.GetComponent<Button>().GetComponentInChildren<Text>().text = "Collected";
            gameObject.GetComponent<Button>().GetComponentInChildren<Text>().fontSize = 30;
        }
        if (PlayerPrefs.GetInt("level") < kosulLevel)
        {
            gameObject.GetComponent<Button>().interactable = false;



        }

    }
}
