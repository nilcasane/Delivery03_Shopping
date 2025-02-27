using UnityEngine;

[CreateAssetMenu(fileName = "Generic", menuName = "Inventory System/Items/Generic")]
public class ItemBase : ScriptableObject
{
    public string Name;
    public string Description;
    public Sprite Image;
    public bool IsStackeable;
    public int Value;
    public int Price;
    public int MinPrice;
    public int MaxPrice;

}
