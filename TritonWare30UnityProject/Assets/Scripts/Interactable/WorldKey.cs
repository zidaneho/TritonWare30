using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class WorldKey : MonoBehaviour, IInteractable
{
    private string pickupSoundEffect = "event:/keys_pickup";
    public string popupDescription => "Pickup";
    public void OnInteract(GameObject interactor)
    {
        var keyComponent = interactor.GetComponent<KeyComponent>();
        if (keyComponent != null) keyComponent.AddKey();
        Util.PlaySound(pickupSoundEffect,gameObject);
        Destroy(gameObject);
    }
}
