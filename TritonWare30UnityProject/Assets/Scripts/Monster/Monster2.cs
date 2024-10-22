using System.Collections;
using UnityEngine;

//Monster2 will be spawned periodically.
//Monster2's spawn will trigger an event where lights will be flashing on and off, before the monster rushes the player.
//The player will have to hide while running from this loud monster.
//The monster will run over and past the player.
public class Monster2 : MonsterController
{
    public enum MonsterState
    {
        WINDUP,
        RUSH,
        END
    }

    public MonsterState monsterState = MonsterState.WINDUP;
    [SerializeField] private float windupTime = 5f;
    [SerializeField] private float extraRushDistance = 50f;
    [SerializeField] private float maxDistanceToHidingPlayer = 3f;
    [SerializeField] private float timeBetweenPlayChase = 5f;

    private Vector3 _targetPosition;
    private OverlapAttack _attack;
    private HitboxGroup _hitboxGroup;
    private Vector3 _rushDirection;
    private bool _reachedPlayer;
    private float _soundTimer;
    private float killTime = 5f;
    private float killTimer = 0f;

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
        Util.PlaySound(GameManager.instance.flickerLightsSoundEvent.Path,gameObject);
    }

    void Update()
    {
        if (monsterState == MonsterState.RUSH)
        {
            if (_reachedPlayer)
            {
                killTimer += Time.deltaTime;
            }
            else
            {
                killTimer = 0f;
            }
            _soundTimer += Time.deltaTime;
            
            if (!_reachedPlayer && _soundTimer >= timeBetweenPlayChase)
            {
                _soundTimer = 0f;
                Util.PlaySound(GameManager.instance.chaseSoundEvent.Path,gameObject);
            }
            if (_reachedPlayer && ai.reachedDestination || _reachedPlayer && killTimer >= killTime)
            {
                monsterState = MonsterState.END;
                Destroy(gameObject);
            }
            else if (!_reachedPlayer && ai.reachedDestination ||
                     player.IsHiding && ai.remainingDistance < maxDistanceToHidingPlayer)
            {
                _reachedPlayer = true;
                _targetPosition = transform.position + _rushDirection * extraRushDistance;
            }
            else
            {
                if (!player.IsHiding)
                {
                    if (_attack.Fire())
                    {
                        Util.PlaySound(GameManager.instance.jumpScareSoundEvent.Path,gameObject);
                    }
                }
            }
        }

        OnSearchPath();
    }

    IEnumerator WindupCoroutine()
    {
        ai.canMove = false;
        yield return new WaitForSeconds(windupTime);

        _rushDirection = (player.transform.position - transform.position).normalized;

        _targetPosition = player.transform.position;

        _attack = new OverlapAttack();
        _attack.attacker = gameObject;
        _attack.attackerSprite = monsterSprite;
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