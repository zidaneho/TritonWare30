using System;
using Pathfinding;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    protected Animator _animator;
    protected IAstarAI ai;

    public string waypointManagerName = "Monster1";
    protected WaypointManager waypointManager;
    public int currentWaypoint;
    public float distanceToPlayer => Vector2.Distance(player.transform.position, transform.position);

    protected PlayerController player;

    protected virtual void Awake()
    {
        var waypointManagers = FindObjectsByType<WaypointManager>(FindObjectsSortMode.None);
        foreach (var waypointM in waypointManagers)
        {
            if (waypointM.managerName == waypointManagerName)
            {
                waypointManager = waypointM;
                break;
            }
        }
        _animator = GetComponentInChildren<Animator>();
        player = FindObjectOfType<PlayerController>();
        ai = GetComponent<IAstarAI>();
    }

   
    
   
}