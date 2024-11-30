using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableTriggerHandler : MonoBehaviour
{
    [SerializeField] private LayerMask _whoCanCollect = LayerMaskHelper.CreateLayerMask(9);

    private Collectable _collectable;

    private void Awake()
    {
        _collectable = GetComponent<Collectable>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (LayerMaskHelper.ObjIsInLayerMask(collision.gameObject, _whoCanCollect))
        {
            _collectable.Collect(collision.gameObject);
            Debug.Log("Destoyed");
            Destroy(gameObject);
        }
    }
}
