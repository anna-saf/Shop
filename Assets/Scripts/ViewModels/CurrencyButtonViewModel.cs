using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyButtonViewModel 
{
    private TextMeshProUGUI productCurrencyPrice;
    private Image currencyImage;

    public CurrencyButtonViewModel(TextMeshProUGUI productCurrencyPrice, Image currencyImage)
    {
        this.productCurrencyPrice = productCurrencyPrice;
        this.currencyImage = currencyImage;
    }

    public void Init(int currencyPrice, Sprite currencyImage)
    {
        productCurrencyPrice.text = currencyPrice.ToString();
        this.currencyImage.sprite = currencyImage;
    }
}
