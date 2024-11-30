using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Collectable/Player Upgrade", fileName = "New Player Upgrade")]
public class CollectableUpgradeSO : CollectableSOBase
{
    private PlayerUpgrades _playerUpgrades;

    private enum UpgradeToGivePlayer
    {
        Bomb,
        Invisibility
    }
    [Header("Collectable Stats")]
    [SerializeField] private UpgradeToGivePlayer _upgradeToGivePlayer;

    public override void Collect(GameObject objectThatCollected)
    {
        GivePowerUp(objectThatCollected);

        if (_playerEffects == null)
            GetReference(objectThatCollected);

        _playerEffects.PlayCollectionEffect(CollectionFlashTime, CollectColor, CollectionClip);
    }

    private void GivePowerUp(GameObject objectThatCollected)
    {
        if (_playerUpgrades == null)
            _playerUpgrades = FinderHelper.GetComponentOnObject<PlayerUpgrades>(objectThatCollected);

        switch (_upgradeToGivePlayer)
        {
            case UpgradeToGivePlayer.Bomb:

                GiveBombPowerUp();
                break;
            case UpgradeToGivePlayer.Invisibility:
                
                GiveInvisibilityPowerUp();
                break;
        }
    }

    private void GiveBombPowerUp()
    {
        _playerUpgrades.UnlockBomb();
    }

    private void GiveInvisibilityPowerUp()
    {
        _playerUpgrades.UnlockInvisibility();
    }
}
