using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    void Damage(float damageAmount, Vector2 attackDirection);

    void Die();

    bool HasTakenDamage { get; set; }

    float MaxHealth { get; set; }
    float CurrentHealth { get; set; }
}
