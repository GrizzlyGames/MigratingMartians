using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Screen_Manager : MonoBehaviour
{

    public Text bankText;
    public Text cannonCostText;
    public Text shieldCostText;
    public Text treadsCostText;
    public Text armourCostText;
    public Text waveText;

    public GameObject notificationGO;

    public int screenIndex;
    public GameObject[] sceenGO;

    public Player_Manager player;
    private Game_Manager game;

    private void Start()
    {
        game = GetComponent<Game_Manager>();
        ScreenChanger(3);
    }

    public void SetStore()
    {
        Debug.Log("Store prices updated.");
        bankText.text = "BANK: " + game.statistics.money.ToString("C00");
        cannonCostText.text = "-" + game.store.cannonCost.ToString("C00");
        shieldCostText.text = "-" + game.store.shieldCost.ToString("C00");
        treadsCostText.text = "-" + game.store.treadCost.ToString("C00");
        armourCostText.text = "-" + game.store.armourCost.ToString("C00");
        waveText.text = "WAVE " + game.statistics.wave.ToString();
    }
    public void ScreenChanger(int index)
    {
        screenIndex = index;
        // Switches Screens
        for (int i = 0; i < sceenGO.Length; i++)
        {
            if (i == screenIndex)
                sceenGO[i].SetActive(true);
            else
                sceenGO[i].SetActive(false);
        }
        foreach (Transform child in game.trashCollocter)
        {
            Destroy(child.gameObject);
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
                    SetStore();
                break;
            case 4: // Gameplay                
                StartCoroutine(game.WaveStart());
                break;
            case 5: // Game Over
                break;
        }
    }
}
