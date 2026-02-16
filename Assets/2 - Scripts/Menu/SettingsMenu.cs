using System;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{

    [Header("Volume Settings")]
    [SerializeField] AudioMixer audioMixer;
    [SerializeField] Slider sliderVolume;
    [SerializeField] TextMeshProUGUI textVolume;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioMixer.GetFloat("MainVolume", out float musicValueForSlider);
        sliderVolume.value = musicValueForSlider;
        int normValue = (int)((musicValueForSlider + 80) / (sliderVolume.maxValue + 80) * 100);
        textVolume.text = Convert.ToString(normValue);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("MainVolume", volume);
        int normValue = (int)((volume + 80) / (sliderVolume.maxValue + 80) * 100);
        textVolume.text = Convert.ToString(normValue);
    }
}
