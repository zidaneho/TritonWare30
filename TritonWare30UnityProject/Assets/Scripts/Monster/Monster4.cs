

//Monster 4 will stay idle when in the light, when in the dark or not lit,
//the monster will run towards the player, when within a certain distance.

using System;
using System.Timers;
using UnityEngine;

public class Monster4 : MonsterController
{
    
    public MonsterState monsterState = MonsterState.IDLE;
    [SerializeField] private float windupTime = 1f;
    [SerializeField] private float chaseSpeed = 6f;
    [SerializeField] private float chaseRadius = 5f;
    [SerializeField] private LayerMask layerMask;
    private LitComponent _litComponent;
    private OverlapAttack _attack;
    private HitboxGroup _hitboxGroup;

    [SerializeField]private float _windupTimer;
    protected override void Awake()
    {
        base.Awake();
        _litComponent = GetComponent<LitComponent>();
        _hitboxGroup = GetComponent<HitboxGroup>();
    }

    private void Update()
    {
        UpdateStates();
        
    }

    void UpdateStates()
    {
        if (_litComponent.isLit)
        {
            monsterState = MonsterState.IDLE;
            _windupTimer = 0f;
        }
        else if (monsterState == MonsterState.IDLE 
                 && !_litComponent.isLit)
        {
            RaycastHit2D hitInfo = Physics2D.Raycast(transform.position,
                (player.transform.position - transform.position).normalized, chaseRadius,layerMask);
            if (hitInfo.transform != null && hitInfo.transform.CompareTag("Player"))
            {
                monsterState = MonsterState.WINDUP;
                _windupTimer = 0f;   
            }
        }

        switch (monsterState)
        {
            case MonsterState.IDLE:
                ai.canMove = false;
                break;
            case MonsterState.WINDUP:
                ai.canMove = false;
                _windupTimer += Time.deltaTime;
                if (_windupTimer >= windupTime)
                {
                    monsterState = MonsterState.CHASE;
                    _attack = new OverlapAttack();
                    _attack.attackerTeam = TeamComponent.TeamIndex.MONSTER;
                    _attack.attacker = gameObject;
                    _attack.hitboxGroup = _hitboxGroup;
                    _attack.damage = 100f;
                }
                break;
            case MonsterState.CHASE:
                ai.canMove = true;
                ai.destination = player.transform.position;
                if (_attack != null && _attack.Fire())
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, (player.transform.position - transform.position).normalized * chaseRadius);
        }
        
        
    }

    public enum MonsterState
    {
        IDLE,
        WINDUP,
        CHASE
    }

}
