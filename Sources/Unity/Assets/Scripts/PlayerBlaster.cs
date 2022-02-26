using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlaster : MonoBehaviour
{
  public bool canShoot = true;

  public GameObject Canon;
  public GameObject missile;
  public GameObject missileClone;

  void Update()
  {
        if (canShoot) {
          StartCoroutine(shootMissile());
    }
  }



  public IEnumerator shootMissile()
  {
    if(Input.GetKeyDown(KeyCode.Space))
    {
      canShoot = false;
      Vector3 playerPos =  new Vector3(Canon.transform.position.x, Canon.transform.position.y , Canon.transform.position.z);

      missileClone = Instantiate(missile, playerPos, Canon.transform.rotation * Quaternion.Euler(0f,0f,90f)) as GameObject;
      yield return new WaitForSeconds(0.20f);
      canShoot = true;
    }
  }

}
