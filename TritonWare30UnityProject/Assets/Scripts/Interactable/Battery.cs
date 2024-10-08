using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour, IInteractable
{
    
    public string popupDescription => "Pickup";
    
    // private InputBank _input;
    
    void Start()
    {
        // _input = GetComponentInParent<InputBank>();
    }
    
    public void OnInteract(GameObject interactor)
    {
        // When clicked on (e or mouse click), make it disappear, and restore flashlight
        
        
        // if (_input.wasBatteryPressedThisFrame)
        // {
        //     Debug.Log("Battery pressed");
        // }
    }

}

