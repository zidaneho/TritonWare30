using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Monster4Animation : MonoBehaviour
{
    //turned string into hashes for optimization
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");
    private static readonly int IsLit = Animator.StringToHash("isLit");
    
    private LitComponent _litComponent;
    private Animator _animator;
    private IAstarAI _ai;


    private void Awake()
    {
        _litComponent = GetComponentInParent<LitComponent>();
        _animator = GetComponent<Animator>();
        _ai = GetComponentInParent<IAstarAI>();
    }

    private void Update()
    {
        Vector2 direction = new Vector2(_ai.velocity.x, _ai.velocity.y).normalized;
        _animator.SetBool(IsMoving, _ai.canMove);
        _animator.SetFloat(MoveX,direction.x);
        _animator.SetFloat(MoveY,direction.y);
        _animator.SetBool(IsLit,_litComponent.isLit);
    }
}
