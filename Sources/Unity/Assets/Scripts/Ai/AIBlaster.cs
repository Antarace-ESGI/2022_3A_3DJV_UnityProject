using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class AIBlaster : MonoBehaviour
{
    public bool canShoot = true;
    [FormerlySerializedAs("AICanon")] public GameObject aiCanon;
    [FormerlySerializedAs("AImissile")] public GameObject aiMissile;
    [FormerlySerializedAs("AImissileClone")] public GameObject aiMissileClone;
    public Transform canonTransform;

    // Update is called once per frame
    private void Update()
    {
        RaycastHit hit;
        Debug.DrawRay(canonTransform.position, canonTransform.forward * 10, Color.red);

        if (Physics.Raycast(canonTransform.position, canonTransform.forward, out hit, 10))
        {
            if (hit.transform.gameObject.CompareTag("Player") || hit.transform.gameObject.CompareTag("AI"))
            {
                if (canShoot)
                {
                    StartCoroutine(EnemyBlaster());
                }
            }

            //IL Y A DES CENTAINES DE DEBUgS A CAUSE DES CHECKPOINTS
        }
    }


    private IEnumerator EnemyBlaster()
    {
        if (Random.Range(0, 15) < 1)
        {
            canShoot = false;
            var position = aiCanon.transform.position;
            Vector3 aiPos = new Vector3(position.x, position.y, position.z);
            aiMissileClone = Instantiate(aiMissile, aiPos, aiCanon.transform.rotation * Quaternion.Euler(0f, 0f, 90f));
            yield return new WaitForSeconds(0.5f);
            canShoot = true;
        }
    }
}