using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private SettingSO audioSettings;

    [Header("Slider Music")]
    [SerializeField] private Slider sliderMaster;
    [SerializeField] private Slider sliderMusic;
    [SerializeField] private Slider sliderSFX;

    [Header("Image")]
    [SerializeField] private Image image1;
    [SerializeField] private Image image2;
    [SerializeField] private Image image3;

    [Header("Sprite")]
    [SerializeField] private Sprite sprite1;
    [SerializeField] private Sprite sprite2;
    [SerializeField] private Sprite sprite3;
    private void Awake()
    {
        audioSettings.LoadVolumes();
    }
    private void Start()
    {

        sliderMaster.value = audioSettings.GetMasterVolume();
        sliderMaster.onValueChanged.AddListener(UpdateMasterVolume);

        sliderMusic.value = audioSettings.GetMusicVolume();
        sliderMusic.onValueChanged.AddListener(UpdateMusicVolume);

        sliderSFX.value = audioSettings.GetSFXVolume();
        sliderSFX.onValueChanged.AddListener(UpdateSFXVolume);

        UpdateMasterVolume(sliderMaster.value);
        UpdateMusicVolume(sliderMusic.value);
        UpdateSFXVolume(sliderSFX.value);
    }
    private void UpdateMasterVolume(float value)
    {
        audioSettings.SetMasterVolume(value);
        if (value == 1)
        {
            image1.sprite = sprite3;
        }
        else if (value>=0.5f)
        {
            image1.sprite = sprite2;
        }
        else
        {
            image1.sprite = sprite1;
        }
    }

    private void UpdateMusicVolume(float value)
    {
        audioSettings.SetMusicVolume(value);
        if (value == 1)
        {
            image2.sprite = sprite3;
        }
        else if (value >= 0.5f)
        {
            image2.sprite = sprite2;
        }
        else
        {
            image2.sprite = sprite1;
        }
    }

    private void UpdateSFXVolume(float value)
    {
        audioSettings.SetSFXVolume(value);
        if (value == 1)
        {
            image3.sprite = sprite3;
        }
        else if (value >= 0.5f)
        {
            image3.sprite = sprite2;
        }
        else
        {
            image3.sprite = sprite1;
        }
    }

}
