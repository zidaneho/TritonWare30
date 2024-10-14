using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModel : MonoBehaviour
{
    private Animator _animator;

    private InputBank _input;

    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponent<Animator>();
        _input = GetComponentInParent<InputBank>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.moveVector.x != 0) _spriteRenderer.flipX = _input.moveVector.x < 0;
        _animator.SetBool("isMoving", _input.moveVector.sqrMagnitude > 0.1f);
        _animator.SetFloat("moveX", _input.moveVector.x);
        _animator.SetFloat("moveY", _input.moveVector.y);
    }
}
