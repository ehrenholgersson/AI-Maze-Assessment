using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    Zone _parentZone;
    // Start is called before the first frame update
    void Start()
    {
        _parentZone = transform.parent.GetComponent<Zone>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            UIText.DisplayText("Solo Man has picked up " + gameObject.name);
            Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        if (_parentZone != null)
        {
            if (_parentZone.RequiredCollectables.Contains(gameObject))
                _parentZone.RequiredCollectables.Remove(gameObject);
            if (_parentZone.BonusCollectables.Contains(gameObject))
                _parentZone.BonusCollectables.Remove(gameObject);
        }
    }
}
