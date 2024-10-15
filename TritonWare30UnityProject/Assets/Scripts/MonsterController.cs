using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    protected Animator _animator;
    protected IAstarAI ai;
    protected TeamComponent teamComponent;
    public float distanceToPlayer => Vector2.Distance(player.transform.position, transform.position);

    protected PlayerController player;
    

    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerController>();
        ai = GetComponent<IAstarAI>();
        teamComponent = GetComponent<TeamComponent>();
    }

   
    
   
}