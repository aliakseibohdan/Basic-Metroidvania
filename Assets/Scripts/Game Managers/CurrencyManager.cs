using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager instance;

    public int CurrentCurrency {  get; private set; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void IncrementCurrency(int amount)
    {
        CurrentCurrency += amount;

        HUDManager.instance.UpdateCurrencyText(CurrentCurrency);
    }
}
