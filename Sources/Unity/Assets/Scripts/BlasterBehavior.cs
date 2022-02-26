using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBehavior : MonoBehaviour
{

  public float VanishTime = 4;
  void Update()
  {
      transform.Translate(new Vector3(0,0,30* Time.deltaTime));


      if(VanishTime > 0){
        VanishTime -= Time.deltaTime;
        // Debug.Log(VanishTime);
      }else{
        // Debug.Log("BONJO");
        Destroy(transform.gameObject);
      }

      }
}
