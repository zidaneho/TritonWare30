using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


public class PlayerController : MonoBehaviour
{
    public bool isHiding;
    
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    
    [SerializeField] private float stamina = 5; // max: 10, for now
    
    private float _currentSpeed;
    
    private Rigidbody2D _rigidbody;
    private InputBank _input;
    private bool _isRunning;

    private void Awake()
    {
        _input = GetComponent<InputBank>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _currentSpeed = walkSpeed;
    }

    private void Start()
    {
        stamina = 10;
    }
    
    private void Update()
    {
        // for debug purposes
        
        UpdateStamina();
        
    }

    private void FixedUpdate()
    {
        // move the player
        Vector2 newPos = (Vector2)transform.position + _currentSpeed * Time.deltaTime * _input.moveVector;
        _rigidbody.MovePosition(newPos);
    }

    public int GetStamina()
    {
        return (int) stamina;
    }

    void UpdateStamina()
    {
        // change from walk/run
        if (_input.isRunPressed && stamina > 0)
        {
            _currentSpeed = runSpeed;
            _isRunning = true;
        }
        else
        {
            _currentSpeed = walkSpeed;
            _isRunning = false;
        }
        
        // if the character is running, consume stamina
        if (_isRunning)
        {
            stamina = Math.Max(stamina - Time.deltaTime, 0);
            // stop running if character runs out of stamina
            if (stamina == 0)
            {
                _isRunning = false;
            }
        }
        // restore stamina back to max if not running
        else
        {
            stamina = Math.Min(stamina + (Time.deltaTime)/2, 10);
        }
    }
    
}
