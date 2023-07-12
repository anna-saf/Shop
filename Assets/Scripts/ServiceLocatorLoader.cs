using Unity.VisualScripting;
using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour
{
    [SerializeField] private CoroutineRunner coroutineRunner;
    [SerializeField] private GameCurrencyManager gameCurrencyManager;
    [SerializeField] private DeviceOrientationManager deviceOrientationManager;
    private IDataManager dataManager;
    private DataAESEncryption dataAESEncryption;
    private WarningWindowViewModel warningWindowViewModel;

    private void Awake()
    {
        ServiceLocator.Init();

        ServiceLocator.Instance.Register<CoroutineRunner>(coroutineRunner);

        dataAESEncryption = new DataAESEncryption(ShopModel.AESKey, ShopModel.AESIv);
        ServiceLocator.Instance.Register<DataAESEncryption>(dataAESEncryption);

        dataManager = new PlayerPrefsManager();
        ServiceLocator.Instance.Register<IDataManager> (dataManager);

        ServiceLocator.Instance.Register<GameCurrencyManager>(gameCurrencyManager);

        ServiceLocator.Instance.Register<DeviceOrientationManager>(deviceOrientationManager);

        warningWindowViewModel = new WarningWindowViewModel();
        ServiceLocator.Instance.Register<WarningWindowViewModel>(warningWindowViewModel);

    }
}
