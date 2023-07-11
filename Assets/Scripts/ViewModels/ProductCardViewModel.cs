using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class ProductCardViewModel 
{
    private ProductSO productSO;

    public event EventHandler<bool> onBuyStateChange;
    public event EventHandler<CurrencyPrice> onCreateCurrencyButton;

    public ReactiveProperty<long> timeSeconds = new ReactiveProperty<long>();
    
    public ProductCardViewModel(ProductSO productSO)
    {
        this.productSO = productSO;
    }

    public void Init()
    {
        string productState = ShopModel.Instance.DataManager.TryReadData(productSO.productName + ShopModel.PLAYER_PREFS_PRODUCT_STATE);
        string lastProductTime = ShopModel.Instance.DataManager.TryReadData(productSO.productName + ShopModel.PLAYER_PREFS_PRODUCT_TIME);
        if(lastProductTime == null)
        {
            lastProductTime = "0";
        }
        CheckProductTime(Convert.ToInt64(lastProductTime));

        ProductState state = GetState(productState);

        if (state == ProductState.Buy)
        {
            if (timeSeconds.Value > 0)
            {
                onBuyStateChange?.Invoke(this, true);
            }
            else
            {
                //Поменять статус на "не куплено"
            }
        }
        else
        {
            onBuyStateChange?.Invoke(this, false);
        }

        foreach (CurrencyPrice currencyPriceInfo in productSO.currencyPrice)
        {
            onCreateCurrencyButton?.Invoke(this, currencyPriceInfo);
        }
    }

    private void CheckProductTime(long lastProductTime)
    {
        if(lastProductTime != 0)
        {
            long timeDiff = lastProductTime - DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            timeSeconds.Value -= timeDiff;
        }
        else
        {
            timeSeconds.Value = productSO.timeMinutes * 60;
        }
    }

    private ProductState GetState(string productState)
    {
        if (productState != null && productState == ProductState.Buy.ToString())
        {
            return ProductState.Buy;
        }
        return ProductState.NotBuy;
    }    


    private IEnumerator Timer()
    {
        while (timeSeconds.Value > 0)
        {
            timeSeconds.Value--; 
            yield return new WaitForSeconds(1f);
        }
    }
}
