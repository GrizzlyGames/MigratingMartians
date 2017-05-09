using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Store_Manager : MonoBehaviour
{
    public Text healthCostText;
    public Text reloadCostText;
    public Text bulletCostText;
    public Text movementCostText;

    public Text[] bankText;
    public Text purchaseCostText;

    public Image currentStatsImage;
    public Image upgradeStatsImage;

    public GameObject openGO;
    public GameObject closeGO;
    public GameObject notificationGO;

    private Game_Manager game;
    public Player_Manager player;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();        
    }

    public void OpenStore()
    {
        Debug.Log("Open store.");
        openGO.SetActive(true);
        closeGO.SetActive(false);
        notificationGO.SetActive(false);
        healthCostText.text = (game.status.baseCost * (game.status.healthLevel + 1)).ToString("C00");
        reloadCostText.text = (game.status.baseCost * (game.status.reloadLevel + 1)).ToString("C00");
        bulletCostText.text = (game.status.baseCost * (game.status.bulletLevel + 1)).ToString("C00");
        movementCostText.text = (game.status.baseCost * (game.status.movementLevel + 1)).ToString("C00");
        bankText[0].text = "BANK: " + game.status.money.ToString("C00");
    }

    public void SetStore(int i)
    {
        Debug.Log("Set store.");
        openGO.SetActive(false);
        closeGO.SetActive(true);
        notificationGO.SetActive(false);
        switch (i)
        {
            case 0:
                purchaseCostText.text = (game.status.baseCost * (game.status.healthLevel + 1)).ToString("C00");
                currentStatsImage.fillAmount = (float)game.status.healthLevel / 10.0f;
                upgradeStatsImage.fillAmount = (float)(game.status.healthLevel + 1) / 10.0f;
                break;
            case 1:
                purchaseCostText.text = (game.status.baseCost * (game.status.reloadLevel + 1)).ToString("C00");
                currentStatsImage.fillAmount = (float)game.status.reloadLevel / 10.0f;
                upgradeStatsImage.fillAmount = (float)(game.status.reloadLevel + 1) / 10.0f;
                break;
            case 2:
                purchaseCostText.text = (game.status.baseCost * (game.status.bulletLevel + 1)).ToString("C00");
                currentStatsImage.fillAmount = (float)game.status.bulletLevel / 10.0f;
                upgradeStatsImage.fillAmount = (float)(game.status.bulletLevel + 1) / 10.0f;
                break;
            case 3:
                purchaseCostText.text = (game.status.baseCost * (game.status.movementLevel + 1)).ToString("C00");
                currentStatsImage.fillAmount = (float)game.status.movementLevel / 10.0f;
                upgradeStatsImage.fillAmount = (float)(game.status.movementLevel + 1) / 10.0f;
                break;
        }
        bankText[1].text = game.status.money.ToString("C00");
    }
    public void Purchase(int i)
    {
        Debug.Log("Purchase store.");
        switch (i)
        {
            case 0:
                if(game.status.money >= (game.status.baseCost * (game.status.healthLevel + 1)))
                {
                    player.health.maxHealth++;
                    game.status.money -= (game.status.baseCost * (game.status.healthLevel + 1));
                    game.status.healthLevel++;
                    OpenStore();
                }       
                else
                    notificationGO.SetActive(true);
                break;
            case 1:
                if (game.status.money >= (game.status.baseCost * (game.status.reloadLevel + 1)))
                {
                    player.weapon.fireRate -= 0.25f;
                    game.status.money -= (game.status.baseCost * (game.status.reloadLevel + 1));
                    game.status.reloadLevel++;
                    OpenStore();
                }
                else
                    notificationGO.SetActive(true);
                break;
            case 2:
                if (game.status.money >= (game.status.baseCost * (game.status.bulletLevel + 1)))
                {
                    player.weapon.bulletSpeed++;
                    game.status.money -= (game.status.baseCost * (game.status.bulletLevel + 1));
                    game.status.bulletLevel++;
                    OpenStore();
                }
                else
                    notificationGO.SetActive(true);
                break;
            case 3:
                if (game.status.money >= (game.status.baseCost * (game.status.movementLevel + 1)))
                {
                    player.movement.speed += 0.5f;
                    game.status.money -= (game.status.baseCost * (game.status.movementLevel + 1));
                    game.status.movementLevel++;
                    OpenStore();
                }
                else
                    notificationGO.SetActive(true);
                break;
        }        
    }
}
