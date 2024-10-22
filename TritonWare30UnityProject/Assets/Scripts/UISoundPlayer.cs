using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{
    private string hoverSoundEvent = "event:/hover";
    private string clickSoundEvent = "event:/click";
    private string pauseSoundEvent = "event:/pause";
    
    public void PlayHover()
    {
        Util.PlaySound(hoverSoundEvent,gameObject);
    }

    public void PlayPause()
    {
        Util.PlaySound(pauseSoundEvent,gameObject);
    }

    public void PlayClick()
    {
        Util.PlaySound(clickSoundEvent,gameObject);
    }
}
