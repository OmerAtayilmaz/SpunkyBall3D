using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Audio[] sounds;

    public static AudioManager instance;
    //starttan önce başlar
    void Awake()
    {
     //   PlayerPrefs.DeleteAll();
       // PlayerPrefs.SetInt("diamond", 999);
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        //yeni sayfaya geçtiğinde ses kapanmaz.
       DontDestroyOnLoad(gameObject);

        foreach(Audio s in sounds)
        {
            s.source= gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
    }

    private void Start()
    {
        Play("background1");
    }
    public void Play (string name)
    {
        Audio s =   Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + "not found!");
            return;
        }
          
        s.source.Play();
    }

}
