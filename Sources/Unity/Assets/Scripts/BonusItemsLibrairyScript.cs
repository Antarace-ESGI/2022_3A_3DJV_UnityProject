using UnityEngine;

public class BonusItemsLibrairyScript : MonoBehaviour
{
    public void use(int index, GameObject player)
    {
        switch (index)
        {
            case 0:
                Debug.Log("Bomb");
                break;
            case 1:
                Debug.Log("Shield");
                break;
            default:
                Debug.Log("Do nothing and eat a peanut");
                break;
        }
    }
    
    // TODO : Implement bonus item method
    
    void Update()
    {
        
    }
}
