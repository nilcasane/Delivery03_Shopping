using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class LanguageDropbox : MonoBehaviour
{
    //public Language Language;

    private TMP_Dropdown _dropdown;

    void Start()
    {
        _dropdown = GetComponent<TMP_Dropdown>();

        // Agregar opciones al dropdown
        _dropdown.options.Clear();
        _dropdown.options.Add(new TMP_Dropdown.OptionData("English"));
        _dropdown.options.Add(new TMP_Dropdown.OptionData("Español"));
        _dropdown.options.Add(new TMP_Dropdown.OptionData("Català"));

        // Suscribirse al evento onValueChanged
        _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
    }
    private void OnDropdownValueChanged(int index)
    {
        Language selectedLanguage = (Language)(index + 1);
        Localizer.SetLanguage(selectedLanguage);
    }
}
