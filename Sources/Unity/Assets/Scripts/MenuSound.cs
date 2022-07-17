using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MenuSound : MonoBehaviour
{
  // Just a script to set select button for the Gamepad option

  private GameObject go;

  public AudioClip MenuMoveSound;
  public AudioClip SelectSound;
  public AudioSource audioSource;
  public AudioSource audioSource2;

  private void OnEnable()
  {
      go = EventSystem.current.currentSelectedGameObject;
  }

  private void Update(){


    if(EventSystem.current.currentSelectedGameObject != go){
      audioSource.clip = MenuMoveSound;
      audioSource.Play();
      go = EventSystem.current.currentSelectedGameObject;
    }else{
    //  Debug.Log("MEME");
    }

  }

  public void selSound()
  {
    audioSource2.clip = SelectSound;
    audioSource2.Play();
  }








}
