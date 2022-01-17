using UnityEngine;
using Random = UnityEngine.Random;

public class BonusScript : MonoBehaviour
{

    public Vector3 originalPosition;
    public Sprite[] items;
    
    private Vector3 storagePosition;
    private float timer = 5.0f;
    
    void Start()
    {
        //Modify if you want
        storagePosition = new Vector3(0.0f, -500.0f, 0.0f);
    }

    private int giveBonusItem()
    {
        float n = Random.Range(0.0f, 1.0f);
        int nb = Mathf.RoundToInt(n);

        return nb;

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 6)
        {
            gameObject.transform.position = storagePosition;
            GameObject player = col.gameObject;
            if (player.GetComponentInParent<PlayerStatsScript>().haveBonus == false)
            {
                int bonusIndex = giveBonusItem();
                col.gameObject.GetComponentInParent<PlayerStatsScript>().haveBonus = true;
                col.gameObject.GetComponentInParent<PlayerStatsScript>().bonusIndex = bonusIndex;
                col.gameObject.GetComponentInParent<PlayerStatsScript>().setBonus(items[bonusIndex]);
            }
        }
    }
    

    void Update()
    {
        if (gameObject.transform.position == storagePosition)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                gameObject.transform.position = originalPosition;
                timer = 5.0f;
            }
        }    
    }
}
