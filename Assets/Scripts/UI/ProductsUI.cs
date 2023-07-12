using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProductsUI : MonoBehaviour
{
    [SerializeField] private GridLayoutGroup productsGridLayoutGroup;
    [SerializeField] private RectTransform rectTransform;

    private void Start()
    {
        ServiceLocator.Instance.Get<DeviceOrientationManager>().OnOrientationChange += ProductUI_OnOrientationChange;
    }

    private void ProductUI_OnOrientationChange(object sender, Orientation e)
    {
        if (e == Orientation.Horizontal)
        {
            rectTransform.pivot = new Vector2 (0, 0.5f);
            rectTransform.localPosition = new Vector2(0, 0);

            productsGridLayoutGroup.cellSize = ShopModel.Instance.ProductCardHorizontalSize;
            productsGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            productsGridLayoutGroup.constraintCount = ShopModel.Instance.ProductCardHorizontalConstrainCount;
        }
        else
        {
            rectTransform.pivot = new Vector2(0.5f, 1);
            rectTransform.localPosition = new Vector2(0, 0);

            productsGridLayoutGroup.cellSize = ShopModel.Instance.ProductCardVerticalSize;
            productsGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            productsGridLayoutGroup.constraintCount = ShopModel.Instance.ProductCardVerticalConstrainCount;
        }
    }
}
