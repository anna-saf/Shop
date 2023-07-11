using Unity.VisualScripting;
using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour
{
    [SerializeField] private CoroutineRunner coroutineRunner;
    private IDataManager dataManager;
    private GameCurrencyManager gameCurrencyManager;

    private void Awake()
    {
        ServiceLocator.Init();

        ServiceLocator.Instance.Register<CoroutineRunner>(coroutineRunner);

        dataManager = new PlayerPrefsManager();
        ServiceLocator.Instance.Register<IDataManager> (dataManager);

        gameCurrencyManager = new GameCurrencyManager();
        ServiceLocator.Instance.Register<GameCurrencyManager>(gameCurrencyManager);

    }
}
