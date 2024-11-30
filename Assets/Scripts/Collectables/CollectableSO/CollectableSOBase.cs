using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableSOBase : ScriptableObject
{
    [Header("Collection Effects")]
    public Color CollectColor;
    public float CollectionFlashTime = 0.5f;
    public AudioClip CollectionClip;

    protected PlayerEffects _playerEffects;

    public abstract void Collect(GameObject objectThatCollected);

    public void GetReference(GameObject objectThatCollected)
    {
        _playerEffects = FinderHelper.GetComponentOnObject<PlayerEffects>(objectThatCollected);
    }
}
