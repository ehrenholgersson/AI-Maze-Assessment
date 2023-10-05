using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    Vector3 _offset = Vector3.zero;
    [SerializeField] GameObject _trackedObject;
    // Start is called before the first frame update
    void Start()
    {
        if (_trackedObject != null)
            _offset = transform.position - _trackedObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_trackedObject != null)
            transform.position = _trackedObject.transform.position + _offset;
    }
}
