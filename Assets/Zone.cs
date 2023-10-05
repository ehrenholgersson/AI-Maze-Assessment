using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zone : MonoBehaviour
{
    public List<GameObject> RequiredCollectables;
    public List<GameObject> BonusCollectables;
    public GameObject Goal; //{ get; private set; }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Hero>(out Hero h))
            h.CurrentZone = this;
    }
}
