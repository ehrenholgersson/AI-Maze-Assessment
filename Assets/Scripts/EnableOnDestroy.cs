using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableOnDestroy : MonoBehaviour
{
    [SerializeField] GameObject _objectToEnable;

    private void OnDestroy()
    {
        if (_objectToEnable != null)
            _objectToEnable.SetActive(true);
    }
}
