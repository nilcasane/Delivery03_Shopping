using System;
using System.Collections;
using UnityEngine;
using static UnityEngine.InputSystem.InputSettings;

public class Player : MonoBehaviour
{
    public static Player Instance { get; private set; }

    public GameObject HealthEffect;
    private Animator _healthAnimator;

    public static int initialMoney = 100;
    public static int MaxHealth = 100;
    public int Money;
    public int Health;

    public static GameObject SelectedItem;

    public static Action<GameObject> OnSelectedItem;
    public static Action<GameObject> OnResetSelectedItems;

    public static Action<int> OnChangeHealth;
    public static Action<int> OnHealthUpdated;

    public static Action<int> OnChangeMoney;
    public static Action<int> OnMoneyUpdated;

    private void Awake()
    {
        Instance = this;
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

    private void Start()
    {
        _healthAnimator = HealthEffect.GetComponent<Animator>();
        HealthEffect.SetActive(false);
        Health = MaxHealth;
        Money = initialMoney;
        OnHealthUpdated?.Invoke(Health);
        OnMoneyUpdated?.Invoke(Money);
    }
    private void Update()
    {
        if (_healthAnimator != null && _healthAnimator.isActiveAndEnabled)
        {
            AnimatorStateInfo stateInfo = _healthAnimator.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.normalizedTime >= 0.8f)
            {
                HealthEffect.SetActive(false);
            }
        }
    }
    private void UpdateHealth(int value)
    {
        if (value > 0)
        {
            Health = Mathf.Min(MaxHealth, Health + value);
        }
        else if (value < 0)
        {
            Health = Mathf.Max(0, Health + value);
        }

        HealthEffect.SetActive(true);
        _healthAnimator.SetFloat("HealthModify", value);
        OnHealthUpdated?.Invoke(Health);

        if (Health == 0) GameplayManager.OnPlayerLose?.Invoke();
    }

    private void UpdateMoney(int value)
    {
        Money = Mathf.Max(0, Money + value);
        OnMoneyUpdated?.Invoke(Money);
    }

    private void UpdateSelectedItem(GameObject item)
    {
        SelectedItem = item;
        OnResetSelectedItems?.Invoke(item);
    }

}
