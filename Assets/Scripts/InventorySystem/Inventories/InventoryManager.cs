using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton

    [SerializeField] public Inventory shopInventory;
    [SerializeField] public Inventory playerInventory;

    public ItemBase[] PlayerItems;
    public ItemBase[] ShopItems;

    public static Action<ItemBase> OnBuyItem;
    public static Action OnBuyInvalid;
    public static Action<ItemBase> OnSellItem;
    public static Action<ConsumableItem> OnUseItem;
    public static Action OnInventoryReroll;

    private int _rerollCost = 15;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        OnInventoryReroll += RerollShop;
        OnBuyItem += BuyItem;
        OnSellItem += SellItem;
        OnUseItem += UseItem;
    }

    private void OnDisable()
    {
        OnInventoryReroll -= RerollShop;
        OnBuyItem -= BuyItem;
        OnSellItem -= SellItem;
        OnUseItem -= UseItem;
    }


    private void Start()
    {
        InitializePlayer();
        InitializeShop();
    }

    private void InitializePlayer()
    {
        playerInventory.ClearInventory();

        for (int i = 0; i < PlayerItems.Length; i++)
        {
            playerInventory.AddItem(PlayerItems[i]);
        }
    }

    private void InitializeShop()
    {
        shopInventory.ClearInventory();
        RandomizeShop();
    }

    private void RandomizeShop()
    {
        shopInventory.ClearInventory();

        int numberItems = Random.Range(18, 24);

        for (int i = 0; i < numberItems; i++)
        {
            ItemBase actualItem = ShopItems[Random.Range(0, ShopItems.Length)];
            if (actualItem != null)
            {
                actualItem.Price = Random.Range(actualItem.MinPrice, actualItem.MaxPrice);
                shopInventory.AddItem(actualItem);
            }
        }
    }

    private void BuyItem(ItemBase item)
    {
        if (Player.Instance.Money >= item.Price)
        {
            if (playerInventory != null && playerInventory.AddItem(item))
            {
                shopInventory.RemoveItem(item);
                Player.OnChangeMoney(-item.Price);
                Player.OnSelectedItem?.Invoke(null);

                if (Player.Instance.Money == 0)
                {
                    GameplayManager.OnPlayerLose?.Invoke();
                }
            }
        }
        else
        {
            OnBuyInvalid?.Invoke();
        }
    }

    private void SellItem(ItemBase item)
    {
        playerInventory.RemoveItem(item);
        Player.OnChangeMoney(item.Value);
        Player.OnSelectedItem?.Invoke(null);

        if (playerInventory != null && playerInventory.Length == 0)
        {
            GameplayManager.OnPlayerLose?.Invoke();
        }
    }

    private void UseItem(ConsumableItem item)
    {
        Player.OnChangeHealth?.Invoke(item.HealthPoints);
        playerInventory.RemoveItem(item);
        Player.OnSelectedItem?.Invoke(null);

        if (playerInventory != null && playerInventory.Length == 0)
        {
            GameplayManager.OnPlayerLose?.Invoke();
        }
    }

    private void RerollShop()
    {
        if (Player.Instance.Money >= _rerollCost)
        {
            RandomizeShop();
            Player.OnChangeMoney(-_rerollCost);
        }
    }
}
