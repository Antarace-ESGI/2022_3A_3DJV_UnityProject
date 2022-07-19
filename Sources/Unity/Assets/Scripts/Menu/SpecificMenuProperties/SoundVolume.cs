using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundVolume : MonoBehaviour
{

  public AudioMixer audioMixer;
  public Text textS;
  public Slider sliderS;

  public static SoundVolume instance;

  void Awake()
  {
    if(instance != null){
      Debug.LogWarning("Il y a plus d'une instance de PlayerMovement dans la scene");
      return ;
    }
    instance = this;
  }


  private void Start()
  {
    if (PlayerPrefs.HasKey("soundVolume"))
    {
        sliderS.value = PlayerPrefs.GetFloat("soundVolume");
    }
  }

  private void OnEnable()
  {
    textS.text = $"{(sliderS.value + 80).ToString()}%";
  }

  public void setEffectsVolume(float EffectsVolume)
  {
    audioMixer.SetFloat("Sound",EffectsVolume);
    textS.text = $"{(sliderS.value + 80).ToString()}%";
  }

  public void saveSoundVolume()
  {
      float valueS = sliderS.value;
      PlayerPrefs.SetFloat("soundVolume",valueS);
      PlayerPrefs.Save();
  }
}
