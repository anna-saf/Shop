using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyButtonViewModel 
{
    private TextMeshProUGUI productCurrencyPrice;
    private Image currencyImage;
    private ProductCardView productCardView;
    private CurrencyPrice currencyPriceInfo;

    public CurrencyButtonViewModel(TextMeshProUGUI productCurrencyPrice, Image currencyImage)
    {
        this.productCurrencyPrice = productCurrencyPrice;
        this.currencyImage = currencyImage;
    }

    public void Init(CurrencyPrice currencyPriceInfo, ProductCardView productCardView)
    {
        this.currencyPriceInfo = currencyPriceInfo;
        productCurrencyPrice.text = currencyPriceInfo.price.ToString();
        currencyImage.sprite = currencyPriceInfo.currency.currencyImage;
        this.productCardView = productCardView;
    }

    public void PurchaseButtonPressed()
    {
        productCardView.ProductCardViewModel.PurchaseButtonPressed(currencyPriceInfo);
    }
}
