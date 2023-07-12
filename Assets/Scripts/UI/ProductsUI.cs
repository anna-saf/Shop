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

            productsGridLayoutGroup.cellSize = new Vector2 (ShopModel.ProductCardHorizontalX, ShopModel.ProductCardHorizontalY);
            productsGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedRowCount;
            productsGridLayoutGroup.constraintCount = 1;
        }
        else
        {
            rectTransform.pivot = new Vector2(0.5f, 1);

            productsGridLayoutGroup.cellSize = new Vector2(ShopModel.ProductCardVerticalX, ShopModel.ProductCardVerticalY);
            productsGridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
            productsGridLayoutGroup.constraintCount = 2;
        }
    }
}
