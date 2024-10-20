using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Switchbox : MonoBehaviour, IInteractable
{
    [SerializeField] private float turnedOnDuration = 120f;
    [SerializeField] private Sprite turnedOnSprite;
    [SerializeField] private Sprite turnedOffSprite;

    [SerializeField] private Light2D[] connectedLights;

    [SerializeField] private bool turnedOn;

    private SpriteRenderer _spriteRenderer;

    public string popupDescription { get; }

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }

    private void OnValidate()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        SwitchValue(turnedOn);
    }

    public void OnInteract(GameObject interactor)
    {
        //We should only force the player to turn on the switchbox
        if (turnedOn) return;

        SwitchValue(true);

        StartCoroutine(DurationCoroutine());
    }

    IEnumerator DurationCoroutine()
    {
        yield return new WaitForSeconds(turnedOnDuration);
        SwitchValue(false);

    }

    void SwitchValue(bool value)
    {
        turnedOn = value;
        _spriteRenderer.sprite = value ? turnedOnSprite : turnedOffSprite;
        foreach (var light2D in connectedLights)
        {
            light2D.enabled = value;
        }
    }
    
}
