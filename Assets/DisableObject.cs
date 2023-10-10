using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableObject : MonoBehaviour
{
    [SerializeField] GameObject _targetObject;
    [SerializeField] string _triggerTag;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == _triggerTag)
            _targetObject.SetActive(false);
    }
}
