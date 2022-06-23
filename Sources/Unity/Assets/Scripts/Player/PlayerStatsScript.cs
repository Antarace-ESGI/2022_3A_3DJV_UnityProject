using System;
using Checkpoints;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CheckpointController))]
public class PlayerStatsScript : MonoBehaviour
{
    // Player global stats

    public int healthPoint = 50;
    public Slider lifebar;

    // Bonus

    private GameObject _gameManager;

    public bool haveBonus;
    public int bonusIndex = -1;
    public Image bonus;
    
    private CheckpointController _checkpointController;

    private void Start()
    {
        lifebar.value = healthPoint;
        _gameManager = GameObject.FindWithTag("GameController");
        _checkpointController = GetComponent<CheckpointController>();
    }
    
    // Main

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

    public void unableBonusUse()
    {
        if (bonus.IsActive() && bonusIndex >= 0)
        {
            setBonus();
            if (_gameManager)
            {
                _gameManager.GetComponent<BonusItemsLibrairyScript>().use(bonusIndex, gameObject);
                haveBonus = false;
                bonusIndex = -1;
            }
        }
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
        loot.transform.localPosition = new Vector3(0.25f, 0.25f, 0.25f);
        loot.transform.position = new Vector3(spawnPos.x, spawnPos.y, spawnPos.z);
        var lootBonusScript = loot.AddComponent<LootBonusScript>();
        lootBonusScript.itemIndex = bonusIndex;
        lootBonusScript.bonusImage = bonus.sprite;
    }

    private void Update()
    {
        if (Math.Abs(healthPoint - lifebar.value) > .1)
        {
            updateLifeBar();
        }

        if (healthPoint <= 0)
        {
            if (haveBonus)
            {
                generateLoot(transform.position);
                haveBonus = false;
                setBonus();
            }

            _checkpointController.RespawnEntity();
            healthPoint = 50;
        }
    }
}