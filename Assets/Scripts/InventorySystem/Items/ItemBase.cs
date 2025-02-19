using UnityEngine;

[CreateAssetMenu(fileName = "Generic", menuName = "Inventory System/Items/Generic")]
public class ItemBase : ScriptableObject
{
    public string Name;
    public string Description;
    public int Price;
    public bool IsStackeable;
    public Sprite Image;
}
