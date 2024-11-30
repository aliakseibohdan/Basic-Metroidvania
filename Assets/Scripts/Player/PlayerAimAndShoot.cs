using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{
    public GameObject gun;
    public BulletBehavior bullet;
    public ParticlePool particles;
    public Transform bulletSpawnPoint;

    private GameObject bulletInst;
    private ParticleSystem particlesInst;

    private Camera cam;
    private Vector2 worldPosition;
    private Vector2 direction;
    private float angle;

    private BulletSpawner bulletSpawner;
    private ParticleSpawner particleSpawner;

    private void Start()
    {
        cam = Camera.main;
        bulletSpawner = GetComponent<BulletSpawner>();
        particleSpawner = GetComponent<ParticleSpawner>();
    }

    private void Update()
    {
        HandleGunRotation();
        HandleGunShooting();
    }

    private void HandleGunRotation()
    {
        worldPosition = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        direction = (worldPosition - (Vector2)gun.transform.position).normalized;
        gun.transform.right = direction;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        Vector3 localScale = Vector3.one;
        if (angle > 90 || angle < -90)
        {
            localScale.y = -1f;
        }
        else
        {
            localScale.y = 1f;
        }

        gun.transform.localScale = localScale;
    }

    private void HandleGunShooting()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            //bulletInst = Instantiate(bullet, bulletSpawnPoint.position, gun.transform.rotation);
            bulletSpawner._pool.Get();

            //particlesInst = Instantiate(particles, bulletSpawnPoint.position, gun.transform.rotation);
            particleSpawner._particlePool.Get();
        }
    }
}
