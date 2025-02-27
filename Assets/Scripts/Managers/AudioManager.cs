using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio Source")]
    public AudioSource _mainSource;
    public AudioSource _effectSource;

    [Header("Audio Clips")]
    public AudioClip _backgroundMusic;

    public AudioClip _moneySound;
    public AudioClip _healthDamage;
    public AudioClip _healthRestore;
    public AudioClip _invalidBuy;
    public AudioClip _rerollShop;
    public AudioClip _loseSound;

    private void OnEnable()
    {
        Player.OnChangeHealth += HealthSound;
        Player.OnChangeMoney += MoneySound;
        InventoryManager.OnBuyInvalid += InvalidBuySound;
        InventoryManager.OnInventoryReroll += RerollSound;
        GameplayManager.OnPlayerLose += LoseSound;
    }

    private void OnDisable()
    {
        Player.OnChangeHealth -= HealthSound;
        Player.OnChangeMoney -= MoneySound;
        InventoryManager.OnBuyInvalid -= InvalidBuySound;
        InventoryManager.OnInventoryReroll -= RerollSound;
        GameplayManager.OnPlayerLose -= LoseSound;
    }

    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            _mainSource.clip = _backgroundMusic;
            _mainSource.Play();
        }
    }

    private void PlaySoundEffect(AudioClip soundEffect)
    {
        _effectSource.clip = soundEffect;
        _effectSource.Play();
    }

    private void HealthSound(int value)
    {
        if (value < 0) PlaySoundEffect(_healthDamage);
        else if (value > 0) PlaySoundEffect(_healthRestore);
    }
    private void MoneySound(int value)
    {
        PlaySoundEffect(_moneySound);
    }
    private void InvalidBuySound()
    {
        PlaySoundEffect(_invalidBuy);
    }
    private void RerollSound()
    {
        PlaySoundEffect(_invalidBuy);
    }
    private void LoseSound()
    {
        PlaySoundEffect(_loseSound);
    }
}
