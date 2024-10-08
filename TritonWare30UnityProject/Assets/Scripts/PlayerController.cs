using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    public IState currentState;
    public IState defaultState;

    public void Update()
    {
        currentState?.OnUpdate();
    }

    public void FixedUpdate()
    {
        currentState?.OnFixedUpdate();
    }
}

public class BaseMonsterState : IState
{
    public void OnEnter()
    {
        throw new NotImplementedException();
    }

    public void OnUpdate()
    {
        throw new NotImplementedException();
    }

    public void OnFixedUpdate()
    {
        throw new NotImplementedException();
    }

    public void OnExit()
    {
        throw new NotImplementedException();
    }
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    
    private float _currentSpeed;
    
    private Rigidbody2D _rigidbody;
    private InputBank _input;

    private void Awake()
    {
        _input = GetComponent<InputBank>();
        _rigidbody = GetComponent<Rigidbody2D>();
        
        _currentSpeed = walkSpeed;
    }

    private void Update()
    {
        if (_input.isRunPressed) _currentSpeed = runSpeed;
        else _currentSpeed = walkSpeed;
    }

    private void FixedUpdate()
    {
        Vector2 newPos = (Vector2)transform.position + _currentSpeed * Time.deltaTime * _input.moveVector;
        _rigidbody.MovePosition(newPos);
    }

    
}
