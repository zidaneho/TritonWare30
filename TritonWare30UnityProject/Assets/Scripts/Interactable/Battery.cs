using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class Battery : MonoBehaviour, IInteractable
{
    [SerializeField] private float batteryAmount = 5f;

    private string soundPath = "event:/battery_pickup";
    public string popupDescription => "Pickup";

    // When clicked on (e or mouse click)
    public void OnInteract(GameObject player)
    {
        var flashlightComponent = player.GetComponentInChildren<FlashlightComponent>();
        if (flashlightComponent == null) return;
        // restore flashlight (do not go over max)
        flashlightComponent.increaseBattery(batteryAmount);  // will not go over max
        Debug.Log("Battery increased to: " + flashlightComponent.getBattery());
        Util.PlaySound(soundPath,gameObject);
        
        // make it disappear
        Destroy(gameObject);
    }

}

