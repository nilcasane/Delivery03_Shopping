using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance; // Singleton
    [SerializeField]
    public Inventory shopInventory;
    [SerializeField]
    public Inventory playerInventory;
    private int playerCoins;

    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        playerCoins = Player.Money;
    }
    public bool BuyItem(ItemBase item)
    {
        if (playerCoins >= item.Value && playerInventory.AddItem(item))
        {
            playerCoins -= item.Value;
            shopInventory.RemoveItem(item);
            Player.OnMoneyUpdated(playerCoins);
            return true;
        }
        return false;
    }

    public bool SellItem(ItemBase item)
    {
        if (shopInventory.AddItem(item))
        {
            playerCoins += item.Value;
            playerInventory.RemoveItem(item);
            Player.OnMoneyUpdated(playerCoins);
            return true;
        }
        return false;
    }
}
