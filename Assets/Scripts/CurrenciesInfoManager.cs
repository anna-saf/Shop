using System.Collections.Generic;
using UniRx;
using UnityEngine;

public class CurrenciesInfoManager : MonoBehaviour
{
    [SerializeField] private Transform currenciesInfoPanel;
    [SerializeField] private CurrencyInfoView currencyInfoPrefab;

    private void Start()
    {
        List<CurrencySO> currencyList = ServiceLocator.Instance.Get<GameCurrencyManager>().CurrencyList;
        foreach (CurrencySO currency in currencyList)
        {
            CreateCurrencyInfo(currency);
        }
    }

    private void CreateCurrencyInfo(CurrencySO currencySO)
    {
        var obj = Instantiate(currencyInfoPrefab, currenciesInfoPanel);
        CurrencyInfoView currencyButtonView = obj.GetComponent<CurrencyInfoView>();
        ReactiveProperty<int> currencyCount = ServiceLocator.Instance.Get<GameCurrencyManager>().GetCurrencyReactiveProperty(currencySO);

        currencyButtonView.CurrencyInfoViewModel.Init(currencySO, currencyCount);
    }
}
