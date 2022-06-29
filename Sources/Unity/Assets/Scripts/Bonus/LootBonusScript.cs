using System;
using UnityEngine;
using UnityEngine.UI;

public class LootBonusScript : MonoBehaviour
{

    public int itemIndex;
    public Sprite bonusImage;

    private Rigidbody rb;
    
    private void OnEnable()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 6)
        {
            GameObject player = col.gameObject;
            if (player.GetComponentInParent<PlayerStatsScript>().haveBonus == false)
            {
                PlayerStatsScript stats = col.gameObject.GetComponentInParent<PlayerStatsScript>();
                stats.haveBonus = true;
                stats.bonusIndex = itemIndex;
                stats.setBonus(bonusImage);
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        rb.AddTorque(Vector3.up * 0.25f);
    }
}
