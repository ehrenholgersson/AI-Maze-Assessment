using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMove : MonoBehaviour
{
    [SerializeField] float _travelTime;
    [SerializeField] float _openTime;
    [SerializeField] Vector3 _closePosition;
    [SerializeField] Vector3 _openPosition;

    enum ObjState { Closed, Open, Closing, Opening }
    ObjState State = ObjState.Closed;
    float _timer;


    private void Start()
    {
        _timer = Time.time;
        transform.position = _closePosition;
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            //case ObjState.Closed:
            //    if (Time.time >= timer + _topWait )
            //    {
            //        State = ObjState.Opening;
            //        timer = Time.time;
            //    }
            //break;
            case ObjState.Open:
                if (Time.time >= _timer + _openTime)
                {
                    State = ObjState.Closing;
                    _timer = Time.time;
                }
                break;
            case ObjState.Closing:
                transform.position = Vector3.Lerp(_openPosition, _closePosition, (Time.time - _timer) / _travelTime);
                if (Time.time >= _timer + _travelTime)
                {
                    transform.position = _closePosition;
                    State = ObjState.Closed;
                    _timer = Time.time;
                }
                break;
            case ObjState.Opening:
                transform.position = Vector3.Lerp(_closePosition, _openPosition, (Time.time - _timer) / _travelTime);
                if (Time.time >= _timer + _travelTime)
                {
                    transform.position = _openPosition;
                    State = ObjState.Open;
                    _timer = Time.time;
                }
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            State = ObjState.Opening;
            _timer = Time.time;
        }

    }
}