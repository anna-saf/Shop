using System.Collections;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour, IService
{  

    // Запустить корутину
    public Coroutine RunCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    // Остановить корутину
    public void StopOneCoroutine(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }
}
