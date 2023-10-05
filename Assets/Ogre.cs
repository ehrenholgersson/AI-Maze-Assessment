using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Ogre : MonoBehaviour
{
    NavMeshAgent _navAgent;
    [SerializeField] GameObject _target;
    [SerializeField] float UncertaintyModifier;
    float _locationUncertainty = 1;
    int _layerMask;

    // Start is called before the first frame update
    void Start()
    {
        // create bitmask for all but layer 3 (Characters) and layer 8 (Hero traversal Zones)
        _layerMask = ~((1 << 3) + (1 << 8));
        _navAgent = GetComponent<NavMeshAgent>();
        StartCoroutine("OgreAi");
    }

    IEnumerator OgreAi()
    {
        while (true)
        {
            //calculate direction and distance for sight check
            Vector3 directionToTarget = (_target.transform.position - transform.position).normalized;
            float distanceToTarget = (_target.transform.position - transform.position).magnitude;
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
                    _navAgent.destination = _target.transform.position + new Vector3(offset.x,offset.y,0) * Random.Range(0f, UncertaintyModifier);
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

            //if so, proceed directly to player location, yield for half second (so not lagging far behind current location) and check if we are close enough to attack
            // otherwise reduce certainty amount then proceed to random point within a distance of player determined by certainty
        }
    }
}
