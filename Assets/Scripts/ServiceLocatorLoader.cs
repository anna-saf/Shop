using Unity.VisualScripting;
using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour
{
    [SerializeField] private CoroutineRunner coroutineRunner;
    [SerializeField] private GameCurrencyManager gameCurrencyManager;
    private IDataManager dataManager;

    private void Awake()
    {
        ServiceLocator.Init();

        ServiceLocator.Instance.Register<CoroutineRunner>(coroutineRunner);

        dataManager = new PlayerPrefsManager();
        ServiceLocator.Instance.Register<IDataManager> (dataManager);

        ServiceLocator.Instance.Register<GameCurrencyManager>(gameCurrencyManager);

    }
}
