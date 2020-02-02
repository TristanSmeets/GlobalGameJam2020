using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChunkyMonkeyBehavior : AIBehavior
{
    [SerializeField]
    private float _preparationForAttackDuration;
    [SerializeField]
    private float _normalAttackRange;
    [SerializeField]
    private float _grenadeTossRange;
    [SerializeField]
    private float _grenadeTossCooldown;
    [SerializeField]
    private int _grenadeFuseTime;

    private float _animationTime = 1;
    private float _preparationForAttackTimer;
    private float _grenadeTossTimer;
    private int _grenadeDamage;
    private bool _gotGrenadeTossPos;
    private Vector3 _grenadeTossPos;
    private GameObject _grenadePrefab;

    protected override void Start()
    {
        base.Start();

        _grenadePrefab = Resources.Load<GameObject>("Enemies/Projectiles/Grenade");
        _enemyStats.AttackRange = _grenadeTossRange;
        _preparationForAttackDuration /= 1 + (_enemyStats.XPercentAttackSpeedIncreasePerYWaves.x * 0.01f * Mathf.FloorToInt(GameStats.CurrentWave / _enemyStats.XPercentAttackSpeedIncreasePerYWaves.y));
        _preparationForAttackTimer = _preparationForAttackDuration;
        _grenadeDamage = _enemyStats.Damage * 2;
    }

    protected override void Update()
    {
        base.Update();

        if(_shouldDie)
            Destroy(this.gameObject);

        _grenadeTossTimer -= Time.deltaTime;
        if(_grenadeTossTimer <= 0)
            _enemyStats.AttackRange = _grenadeTossRange;

        switch(GetAIState())
        {
            case AIState.Attacking:
                _preparationForAttackTimer -= Time.deltaTime;
                if(_preparationForAttackTimer <= 0)
                {
                    //Play attack animation and Wait() after it's finished
                    _animationTime -= Time.deltaTime;

                    if(_grenadeTossTimer <= 0 && !_gotGrenadeTossPos && _animationTime > 0.5f)
                    {
                        if(_aiMoveToTarget.GetDistanceToTarget() > _enemyStats.AttackRange * 0.5f)
                        {
                            _grenadeTossPos = _aiMoveToTarget.TargetTransform.position;// - Vector3.up * _aiMoveToTarget.TargetTransform.GetComponentInChildren<Collider>().bounds.extents.y;
                            _gotGrenadeTossPos = true;
                        }
                    }

                    if(_gotGrenadeTossPos)
                    {
                        TossGrenade(_grenadeTossPos);
                        _grenadeTossTimer = _grenadeTossCooldown;
                        _gotGrenadeTossPos = false;
                    }

                    if(_animationTime <= 0.25f)
                    {
                        _normalAttackCollider.enabled = true;
                    }
                }

                if(_animationTime > 0)
                    return;

                _preparationForAttackTimer = _preparationForAttackDuration;
                _animationTime = 1;
                _gotGrenadeTossPos = false;
                _enemyStats.AttackRange = _normalAttackRange;
                _normalAttackCollider.enabled = false;
                Wait(_enemyStats.WaitTimeAfterAttack);
                break;
        }
    }

    private void TossGrenade(Vector3 pTargetLocation)
    {
        GameObject go = Instantiate(_grenadePrefab, transform.position + transform.forward * 0.2f, Quaternion.identity);
        go.GetComponent<GrenadeTravel>().Init(pTargetLocation, _grenadeFuseTime, _grenadeDamage);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<IDamageable>() != null)
        {
            if(_normalAttackCollider.enabled)
            {
                other.GetComponent<IDamageable>().TakeDamage(_enemyStats.Damage);
            }
        }
    }
}
