using TMPro;
using UnityEngine;

public class MoneyText : MonoBehaviour
{
    public TextMeshProUGUI Label;
    private void Start()
    {
        Label.text = Player.Instance.Money.ToString();
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
        Label.text = Money.ToString();
    }
}
