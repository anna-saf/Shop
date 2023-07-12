using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModel : MonoBehaviour
{

    public const string PLAYER_PREFS_PRODUCT_STATE = "ProductState";
    public const string PLAYER_PREFS_PRODUCT_TIME = "ProductTimePurchase";

    public const int ProductCardVerticalX = 300;
    public const int ProductCardVerticalY = 500;
    public const int ProductCardVerticalConstrainCount = 2;
    public const int ProductCardHorizontalX = 120;
    public const int ProductCardHorizontalY = 200;
    public const int ProductCardHorizontalConstrainCount = 1;

    public static ShopModel Instance { get; private set; } 


    private void Awake()
    {
        Instance = this;
    }
}
