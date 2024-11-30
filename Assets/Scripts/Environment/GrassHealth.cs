using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrassHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 1f;
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private ScreenShakeProfile profile;

    private float currentHealth;

    private ParticleSystem damageParticlesInstance;

    public bool HasTakenDamage { get; set; }
    public float MaxHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private CinemachineImpulseSource impulseSource;

    private void Start()
    {
        currentHealth = maxHealth;

        impulseSource = GetComponent<CinemachineImpulseSource>();
    }

    public void Damage(float damageAmount, Vector2 attackDirection)
    {
        //CameraShakeManager.instance.CameraShake(impulseSource);
        CameraShakeManager.instance.ScreenShakeFromProfile(profile, impulseSource);

        HasTakenDamage = true;

        currentHealth -= damageAmount;

        SpawnDamageParticles(attackDirection);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void SpawnDamageParticles(Vector2 attackDirection)
    {
        Quaternion spawnRotation = Quaternion.FromToRotation(Vector2.right, attackDirection);

        damageParticlesInstance = Instantiate(damageParticles, transform.position, spawnRotation);
    }

    void IDamageable.Die()
    {
        throw new System.NotImplementedException();
    }
}
