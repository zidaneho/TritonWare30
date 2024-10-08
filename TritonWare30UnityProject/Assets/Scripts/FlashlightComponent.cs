    using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class FlashlightComponent : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 10f;

    private Light2D _light;
    private InputBank _input;
    private float _battery;
    private bool _isTurnedOn; 

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _input = GetComponentInParent<InputBank>();
        _battery = 10;
        _isTurnedOn = true;
    }

    private void Update()
    {
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
            _battery = Math.Max(_battery - Time.deltaTime, 0);
        }
        _light.enabled = _battery > 0 && _isTurnedOn;            
        
        // turn on/off battery when t is clicked
        if (_input.wasFlashlightPressedThisFrame)
        {
            _isTurnedOn = !_isTurnedOn;
        }

        Debug.Log(_battery);
    }

    public void Toggle(bool value)
    {
        _light.enabled = value;
    }
}
