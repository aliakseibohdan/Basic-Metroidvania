using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShoot : MonoBehaviour
{
    [SerializeField] private Rigidbody2D bulletPrefab;
    [SerializeField] private float bulletSpeed = 15f;
    [SerializeField] private float timeBtwAttacks = 2f;

    private float shootTimer;

    private Rigidbody2D bulletRB;

    private EnemyProjectile enemyProjectile;

    private Collider2D coll;

    private Transform playerTrans;

    private void Start()
    {
        coll = GetComponent<Collider2D>();

        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer > timeBtwAttacks)
        {
            shootTimer = 0;

            Shoot();
        }
    }

    private void Shoot()
    {
        bulletRB = Instantiate(bulletPrefab, transform.position, transform.rotation);

        bulletRB.transform.right = GetShootDirection();

        bulletRB.velocity = bulletRB.transform.right * bulletSpeed;

        enemyProjectile = bulletRB.gameObject.GetComponent<EnemyProjectile>();

        enemyProjectile.EnemyColl = coll;
    }

    public Vector2 GetShootDirection()
    {
        return (playerTrans.position - transform.position).normalized;
    }
}
