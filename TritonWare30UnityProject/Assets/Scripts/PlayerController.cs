using System;
using System.Collections;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    [Header("UI")] 
    [SerializeField] private ProgressBar staminaBar;
    [SerializeField] private FadeUI staminaFade;
    [SerializeField] private float staminaFadeTolerance;
    [Header("FMOD")] 
    [SerializeField] private EventReference footStepsSoundEvent;
    [Header("Sound Settings")] [SerializeField]
    private float walkingFootStepTime = 0.5f;

    [SerializeField] private float runningFootStepTime = 0.4f;
    private float _footStepsTimer;
    [Header("Settings")]
    [SerializeField] private float walkSpeed;
    [SerializeField] private float runSpeed;
    [SerializeField] private float stamina; // max: 5, for now
    [SerializeField] private float staminaGainFactor = 0.7f;
    [SerializeField] private float staminaDrainFactor = 1;
    [SerializeField] private float cancelRunRestTime = 2f;
    [SerializeField] private float outOfStaminaRestTime = 5f;
    [SerializeField] private float maxStamina = 5;
    
    [Header("Runtime")]
    [SerializeField] private float restTime;
    [SerializeField] private bool isHiding;
    private Coroutine _hideCoroutine;
    private SpriteRenderer _spriteRenderer;
    private Rigidbody2D _rigidbody;
    private InputBank _input;
    private float _currentSpeed;

    private void Awake()
    {
        _input = GetComponent<InputBank>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        
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
        UpdateFootsteps();
        
       
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
        var oldStamina = stamina;
        
        // if stamina is 0, you need to rest
        if (stamina <= 0)
        {
            restTime = outOfStaminaRestTime;
            stamina = 0.01f;
        }
        // change from walk/run
        if (_input.isRunPressed && stamina > 0 && restTime <= 0)
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
            if (restTime >= 0) restTime -= Time.deltaTime;
            stamina = Math.Min(stamina + staminaGainFactor * Time.deltaTime, maxStamina);
        }
        
        //Updating UI here
        if (Math.Abs(oldStamina - stamina) > staminaFadeTolerance)
        {
            staminaFade.TryFadeIn();
        }
        else
        {
            staminaFade.TryFadeOut();
        }
        staminaBar.SetProgress(stamina, maxStamina);
    }
    void UpdateFootsteps()
    {
        //Player isnt moving, return
        if (_input.moveVector.sqrMagnitude < 0.1f)
        {
            _footStepsTimer = 0f;
            return;
        }

        var currentFootStepTime = _currentSpeed > walkSpeed ? runningFootStepTime : walkingFootStepTime;
        
        _footStepsTimer += Time.deltaTime;
        if (_footStepsTimer >= currentFootStepTime)
        {
            _footStepsTimer = 0f;
            Util.PlaySound(footStepsSoundEvent.Path,gameObject);
        }
    }

    void OnRunCanceled()
    {
        if (restTime <= 0) restTime = Math.Max(cancelRunRestTime, restTime);
    }
    
    public void Hide(GameObject hidingSpot, float maximumHidingTime, bool hideSprite = false)
    {
        isHiding = true;
        _hideCoroutine = StartCoroutine(HideCoroutine(maximumHidingTime));
        _input.ToggleMoveInput(false);

        if (hideSprite)
        {
            _spriteRenderer.enabled = false;
        }
    }

    public void Unhide()
    {
        isHiding = false;
        _spriteRenderer.enabled = true;
        if (_hideCoroutine != null) StopCoroutine(_hideCoroutine);
        _input.ToggleMoveInput(true);
    }

    IEnumerator HideCoroutine(float hideTime)
    {
        yield return new WaitForSeconds(hideTime);
        Unhide();
    }

    public float RunSpeed => runSpeed;
    public float CurrentSpeed => _currentSpeed;
    public bool IsHiding => isHiding;

}
