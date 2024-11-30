using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallDamage : MonoBehaviour
{
    private PlayerHealth playerHealth;
    //private SafeGroundSaver safeGroundSaver;
    private SafeGroundCheckpointSaver checkpointSaver;

    private void Start()
    {
        //safeGroundSaver = GameObject.FindGameObjectWithTag("Player").GetComponent<SafeGroundSaver>();
        checkpointSaver = GameObject.FindGameObjectWithTag("Player").GetComponent<SafeGroundCheckpointSaver>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerHealth = collision.gameObject.GetComponent<PlayerHealth>();

            playerHealth.Damage(1f, transform.position);

            //safeGroundSaver.WarpPlayerToSafeGround();
            checkpointSaver.WarpPlayerToSafeGround();
        }
    }
}
