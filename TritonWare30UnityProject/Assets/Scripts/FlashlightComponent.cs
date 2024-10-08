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

    private void Awake()
    {
        _light = GetComponent<Light2D>();
        _input = GetComponentInParent<InputBank>();
    }

    private void Update()
    {
        if (_input.moveVector != Vector2.zero)
        {
            float angle = Mathf.Rad2Deg * Mathf.Atan2(-_input.moveVector.x, _input.moveVector.y);
            float targetAngle = Mathf.LerpAngle(transform.eulerAngles.z, angle, rotationSpeed * Time.deltaTime);
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y,
                targetAngle);   
        }
    }


    public void Toggle(bool value)
    {
        _light.enabled = value;
    }
}