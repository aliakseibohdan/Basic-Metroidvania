using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletBehavior : MonoBehaviour
{
    [Header("General Bullet Stats")]
    [SerializeField] private LayerMask whatDestroysBullet;
    [SerializeField] private float destroyTime = 3f;

    [Header("Normal Bullet Stats")]
    [SerializeField] private float normalBulletSpeed = 15f;
    [SerializeField] private float normalBulletDamage = 1f;

    [Header("Physics Bullet Stats")]
    public float physicsBulletSpeed = 17.5f;
    public float physicsBulletGravity = 3f;
    [SerializeField] private float physicsBulletDamage = 2f;

    private Rigidbody2D rb;
    private float damage;

    private ObjectPool<BulletBehavior> _pool;

    public enum BulletType
    {
        Normal,
        Physics
    }
    public BulletType bulletType;

    private Coroutine deactivateBulletAfterTimeCoroutine;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        SetRBStats();
    }

    private void OnEnable()
    {
        deactivateBulletAfterTimeCoroutine = StartCoroutine(DeactivateBulletAfterTime());

        InitializeBulletStats();
    }

    private void FixedUpdate()
    {
        if (bulletType == BulletType.Physics)
        {
            transform.right = rb.velocity;
        }
    }

    private void InitializeBulletStats()
    {
        if (bulletType == BulletType.Normal)
        {
            SetStraightVelocity();
            damage = normalBulletDamage;
        }

        else if (bulletType == BulletType.Physics)
        {
            SetPhysicsVelocity();
            damage = physicsBulletDamage;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0) 
        {
            // spawn particles

            // play sound FX

            // ScreenShake

            // Damage Enemy
            IDamageable iDamageable = collision.gameObject.GetComponent<IDamageable>();
            if (iDamageable != null)
            {
                iDamageable.Damage(damage, transform.right);
            }

            // Destroy the bullet
            //Destroy(gameObject);
            _pool.Release(this);
        }
    }

    private void SetStraightVelocity()
    {
        rb.velocity = transform.right * normalBulletSpeed;
    }

    private void SetPhysicsVelocity()
    {
        rb.velocity = transform.right * physicsBulletSpeed;
    }

    private void SetRBStats()
    {
        if (bulletType == BulletType.Normal)
        {
            rb.gravityScale = 0f;
        }

        else if (bulletType == BulletType.Physics)
        {
            rb.gravityScale = physicsBulletGravity;
        }
    }

    public void SetPool(ObjectPool<BulletBehavior> pool)
    {
        _pool = pool;
    }

    private IEnumerator DeactivateBulletAfterTime()
    {
        float elapsedTime = 0f;
        while(elapsedTime < destroyTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        _pool.Release(this);
    }
}
