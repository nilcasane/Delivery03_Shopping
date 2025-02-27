using System;
using UnityEngine;
using UnityEngine.UI;

public class RerollLogic : MonoBehaviour
{

    public Button rerollButton;

    private void Start()
    {
        rerollButton.onClick.AddListener(OnRerollButtonClicked);
    }
    private void OnRerollButtonClicked()
    {
        InventoryManager.OnInventoryReroll?.Invoke();
    }
}
