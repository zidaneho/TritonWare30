    using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightComponent : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private ProgressBar batteryBar;
    [SerializeField] private FadeUI batteryFade;
    [SerializeField] private float batteryFadeTolerance;
    [Header("FMOD")] 
    [SerializeField] private EventReference turnOnSoundEvent;
    [SerializeField] private EventReference turnOffSoundEvent;
    [Header("Settings")] 
    [SerializeField] private float maxBattery = 15f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float batteryDrainFactor = 1;
    
    [Header("Runtime")]
    [SerializeField] private float _battery = 15;
    [SerializeField] private bool _isTurnedOn; 
    
    private Light2D _light;
    private InputBank _input;

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _input = GetComponentInParent<InputBank>();
        _isTurnedOn = true;
    }

    private void Update()
    {
        // for debug purposes
        var oldBattery = _battery;
        
        // rotate flashlight
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
        _light.enabled = _battery > 0 && _isTurnedOn;            
        
        // turn on/off battery when t is clicked
        if (_input.wasFlashlightPressedThisFrame && _battery > 0f)
        {
            _isTurnedOn = !_isTurnedOn;
            if (_isTurnedOn)
            {
                Util.PlaySound(turnOnSoundEvent.Path, gameObject);
            }
            else
            {
                Util.PlaySound(turnOffSoundEvent.Path, gameObject);
            }
        }
        
        // for debug purposes
        // if (Math.Floor(_battery) != Math.Floor(prevBattery) || _battery == 0) Debug.Log("Battery: " + Math.Ceiling(_battery));
        
        batteryBar.SetProgress(_battery, maxBattery);
        if (Math.Abs(_battery - oldBattery) > batteryFadeTolerance)
        {
            batteryFade.TryFadeIn();
        }
        else
        {
            batteryFade.TryFadeOut();   
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        var litComponent = collision.gameObject.GetComponent<LitComponent>();

        if (litComponent != null)
        {
            litComponent.litObjects++;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        var litComponent = collision.gameObject.GetComponent<LitComponent>();

        if (litComponent != null)
        {
            litComponent.litObjects--;
        }
    }
    
    // Toggle light on and off
    public void Toggle(bool value)
    {
        _light.enabled = value;
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
