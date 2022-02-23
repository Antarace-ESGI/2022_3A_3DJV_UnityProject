using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{

  private Transform playerSpawn;

  private void Awake(){
    playerSpawn =  GameObject.FindGameObjectWithTag("PlayerSpawn").transform;
  }

      private void OnTriggerEnter(Collider collision){

        if(collision.CompareTag("Player")){

          StartCoroutine(ReplacePlayer(collision));

        }
      }

      private IEnumerator ReplacePlayer(Collider collision){
        yield return new WaitForSeconds(0.8f);
      collision.transform.position = playerSpawn.position;
      }







}
