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
    [SerializeField] private float staminaDrainFactor = 1;
    [SerializeField] private float cancelRunRestTime = 2f;
    [SerializeField] private float outOfStaminaRestTime = 5f;
   
    [SerializeField] private float maxStamina = 5;
    [Header("Runtime")]
    [SerializeField] private float _restTime;
    
    private Rigidbody2D _rigidbody;
    private InputBank _input;
    private float _currentSpeed;

    private void Awake()
    {
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
            _restTime = outOfStaminaRestTime;
            stamina = 0.01f;
        }
        // change from walk/run
        if (_input.isRunPressed && stamina > 0 && _restTime <= 0)
        {
            _currentSpeed = runSpeed;
            // _isRunning = true;
            // if the character is running, consume stamina
            stamina = Math.Max(stamina - staminaDrainFactor * Time.deltaTime, 0);
        }
        // restore stamina back to max if not running, after rest
        else
        {
            _currentSpeed = walkSpeed;
            // _isRunning = false;
            if (_restTime >= 0) _restTime -= Time.deltaTime;
            stamina = Math.Min(stamina + (Time.deltaTime/1.5f), maxStamina);
        }
    }

    void OnRunCanceled()
    {
        if (_restTime <= 0) _restTime = Math.Max(cancelRunRestTime, _restTime);
    }

    public float RunSpeed => runSpeed;
    public float CurrentSpeed => _currentSpeed;

}
