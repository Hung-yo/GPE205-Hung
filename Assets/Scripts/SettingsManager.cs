using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public AudioMixer masterAudioMixer;
    public AudioMixer sfxAudioMixer;
    public AudioMixer musicAudioMixer;
    public Slider masterVolumeSlider;
    public Slider sfxVolumeSlider;
    public Slider musicVolumeSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnMasterVolumeSliderChange();
        OnSFXVolumeSliderChange();
        OnMusicVolumeSliderChange();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMasterVolumeSliderChange()
    {
        OnMasterVolumeChange();
    }
    public void OnSFXVolumeSliderChange()
    {
        OnSFXVolumeChange();
    }
    public void OnMusicVolumeSliderChange()
    {
        OnMusicVolumeChange();
    }

    public void OnMasterVolumeChange()
    {
        float newVolume = masterVolumeSlider.value;
        if (newVolume <= 0)
            newVolume = -80;
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;
        }

        masterAudioMixer.SetFloat("MasterVolume", newVolume);
    }
    public void OnSFXVolumeChange()
    {
        float newVolume = sfxVolumeSlider.value;
        if (newVolume <= 0)
            newVolume = -80;
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;
        }

        masterAudioMixer.SetFloat("SFXVolume", newVolume);
    }
    public void OnMusicVolumeChange()
    {
        float newVolume = musicVolumeSlider.value;
        if (newVolume <= 0)
            newVolume = -80;
        else
        {
            newVolume = Mathf.Log10(newVolume);
            newVolume = newVolume * 20;
        }

        masterAudioMixer.SetFloat("MusicVolume", newVolume);
    }
}
