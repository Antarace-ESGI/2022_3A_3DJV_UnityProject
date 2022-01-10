using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeScript : MonoBehaviour
{
    public Slider slider;
    public Text text;

    private AudioListener[] audioListeners;
    
    private void setVolumeSlider()
    {
        float value = slider.value;
        PlayerPrefs.SetFloat("musicVolume",value);
    }

    public void saveVolumeParameters()
    {
        float value = slider.value;
        Debug.Log(PlayerPrefs.GetFloat("musicVolume"));
        PlayerPrefs.SetFloat("musicVolume",value);
        PlayerPrefs.Save();
    }

    private void textUpdateVolume()
    {
        text.text = $"{slider.value.ToString()}%";
    }
    
    void Start()
    {
        audioListeners = FindObjectsOfType(typeof(AudioListener)) as AudioListener[];
        if (audioListeners.Length != 0)
        {
            Debug.Log("Find one or more audio source.s !");
        }
        
        //deleteKey();
        slider.onValueChanged.AddListener(delegate {
            textUpdateVolume();
            //setVolumeSlider();
        });
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
