using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PathBuilderScr : MonoBehaviour
{
    [Header("Yol Olusturma")]
    public GameObject[] pathPrefabs;


    [Header("Yol Başlangıç Kordinat")]
    public GameObject path;
    public float ilerle;

    float yeniKordi;
    float ScaleDegeri;
    GameObject olusanYol;

    GameObject[] olusturulanYollar;

    [Header("Bitişler")]
    public GameObject[] final;

    public float mesafe;
    GameManagerScr yoneticiScript;

    public float mesafeOlc;
    private void Awake()
    {
        mesafeOlc = 0;
        int olusacakYolSayisi = (PlayerPrefs.GetInt("level") / 10)+5;
        yoneticiScript = GameObject.Find("GameManager").GetComponent<GameManagerScr>();
        StartCoroutine(yolOlustur(olusacakYolSayisi));

    }
    void Start()
    {
        ilerle = 0;
        StartCoroutine(yolDiz());
        mesafe = final[0].transform.position.z;
    
    }
  



    IEnumerator yolOlustur(int count )
    {
        olusturulanYollar = new GameObject[count];
       for(int i = 0; i <= count-1; i++)
        {
           int rast =  UnityEngine.Random.Range(0,7);
            if (i == (count-1))
            {
                GameObject finish= final[0];

                olusturulanYollar[i] = Instantiate(finish);
            }
            else
            {
                GameObject nesne = Instantiate(pathPrefabs[rast]);
                olusturulanYollar[i] = nesne;

            }
            olusturulanYollar[i].transform.SetParent(path.transform);
            olusturulanYollar[i].SetActive(false);

        }
        yield return new WaitForSeconds(0.0f);

        StopCoroutine(yolOlustur(0));
    }

    IEnumerator yolDiz()
    {
        int yolSayisi = olusturulanYollar.Length;


        for (int i = 0; i < yolSayisi; i++)
        {

            olusturulanYollar[i].SetActive(true);

            if (i != 0)
            {
          
                float boyut = Convert.ToInt32(olusturulanYollar[i - 1].gameObject.tag.ToString().Substring(0, 1)); 
                olusturulanYollar[i].gameObject.transform.position = new Vector3(0, 0, (olusturulanYollar[i - 1].transform.position.z + boyut));
                mesafeOlc += boyut;

            }
            else
            {
                mesafeOlc += path.transform.position.z ;
                olusturulanYollar[i].gameObject.transform.position = new Vector3(0, 0, path.transform.position.z-1.5f);
            }
        }

        yield return new WaitForSeconds(0.0f);
    }

   
       
}
