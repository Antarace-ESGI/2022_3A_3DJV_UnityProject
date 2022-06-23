using UnityEngine;
using Random = UnityEngine.Random;

public class BonusScript : MonoBehaviour
{

    [SerializeField] private Vector3 originalPosition;

    #if UNITY_EDITOR
    [SerializeField] private bool cheatWithRandom;
    [SerializeField] private int cheatInteger = 0;
    #endif
    
    private GameObject _gameManager;
    private Vector3 storagePosition;
    private float timer = 5.0f;
    
    void Start()
    {
        _gameManager = _gameManager = GameObject.FindWithTag("GameController");
        storagePosition = new Vector3(0.0f, -500.0f, 0.0f);
    }

    private int giveBonusItem()
    {
        float n = Random.Range(0.0f, 1.0f);
        int nb = Mathf.RoundToInt(n);

        #if UNITY_EDITOR
        if (cheatWithRandom)
            nb = cheatInteger;
        #endif
        
        return nb;

    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("Player") && _gameManager)
        {
            gameObject.transform.position = storagePosition;
            GameObject player = col.gameObject;
            if (player.GetComponent<PlayerStatsScript>().haveBonus == false)
            {
                PlayerStatsScript stats = player.GetComponent<PlayerStatsScript>();
                int bonusIndex = giveBonusItem();
                stats.haveBonus = true;
                stats.bonusIndex = bonusIndex;
                stats.setBonus(_gameManager.GetComponent<BonusItemsLibrairyScript>().GetImage(bonusIndex));
            }
        }
    }
    

    void Update()
    {
        if (gameObject.transform.position == storagePosition)
        {
            timer -= Time.deltaTime;
            if (timer <= 0.0f)
            {
                gameObject.transform.position = originalPosition;
                timer = 5.0f;
            }
        }    
    }
}
