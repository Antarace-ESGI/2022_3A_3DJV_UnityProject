using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlasterBehavior : MonoBehaviour
{

  public float VanishTime = 4;
  public float slowdown = 2000f;

  void Update()
  {
      transform.Translate(new Vector3(0,0,30* Time.deltaTime));

      if(VanishTime > 0){
        VanishTime -= Time.deltaTime;
      }else{
        Destroy(transform.gameObject);
      }

      }

      private void OnTriggerEnter(Collider collision){

        if(collision.CompareTag("AI")){
          //Coroutine degats
          StartCoroutine(AiGetShot(collision));
        }
      }

      private IEnumerator AiGetShot(Collider collision){

        yield return new WaitForSeconds(0.05f);
        collision.GetComponent<Rigidbody>().AddForce(-transform.forward * slowdown, ForceMode.Force);
    //   Rigidbody rb = collision.GetComponent<Rigidbody>();
    //
    // Vector3 slowingDown = new Vector3(-transform.forward.x * rb.velocity.x,-transform.forward.y * rb.velocity.y,-transform.forward.z * rb.velocity.z);
    //
    //   rb.AddForce(slowingDown, ForceMode.Force);

        AiController AI = collision.gameObject.GetComponent<AiController>();
        AI.AiLife = AI.AiLife - 10;
        Debug.Log("get hit");
         Destroy(transform.gameObject);
      }


}
