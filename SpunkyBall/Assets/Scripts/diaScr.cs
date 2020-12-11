using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class diaScr : MonoBehaviour
{
    GameManagerScr yoneticiScr;
    AudioManager sesKontrol;
    void Start()
    {
        gameObject.name = Random.Range(0, 2).ToString();
        if(gameObject.name=="0")
        {
            gameObject.SetActive(false);
        }
        else
        {
            StartCoroutine(hareket());
            yoneticiScr = GameObject.Find("GameManager").GetComponent<GameManagerScr>();
            sesKontrol = GameObject.Find("AudioManager").GetComponent<AudioManager>();
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "character")
        {
            sesKontrol.Play("altinToplandi");
            PlayerPrefs.SetInt("diamond", PlayerPrefs.GetInt("diamond", 0) + 1);
            PlayerPrefs.SetInt("toplanan", (PlayerPrefs.GetInt("toplanan") + 1));
            yoneticiScr.ElmasYazdir();
            Destroy(gameObject, 0.1f);

        }
    }

    IEnumerator hareket()
    {
        gameObject.transform.Rotate(0, 0, 30 * Time.deltaTime);
        yield return new WaitForSeconds(0.0f);
        StartCoroutine(hareket());
    }
}
