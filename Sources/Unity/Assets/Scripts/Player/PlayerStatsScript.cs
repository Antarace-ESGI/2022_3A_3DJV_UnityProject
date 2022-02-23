using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsScript : MonoBehaviour
{
    
    // Player global stats

    public int healthPoint = 10;
    public Slider lifebar;
    
    // Bonus 
    
    public bool haveBonus = false;
    public int bonusIndex = -1;
    public Image bonus;

    private void Start()
    {
        lifebar.value = healthPoint;
        gameObject.AddComponent<BonusItemsLibrairyScript>();
    }

    public void setBonus(Sprite item = null)
    {
        bonus.GetComponent<Image>().sprite = item;
        if (item != null)
        {
            Color tmpcolor = bonus.GetComponent<Image>().color;
            tmpcolor.a = 1;
            bonus.GetComponent<Image>().color = tmpcolor;
        }
        else
        {
            Color tmpcolor = bonus.GetComponent<Image>().color;
            tmpcolor.a = 0;
            bonus.GetComponent<Image>().color = tmpcolor;
        }
    }
    
    private void unableBonusUse()
    {
        setBonus();
        BonusItemsLibrairyScript librarian = gameObject.GetComponent<BonusItemsLibrairyScript>();
        librarian.use(bonusIndex, gameObject);
        haveBonus = false;
        bonusIndex = -1;
    }

    private void updateLifeBar()
    {
        lifebar.value = healthPoint;
    }
    
    public void generateLoot(Vector3 spawnPos)
    {
        GameObject loot = GameObject.CreatePrimitive(PrimitiveType.Cube);
        loot.name = "loot";
        loot.GetComponent<BoxCollider>().isTrigger = true;
        loot.AddComponent<Rigidbody>().useGravity = false;
        loot.transform.localPosition = new Vector3(0.25f,0.25f,0.25f);
        loot.transform.position = new Vector3(spawnPos.x,spawnPos.y,spawnPos.z);
        loot.AddComponent<LootBonusScript>();
        loot.GetComponent<LootBonusScript>().itemIndex = bonusIndex;
        loot.GetComponent<LootBonusScript>().bonusImage = bonus.sprite;
    }

    private void Update()
    {
        if ((haveBonus && Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Joystick1Button7)) && bonus.IsActive() && bonusIndex >= 0)
        {
            unableBonusUse();
        }

        if (healthPoint != lifebar.value)
        {
            updateLifeBar();
        }
        
        if (healthPoint == 0)
        {
            if (haveBonus)
            {
                generateLoot(transform.position);
                haveBonus = false;
                setBonus();
            }
            gameObject.transform.position = new Vector3(0, 0, 0); // temporary checkpoint for test
            healthPoint = 10;
        }
        
    }
}
