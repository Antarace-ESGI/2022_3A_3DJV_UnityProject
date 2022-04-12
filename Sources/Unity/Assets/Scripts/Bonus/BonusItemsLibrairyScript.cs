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
                GameObject bomb = Instantiate(bonus[0]);
                bomb.GetComponent<BombScript>().SetUser(player);
                break;
            case 1:
                Debug.Log("Cloud");
                
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
                
                break;
            default: break;
        }
    }

    public Sprite GetImage(int index)
    {
        return sprites[index];
    }

}
