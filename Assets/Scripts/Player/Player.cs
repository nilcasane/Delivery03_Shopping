using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputSettings;

public class Player : MonoBehaviour
{
    public static int Money = 1000;
    public static int MaxHealth = 100;
    public int Health;

    public static Action<int> OnHealthChange;
    public static Action<int> OnHealthUpdated;

    public static Action<int> OnMoneyChange;
    public static Action<int> OnMoneyUpdated;

    private void Start()
    {
        Health = MaxHealth;
        OnHealthUpdated?.Invoke(Health);
        OnMoneyUpdated?.Invoke(Money);
    }

    private void OnEnable()
    {
        OnHealthChange += UpdateHealth;
        OnMoneyChange += UpdateMoney;
    }

    private void OnDisable()
    {
        OnHealthChange -= UpdateHealth;
        OnMoneyChange -= UpdateMoney;
    }

    private void UpdateHealth(int value)
    {
        if ((Health + value) == 0)
        {
            GameplayManager.OnPlayerLose?.Invoke();
        }
        else
        {
            Health += value;
            OnHealthUpdated?.Invoke(Health);
            Money += value;
            OnMoneyUpdated?.Invoke(Money);
        }
    }

    private void UpdateMoney(int value)
    {
        if ((Money + value) > 0)
        {
            Money += value;
            OnMoneyUpdated?.Invoke(Money);
        }
    }
}
