using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionMenu : MonoBehaviour
{
    [Header("Audio Stuff")]
    public AudioMixer audioMixer;
    
    [Header("Resolution Set")]
    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    
    [Header("Quality Set")]
    public TMP_Dropdown qualityDropdown;
    
    [Header("Texture Set")]
    public TMP_Dropdown textureDropdown;

    [Header("Volume Set")]
    public Slider masterVolume;
    public Slider sfxVolumeSlider;
    float currentVolume;
    
    
    public static bool loadData = false;

    public void Awake()
    {
        #region Resolution Start
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();
        resolutions = Screen.resolutions;
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " +
                            resolutions[i].height;
            options.Add(option);
            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i; 
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.RefreshShownValue();

        #endregion
     
        LoadSettings(currentResolutionIndex);
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
        currentVolume = volume;
        audioMixer.SetFloat("masterVolume",volume);
    }

    public void SFXVolume(float volume)
    {
        PlayerPrefs.SetFloat("SFXVolume",volume);
                
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,
            resolution.height, Screen.fullScreen);
    }

    public void SetTextureQuality(int textureIndex)
    {
        QualitySettings.masterTextureLimit = textureIndex;
        qualityDropdown.value = 6;
    }
    
    public void SaveSettings()
    {
        PlayerPrefs.SetInt("QualitySettingPreference",
            qualityDropdown.value);
        PlayerPrefs.SetInt("ResolutionPreference",
            resolutionDropdown.value);
        PlayerPrefs.SetInt("TextureQualityPreference",
            textureDropdown.value);
        PlayerPrefs.SetInt("FullscreenPreference",
            Convert.ToInt32(Screen.fullScreen));
        PlayerPrefs.SetFloat("VolumePreference",
            currentVolume);
    }

    public void LoadSettings(int currentResolutionIndex)
    {
        if (PlayerPrefs.HasKey("QualitySettingPreference"))
            qualityDropdown.value =
                PlayerPrefs.GetInt("QualitySettingPreference");
        else
            qualityDropdown.value = 3;
        if (PlayerPrefs.HasKey("ResolutionPreference"))
            resolutionDropdown.value =
                PlayerPrefs.GetInt("ResolutionPreference");
        else
            resolutionDropdown.value = currentResolutionIndex;
        if (PlayerPrefs.HasKey("TextureQualityPreference"))
            textureDropdown.value =
                PlayerPrefs.GetInt("TextureQualityPreference");
        else
            textureDropdown.value = 0;
        if (PlayerPrefs.HasKey("FullscreenPreference"))
            Screen.fullScreen =
                Convert.ToBoolean(PlayerPrefs.GetInt("FullscreenPreference"));
        else
            Screen.fullScreen = true;
        if (PlayerPrefs.HasKey("VolumePreference"))
            masterVolume.value =
                PlayerPrefs.GetFloat("VolumePreference");
        else
            masterVolume.value =
                PlayerPrefs.GetFloat("VolumePreference");
        if (PlayerPrefs.HasKey("SFXVolume"))
            sfxVolumeSlider.value =
                PlayerPrefs.GetFloat("SFXVolume");
        else
            sfxVolumeSlider.value = 
                PlayerPrefs.GetFloat("SFXVolume");
    }
}
