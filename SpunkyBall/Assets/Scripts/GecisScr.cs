using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GecisScr : MonoBehaviour
{

    public void Acilis()
    {
        gameObject.SetActive(false);
    }
    public void Kapanis()
    {
        gameObject.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
