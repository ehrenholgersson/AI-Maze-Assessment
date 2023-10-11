using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ai : MonoBehaviour
{
    protected NavMeshAgent _navAgent;
    protected int _layerMask;
    protected Animator _animator;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _layerMask = ~((1 << 3) + (1 << 8));
        _navAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponentInChildren<Animator>();
        StartCoroutine("Ai");
    }
    protected bool CheckLOS(GameObject target)
    {
        int layerMask = ~((1 << 3) + (1 << 8));
        RaycastHit hit;
        Vector3 directionToTarget = (target.transform.position - transform.position).normalized;
        float distanceToTarget = (target.transform.position - transform.position).magnitude;

        if (Physics.Raycast(transform.position, directionToTarget, out hit, distanceToTarget, layerMask))
            return false;
        else
            return true;
    }

}
