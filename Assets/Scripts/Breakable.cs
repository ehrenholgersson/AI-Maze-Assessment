using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class Breakable : MonoBehaviour
{
    [SerializeField] NavMeshAgent _heroAgent;
    NavMeshModifierVolume _volume;
    // Start is called before the first frame update
    void Start()
    {
        _volume = GetComponent<NavMeshModifierVolume>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        if (_heroAgent != null) 
            _heroAgent.areaMask += _volume.area;
        // trigger particle effect
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ogre")
            Destroy(gameObject);
    }
}
