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

    private readonly long productTimeSecond;

    private long currentTime; 

    private IEnumerator timeUpdateCorountine;
    public ProductCardViewModel(ProductSO productSO)
    {
        this.productSO = productSO;
        productTimeSecond = productSO.timeMinutes * 60;
    }

    private ProductState state;

    public void Init()
    {
        //PlayerPrefs.DeleteAll();
        //ServiceLocator.Instance.Get<GameCurrencyManager>().AddCurrency(ShopModel.Instance.CurrencyList[0], 20);
        //ServiceLocator.Instance.Get<GameCurrencyManager>().AddCurrency(ShopModel.Instance.CurrencyList[1], 20);

        string productState = ServiceLocator.Instance.Get<IDataManager>().TryReadData(productSO.productName + ShopModel.PLAYER_PREFS_PRODUCT_STATE);
        string lastProductTime = ServiceLocator.Instance.Get<IDataManager>().TryReadData(productSO.productName + ShopModel.PLAYER_PREFS_PRODUCT_TIME);

        CreateCurrencyButtons();

        if (lastProductTime == null)
        {
            lastProductTime = "0";
        }
        CheckProductTime(Convert.ToInt64(lastProductTime));

        state = GetState(productState);

        if (state == ProductState.Buy)
        {
            if (timeSeconds.Value > 0 || productSO.timeMinutes == 0)
            {
                BuyStateUpdate();
            }
            else
            {
                ProductNotPurchase();
            }
        }
        else
        {
            onBuyStateChange?.Invoke(this, false);
        }

        
    }

    private void CreateCurrencyButtons()
    {
        foreach (CurrencyPrice currencyPriceInfo in productSO.currencyPrice)
        {
            onCreateCurrencyButton?.Invoke(this, currencyPriceInfo);
        }
    } 

    private void CheckTimeEnd()
    {
        if(timeSeconds.Value <= 0 && productSO.timeMinutes > 0)
        {
            ProductNotPurchase();
        }
    }

    private void CheckProductTime(long lastProductTime)
    {
        if(lastProductTime != 0)
        {
            long timeDiff = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - lastProductTime;
            timeSeconds.Value = productTimeSecond - timeDiff;
        }
        else
        {
            timeSeconds.Value = productTimeSecond;
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
            timeSeconds.Value -= DateTimeOffset.UtcNow.ToUnixTimeSeconds() - currentTime;
            currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            yield return new WaitForSeconds(1f);
        }
    }

    public void PurchaseButtonPressed(CurrencyPrice currencyPriceInfo)
    {
        int currencyCount = ServiceLocator.Instance.Get<GameCurrencyManager>().GetCurrencyInfo(currencyPriceInfo.currency);
        if (currencyCount > currencyPriceInfo.price )
        {
            if (state == ProductState.NotBuy) {
                state = ProductState.Buy;
                BuyStateUpdate();
                ServiceLocator.Instance.Get<IDataManager>().WriteData(productSO.productName + ShopModel.PLAYER_PREFS_PRODUCT_TIME, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
                ServiceLocator.Instance.Get<IDataManager>().WriteData(productSO.productName + ShopModel.PLAYER_PREFS_PRODUCT_STATE, state.ToString());
                ServiceLocator.Instance.Get<GameCurrencyManager>().SubtractCurrency(currencyPriceInfo.currency, currencyPriceInfo.price);
                Debug.Log(ServiceLocator.Instance.Get<GameCurrencyManager>().GetCurrencyInfo(ShopModel.Instance.CurrencyList[0]));
                Debug.Log(ServiceLocator.Instance.Get<GameCurrencyManager>().GetCurrencyInfo(ShopModel.Instance.CurrencyList[1]));
            }
        }
        else
        {
            Debug.Log("недостаточно средств");
            //Вывести окно, что недостаточно средств
        }
    }

    private void BuyStateUpdate()
    {
        onBuyStateChange?.Invoke(this, true);
        if (productSO.timeMinutes > 0)
        {
            timeSeconds.Subscribe(_ => CheckTimeEnd());
            currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - 1;
            timeUpdateCorountine = Timer();
            ServiceLocator.Instance.Get<CoroutineRunner>().RunCoroutine(timeUpdateCorountine);
        }
    }

    private void ProductNotPurchase()
    {
        onBuyStateChange?.Invoke(this, false);
        state = ProductState.NotBuy;
        timeSeconds.Value = productTimeSecond;
        if (productSO.timeMinutes > 0)
        {
            if (timeUpdateCorountine != null)
            {
                ServiceLocator.Instance.Get<CoroutineRunner>().StopOneCoroutine(timeUpdateCorountine); 
                timeUpdateCorountine = null;
            }
        }
        ServiceLocator.Instance.Get<IDataManager>().WriteData(productSO.productName + ShopModel.PLAYER_PREFS_PRODUCT_STATE, ProductState.NotBuy.ToString());
    }
}
