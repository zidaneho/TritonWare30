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
    [SerializeField] private float stamina; // max: 5, for now
    [SerializeField] private float restTime;
    [SerializeField] private const float MAX_STAMINA = 5;

    public HealthComponent healthComponent;
    private Rigidbody2D _rigidbody;
    private InputBank _input;
    private float _currentSpeed;

    private void Awake()
    {
        healthComponent = GetComponent<HealthComponent>();
        _input = GetComponent<InputBank>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _currentSpeed = walkSpeed;
    }

    private void OnEnable()
    {
        _input.RunCanceled += OnRunCanceled;
    }

    private void OnDisable()
    {
        _input.RunCanceled -= OnRunCanceled;
    }
    
    private void Update()
    {
        UpdateStamina();
    }

    private void FixedUpdate()
    {
        // move the player
        Vector2 newPos = (Vector2)transform.position + _currentSpeed * Time.deltaTime * _input.moveVector;
        _rigidbody.MovePosition(newPos);
    }

    public void Hide(GameObject hidingSpot, float maximumHidingTime, Vector2 facingDirection)
    {
        
    }

    public int GetStamina()
    {
        return (int) stamina;
    }

    void UpdateStamina()
    {
        // if stamina is 0, you need to rest
        if (stamina <= 0)
        {
            restTime = 5f;
            stamina = 0.01f;
        }
        // change from walk/run
        if (_input.isRunPressed && stamina > 0 && restTime <= 0)
        {
            _currentSpeed = runSpeed;
            // _isRunning = true;
            // if the character is running, consume stamina
            stamina = Math.Max(stamina - Time.deltaTime, 0);
        }
        // restore stamina back to max if not running, after rest
        else
        {
            _currentSpeed = walkSpeed;
            // _isRunning = false;
            if (restTime >= 0) restTime -= Time.deltaTime;
            stamina = Math.Min(stamina + (Time.deltaTime/1.5f), MAX_STAMINA);
        }
    }

    void OnRunCanceled()
    {
        if (restTime <= 0) restTime = Math.Max(2f, restTime);
    }
    
}
