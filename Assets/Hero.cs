using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Hero : MonoBehaviour
{
    NavMeshAgent _navAgent;
    public Zone CurrentZone;
    float _health = 100;
    float _stamina = 100;
    float _fear = 0;
    static List<GameObject> Enemies = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        _navAgent = GetComponent<NavMeshAgent>();
        StartCoroutine("HeroAi");
    }

    bool CheckLOS(GameObject target)
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

    IEnumerator HeroAi()
    {
        while (true)
        {
            Debug.Log("Hero Picking Destination");
            if (CurrentZone != null && _navAgent != null)
            {
                if (CurrentZone.RequiredCollectables.Count > 0)
                {
                    _navAgent.destination = CurrentZone.RequiredCollectables[0].transform.position;
                    Debug.Log("set goal as required item");
                }
                else if (CurrentZone.BonusCollectables.Count > 0)
                {
                    _navAgent.destination = CurrentZone.BonusCollectables[0].transform.position;
                    Debug.Log("set goal as bonus item");
                }
                else
                {
                    _navAgent.destination = CurrentZone.Goal.transform.position;
                    Debug.Log("set goal as detination");
                }

            }
            yield return new WaitForSeconds(3);
        }
    }

    //// Update is called once per frame
    //void Update()
    //{
        
    //}
}
