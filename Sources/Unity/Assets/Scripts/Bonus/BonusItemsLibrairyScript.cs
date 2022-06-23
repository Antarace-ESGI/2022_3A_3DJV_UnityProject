using System.Collections.Generic;
using UnityEngine;

public class BonusItemsLibrairyScript : MonoBehaviour
{

    [SerializeField] private GameObject[] bonus;
    [SerializeField] private Sprite[] sprites;
    
    public void use(int index, GameObject player)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Bomb");
                SetupBomb(player);
                break;
            case 1:
                Debug.Log("Cloud");
                SetupCloud(player);
                break;
            default: break;
        }
    }

    public Sprite GetImage(int index)
    {
        return sprites[index];
    }
    
    // Bonus setup

    private void SetupCloud(GameObject player)
    {
        GameObject[] ais = GameObject.FindGameObjectsWithTag("AI");
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        
        GameObject[] entities = new GameObject[ais.Length + players.Length];
        ais.CopyTo(entities,0);
        players.CopyTo(entities,ais.Length);
                
        foreach (GameObject obj in entities)
        {
            if (obj.GetInstanceID() != player.GetInstanceID())
            {
                var elem = Instantiate(bonus[1]);
                elem.GetComponent<ElectricDragScript>().SetPlayerTarget(obj);
            }
        }
    }

    private void SetupBomb(GameObject player)
    {
        GameObject bomb = Instantiate(bonus[0]);
        bomb.GetComponent<BombScript>().SetUser(player);
    }
}
