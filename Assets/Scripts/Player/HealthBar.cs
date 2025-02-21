using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour, IPointerClickHandler
{
    private Slider slider;
    public Gradient ColorGradient;
    public Image FillImage;

    private void OnEnable()
    {
        Player.OnHealthUpdated += SetValue;
    }
    private void OnDisable()
    {
        Player.OnHealthUpdated -= SetValue;
    }
    void Start()
    {
        slider = GetComponent<Slider>();
        SetValue(Player.MaxHealth);
    }

    public void SetValue(int Health)
    {
        float fractionHealth = Health / (Player.MaxHealth * 1f);
        slider.value = fractionHealth;
        FillImage.color = ColorGradient.Evaluate(fractionHealth);
        slider.gameObject.SetActive(true);
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        Player.OnHealthChange?.Invoke(-10);
    }
}
