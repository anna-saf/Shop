using System;
using UniRx;
using UnityEngine;

public class CurrencyInfoViewModel
{
    public ReactiveProperty<int> CurrencyCount { get; private set; }

    public event EventHandler OnInitialiseComplete;
    public Sprite CurrencyImage {get; private set; }


    public void Init(CurrencySO currencySO, ReactiveProperty<int> currencyCount)
    {
        CurrencyCount = currencyCount;
        CurrencyImage = currencySO.currencyImage;
        OnInitialiseComplete?.Invoke(this, EventArgs.Empty);
        CurrencyCount.Value = ServiceLocator.Instance.Get<GameCurrencyManager>().GetCurrencyInfo(currencySO);
    }
}
