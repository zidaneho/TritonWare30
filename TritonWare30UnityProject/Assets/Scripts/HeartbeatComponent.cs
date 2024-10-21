using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

public class HeartbeatComponent : MonoBehaviour
{
    [SerializeField] private EventReference heartbeatSoundEvent;
    [SerializeField] private float cooldownTime = 30f;

    private bool _canPlay = true;
   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_canPlay && other.CompareTag("Monster"))
        {
            _canPlay = false;
            Util.PlaySound(heartbeatSoundEvent.Path,gameObject);
            StartCoroutine(CooldownCoroutine());
        }
    }

    IEnumerator CooldownCoroutine()
    {
        yield return new WaitForSeconds(cooldownTime);
        _canPlay = true;
    }
}
