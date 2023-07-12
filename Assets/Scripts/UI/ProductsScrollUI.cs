using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ProductsScrollUI : MonoBehaviour
{
    [SerializeField] private ScrollRect productsScrollRect;

    private void Start()
    {
        ServiceLocator.Instance.Get<DeviceOrientationManager>().OnOrientationChange += ProductScrollUI_OnOrientationChange;
    }

    private void ProductScrollUI_OnOrientationChange(object sender, Orientation e)
    {
        if (e == Orientation.Horizontal)
        {
            productsScrollRect.horizontal = true;
            productsScrollRect.vertical = false;
        }
        else
        {
            productsScrollRect.horizontal = false;
            productsScrollRect.vertical = true;
        }
    }
}
