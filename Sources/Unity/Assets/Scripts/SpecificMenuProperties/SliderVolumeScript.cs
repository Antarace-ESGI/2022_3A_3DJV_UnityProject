using UnityEngine;
using UnityEngine.UI;

public class SliderVolumeScript : MonoBehaviour
{
    public Slider slider;
    
    private void setVolumeSlider()
    {
        float value = slider.value;
        PlayerPrefs.SetFloat("musicVolume",value);
    }

    public void saveVolumeParameters()
    {
        Debug.Log(PlayerPrefs.GetFloat("musicVolume"));
        PlayerPrefs.Save();
    }
    
    void Start()
    {
        //deleteKey();
        slider.onValueChanged.AddListener(delegate {
            setVolumeSlider();
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
