using UnityEngine;
using UnityEngine.UI;

public class PlayerStatsScript : MonoBehaviour
{
    // Bonus 
    public bool haveBonus = false;
    public int bonusIndex;
    public Image bonus;

    private void Start()
    {
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
    }

    private void Update()
    {
        if ((haveBonus && Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Joystick1Button7)) && bonus.IsActive())
        {
            unableBonusUse();
        }
    }
}
