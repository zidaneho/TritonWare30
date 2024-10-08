using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [SerializeField] private Transform waypoints;
    
    
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    
    private float _currentSpeed;
    
    private Rigidbody2D _rigidbody;
    private InputBank _input;
    [SerializeField] private float _stamina; // max: 10, for now
    private bool isRunning;

    private void Awake()
    {
        _input = GetComponent<InputBank>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _currentSpeed = walkSpeed;
    }

    private void Start()
    {
        _stamina = 10;
    }
    
    private void Update()
    {
        // for debug purposes
        
        
        // change from walk/run
        if (_input.isRunPressed && _stamina > 0)
        {
            _currentSpeed = runSpeed;
            isRunning = true;
        }
        else
        {
            _currentSpeed = walkSpeed;
            isRunning = false;
        }
        
        // if the character is running, consume stamina
        if (isRunning)
        {
            _stamina = Math.Max(_stamina - Time.deltaTime, 0);
            // stop running if character runs out of stamina
            if (_stamina == 0)
            {
                isRunning = false;
            }
        }
        // restore stamina back to max if not running
        else
        {
            _stamina = Math.Min(_stamina + (Time.deltaTime)/2, 10);
        }
    }

    private void FixedUpdate()
    {
        // move the player
        Vector2 newPos = (Vector2)transform.position + _currentSpeed * Time.deltaTime * _input.moveVector;
        _rigidbody.MovePosition(newPos);
    }

    public int getStamina()
    {
        return (int) _stamina;
    }
    
}
