    using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;
//Should not be used on the player. This is for monsters
public class FlashlightComponent : MonoBehaviour
{
    [SerializeField] private GameObject lightObject;
    [Header("UI")] 
    [SerializeField] private ProgressBar batteryBar;
    [SerializeField] private FadeUI batteryFade;
    [SerializeField] private float batteryFadeTolerance;
    [Header("FMOD")] private string turnOnSoundEvent = "event:/flashlight_on";
    private string turnOffSoundEvent = "event:/flashlight_off";
    [Header("Settings")] 
    [SerializeField] private float maxBattery = 15f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float batteryDrainFactor = 1;
    
    [Header("Runtime")]
    [SerializeField] private float _battery = 15;
    [SerializeField] private bool _isTurnedOn; 
  
    private InputBank _input;
  
    private void Awake()
    {
        _input = GetComponentInParent<InputBank>();
        _isTurnedOn = true;
    }

    private void Update()
    {
        // for debug purposes
        var oldBattery = _battery;
        
        // turn flashlight
        if (_input.moveVector != Vector2.zero)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(-_input.moveVector.x, _input.moveVector.y);
            float targetAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
                targetAngle);   
        }

        // batteries run out if on
        if (_isTurnedOn)
        {
            _battery = Math.Max(_battery - batteryDrainFactor * Time.deltaTime, 0);
        }

        bool enableLight = _battery > 0 && _isTurnedOn;
        lightObject.SetActive(enableLight);
        
        // turn on/off battery when t is clicked
        if (_input.wasFlashlightPressedThisFrame && _battery > 0f)
        {
            _isTurnedOn = !_isTurnedOn;
            if (_isTurnedOn)
            {
                Util.PlaySound(turnOnSoundEvent, gameObject);
            }
            else
            {
                Util.PlaySound(turnOffSoundEvent, gameObject);
            }
        }
        else if (_battery <= 0)
        {
            _isTurnedOn = false;
        }
        
        // for debug purposes
        // if (Math.Floor(_battery) != Math.Floor(prevBattery) || _battery == 0) Debug.Log("Battery: " + Math.Ceiling(_battery));
        
        batteryBar.SetProgress(_battery, maxBattery);
        if (_isTurnedOn)
        {
            batteryFade.TryFadeIn();
        }
        else
        {
            batteryFade.TryFadeOut();
        }
    }
    
    
    // Getter and setter for _battery variable
    public void increaseBattery(float inc)
    {
        _battery = Math.Max(0, Math.Min(_battery+inc, 15));
    }

    public int getBattery()
    {
        return (int) Math.Floor(_battery);
    }
}
