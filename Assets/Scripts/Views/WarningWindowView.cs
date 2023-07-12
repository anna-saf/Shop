using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WarningWindowView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI messageText;
    public WarningWindowViewModel WarningWindowViewModel { get; private set; }

    public void Start()
    {
        WarningWindowViewModel = ServiceLocator.Instance.Get<WarningWindowViewModel>();
        WarningWindowViewModel.OnShowMessage += WarningWindowViewModel_OnShowMessage;
        WarningWindowViewModel.OnHideMessage += WarningWindowViewModel_OnHideMessage;
        WarningWindowViewModel.Init();
        gameObject.SetActive(false);
    }

    private void WarningWindowViewModel_OnHideMessage(object sender, System.EventArgs e)
    {
        gameObject.SetActive(false);
    }

    private void WarningWindowViewModel_OnShowMessage(object sender, string e)
    {
        gameObject.SetActive(true);
        messageText.text = e;
    }
}
