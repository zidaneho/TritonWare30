
//Monster2 will be spawned periodically.
//Monster2's spawn will trigger an event where lights will be flashing on and off, before the monster rushes the player.
//The player will have to hide while running from this loud monster.
//The monster will run over and past the player.

using System.Collections;
using UnityEngine;

public class Monster2 : MonsterController
{
    public enum MonsterState { WINDUP, RUSH, END }

    public MonsterState monsterState = MonsterState.WINDUP;
    [SerializeField] private float windupTime = 5f;
    [SerializeField] private float extraRushDistance = 50f;
    
    private Vector3 _targetPosition;
    private OverlapAttack _attack;
    private HitboxGroup _hitboxGroup;
    private Vector3 _rushDirection;
    private bool _reachedPlayer;

    protected override void Awake()
    {
        base.Awake();
        _targetPosition = player.transform.position;
        _hitboxGroup = GetComponent<HitboxGroup>();
    }
    private void OnEnable()
    {
        ai.onSearchPath += OnSearchPath;
    }

    private void OnDisable()
    {
        ai.onSearchPath -= OnSearchPath;
    }

    void Start()
    {
        monsterState = MonsterState.WINDUP;
        StartCoroutine(WindupCoroutine());
    }

    void Update()
    {
        if (monsterState == MonsterState.RUSH)
        {
            if (!_reachedPlayer && ai.reachedDestination)
            {
                _reachedPlayer = true;
                _rushDirection = (player.transform.position - transform.position).normalized;
                _targetPosition = _rushDirection * extraRushDistance;
            }
            else if (_reachedPlayer && ai.reachedDestination)
            {
                monsterState = MonsterState.END;
                Destroy(gameObject);
            }
            else
            {
                if (!player.isHiding) Debug.Log(_attack.Fire());
                if (!_reachedPlayer) _targetPosition = player.transform.position;
            }
        }
        OnSearchPath();
    }

    IEnumerator WindupCoroutine()
    {
        ai.canMove = false;
        yield return new WaitForSeconds(windupTime);

        _targetPosition = player.transform.position;

        _attack = new OverlapAttack();
        _attack.attacker = gameObject;
        _attack.hitboxGroup = _hitboxGroup;
        _attack.damage = 100f;
        _attack.attackerTeam = teamComponent.teamIndex;
        
        
        ai.canMove = true;
        monsterState = MonsterState.RUSH;
    }
    void OnSearchPath()
    {
        ai.destination = _targetPosition;
    }

}