using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class BlasterBehavior : MonoBehaviour
{
    [FormerlySerializedAs("VanishTime")] public float vanishTime = 4;

    void Update()
    {
        transform.Translate(new Vector3(0, 0, 30 * Time.deltaTime));

        if (vanishTime > 0)
        {
            vanishTime -= Time.deltaTime;
        }
        else
        {
            Destroy(transform.gameObject);
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        // TODO: Factoriser ces deux méthodes
        if (collision.CompareTag("AI"))
        {
            //Coroutine degats
            StartCoroutine(AiGetShot(collision));
        }

        if (collision.CompareTag("Player"))
        {
            //Coroutine degats
            StartCoroutine(PlayerGetShot(collision));
        }
    }

    private IEnumerator AiGetShot(Collider collision)
    {
        yield return new WaitForSeconds(0.05f);

        // JE SAIS PLUS CE QU4IL FALLAIT FAIRE POUR LE RALENTI

        //  collision.GetComponent<Rigidbody>().AddForce(-transform.forward *slowdown, ForceMode.Force);

        Rigidbody rb = collision.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        //
        // Vector3 slowingDown = new Vector3(-transform.forward.x * rb.velocity.x,-transform.forward.y * rb.velocity.y,-transform.forward.z * rb.velocity.z);
        //
        //   rb.AddForce(slowingDown, ForceMode.Force);

        AiController ai = collision.gameObject.GetComponent<AiController>();
        ai.aiLife -= 10;
        Destroy(transform.gameObject);
    }


    private IEnumerator PlayerGetShot(Collider collision)
    {
        yield return new WaitForSeconds(0.05f);

        // JE SAIS PLUS CE QU4IL FALLAIT FAIRE POUR LE RALENTI

        //  collision.GetComponent<Rigidbody>().AddForce(-transform.forward *slowdown, ForceMode.Force);

        //   Rigidbody rb = collision.GetComponent<Rigidbody>();
        //
        // Vector3 slowingDown = new Vector3(-transform.forward.x * rb.velocity.x,-transform.forward.y * rb.velocity.y,-transform.forward.z * rb.velocity.z);
        //
        //   rb.AddForce(slowingDown, ForceMode.Force);

        PlayerStatsScript pss = collision.gameObject.GetComponent<PlayerStatsScript>();
        pss.healthPoint -= 10;
        Debug.Log("Player get hit");
        Destroy(transform.gameObject);
    }
}