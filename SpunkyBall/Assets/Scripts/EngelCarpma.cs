using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EngelCarpma : MonoBehaviour
{

    GameManagerScr managerScr;
    buttonManager buttonManagerKontrol;
    void Start()
    {
        PlayerPrefs.SetInt("toplanan", 0);
        buttonManagerKontrol = GameObject.Find("UIManager").GetComponent<buttonManager>();
        managerScr = GameObject.Find("GameManager").GetComponent<GameManagerScr>();

    }

   
    void Update()
    {
        //if (managerScr.OyunDurum == 1)
        //{


        //    managerScr.characterFalling();
        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="engel")
        {
            managerScr.textSkorWin.text = "+ " + PlayerPrefs.GetInt("toplanan");
            managerScr.OyunDurum = 2;
            managerScr.Kaybettiniz();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag=="Finish")
        {
            managerScr.textSkorWin.text = "+ " + PlayerPrefs.GetInt("toplanan");
           

            buttonManagerKontrol.PanelNextLevel();
        }
    }
}
