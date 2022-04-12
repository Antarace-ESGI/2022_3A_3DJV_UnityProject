using System.Collections;
using UnityEngine;

public class BombScript : MonoBehaviour
{

    [SerializeField] private float timer = 5.0f;
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private int damage = 5;
    [SerializeField] private GameObject explosionArea;

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

        Collider[] colliders = Physics.OverlapSphere(center, radius,LayerMask.NameToLayer("Player"));

        foreach (Collider collider in colliders)
        {
            collider.gameObject.GetComponent<PlayerStatsScript>().healthPoint -= damage;
        }
        
        Destroy(gameObject);
        
    }
}
