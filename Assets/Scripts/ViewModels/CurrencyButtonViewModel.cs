using System;
using UnityEngine;

public class CurrencyButtonViewModel 
{
    public string ProductCurrencyPrice {  get; private set; }
    public Sprite CurrencyImage { get; private set; }
    private ProductCardView productCardView;
    private CurrencyPrice currencyPriceInfo;

    public event EventHandler OnInitialiseComplete;

    public void Init(CurrencyPrice currencyPriceInfo, ProductCardView productCardView)
    {
        this.currencyPriceInfo = currencyPriceInfo;
        ProductCurrencyPrice = currencyPriceInfo.price.ToString();
        CurrencyImage = currencyPriceInfo.currency.currencyImage;
        this.productCardView = productCardView;
        OnInitialiseComplete?.Invoke(this, EventArgs.Empty);
    }

    public void PurchaseButtonPressed()
    {
        productCardView.ProductCardViewModel.PurchaseButtonPressed(currencyPriceInfo);
    }
}
