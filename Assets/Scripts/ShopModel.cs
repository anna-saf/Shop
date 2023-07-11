using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModel : MonoBehaviour
{
    [SerializeField] private List<CurrencySO> currencyList;

    public const string PLAYER_PREFS_PRODUCT_STATE = "ProductState";
    public const string PLAYER_PREFS_PRODUCT_TIME = "ProductTimePurchase";

    public List<CurrencySO> CurrencyList { get { return currencyList; } }

    public static ShopModel Instance { get; private set; } 


    private void Awake()
    {
        Instance = this;
    }
}
