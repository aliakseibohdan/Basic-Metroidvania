using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [Range(2f, 100f), SerializeField] private float _followSensitivity = 3f;

    private Camera _camera;
    private Transform _playerTransform;

    private Rect _screenRect;

    private Vector3 _targetPos;

    private void Start()
    {
        _camera = Camera.main;
        _screenRect = new Rect(0f, 0f, Screen.width, Screen.height);
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnValidate()
    {
        _playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        transform.position = _playerTransform.position;
    }

    private void Update()
    {
        if (_playerTransform != null && _screenRect.Contains(InputManager.instance.MousePosition))
        {
            Ray ray = _camera.ScreenPointToRay(InputManager.instance.MousePosition);

            _targetPos = ray.origin + ray.direction * Mathf.Abs(_camera.transform.position.z);
            _targetPos.z = 0f;

            Vector3 followObjectPosition = (_targetPos + (_followSensitivity - 1) * _playerTransform.position) / _followSensitivity;

            transform.position = followObjectPosition;
        }
    }
}
