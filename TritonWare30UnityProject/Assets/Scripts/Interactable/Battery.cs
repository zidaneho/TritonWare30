using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.DeviceSimulation;
using UnityEngine;

public class Battery : MonoBehaviour, IInteractable
{
    public GameObject flashLight;
    private FlashlightComponent flashlightComponent;
    public string popupDescription => "Pickup";
    private void Start()
    {
        // find the flashlight
        flashLight = GameObject.FindGameObjectWithTag("flashLight");
        flashlightComponent = flashLight.GetComponent<FlashlightComponent>(); 
    }

    // When clicked on (e or mouse click)
    public void OnInteract(GameObject interactor)
    {

        // restore flashlight (do not go over max)
        flashlightComponent.increaseBattery(5);  // will not go over max
        Debug.Log("Battery increased to: " + flashlightComponent.getBattery());
        
        // make it disappear
        Destroy(gameObject);
    }

}

