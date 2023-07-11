using System;
using UniRx;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProductCardView : MonoBehaviour
{
    [SerializeField] private ProductSO productSO;
    [SerializeField] private Transform notBuyView;
    [SerializeField] private Transform currencyButtonsView;
    [SerializeField] private Transform alreadyBuyView;
    [SerializeField] private Image productImage;
    [SerializeField] private TextMeshProUGUI timer;

    [SerializeField] private CurrencyButtonView currencyButtonViewPrefab;

    public ProductCardViewModel ProductCardViewModel { get; private set; }  

    private void Awake()
    {
        ProductCardViewModel = new ProductCardViewModel(productSO);
        productImage.sprite = productSO.productImage;
        ProductCardViewModel.onBuyStateChange += ProductCardViewModel_onBuyStateChange;
        ProductCardViewModel.onCreateCurrencyButton += ProductCardViewModel_onCreateCurrencyButton; ;
        ProductCardViewModel.timeSeconds.Subscribe(_ => UpdateTime(_));
            
    }

    private void ProductCardViewModel_onCreateCurrencyButton(object sender, CurrencyPrice e)
    {
        CreateCurrencyButton(currencyButtonViewPrefab, currencyButtonsView, e);
    }

    private void ProductCardViewModel_onBuyStateChange(object sender, bool e)
    {
        isBuyViewState(e);
    }

    private void Start()
    {
        ProductCardViewModel.Init();
    }

    private void isBuyViewState(bool isbuy)
    {
        notBuyView.gameObject.SetActive(!isbuy);
        currencyButtonsView.gameObject.SetActive(!isbuy);
        alreadyBuyView.gameObject.SetActive(isbuy);
        timer.gameObject.SetActive(isbuy);
    }

    private void UpdateTime(long timeSeconds)
    {
        if (timeSeconds > 0)
        {
            TimeSpan time = TimeSpan.FromSeconds(timeSeconds);
            timer.text = time.ToString(@"hh\:mm\:ss");
        }
    }

    private void CreateCurrencyButton(CurrencyButtonView currencyButtonViewPrefab, Transform currencyButtonsView, CurrencyPrice currencyPriceInfo)
    {
        var obj = Instantiate(currencyButtonViewPrefab, currencyButtonsView);
        CurrencyButtonView currencyButtonView = obj.GetComponent<CurrencyButtonView>();
        currencyButtonView.CurrencyButtonViewModel.Init(currencyPriceInfo, this);
    }

}
