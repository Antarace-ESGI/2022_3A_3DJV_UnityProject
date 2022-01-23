using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootBonusScript : MonoBehaviour
{

    public void generateLoot(Vector3 spawnPos)
    {
        GameObject loot = GameObject.CreatePrimitive(PrimitiveType.Cube);
        loot.name = "loot";
        loot.GetComponent<BoxCollider>().isTrigger = true;
        loot.transform.localPosition = new Vector3(0.25f,0.25f,0.25f);
        loot.transform.position = new Vector3(spawnPos.x,spawnPos.y,spawnPos.z);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
