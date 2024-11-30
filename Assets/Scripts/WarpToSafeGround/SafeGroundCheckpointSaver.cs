using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeGroundCheckpointSaver : MonoBehaviour
{
    [SerializeField] private LayerMask whatIsCheckpoint;

    public Vector2 SafeGroundLocation { get; private set; } = Vector2.zero;

    private Collider2D coll;
    private float safeSpotYOffset;

    private void Start()
    {
        SafeGroundLocation = transform.position;

        coll = GetComponent<Collider2D>();

        safeSpotYOffset = (coll.bounds.size.y / 2);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if((whatIsCheckpoint.value & (1 << collision.gameObject.layer)) > 0)
        {
            SafeGroundLocation = new Vector2(collision.bounds.center.x, collision.bounds.min.y + safeSpotYOffset);
        }
    }

    public void WarpPlayerToSafeGround()
    {
        transform.position = SafeGroundLocation;
    }
}
