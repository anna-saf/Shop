using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu()]
public class ProductSO : ScriptableObject
{
    public string productName;
    public Sprite productImage;
    public int timeMinutes;
    public List<CurrencyPrice> currencyPrice;
}

[System.Serializable]
public class CurrencyPrice
{
    public CurrencySO currency;
    public int price;
}