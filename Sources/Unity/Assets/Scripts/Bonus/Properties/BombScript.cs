using System.Collections;
using UnityEngine;

public class BombScript : MonoBehaviour
{

    [SerializeField] private float timer = 1.0f;
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private int damage = 50;
    [SerializeField] private GameObject explosionArea;

    public AudioClip ExplosionSound;
    public AudioSource audioSource;

    private GameObject _user;

    public void SetUser(GameObject user)
    {
        _user = user;
    }

    void Start()
    {
        float time = timer;

        gameObject.transform.position = _user.transform.position;

        StartCoroutine(PrintArea(time - 2.0f));
        StartCoroutine(Explode(time));
    }

    IEnumerator PrintArea(float time)
    {
        yield return new WaitForSeconds(time);

        explosionArea.transform.localScale = new Vector3(radius, radius, radius);
        explosionArea.SetActive(true);

    }

    IEnumerator Explode(float time)
    {
        yield return new WaitForSeconds(time);

        var center = _user.transform.position;

        Collider[] colliders = Physics.OverlapSphere(center, radius);
        audioSource.clip = ExplosionSound;
        audioSource.Play();
        foreach (Collider collider in colliders)
        {
            if(collider.CompareTag("Player") || collider.CompareTag("AI"))
                collider.gameObject.GetComponent<PlayerStatsScript>().healthPoint -= damage;
        }
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);

    }
}
