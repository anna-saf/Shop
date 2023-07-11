using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

public class CurrencyInfoView : MonoBehaviour
{
    [SerializeField] private Image currencyImage;
    [SerializeField] private TextMeshProUGUI currencyText;

    public CurrencyInfoViewModel CurrencyInfoViewModel { get; private set; }

    private void Awake()
    {
        CurrencyInfoViewModel = new CurrencyInfoViewModel();
        CurrencyInfoViewModel.OnInitialiseComplete += CurrencyInfoViewModel_OnInitialiseComplete;
    }

    private void CurrencyInfoViewModel_OnInitialiseComplete(object sender, System.EventArgs e)
    {
        CurrencyInfoViewModel.CurrencyCount.Subscribe(_ => ChangeCurrencyCount(_)); 
        currencyImage.sprite = CurrencyInfoViewModel.CurrencyImage;
    }

    private void ChangeCurrencyCount(int currencyCount)
    {
        currencyText.text = currencyCount.ToString();
    } 
}
