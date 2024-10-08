using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldKey : MonoBehaviour, IInteractable
{
    public string popupDescription => "Pickup";
    public void OnInteract(GameObject interactor)
    {
        var keyComponent = interactor.GetComponent<KeyComponent>();
        if (keyComponent != null) keyComponent.AddKey();
        Destroy(gameObject);
    }
}
