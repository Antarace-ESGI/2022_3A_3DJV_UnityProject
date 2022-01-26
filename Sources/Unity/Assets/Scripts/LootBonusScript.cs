using System;
using UnityEngine;
using UnityEngine.UI;

public class LootBonusScript : MonoBehaviour
{

    public int itemIndex;
    public Sprite bonusImage;
    
    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.layer == 6)
        {
            GameObject player = col.gameObject;
            if (player.GetComponentInParent<PlayerStatsScript>().haveBonus == false)
            {
                col.gameObject.GetComponentInParent<PlayerStatsScript>().haveBonus = true;
                col.gameObject.GetComponentInParent<PlayerStatsScript>().bonusIndex = itemIndex;
                col.gameObject.GetComponentInParent<PlayerStatsScript>().setBonus(bonusImage);
                Destroy(gameObject);
            }
        }
    }
    
}
