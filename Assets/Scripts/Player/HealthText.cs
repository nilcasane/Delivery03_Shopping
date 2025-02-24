using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public TextMeshProUGUI Label;

    private void Start()
    {
        Label.text = Player.Instance.Health.ToString();
    }
    private void OnEnable()
    {
        Player.OnHealthUpdated += HealthChange;
    }

    private void OnDisable()
    {
        Player.OnHealthUpdated -= HealthChange;
    }

    private void HealthChange(int Health)
    {
        Label.text = Health.ToString();
    }
}
