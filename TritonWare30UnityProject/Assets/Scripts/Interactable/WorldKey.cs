using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class WorldKey : MonoBehaviour, IInteractable
{
    [SerializeField] private EventReference pickupSoundEffect;
    public string popupDescription => "Pickup";
    public void OnInteract(GameObject interactor)
    {
        var keyComponent = interactor.GetComponent<KeyComponent>();
        if (keyComponent != null) keyComponent.AddKey();
        Util.PlaySound(pickupSoundEffect.Path,gameObject);
        Destroy(gameObject);
    }
}
