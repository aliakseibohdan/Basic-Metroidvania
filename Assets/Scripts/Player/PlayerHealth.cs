using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
{
    [SerializeField] private float maxHealth = 5f;
    [SerializeField] private ParticleSystem damageParticles;
    [SerializeField] private AudioClip[] damageSounds;
    [SerializeField] private AudioClip deathSound;

    private ParticleSystem damageParticlesInstance;

    private float currentHealth;

    private KnockBack knockback;

    public bool HasTakenDamage { get; set; }
    public float MaxHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private void Start()
    {
        currentHealth = maxHealth;
        knockback = GetComponent<KnockBack>();
    }

    public void Damage(float damageAmount, Vector2 attackDirection)
    {
        HasTakenDamage = true;

        currentHealth -= damageAmount;

        SpawnDamageParticles();

        if (currentHealth <= 0)
        {
            SoundFXManager.instance.PlaySoundFXClip(deathSound, transform, 1f);
            Die();
        }

        SoundFXManager.instance.PlayRandomSoundFXClip(damageSounds, transform, 1f);
        //knockback.CallKnockback(attackDirection, Vector2.up, UserInput.instance.moveInput.x);
    }

    private void Die()
    {
        Destroy(gameObject);
    }

    private void SpawnDamageParticles()
    {
        damageParticlesInstance = Instantiate(damageParticles, transform.position, Quaternion.identity);
    }

    void IDamageable.Die()
    {
        throw new System.NotImplementedException();
    }
}
