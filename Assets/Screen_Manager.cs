using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Screen_Manager : MonoBehaviour {

    public Text bankText;
    public Text cannonCostText;
    public Text shieldCostText;
    public Text treadsCostText;
    public Text armourCostText;

    public GameObject notificationGO;

    public int screenIndex;
    public GameObject[] sceenGO;

    public Player_Manager player;    
    private Game_Manager game;

    private void Start () {
        game = GetComponent<Game_Manager>();
        ScreenChanger(3);
    }
    private void ClearScreen()
    {
        foreach (GameObject element in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(element);
        }
        foreach (GameObject element in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(element);
        }
        foreach (GameObject element in GameObject.FindGameObjectsWithTag("Health"))
        {
            Destroy(element);
        }
    }

    public void OpenStore()
    {
        Debug.Log("Open store.");
        notificationGO.SetActive(false);
        cannonCostText.text = "-" + game.store.cannonCost.ToString("C00");
        shieldCostText.text = "-" + game.store.shieldCost.ToString("C00");
        treadsCostText.text = "-" + game.store.treadCost.ToString("C00");
        armourCostText.text = "-" + game.store.armourCost.ToString("C00");
        bankText.text = "BANK: " + game.statistics.money.ToString("C00");
    }

    public void ScreenChanger(int index)
    {
        screenIndex = index;
        ClearScreen(); // Destroys gameplay Gameobjects   

        // Switches Screens
        for (int i = 0; i < sceenGO.Length; i++)
        {
            if (i == screenIndex)
                sceenGO[i].SetActive(true);
            else
                sceenGO[i].SetActive(false);
        }
        

        switch (screenIndex)
        {
            case 0: // Splash
                player.PlayerReset();
                game.statistics.wave = 1;
                game.statistics.money = 0;
                break;
            case 1: // Home
                break;
            case 2: // Score          
                break;
            case 3: // Game-Menu
                OpenStore();
                break;
            case 4: // Gameplay                
                StartCoroutine(game.WaveStart());
                break;
            case 5: // Game Over
                break;
        }
    }
}
