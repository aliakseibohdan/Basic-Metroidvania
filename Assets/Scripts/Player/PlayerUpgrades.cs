using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUpgrades : MonoBehaviour
{
    public bool BombUpgradeUnlocked { get; private set; }
    public bool InvisibilityUpgradeUnlocked { get; private set; }

    private PlayerBomb _playerBomb;
    private PlayerInvisibility _playerInvisibility;

    private void Awake()
    {
        _playerBomb = GetComponent<PlayerBomb>();
        _playerInvisibility = GetComponent<PlayerInvisibility>();

        _playerBomb.enabled = false;
        _playerInvisibility.enabled = false;
    }

    public void UnlockBomb()
    {
        BombUpgradeUnlocked = true;
        _playerBomb.enabled = true;
    }

    public void UnlockInvisibility()
    {
        InvisibilityUpgradeUnlocked = true;
        _playerInvisibility.enabled = true;
    }
}
