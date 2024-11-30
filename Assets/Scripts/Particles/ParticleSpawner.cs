using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.ParticleSystem;

public class ParticleSpawner : MonoBehaviour
{
    public ObjectPool<ParticlePool> _particlePool;

    private PlayerAimAndShoot _playerAimAndShoot;

    private void Start()
    {
        _playerAimAndShoot = GetComponent<PlayerAimAndShoot>();
        _particlePool = new ObjectPool<ParticlePool>(CreateParticles, OnTakeParticlesFromPool, OnReturnParticlesToPool, OnDestroyParticles, true, 1000, 1500);
    }

    private ParticlePool CreateParticles()
    {
        ParticlePool particles = Instantiate(_playerAimAndShoot.particles, _playerAimAndShoot.bulletSpawnPoint.position, _playerAimAndShoot.gun.transform.rotation);

        particles.SetPool(_particlePool);

        return particles;
    }

    private void OnTakeParticlesFromPool(ParticlePool particles)
    {
        particles.transform.position = _playerAimAndShoot.bulletSpawnPoint.position;
        particles.transform.right = _playerAimAndShoot.gun.transform.right;

        particles.gameObject.SetActive(true);
    }

    private void OnReturnParticlesToPool(ParticlePool particles)
    {
        particles.gameObject.SetActive(false);
    }

    private void OnDestroyParticles(ParticlePool particles)
    {
        Destroy(particles.gameObject);
    }
}
