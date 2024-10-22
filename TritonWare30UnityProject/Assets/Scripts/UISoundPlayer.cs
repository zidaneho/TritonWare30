using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class UISoundPlayer : MonoBehaviour
{
    public EventReference hoverSoundEvent;
    public EventReference clickSoundEvent;
    public EventReference pauseSoundEvent;
    
    public void PlayHover()
    {
        Util.PlaySound(hoverSoundEvent.Path,gameObject);
    }

    public void PlayPause()
    {
        Util.PlaySound(pauseSoundEvent.Path,gameObject);
    }

    public void PlayClick()
    {
        Util.PlaySound(clickSoundEvent.Path,gameObject);
    }
}
