using UnityEngine;

public class PlayerStatsScript : MonoBehaviour
{
    // Bonus (temporary integration) 
    private bool haveBonus = false;
    //

    private void unableBonusUse()
    {
        Debug.Log("Using a super bonus that work perfectly !(Please)");
        haveBonus = false;
    }

    private void Update()
    {
        if (haveBonus && Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.Joystick1Button7))
        {
            unableBonusUse();
        }
    }
}
