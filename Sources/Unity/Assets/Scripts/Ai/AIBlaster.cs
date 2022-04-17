using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIBlaster : MonoBehaviour
{
  public bool canShoot = true;
  public GameObject AICanon;
  public GameObject AImissile;
  public GameObject AImissileClone;
  public Transform canonTransform;

    // Update is called once per frame
    private void Update()
    {
      RaycastHit hit;

      Debug.DrawRay(canonTransform.position, canonTransform.forward*10, Color.red);

      if (Physics.Raycast(canonTransform.position, canonTransform.forward, out hit, 10))
      {
        if(hit.transform.gameObject.tag == "Player" || hit.transform.gameObject.tag == "AI" ){

          Debug.Log(hit.transform.name);

          if (canShoot) {
            StartCoroutine(EnemyBlaster());
          }
        }

        //IL Y A DES CENTAINES DE DEBUgS A CAUSE DES CHECKPOINTS
      }

    }


    public IEnumerator  EnemyBlaster()
      {
        if (Random.Range(0, 15) <1)
          {
            canShoot = false;
            Vector3 AIPos =  new Vector3(AICanon.transform.position.x, AICanon.transform.position.y , AICanon.transform.position.z);
            AImissileClone = Instantiate(AImissile, AIPos, AICanon.transform.rotation * Quaternion.Euler(0f,0f,90f)) as GameObject;
            yield return new WaitForSeconds(0.5f);
            canShoot = true;

          }
      }



}
