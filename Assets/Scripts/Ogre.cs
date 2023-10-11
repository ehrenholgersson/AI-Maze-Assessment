using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ogre : Ai
{
    //NavMeshAgent _navAgent;
    [SerializeField] GameObject _target;
    [SerializeField] float UncertaintyModifier;
    float _locationUncertainty = 1;
    //int _layerMask;
    //Animator _animator;
    Vector3 _lastposition;
    bool _attacking;

    // Start is called before the first frame update
    protected override void Start()
    {
        //// create bitmask for all but layer 3 (Characters) and layer 8 (Hero traversal Zones)
        //_layerMask = ~((1 << 3) + (1 << 8));
        //_navAgent = GetComponent<NavMeshAgent>();
        //_animator = GetComponentInChildren<Animator>();
        //StartCoroutine("OgreAi");
        base.Start();
        Hero.Enemies.Add(gameObject);
    }

    IEnumerator Ai()
    {
        while (true)
        {
            //calculate direction and distance for sight check
            Vector3 directionToTarget = (_target.transform.position - transform.position).normalized;
            float distanceToTarget = (_target.transform.position - transform.position).magnitude;

            if (!_attacking) 
            {
                // raycast to see if Hero character is in sight
                RaycastHit hit;
                if (Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, _layerMask))
                {
                    // something in the way
                    Debug.Log(hit.transform.name + " is in the way of line of sight to Hero");

                    if (_locationUncertainty < 1)
                        _locationUncertainty += 0.05f;

                    if (_navAgent != null)
                    {
                        Vector2 offset = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                        _navAgent.destination = _target.transform.position + new Vector3(offset.x, offset.y, 0) * Random.Range(0f, UncertaintyModifier);
                    }
                    yield return new WaitForSeconds(3);
                }
                else
                {
                    _locationUncertainty = 0;
                    // nothing blocking line of sight
                    _navAgent.destination = _target.transform.position;
                    yield return new WaitForSeconds(0.5f);
                }
            }
            else
                yield return new WaitForSeconds(3);
        }
    }

    IEnumerator Attack()
    {
        _navAgent.isStopped = true;
        _attacking = true;
        _animator.Play("attack 2");
        _target.GetComponent<Hero>().ApplyDamage(70);
        yield return new WaitForSeconds(2);
        _navAgent.isStopped = false;
        _attacking = false;
    }

    void FixedUpdate()
    {
        _animator.SetFloat("Speed", Mathf.Abs((transform.position - _lastposition).magnitude));
        _lastposition = transform.position;
        float distanceToTarget = (_target.transform.position - transform.position).magnitude;
        if (distanceToTarget < 2 && !_attacking) //attack if in range
            StartCoroutine(Attack());
    }
}

