using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateStartCurrency : MonoBehaviour
{
    [SerializeField] private int coinCount = 0;
    [SerializeField] private int diamondCount = 0;
    [SerializeField] private bool cleanAllData;

    void Start()
    {
        if (cleanAllData)
        {
            PlayerPrefs.DeleteAll();
        }
        if (coinCount > 0)
        {
            ServiceLocator.Instance.Get<GameCurrencyManager>().AddCurrency(ServiceLocator.Instance.Get<GameCurrencyManager>().CurrencyList[0], coinCount);
        }

        if (diamondCount > 0)
        {
            ServiceLocator.Instance.Get<GameCurrencyManager>().AddCurrency(ServiceLocator.Instance.Get<GameCurrencyManager>().CurrencyList[1], diamondCount);
        }
    }
}
