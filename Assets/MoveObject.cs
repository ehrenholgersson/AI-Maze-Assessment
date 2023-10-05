using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] float _travelTime;
    [SerializeField] float _topWait;
    [SerializeField] float _bottomWait;
    [SerializeField] Vector3 _topPosition;
    [SerializeField] Vector3 _bottomPosition;
    [SerializeField] GameObject _topLink;
    [SerializeField] GameObject _bottomLink;
    enum PlatState { Top , Bottom, TravelUp, TravelDown }
    PlatState State = PlatState.Top;
    float timer;


    private void Start()
    {
        timer = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case PlatState.Top:
                if (Time.time >= timer + _topWait )
                {
                    State = PlatState.TravelDown;
                    timer = Time.time;
                    if (_topLink != null)
                        _topLink.SetActive(false);
                }
                break;
            case PlatState.Bottom:
                if (Time.time >= timer + _bottomWait)
                {
                    State = PlatState.TravelUp;
                    timer = Time.time;
                    if (_bottomLink != null)
                        _bottomLink.SetActive(false);
                }
                break;
            case PlatState.TravelUp:
                transform.position = Vector3.Lerp(_bottomPosition, _topPosition, (Time.time - timer) / _travelTime);
                if (Time.time >= timer + _travelTime)
                {
                    transform.position = _topPosition;
                    State = PlatState.Top;
                    if (_topLink!=null)
                        _topLink.SetActive(true);
                    timer = Time.time;
                }
                break;
            case PlatState.TravelDown:
                transform.position = Vector3.Lerp(_topPosition, _bottomPosition, (Time.time - timer) / _travelTime);
                if (Time.time >= timer + _travelTime)
                {
                    transform.position = _bottomPosition;
                    State = PlatState.Bottom;
                    if (_bottomLink != null)
                        _bottomLink.SetActive(true);
                    timer = Time.time;
                }
                break;
        }
    }
}
