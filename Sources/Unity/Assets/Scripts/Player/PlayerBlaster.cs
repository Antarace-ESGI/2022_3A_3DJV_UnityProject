using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerBlaster : MonoBehaviour
{
    public bool canShoot = true;

    [FormerlySerializedAs("Canon")] public GameObject canon;
    public GameObject missile;
    public GameObject missileClone;

    public AudioClip BlasterSound;
    public AudioSource audioSource;

    public void Shoot()
    {
        if (canShoot)
        {
            StartCoroutine(ShootMissile());
            audioSource.clip = BlasterSound;
            audioSource.Play();

        }
    }

    private IEnumerator ShootMissile()
    {
        canShoot = false;
        var position = canon.transform.position;
        Vector3 playerPos = new Vector3(position.x, position.y, position.z);

        missileClone = Instantiate(missile, playerPos, canon.transform.rotation * Quaternion.Euler(0f, 0f, 90f));
        yield return new WaitForSeconds(0.20f);
        canShoot = true;
    }
}
