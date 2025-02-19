using UnityEngine;

public enum WeaponType
{
    Casco,
    Otros
}

[CreateAssetMenu(fileName = "Weapon", menuName = "Inventory System/Items/Weapon")]
public class ItemWeapon : ItemBase
{
    [SerializeField]
    public WeaponType Type;
}
