using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassExternalVelocityTrigger : MonoBehaviour
{
    private GrassVelocityController _grassVelocityController;

    private GameObject _player;

    private Material _material;

    private Rigidbody2D _playerRB;

    private bool _easeInCoroutineRunning;
    private bool _easeOutCoroutineRunning;

    private int _externalInfluence = Shader.PropertyToID("_ExternalInfluence");

    private float _startingXVelocity;
    private float _velocityLastFrame;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerRB = _player.GetComponent<Rigidbody2D>();
        _grassVelocityController = GetComponentInParent<GrassVelocityController>();

        _material = GetComponent<SpriteRenderer>().material;
        _startingXVelocity = _material.GetFloat(_externalInfluence);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            if (!_easeInCoroutineRunning && Mathf.Abs(_playerRB.velocity.x) > Mathf.Abs(_grassVelocityController.VelocityThreshold))
            {
                StartCoroutine(EaseIn(_playerRB.velocity.x * _grassVelocityController.ExternalInfluenceStrength));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            StartCoroutine(EaseOut());
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject == _player)
        {
            if(Mathf.Abs(_velocityLastFrame) > Mathf.Abs(_grassVelocityController.VelocityThreshold) &&
               Mathf.Abs(_playerRB.velocity.x) < Mathf.Abs(_grassVelocityController.VelocityThreshold))
            {
                StartCoroutine(EaseOut());
            }

            else if (Mathf.Abs(_velocityLastFrame) < Mathf.Abs(_grassVelocityController.VelocityThreshold) &&
               Mathf.Abs(_playerRB.velocity.x) > Mathf.Abs(_grassVelocityController.VelocityThreshold))
            {
                StartCoroutine(EaseIn(_playerRB.velocity.x * _grassVelocityController.ExternalInfluenceStrength));
            }

            else if (!_easeInCoroutineRunning && !_easeOutCoroutineRunning &&
                Mathf.Abs(_playerRB.velocity.x) > Mathf.Abs(_grassVelocityController.VelocityThreshold))
            {
                _grassVelocityController.InfluenceGrass(_material, _playerRB.velocity.x * _grassVelocityController.ExternalInfluenceStrength);
            }

            _velocityLastFrame = _playerRB.velocity.x;
        }
    }

    private IEnumerator EaseIn(float XVelocity)
    {
        _easeInCoroutineRunning = true;

        float elapsedTime = 0f;
        while (elapsedTime < _grassVelocityController.EaseInTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedAmount = Mathf.Lerp(_startingXVelocity, XVelocity, (elapsedTime / _grassVelocityController.EaseInTime));
            _grassVelocityController.InfluenceGrass(_material, lerpedAmount);

            yield return null;
        }

        _easeInCoroutineRunning = false;
    }

    private IEnumerator EaseOut()
    {
        _easeOutCoroutineRunning = true;
        float currentXInfluence = _material.GetFloat(_externalInfluence);

        float elapsedTime = 0f;
        while (elapsedTime < _grassVelocityController.EaseOutTime)
        {
            elapsedTime += Time.deltaTime;

            float lerpedAmount = Mathf.Lerp(currentXInfluence, _startingXVelocity, (elapsedTime / _grassVelocityController.EaseOutTime));
            _grassVelocityController.InfluenceGrass(_material, lerpedAmount);

            yield return null;
        }

        _easeOutCoroutineRunning = false;
    }
}
