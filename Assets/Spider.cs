using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spider : Ai
{
    [SerializeField] float _patrolDistance;
    [SerializeField] float _sightRange;    
    [SerializeField] float _attackRange;
    [SerializeField] GameObject _target;
    float _locationUncertainty = 1;
    Vector3 _origin;
    // Start is called before the first frame update

    protected override void Start()
    {
        // create bitmask for all but layer 3 (Characters) and layer 8 (Hero traversal Zones)
        base.Start();
        Hero.Enemies.Add(gameObject);
        _origin = transform.position;
    }
    IEnumerator Ai()
    {
        _navAgent.destination = new Vector3(_origin.x + Random.Range(-_patrolDistance, _patrolDistance), _origin.y, _origin.z + Random.Range(-_patrolDistance, _patrolDistance)); // create initial patrol waypoint
        //Debug.Log("set spider nav destination to " + _navAgent.destination);
        while (true)
        {
            _locationUncertainty += 0.05f;
            if (_locationUncertainty > 0.3f && ((new Vector2(transform.position.x, transform.position.z) - new Vector2(_navAgent.destination.x, _navAgent.destination.z)).magnitude < 1||(_navAgent.destination - _origin).magnitude > _patrolDistance)) // if we get to our patrol waypoint then pick a new one, this will then be ovveriden if agent spots the target 
            {
                //Debug.Log("spider picking new destination");
                _navAgent.destination = new Vector3(_origin.x + Random.Range(-_patrolDistance, _patrolDistance), _origin.y, _origin.z + Random.Range(-_patrolDistance, _patrolDistance));
            }
            //else Debug.Log("spider " + (new Vector2(transform.position.x, transform.position.z) - new Vector2(_navAgent.destination.x, _navAgent.destination.z)).magnitude + " from destination");

            float distanceToTarget = (_target.transform.position - transform.position).magnitude;
            if (distanceToTarget < _sightRange)
            {
                if (CheckLOS(_target))
                {
                    _locationUncertainty = 0;
                    _navAgent.destination = _target.transform.position;
                    if (distanceToTarget < _attackRange && Random.Range(0f,1f)>0.5f)
                    {
                        Attack();
                    }
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }

    void Attack()
    {
        Debug.Log("speder has attacked");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
