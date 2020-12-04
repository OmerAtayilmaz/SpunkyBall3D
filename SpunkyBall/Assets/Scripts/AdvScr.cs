using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdvScr : MonoBehaviour
{
   
    void Start()
    {
        
    }

   
    void Update()
    {
        transform.Rotate(50*Time.deltaTime, 50 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "character")
        {
            Debug.Log("JOKER ALINDI");
            Destroy(gameObject, 1f);
        }
    }
}
