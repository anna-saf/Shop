using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class GameCurrencyManager : MonoBehaviour, IService
{
    [SerializeField] private List<CurrencySO> currencyList;

    public List<CurrencySO> CurrencyList { get { return currencyList; } }

    private Dictionary<CurrencySO, ReactiveProperty<int>> currenciesCountDict = new Dictionary<CurrencySO, ReactiveProperty<int>>();

    private void Awake()
    {
        foreach (var currency in currencyList)
        {
            currenciesCountDict.Add(currency, new ReactiveProperty<int>(0));
        }
    }

    public ReactiveProperty<int> GetCurrencyReactiveProperty(CurrencySO currencySO)
    {
        return currenciesCountDict[currencySO];
    }

    public int GetCurrencyInfo (CurrencySO currency)
    {
        string currencyCount = ServiceLocator.Instance.Get<IDataManager>().TryReadData(currency.name);
        if(currencyCount != null)
        {
            return Convert.ToInt32 (currencyCount);
        }
        return 0;
    }

    public void AddCurrency(CurrencySO currency, int currencyCount)
    {
        string currencyCountInData = ServiceLocator.Instance.Get<IDataManager>().TryReadData(currency.name);
        if (currencyCountInData == null)
        {
            currencyCountInData = "0";
        }

        currenciesCountDict[currency].Value = Convert.ToInt32(currencyCountInData) + currencyCount;
        ServiceLocator.Instance.Get<IDataManager>().WriteData(currency.name, (Convert.ToInt32(currencyCountInData) + currencyCount).ToString());
    }

    public void SubtractCurrency(CurrencySO currency, int currencyCount)
    {
        string currencyCountInData = ServiceLocator.Instance.Get<IDataManager>().TryReadData(currency.name);
        if (currencyCountInData != null)
        {
            currenciesCountDict[currency].Value = Convert.ToInt32(currencyCountInData) - currencyCount;
            ServiceLocator.Instance.Get<IDataManager>().WriteData(currency.name, (Convert.ToInt32(currencyCountInData) - currencyCount).ToString());
        }
    }


}
