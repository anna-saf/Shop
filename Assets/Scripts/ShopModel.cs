using UnityEngine;

public class ShopModel : MonoBehaviour
{
    [SerializeField] private Vector2 productCardVerticalSize;
    [SerializeField] private Vector2 productCardHorizontalSize;
    [SerializeField] private int productCardVerticalConstrainCount;
    [SerializeField] private int productCardHorizontalConstrainCount;
    [SerializeField] private int warningMessageShowTime;
    [SerializeField] private string currencyNotEnoughMessage;


    public const string AESKey = "p5zcyhrlW9s94SbELpE+4R/Kz+4foOwm";
    public const string AESIv = "d5bvgu73fhodb7kc";

    public const string PLAYER_PREFS_PRODUCT_STATE = "ProductState";
    public const string PLAYER_PREFS_PRODUCT_TIME = "ProductTimePurchase";

    public Vector2 ProductCardVerticalSize { get { return productCardVerticalSize; } }
    public Vector2 ProductCardHorizontalSize { get { return productCardHorizontalSize; } }
    public int ProductCardVerticalConstrainCount { get { return productCardVerticalConstrainCount; } }
    public int ProductCardHorizontalConstrainCount { get { return productCardHorizontalConstrainCount; } }
    public int WarningMessageShowTime { get { return warningMessageShowTime; } }
    public string CurrencyNotEnoughMessage { get { return currencyNotEnoughMessage; } }

    public static ShopModel Instance { get; private set; } 


    private void Awake()
    {
        Instance = this;
    }
}
