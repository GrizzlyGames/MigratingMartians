using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Script_UpgradeSelection : MonoBehaviour {

    public GameObject CloseGO;

    public int baseCost = 100;
    public int healthLevel = 0;
    public int reloadLevel = 0;
    public int bulletLevel = 0;
    public int movementLevel = 0;

    public Sprite[] HealthSprite;
    public Sprite[] ReloadSprite;
    public Sprite[] BulletSprite;
    public Sprite[] MovementSprite;

    public Image garageImage;
    public Image storeImage;
    public Text garageDescriptionText;
    public Text garageBankText;
    public Text storeDescriptionText;
    public Text storeCostText;

    private Game_Manager game;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
    }

    public void SelectionMade(int index)
    {
        CloseGO.SetActive(false);
        garageBankText.text = game.status.money.ToString("C");
        switch (index)
        {
            case 0:// Health
                garageImage.sprite = HealthSprite[healthLevel];
                storeImage.sprite = HealthSprite[healthLevel+1];
                garageDescriptionText.text = "CURRENT HEALTH";
                garageBankText.text = game.status.money.ToString("C");
                storeDescriptionText.text = "UPGRADE HEALTH";
                storeCostText.text = (baseCost * (healthLevel + 1)).ToString("C");
                break;
            case 1: // Reload
                garageImage.sprite = ReloadSprite[reloadLevel];
                storeImage.sprite = ReloadSprite[reloadLevel + 1];
                garageDescriptionText.text = "CURRENT RELOAD TIME";
                storeDescriptionText.text = "UPGRADE RELOAD TIME";
                storeCostText.text = (baseCost * (reloadLevel + 1)).ToString("C");
                break;
            case 2: // Bullet Speed
                garageImage.sprite = BulletSprite[bulletLevel];
                storeImage.sprite = BulletSprite[bulletLevel + 1];
                garageDescriptionText.text = "CURRENT BULLET VELOCITY";
                storeDescriptionText.text = "UPGRADE BULLET VELOCITY";
                storeCostText.text = (baseCost * (bulletLevel + 1)).ToString("C");
                break;
            case 3: // Movement speed
                garageImage.sprite = MovementSprite[movementLevel];
                storeImage.sprite = MovementSprite[movementLevel + 1];
                garageDescriptionText.text = "CURRENT MOVEMENT SPEED";
                storeDescriptionText.text = "UPGRADE MOVEMENT SPEED";
                storeCostText.text = (baseCost * (movementLevel + 1)).ToString("C");
                break;
        }
    }
}
