using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
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
        text.text = Health.ToString();
    }
}
