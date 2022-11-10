using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
<<<<<<< Updated upstream
    //카메라 컨트롤러 주석
=======

    /// <summary>
    /// dtdtdtdtd
    /// </summary>
>>>>>>> Stashed changes
    GameObject _player = null;
    Camera _mainCamera = null;
    Vector3 _delta = new Vector3(0, 0, -10);

    float _size = 10;

    void Start()
    {
        _mainCamera = GetComponent<Camera>();
        _mainCamera.orthographicSize = _size;
        _player = GameObject.FindGameObjectWithTag("Player");
    }
    void LateUpdate()
    {
        if (_player.IsValid())
        transform.position = _player.transform.position + _delta;
        else
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    public void SetCameraSize(float size)
    {
        _mainCamera.orthographicSize = size;
    }
}
