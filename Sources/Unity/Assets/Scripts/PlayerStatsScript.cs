using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsScript : MonoBehaviour
{
    
    // Player global stats

    public int healthPoint = 10;
    public Slider lifebar;
    
    // Bonus 
    
    public bool haveBonus = false;
    public int bonusIndex;
    public Image bonus;

    private void Start()
    {
        lifebar.value = healthPoint;
        gameObject.AddComponent<BonusItemsLibrairyScript>();
        gameObject.AddComponent<LootBonusScript>();
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
    }

    private void updateLifeBar()
    {
        lifebar.value = healthPoint;
    }

    private void Update()
    {
        if ((haveBonus && Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Joystick1Button7)) && bonus.IsActive())
        {
            unableBonusUse();
        }

        if (healthPoint != lifebar.value)
        {
            updateLifeBar();
        }
        
        if (healthPoint == 0)
        {
            LootBonusScript loot = gameObject.GetComponent<LootBonusScript>();
            loot.generateLoot(gameObject.transform.position);
            gameObject.transform.position = new Vector3(0, 0, 0); // temporary checkpoint for test
            healthPoint = 10; 
        }
        
    }
}
