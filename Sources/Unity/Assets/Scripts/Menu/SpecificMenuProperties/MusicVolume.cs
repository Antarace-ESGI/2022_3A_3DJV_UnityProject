using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MusicVolume : MonoBehaviour
{

  public AudioMixer audioMixer;
  public Text textM;
  public Slider sliderM;

  public static MusicVolume instance;



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
    if (PlayerPrefs.HasKey("musicVolume"))
    {
        sliderM.value = PlayerPrefs.GetFloat("musicVolume");
    }
  }

  private void OnEnable()
  {
    textM.text = $"{(sliderM.value + 80).ToString()}%";
  }

  public void setMusicVolume(float MusicVolume)
  {
    audioMixer.SetFloat("Music",MusicVolume);
    textM.text = $"{(sliderM.value + 80).ToString()}%";
  }

  public void saveMusicVolume()
  {
      float valueM = sliderM.value / 100.0f;
      PlayerPrefs.SetFloat("musicVolume",valueM);
      PlayerPrefs.Save();
  }
}
