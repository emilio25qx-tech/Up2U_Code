using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    [Header("Volume Setting")]
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start()
    {
        LoadVolume();
    }

    public void SetMasterVolume()
    {
        float volume = Mathf.Clamp(masterSlider.value, 0.0001f, 1f);
        myMixer.SetFloat("master", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("masterVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = Mathf.Clamp(musicSlider.value, 0.0001f, 1f);
        myMixer.SetFloat("music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }

    public void SetSFXVolume()
    {
        float volume = Mathf.Clamp(sfxSlider.value, 0.0001f, 1f);
        myMixer.SetFloat("sfx", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }

    private void LoadVolume()
    {
        float master = PlayerPrefs.GetFloat("masterVolume", 1f);
        float music = PlayerPrefs.GetFloat("musicVolume", 1f);
        float sfx = PlayerPrefs.GetFloat("sfxVolume", 1f);

        masterSlider.value = master;
        musicSlider.value = music;
        sfxSlider.value = sfx;

        myMixer.SetFloat("master", Mathf.Log10(Mathf.Clamp(master, 0.0001f, 1f)) * 20);
        myMixer.SetFloat("music", Mathf.Log10(Mathf.Clamp(music, 0.0001f, 1f)) * 20);
        myMixer.SetFloat("sfx", Mathf.Log10(Mathf.Clamp(sfx, 0.0001f, 1f)) * 20);
    }
}
