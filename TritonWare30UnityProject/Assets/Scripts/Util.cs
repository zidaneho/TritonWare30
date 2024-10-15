using FMOD.Studio;
using FMODUnity;

public static class Util
{
    public static void PlaySound(string eventName)
    {
        EventInstance eventInstance = RuntimeManager.CreateInstance(eventName);
        eventInstance.start();
        eventInstance.release();
    }
}