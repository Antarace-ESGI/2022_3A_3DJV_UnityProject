using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeScript : MonoBehaviour
{
    public Slider slider;
    public Text text;

    private AudioSource[] audioSources;

    private void initVolume()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            slider.value = PlayerPrefs.GetFloat("musicVolume") * 100.0f;
        }
        text.text = $"{(slider.value).ToString()}%";
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].CompareTag("Music"))
            {
                audioSources[i].GetComponent<AudioSource>().volume = slider.value / 100.0f;
                audioSources[i].Play();
            }
        }
    }

    public void saveVolumeParameters()
    {
        float value = slider.value / 100.0f;
        PlayerPrefs.SetFloat("musicVolume",value);
        PlayerPrefs.Save();
    }

    private void updateVolume()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].CompareTag("Music"))
            {
                audioSources[i].GetComponent<AudioSource>().volume = slider.value / 100.0f;
            }
        }
        text.text = $"{(slider.value).ToString()}%";
    }
    

    private void OnEnable()
    {
        audioSources = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        
        initVolume();
        
        //deleteKey();
        
        slider.onValueChanged.AddListener(delegate {
            updateVolume();
        });
    }

    private void OnDisable()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            if (audioSources[i].CompareTag("Music"))
            {
                audioSources[i].Stop();
            }
        }
    }


    // -------- Test ------------ //

    // For testing purpose only
    private void deleteKey()
    {
        if (PlayerPrefs.HasKey("musicVolume"))
        {   
            PlayerPrefs.DeleteKey("musicVolume");
        }
    }

}
