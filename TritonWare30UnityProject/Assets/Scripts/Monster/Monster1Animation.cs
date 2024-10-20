using System;
using System.Collections;
using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

public class Monster1Animation : MonoBehaviour
{
    //turned string into hashes for optimization
    private static readonly int MoveX = Animator.StringToHash("moveX");
    private static readonly int MoveY = Animator.StringToHash("moveY");
    private static readonly int IsMoving = Animator.StringToHash("isMoving");

    private IAstarAI _ai;
    private Animator _animator;

    private void Awake()
    {
        _ai = GetComponentInParent<IAstarAI>();
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        Vector2 direction = new Vector2(_ai.velocity.x, _ai.velocity.y).normalized;
        _animator.SetBool(IsMoving, _ai.canMove);
        _animator.SetFloat(MoveX,direction.x);
        _animator.SetFloat(MoveY,direction.y);
    }
}
