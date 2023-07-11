using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CoroutineRunner : MonoBehaviour, IService
{  

    // ��������� ��������
    public Coroutine RunCoroutine(IEnumerator coroutine)
    {
        return StartCoroutine(coroutine);
    }

    // ���������� ��������
    public void StopOneCoroutine(IEnumerator coroutine)
    {
        StopCoroutine(coroutine);
    }
}
