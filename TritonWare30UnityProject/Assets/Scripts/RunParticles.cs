using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunParticles : MonoBehaviour
{
    [SerializeField] GameObject particlePrefab;
    private PlayerController _player;
    private InputBank _input;

    public float timeBetweenBurst = 0.3f;
    private float _timer;

    private void Awake()
    {
        _player = GetComponentInParent<PlayerController>();
        _input = GetComponentInParent<InputBank>();
    }

    private void Update()
    {
            
        
        if (_player.CurrentSpeed >= _player.RunSpeed)
        {
            _timer += Time.deltaTime;
            if (_timer > timeBetweenBurst)
            {
                _timer = 0f;
                GameObject go = Instantiate(particlePrefab, transform.position, Quaternion.identity);
                float angle = Mathf.Rad2Deg * Mathf.Atan2(-_input.moveVector.y, -_input.moveVector.x);
                go.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            }
        }
    }
}
