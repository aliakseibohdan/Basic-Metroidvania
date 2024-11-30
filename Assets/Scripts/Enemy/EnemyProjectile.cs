using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour, IDeflectable
{
    [SerializeField] private float damageAmount;
    [SerializeField] private AnimationCurve speedCurve;
    private IDamageable iDamageable;
    private int LayerIgnoreCollision;

    public Collider2D EnemyColl { get; set; }

    [field: SerializeField] public float ReturnSpeed { get; set; } = 10f;
    public bool IsDeflecting { get; set; }

    private Collider2D coll;
    private Rigidbody2D rb;

    private float speed, time;

    private void Start()
    {
        coll = GetComponent<Collider2D>();
        rb = GetComponent<Rigidbody2D>();

        LayerIgnoreCollision = LayerMask.NameToLayer("Checkpoint");

        IgnoreCollisionWithEnemyToggle();
    }

    private void FixedUpdate()
    {
        if (IsDeflecting)
        {
            speed = speedCurve.Evaluate(time);
            time += Time.fixedDeltaTime;

            rb.velocity = transform.right * speed * ReturnSpeed;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        iDamageable = collision.gameObject.GetComponent<IDamageable>();
        if (iDamageable != null)
        {
            iDamageable.Damage(damageAmount, transform.right);
        }

        if (collision.gameObject.layer != LayerIgnoreCollision)
        {
            Destroy(gameObject);
        }
    }

    private void IgnoreCollisionWithEnemyToggle()
    {
        if (!Physics2D.GetIgnoreCollision(coll, EnemyColl))
        {
            Physics2D.IgnoreCollision(coll, EnemyColl, true);
        }

        else
        {
            Physics2D.IgnoreCollision(coll, EnemyColl, false);
        }
    }

    public void Deflect(Vector2 direction)
    {
        IsDeflecting = true;

        IgnoreCollisionWithEnemyToggle();

        if ((direction.x > 0 && transform.right.x < 0) || (direction.x < 0 && transform.right.x > 0))
        {
            transform.right = -transform.right;
        }

        rb.velocity = transform.right * ReturnSpeed;
    }
}
