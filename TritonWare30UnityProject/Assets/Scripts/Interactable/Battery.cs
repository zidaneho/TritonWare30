using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;

public class Battery : MonoBehaviour, IInteractable
{
    public string popupDescription => "Pickup";

    // When clicked on (e or mouse click)
    public void OnInteract(GameObject player)
    {
        var flashlightComponent = player.GetComponentInChildren<FlashlightComponent>();
        if (flashlightComponent == null) return;
        // restore flashlight (do not go over max)
        flashlightComponent.increaseBattery(5);  // will not go over max
        Debug.Log("Battery increased to: " + flashlightComponent.getBattery());
        
        // make it disappear
        Destroy(gameObject);
    }

}

