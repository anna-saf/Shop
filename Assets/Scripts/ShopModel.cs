using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModel : MonoBehaviour
{

    public const string PLAYER_PREFS_PRODUCT_STATE = "ProductState";
    public const string PLAYER_PREFS_PRODUCT_TIME = "ProductTimePurchase";

    public static ShopModel Instance { get; private set; } 


    private void Awake()
    {
        Instance = this;
    }
}
