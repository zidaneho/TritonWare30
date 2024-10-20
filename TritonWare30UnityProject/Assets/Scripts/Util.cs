using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public static class Util
{
    public static void PlaySound(string eventName, GameObject gameObject)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventName);
        
        var transform = new FMOD.ATTRIBUTES_3D
        {
            position = new FMOD.VECTOR { x = gameObject.transform.position.x, y = gameObject.transform.position.y, z = 0 }, // Set Z to 0 for 2D
            forward = new FMOD.VECTOR { x = 0, y = 0, z = 1 },  // Direction the sound is facing
            up = new FMOD.VECTOR { x = 0, y = 1, z = 0 }       // Up direction
        };
        eventInstance.set3DAttributes(transform);
        eventInstance.start();
        eventInstance.release();
    }
}   