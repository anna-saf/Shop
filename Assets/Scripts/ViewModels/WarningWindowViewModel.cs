using System;
using System.Collections;
using UnityEngine;

public class WarningWindowViewModel : IService
{
    public event EventHandler<string> OnShowMessage;
    public event EventHandler OnHideMessage;

    private bool AlreadyShow = false;


    private int showTime;
    private int currentShowTime;
    private IEnumerator timeUpdateCorountine;

    private long currentTime = 0;

    public void Init()
    {
        currentShowTime = ShopModel.Instance.WarningMessageShowTime;
        showTime = currentShowTime;
    }

    public void ShowMessage(string message)
    {
        showTime = currentShowTime;
        if (!AlreadyShow)
        {
            AlreadyShow = true;
            currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds()-1;
            timeUpdateCorountine = Timer();
            ServiceLocator.Instance.Get<CoroutineRunner>().RunCoroutine(timeUpdateCorountine);
            OnShowMessage?.Invoke(this, message);
        }
    }

    public void HideMessage()
    {
        AlreadyShow = false;
        OnHideMessage?.Invoke(this, EventArgs.Empty);
        if (timeUpdateCorountine != null)
        {
            ServiceLocator.Instance.Get<CoroutineRunner>().StopCoroutine(timeUpdateCorountine);
        }
        timeUpdateCorountine = null;
    }

    private IEnumerator Timer()
    {
        while (showTime > 0)
        {
            long diff = DateTimeOffset.UtcNow.ToUnixTimeSeconds() - currentTime;
            showTime -= (int)diff;
            currentTime = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            yield return new WaitForSeconds(1f);
        }
        HideMessage();
    }
}
