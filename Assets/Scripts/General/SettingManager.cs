using System;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.Events;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private SettingSO audioSettings;

    [Header("Slider Music")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;

    [Header("Slider Sensibility")]
    [SerializeField] private Slider sliderSensibility;

    public UnityEvent OnFinishSettings;

    static public event Action<float> eventSensibility;
    private float currentSensibility;

    private void Start()
    {
        audioSettings.LoadSetting();

        sliderMaster.value = audioSettings.GetMasterVolume();
        sliderMaster.onValueChanged.AddListener(UpdateMasterVolume);

        sliderMusic.value = audioSettings.GetMusicVolume();
        sliderMusic.onValueChanged.AddListener(UpdateMusicVolume);

        sliderSFX.value = audioSettings.GetSFXVolume();
        sliderSFX.onValueChanged.AddListener(UpdateSFXVolume);

        sliderSensibility.value = audioSettings.GetSensibility();
        sliderSensibility.onValueChanged.AddListener(UpdateSensibility);

        UpdateSensibility(audioSettings.GetSensibility());
        OnFinishSettings.Invoke();
    }
    private void ActiveEventSensibility()
    {
        eventSensibility?.Invoke(currentSensibility);
    }

    private void UpdateMasterVolume(float value)
    {
        audioSettings.SetMasterVolume(value);
    }

    private void UpdateMusicVolume(float value)
    {
        audioSettings.SetMusicVolume(value);
    }

    private void UpdateSFXVolume(float value)
    {
        audioSettings.SetSFXVolume(value);
    }

    private void UpdateSensibility(float value)
    {
        audioSettings.SetSensibility(value);
        this.currentSensibility = value * 100;
        ActiveEventSensibility();
    }

}
