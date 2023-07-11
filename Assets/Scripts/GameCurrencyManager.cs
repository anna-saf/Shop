using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCurrencyManager : IService
{
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

        ServiceLocator.Instance.Get<IDataManager>().WriteData(currency.name, (Convert.ToInt32(currencyCountInData) + currencyCount).ToString());
    }

    public void SubtractCurrency(CurrencySO currency, int currencyCount)
    {
        string currencyCountInData = ServiceLocator.Instance.Get<IDataManager>().TryReadData(currency.name);
        if (currencyCountInData != null)
        {
            ServiceLocator.Instance.Get<IDataManager>().WriteData(currency.name, (Convert.ToInt32(currencyCountInData) - currencyCount).ToString());
        }
    }


}
