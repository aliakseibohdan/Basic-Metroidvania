using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class CameraTargetSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject _cameraFollow;
    [SerializeField] private GameObject _transitionObject;
    [SerializeField] private float _duration = 0.75f;

    private CinemachineVirtualCamera _virtualCamera;

    private GameObject _player;

    private bool _goToCameraFollow;
    private bool _goToPlayer;

    private float _timer;
    private Vector3 _startPos;

    private void Awake()
    {
        _virtualCamera = GetComponent<CinemachineVirtualCamera>();

        _player = GameObject.FindGameObjectWithTag("Player");
        _transitionObject.transform.position = _player.transform.position;
    }

    private void Update()
    {
        if (_goToPlayer)
        {
            MoveTransitionObject(_player.transform.position);

            if (_timer >= _duration)
            {
                _goToPlayer = false;
                _virtualCamera.Follow = _player.transform;
            }
        }

        else if (_goToCameraFollow)
        {
            MoveTransitionObject(_cameraFollow.transform.position);

            if (_timer >= _duration)
            {
                _goToCameraFollow = false;
                _virtualCamera.Follow = _cameraFollow.transform;
            }
        }
    }

    private void MoveTransitionObject(Vector2 endPos)
    {
        _timer += Time.deltaTime;
        Vector2 transitionPosition = Vector2.Lerp(_startPos, endPos, (_timer / _duration));
        _transitionObject.transform.position = transitionPosition;
    }

    public void SwitchCameraFollow(PlayerInput input)
    {
        if (InputManager.CurrentControlScheme == InputManager.KeyboardAndMouseControlScheme)
        {
            _virtualCamera.Follow = _transitionObject.transform;
            _startPos = _virtualCamera.transform.position;
            _goToCameraFollow = true;
            _goToPlayer = false;
            _timer = 0f;
        }

        else if (InputManager.CurrentControlScheme == InputManager.GamepadControlScheme)
        {
            _virtualCamera.Follow = _transitionObject.transform;
            _startPos = _virtualCamera.transform.position;
            _goToPlayer = true;
            _goToCameraFollow = false;
            _timer = 0f;
        }
    }
}
