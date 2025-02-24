using UnityEngine;

public class InventoryManager : MonoBehaviour, IConsume
{
    public static InventoryManager Instance; // Singleton
    [SerializeField]
    public Inventory shopInventory;
    [SerializeField]
    public Inventory playerInventory;

    public ItemBase[] PlayerItems;
    public ItemBase[] ShopItems;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        RerollLogic.OnInventoryReroll += RandomizeShop;
    }

    private void OnDisable()
    {
        RerollLogic.OnInventoryReroll -= RandomizeShop;
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
                actualItem.Value = Random.Range(actualItem.MinPrice, actualItem.MaxPrice);
                shopInventory.AddItem(actualItem);
            }
        }
    }


    public bool BuyItem(ItemBase item)
    {
        if (Player.Instance.Money >= item.Value)
        {
            if (playerInventory.AddItem(item))
            {
                shopInventory.RemoveItem(item);
                Player.OnChangeMoney(-item.Value);
                return true;
            }
        }
        return false;
    }

    public void SellItem(ItemBase item)
    {
        playerInventory.RemoveItem(item);
        Player.OnChangeMoney(item.Value);
        if (playerInventory.Length == 0)
        {
            GameplayManager.OnPlayerLose?.Invoke();
        }
    }

    public void Use(ConsumableItem item)
    {
        if (item is ItemPotion potion)
        {
            playerInventory.RemoveItem(potion);
            Player.OnChangeHealth(potion.HealthPoints);
        }
    }
}
