using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyButtonView : MonoBehaviour
{
    [SerializeField] private Button currencyButton;
    [SerializeField] private TextMeshProUGUI productCurrencyPrice;
    [SerializeField] private Image currencyImage;

    public CurrencyButtonViewModel CurrencyButtonViewModel { get; private set; }

    private void Awake()
    {
        CurrencyButtonViewModel = new CurrencyButtonViewModel(productCurrencyPrice, currencyImage);
        /*currencyButton.onClick.AddListener(() =>
        {

        });*/    
    }
}
