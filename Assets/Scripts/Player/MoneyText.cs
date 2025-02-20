using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    private void OnEnable()
    {
        Player.OnMoneyUpdated += MoneyChange;
    }

    private void OnDisable()
    {
        Player.OnMoneyUpdated -= MoneyChange;
    }

    private void MoneyChange(int Money)
    {
        text.text = Money.ToString();
    }
}
