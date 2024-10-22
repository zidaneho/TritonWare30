using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class MainMenuMusic : MonoBehaviour
{
    private string mainMusicEvent = "event:/main_menu_bg";
    private string windAmbienceEvent = "event:/wind";
    
    private EventInstance windAmbience;
    private EventInstance music;

    private void Start()
    {
        music = Util.PlaySound(mainMusicEvent,gameObject, false);
        windAmbience = Util.PlaySound(windAmbienceEvent,gameObject, false);
    }

    private void OnDestroy()
    {
        music.stop(STOP_MODE.IMMEDIATE);
        windAmbience.stop(STOP_MODE.IMMEDIATE);
    }
}
