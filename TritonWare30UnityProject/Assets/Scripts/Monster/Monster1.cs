using System;
using System.Collections;
using Pathfinding;
using UnityEngine;


//Monster 1 has a patrol, windup, and rush state
//During its rush state, Monster 1 will path toward the player. Once reaching its destination, it will choose a random waypoint and start running there.
public class Monster1 : MonsterController
{
    public enum MonsterState { PATROL, CHASE, COOLDOWN }
    
    public string waypointManagerName = "Monster1";
    protected WaypointManager waypointManager;
    public int currentWaypoint;

    public MonsterState monsterState;

    [SerializeField] private LayerMask raycastLayer;
    [SerializeField] private Transform target;
    [SerializeField] private float monsterSpeed = 4f;
    [SerializeField] private float chaseTime = 50f;
    [SerializeField] private float lostTime = 3f;
    [SerializeField] private float chaseRadius = 25f;
    [SerializeField] private float cooldownTime = 5f;
    [SerializeField] private float timeBetweenPlayIdle = 10f;
    [SerializeField] private float timeBetweenPlayChase = 5f;
    
    [Header("Runtime")]
    private float timer;
    [SerializeField] private float lostTimer;
    private OverlapAttack attack;
    private float _soundTimer = 0f;

    protected override void Awake()
    {
        base.Awake();
        var waypointManagers = FindObjectsByType<WaypointManager>(FindObjectsSortMode.None);
        foreach (var waypointM in waypointManagers)
        {
            if (waypointM.managerName == waypointManagerName)
            {
                waypointManager = waypointM;
                break;
            }
        }
    }

    private void OnEnable()
    {
        ai.onSearchPath += OnSearchPath;
    }

    private void OnDisable()
    {
        ai.onSearchPath -= OnSearchPath;
    }

    private void OnValidate()
    {
        ai = GetComponent<IAstarAI>();
        if (ai != null) ai.maxSpeed = monsterSpeed;
    }

    private void Start()
    {
        if (waypointManager == null)
        {
            Debug.LogError("Please assign waypoints for Monster 1 to patrol, using the Waypoint Manager script");
            return;
        }
        target = waypointManager.waypoints[0];
        ai.maxSpeed = monsterSpeed;
    }


    private void Update()
    {
        if (waypointManager == null)
        {
            return;
        }
        if (monsterState == MonsterState.PATROL)
        {
            _soundTimer += Time.deltaTime;
            if (_soundTimer >= timeBetweenPlayIdle)
            {
                _soundTimer = 0f;
                Util.PlaySound(GameManager.idleSoundEvent,gameObject);
            }
            target = waypointManager.waypoints[currentWaypoint];
            if (ai.reachedDestination)
            {
                SetNextWaypoint();
            }

            if (!player.IsHiding && Vector2.Distance(player.transform.position, transform.position) < chaseRadius)
            {
                RaycastHit2D hitInfo =
                    Physics2D.Raycast(transform.position, player.transform.position - transform.position,chaseRadius,raycastLayer);
                if (hitInfo.transform != null && hitInfo.transform.CompareTag("Player"))
                {
                    Windup();
                }
            }
            
        }
        else if (monsterState == MonsterState.CHASE)
        {
            timer += Time.deltaTime;
            
            _soundTimer += Time.deltaTime;
            if (_soundTimer >= timeBetweenPlayChase)
            {
                _soundTimer = 0f;
                Util.PlaySound(GameManager.chaseSoundEvent,gameObject);
            }
            
            if (player.IsHiding)
            {
                lostTimer += Time.deltaTime;
                if (lostTimer >= lostTime)
                {
                    monsterState = MonsterState.PATROL;
                    SetClosestWaypoint();
                }
            }
            else if (!player.IsHiding && Vector2.Distance(player.transform.position, transform.position) < chaseRadius)
            {
                target = player.transform;
                lostTimer = 0f;
                
                if (timer >= chaseTime)
                {
                    
                    monsterState = MonsterState.COOLDOWN;
                    StartCoroutine(CooldownCoroutine());
                }
                else if (attack.Fire())
                {
                    Util.PlaySound(GameManager.jumpScareSoundEvent,gameObject);
                    monsterState = MonsterState.COOLDOWN;
                    StartCoroutine(CooldownCoroutine());
                }
            }
            else
            {
                monsterState = MonsterState.PATROL;
                SetClosestWaypoint();
            }
            
            
        }
        
        OnSearchPath();
    }

    void Windup()
    {       
        //Play light events and starting sounds here.
        attack = new OverlapAttack();
        attack.attacker = gameObject;
        attack.attackerSprite = monsterSprite;
        attack.damage = 100;
        attack.attackerTeam = teamComponent.teamIndex;
        attack.hitboxGroup = GetComponent<HitboxGroup>();
        monsterState = MonsterState.CHASE;
        

        timer = 0f;
        lostTimer = 0f;
    }

    IEnumerator CooldownCoroutine()
    {
        ai.canMove = false;
        yield return new WaitForSeconds(cooldownTime);
        ai.canMove = true;
        monsterState = MonsterState.PATROL;
    }

    void SetNextWaypoint()
    {
        currentWaypoint++;
        if (currentWaypoint >= waypointManager.waypoints.Length || currentWaypoint < 0)
        {
            currentWaypoint = 0;
        }

        target = waypointManager.waypoints[currentWaypoint];
    }

    void SetClosestWaypoint()
    {
        var shortest = float.MaxValue;
        for (int i = 0; i< waypointManager.waypoints.Length; i++)
        {
            var dist = Vector2.Distance(transform.position, waypointManager.waypoints[i].position);
            if (dist < shortest)
            {
                currentWaypoint = i;
            }
        }
        
    }
    

    void OnSearchPath()
    {
        if (target != null) ai.destination = target.position;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(transform.position, chaseRadius);
    }
}