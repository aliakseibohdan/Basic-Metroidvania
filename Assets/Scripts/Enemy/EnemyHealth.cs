using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth = 3f;
    [SerializeField] private ParticleSystem _damageParticles;
    [SerializeField] private AudioClip _damageSound;
    [SerializeField] private ScreenShakeProfile _profile;

    private float _currentHealth;

    private ParticleSystem _damageParticlesInstance;

    private CinemachineImpulseSource _impulseSource;

    private HealthBar _healthBar;

    private DamageFlash _damageFlash;

    public bool HasTakenDamage { get; set; }
    public float MaxHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Start()
    {
        _currentHealth = _maxHealth;

        _impulseSource = GetComponent<CinemachineImpulseSource>();

        _healthBar = GetComponentInChildren<HealthBar>();

        _damageFlash = GetComponent<DamageFlash>();
    }

    public void Damage(float damageAmount, Vector2 attackDirection)
    {
        //CameraShakeManager.instance.CameraShake(impulseSource);
        CameraShakeManager.instance.ScreenShakeFromProfile(_profile, _impulseSource);

        HasTakenDamage = true;

        _currentHealth -= damageAmount;

        SoundFXManager.instance.PlaySoundFXClip(_damageSound, transform, 1f);

        SpawnDamageParticles(attackDirection);

        _healthBar.UpdateHealthBar(_maxHealth, _currentHealth);

        if (_currentHealth <= 0)
        {
            Die();
        }

        _damageFlash.CallDamageFlash();
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void SpawnDamageParticles(Vector2 attackDirection)
    {
        Quaternion spawnRotation = Quaternion.FromToRotation(Vector2.right, attackDirection);

        _damageParticlesInstance = Instantiate(_damageParticles, transform.position, spawnRotation);
    }

    void IDamageable.Die()
    {
        throw new System.NotImplementedException();
    }
}
