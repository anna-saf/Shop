using System;
using System.Collections;
using UniRx;
using UnityEngine;

public class ProductCardViewModel 
{
    private ProductSO productSO;

    public event EventHandler<bool> onBuyStateChange;
    public event EventHandler<CurrencyPrice> onCreateCurrencyButton;

    public ReactiveProperty<long> TimeSeconds {get; private set;}   

    private readonly long productTimeSecond;

    private long currentTime; 

    private IEnumerator timeUpdateCorountine;
    public ProductCardViewModel(ProductSO productSO)
    {
        TimeSeconds = new ReactiveProperty<long>();
        this.productSO = productSO;
        productTimeSecond = productSO.timeMinutes * 60;
    }

    private ProductState state;

    public void Init()
    {

        string productState = ServiceLocator.Instance.Get<IDataManager>().TryReadData(productSO.productName + ShopModel.PLAYER_PREFS_PRODUCT_STATE);
        string lastProductTime = ServiceLocator.Instance.Get<IDataManager>().TryReadData(productSO.productName + ShopModel.PLAYER_PREFS_PRODUCT_TIME);

        CreateCurrencyButtons();

        if (lastProductTime == null)
        {
            lastProductTime = "0";
        }

        state = GetState(productState);

        CheckProductTime(Convert.ToInt64(lastProductTime));

        if (state == ProductState.Buy)
        {
            if (TimeSeconds.Value > 0 || productSO.timeMinutes == 0)
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
        if(TimeSeconds.Value <= 0 && productSO.timeMinutes > 0)
        {
            ProductNotPurchase();
        }
    }

    private void CheckProductTime(long lastProductTime)
    {
        if(lastProductTime != 0 && state == ProductState.Buy)
        {
            long timeDiff = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - lastProductTime;
            TimeSeconds.Value = productTimeSecond - timeDiff;
        }
        else
        {
            TimeSeconds.Value = productTimeSecond;
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
        while (TimeSeconds.Value > 0)
        {
            long diff = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - currentTime;
            TimeSeconds.Value -= diff;
            currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            yield return new WaitForSeconds(1f);
        }
    }

    public void PurchaseButtonPressed(CurrencyPrice currencyPriceInfo)
    {
        int currencyCount = ServiceLocator.Instance.Get<GameCurrencyManager>().GetCurrencyInfo(currencyPriceInfo.currency);
        if (currencyCount >= currencyPriceInfo.price )
        {
            if (state == ProductState.NotBuy) {
                ServiceLocator.Instance.Get<WarningWindowViewModel>().HideMessage();
                state = ProductState.Buy;
                BuyStateUpdate();
                ServiceLocator.Instance.Get<IDataManager>().WriteData(productSO.productName + ShopModel.PLAYER_PREFS_PRODUCT_TIME, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString());
                ServiceLocator.Instance.Get<IDataManager>().WriteData(productSO.productName + ShopModel.PLAYER_PREFS_PRODUCT_STATE, state.ToString());
                ServiceLocator.Instance.Get<GameCurrencyManager>().SubtractCurrency(currencyPriceInfo.currency, currencyPriceInfo.price);
            }
        }
        else
        {
            ServiceLocator.Instance.Get<WarningWindowViewModel>().ShowMessage(ShopModel.Instance.CurrencyNotEnoughMessage);
        }
    }

    private void BuyStateUpdate()
    {
        onBuyStateChange?.Invoke(this, true);
        if (productSO.timeMinutes > 0)
        {
            TimeSeconds.Subscribe(_ => CheckTimeEnd());
            currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds()-1;
            timeUpdateCorountine = Timer();
            ServiceLocator.Instance.Get<CoroutineRunner>().RunCoroutine(timeUpdateCorountine);
        }
    }

    private void ProductNotPurchase()
    {
        onBuyStateChange?.Invoke(this, false);
        state = ProductState.NotBuy;
        TimeSeconds.Value = productTimeSecond;
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
