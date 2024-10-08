using System;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    protected Animator _animator;
    protected NavMeshAgent agent;

    public Transform[] waypoints;
    public float distanceToPlayer => Vector2.Distance(player.transform.position, transform.position);

    public PlayerController player;

    protected virtual void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerController>();
        agent = GetComponent<NavMeshAgent>();
    }

   
    
   
}