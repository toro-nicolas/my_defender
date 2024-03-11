using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class settings_menu : MonoBehaviour
{/*
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }*/
    public AudioSource Musics;
    public AudioSource Sounds;
    public Dropdown resolution;
    
    public void set_musics_volume(float volume)
    {
        Musics.volume = volume;
    }
    
    public void set_sounds_volume(float volume)
    {
        Sounds.volume = volume;
    }

    public void set_fullscreen(bool state)
    {
        Screen.fullScreen = state;
    }

    public void set_resolution(int index)
    {
        if (index == 0)
            Screen.SetResolution(1920, 1080, Screen.fullScreen);
        if (index == 1)
            Screen.SetResolution(1440, 810, Screen.fullScreen);
        if (index == 2)
            Screen.SetResolution(960, 540, Screen.fullScreen);
    }
}
