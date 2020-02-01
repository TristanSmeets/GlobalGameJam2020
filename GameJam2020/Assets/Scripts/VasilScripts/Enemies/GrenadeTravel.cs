using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeTravel : MonoBehaviour
{
    private Vector3 _startPos;
    private Vector3 _endPos;
    private Vector3[] _waypoints = new Vector3[5];
    private float _startTime;
    private float _travelLenght;
    private int _currentStartPoint = 0;
    private bool _shouldExplode;
    private SphereCollider _damageCollider;
    private float _fuseTime;
    private int _shouldDieInFrames = 2;
    private int _damage;

    void Start()
    {
        SphereCollider[] cols = GetComponents<SphereCollider>();
        if(cols.Length > 0)
        {
            for(int i = 0; i < cols.Length; i++)
            {
                if(cols[i].isTrigger)
                {
                    _damageCollider = cols[i];
                    _damageCollider.enabled = false;
                    break;
                }
            }
        }
    }

    void Update()
    {
        float distCovered = (Time.time - _startTime) * 30;
        float fracJourney = distCovered / _travelLenght;
        transform.position = Vector3.Lerp(_startPos, _endPos, fracJourney);
        if(fracJourney >= 1f && _currentStartPoint + 2 < _waypoints.Length)
        {
            _currentStartPoint++;
            _startPos = _waypoints[_currentStartPoint];
            _endPos = _waypoints[_currentStartPoint + 1];
            _travelLenght = Vector3.Distance(_endPos, _startPos);
            _startTime = Time.time;
        }

        if(_currentStartPoint == _waypoints.Length - 2)
        {
            _shouldExplode = true;
        }

        if(!_shouldExplode)
            return;

        _fuseTime -= Time.deltaTime;
        transform.localScale += new Vector3(1, 1, 1) * Time.deltaTime * 2;
        if(_fuseTime <= 0)
        {
            _damageCollider.enabled = true;
            if(_shouldDieInFrames == 0)
                Destroy(this.gameObject);
            _shouldDieInFrames--;
        }
    }

    public void Init(Vector3 pTargetLocation, float pFuseTime, int pDamage)
    {
        _fuseTime = pFuseTime;
        _damage = pDamage;
        Vector3 increment = (pTargetLocation - transform.position) / (_waypoints.Length - 1);
        for(int i = 0; i < _waypoints.Length; i++)
        {
            _waypoints[i] = transform.position + increment * i;
        }

        _startPos = _waypoints[0];
        _waypoints[1].y += 1;
        _waypoints[2].y += 2;
        _waypoints[3].y += 1;
        _endPos = _waypoints[1];

        _travelLenght = Vector3.Distance(_endPos, _startPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<AIBehavior>())
        {
            if(_damageCollider.enabled)
                other.GetComponent<AIBehavior>().TakeDamage(_damage / 2, 100);
        }
        else if(other.GetComponent<IDamageable>() != null)
        {
            if(_damageCollider.enabled)
                other.GetComponent<IDamageable>().TakeDamage(_damage);
        }
    }
}
