using System;
using System.Collections;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

public class HeartbeatComponent : MonoBehaviour
{
    [SerializeField] private EventReference heartbeatSoundEvent;
    [SerializeField] private float cooldownTime = 30f;

    private bool _canPlay = true;
    private EventInstance _eventInstance;
    private HealthComponent _healthComponent;

    private void Awake()
    {
        _healthComponent = GetComponentInParent<HealthComponent>();
    }

    void OnEnable()
    {
        _healthComponent.Died += OnDeath;
    }

    private void OnDisable()
    {
        _healthComponent.Died -= OnDeath;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_canPlay && other.CompareTag("Monster"))
        {
            _canPlay = false;
            _eventInstance= RuntimeManager.CreateInstance(heartbeatSoundEvent);
            _eventInstance.start(); // Start the sound
            StartCoroutine(CooldownCoroutine());
        }
    }

    void OnDeath(Sprite attackerSpritec)
    {
        if (_eventInstance.isValid())
        {
            _eventInstance.stop(STOP_MODE.IMMEDIATE);
            _eventInstance.release();   
        }
    }
    

    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        if (_eventInstance.isValid()) _eventInstance.release();
        _canPlay = true;
    }
}
