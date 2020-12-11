using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [Space]
    [Description("Gökyüzü materyalleri buraya sürüklenir")]
    [Header("Gökyüzü")]
    [SerializeField]
    private GameObject[] gokyuzuPrefabs;

    [Space]
    [Description("Karakter Takip Sistemi")]
    [Header("Takip Edilecek Obje")]
    [SerializeField]
    private GameObject takipObjesi;



   
    void LateUpdate()
    {
        transform.position = new Vector3(takipObjesi.transform.position.x, takipObjesi.transform.position.y+1.0f, takipObjesi.transform.position.z -1.0f);
    }


}
