using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopModel : MonoBehaviour
{
    public const string PLAYER_PREFS_PRODUCT_STATE = "ProductState";
    public const string PLAYER_PREFS_PRODUCT_TIME = "ProductTime";

    public IDataManager DataManager { get; private set; }

    public static ShopModel Instance { get; private set; } 

    public long lastTimeRun { get; private set; }

    private void Awake()
    {
        Instance = this;
        DataManager = new PlayerPrefsManager();
    }
}
