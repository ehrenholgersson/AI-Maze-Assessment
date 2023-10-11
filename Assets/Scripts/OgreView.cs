using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class OgreView : MonoBehaviour
{
    [SerializeField] RenderTexture _renderTexture;
    [SerializeField] GameObject _displaywindow;
    [SerializeField] float _distance;
    Camera _camera;
    [SerializeField] Vector3 _cameraRotation;

    private void OnEnable()
    {
        _camera = GetComponent<Camera>();
        // make sure camera starts in correct position
        transform.rotation = Quaternion.Euler(_cameraRotation.x, _cameraRotation.y, _cameraRotation.z);
        transform.position = GetComponent<CameraFollow>().TrackedObject.transform.position - (transform.forward * _distance);
        UIText.DisplayText("The Ogre is Chasing!");
        TempView();
    }

    public async void TempView() // switch camera to ogre for a sec, then turn on chase view in UI
    {
        Camera origCam = Camera.main;
        origCam.enabled = false;
        await Task.Delay(2000);
        _camera.targetTexture = _renderTexture;
        origCam.enabled = true;
        _displaywindow.SetActive(true);


    }

}
