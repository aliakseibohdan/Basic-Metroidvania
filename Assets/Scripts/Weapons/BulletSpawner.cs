using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public ObjectPool<BulletBehavior> _pool;

    private PlayerAimAndShoot _playerAimAndShoot;

    private void Start()
    {
        _playerAimAndShoot = GetComponent<PlayerAimAndShoot>();
        _pool = new ObjectPool<BulletBehavior>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, true, 1000, 1500);
    }

    private BulletBehavior CreateBullet()
    {
        BulletBehavior bullet = Instantiate(_playerAimAndShoot.bullet, _playerAimAndShoot.bulletSpawnPoint.position, _playerAimAndShoot.gun.transform.rotation);

        bullet.SetPool(_pool);

        return bullet;
    }

    private void OnTakeBulletFromPool(BulletBehavior bullet)
    {
        bullet.transform.position = _playerAimAndShoot.bulletSpawnPoint.position;
        bullet.transform.right = _playerAimAndShoot.gun.transform.right;

        bullet.gameObject.SetActive(true);
    }

    private void OnReturnBulletToPool(BulletBehavior bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(BulletBehavior bullet)
    {
        Destroy(bullet.gameObject);
    }
}
