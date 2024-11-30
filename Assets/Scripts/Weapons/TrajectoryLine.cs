using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PlayerAimAndShoot _playerAimAndShoot;
    [SerializeField] private Transform _bulletSpawnPoint;

    [Header("Trajectory Line Smoothness/Length")]
    [SerializeField] private int _segmentCount = 50;
    [SerializeField] private float _curveLength = 3.5f;

    private Vector2[] _segments;
    private LineRenderer _lineRenderer;

    private BulletBehavior _bulletBehavior;

    private float _projectileSpeed;
    private float _projectileGravityFromRB;

    private const float TIME_CURVE_ADDITION = 0.5f;

    private void Start()
    {
        _segments = new Vector2[_segmentCount];

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _segmentCount;

        _bulletBehavior = _playerAimAndShoot.bullet.GetComponent<BulletBehavior>();
        _projectileSpeed = _bulletBehavior.physicsBulletSpeed;
        _projectileGravityFromRB = _bulletBehavior.physicsBulletGravity;
    }

    private void Update()
    {
        Vector2 startPos = _bulletSpawnPoint.position;
        _segments[0] = startPos;
        _lineRenderer.SetPosition(0, startPos);

        Vector2 startVelocity = transform.right * _projectileSpeed;

        for (int i = 1; i < _segmentCount; i++)
        {
            float timeOffset = (i * Time.fixedDeltaTime * _curveLength);

            Vector2 gravityOffset = TIME_CURVE_ADDITION * Physics2D.gravity * _projectileGravityFromRB * Mathf.Pow(timeOffset, 2);

            _segments[i] = _segments[0] + startVelocity * timeOffset + gravityOffset;
            _lineRenderer.SetPosition(i, _segments[i]);
        }
    }
}
