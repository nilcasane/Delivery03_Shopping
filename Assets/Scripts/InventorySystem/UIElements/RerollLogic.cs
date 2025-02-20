using System;
using UnityEngine;
using UnityEngine.UI;

public class RerollLogic : MonoBehaviour
{
    public static Action OnInventoryReroll;

    public Button rerollButton;

    private void Start()
    {
        rerollButton.onClick.AddListener(OnRerollButtonClicked);
    }
    private void OnRerollButtonClicked()
    {
        OnInventoryReroll?.Invoke();
    }
}
