using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectable/Currency", fileName = "New Coin Collectable")]
public class CollectableCurrencySO : CollectableSOBase
{
    [Header("Collectable Stats")]
    public int CurrencyAmount = 1;

    public override void Collect(GameObject objectThatCollected)
    {
        CurrencyManager.instance.IncrementCurrency(CurrencyAmount);

        if (_playerEffects == null)
            GetReference(objectThatCollected);

        _playerEffects.PlayCollectionEffect(CollectionFlashTime, CollectColor, CollectionClip);
    }
}
