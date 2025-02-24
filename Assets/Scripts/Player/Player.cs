using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputSettings;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public static int initialMoney = 100;
    public static int MaxHealth = 100;
    public int Money;
    public int Health;

    public static GameObject SelectedItem;

    public static Action<GameObject> OnSelectedItem;
    public static Action OnResetSelectedItems;

    public static Action<int> OnChangeHealth;
    public static Action<int> OnHealthUpdated;

    public static Action<int> OnChangeMoney;
    public static Action<int> OnMoneyUpdated;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Health = MaxHealth;
        Money = initialMoney;
        OnHealthUpdated?.Invoke(Health);
        OnMoneyUpdated?.Invoke(Money);
    }

    private void OnEnable()
    {
        OnChangeHealth += UpdateHealth;
        OnChangeMoney += UpdateMoney;
        OnSelectedItem += UpdateSelectedItem;
    }

    private void OnDisable()
    {
        OnChangeHealth -= UpdateHealth;
        OnChangeMoney -= UpdateMoney;
        OnSelectedItem -= UpdateSelectedItem;
    }

    private void UpdateHealth(int value)
    {
        Health = Mathf.Max(0, Health + value);

        if (Health == 0) GameplayManager.OnPlayerLose?.Invoke();
        else OnHealthUpdated?.Invoke(Health);
    }

    private void UpdateMoney(int value)
    {
        Money = Mathf.Max(0, Money + value);
        OnMoneyUpdated?.Invoke(Money);
    }

    private void UpdateSelectedItem(GameObject item)
    {
        SelectedItem = item;
        OnResetSelectedItems?.Invoke();
    }

}
