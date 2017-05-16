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

    public Text playerBulletsFired;
    public Text playerArmourRepaires;
    public Text enemyBullet3Fired;
    public Text enemyBullet4Fired;
    public Text enemyType1Killed;
    public Text enemyType2Killed;
    public Text enemyType3Killed;
    public Text enemyType4Killed;

    public Text wavesCompleted;
    public Text subTotal;
    public Text total;

    public Text playerBulletsFiredTotal;
    public Text playerArmourRepairesTotal;
    public Text enemyBullet3FiredTotal;
    public Text enemyBullet4FiredTotal;
    public Text enemyType1KilledTotal;
    public Text enemyType2KilledTotal;
    public Text enemyType3KilledTotal;
    public Text enemyType4KilledTotal;

    public GameObject notificationGO;

    public int screenIndex;
    public GameObject[] sceenGO;

    public Player_Manager player;
    private Game_Manager game;
    public Audio_Manager _audio;

    private void Start()
    {
        game = GetComponent<Game_Manager>();
        ScreenChanger(screenIndex);
    }

    public void SetStore()
    {
        Debug.Log("Store prices updated.");
        bankText.text = game.statistics.money.ToString("C00");
        cannonCostText.text = "-" + game.store.cannonCost.ToString("C00");
        shieldCostText.text = "-" + game.store.shieldCost.ToString("C00");
        treadsCostText.text = "-" + game.store.treadCost.ToString("C00");
        armourCostText.text = "-" + game.store.armourCost.ToString("C00");
        waveText.text = "WAVE " + game.statistics.wave.ToString();
    }
    public void ScreenChanger(int index)
    {
        screenIndex = index;
        _audio.ChangeSceneMusic(index);
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
                if (player.armour.isAlive)
                {
                    player.shield.shieldTotal = 1;
                    SetStore();
                }                    
                else
                    ScreenChanger(5);
                break;
            case 4: // Gameplay
                if (player.armour.isAlive)
                    StartCoroutine(game.WaveStart());
                else
                    ScreenChanger(5);                
                break;
            case 5: // Game Over
                playerBulletsFired.text = game.statistics.playerBulletsFired.ToString();
                playerArmourRepaires.text = game.statistics.playerArmourRepairs.ToString();
                enemyBullet4Fired.text = game.statistics.enemyBulletType4Destroyed.ToString();
                enemyBullet3Fired.text = game.statistics.enemyBulletType3Destroyed.ToString();
                enemyType1Killed.text = game.statistics.enemyType1Killed.ToString();
                enemyType2Killed.text = game.statistics.enemyType2Killed.ToString();
                enemyType3Killed.text = game.statistics.enemyType3Killed.ToString();
                enemyType4Killed.text = game.statistics.enemyType4Killed.ToString();

                playerBulletsFiredTotal.text = "-"+(game.statistics.playerBulletsFired * 500).ToString("C00");
                playerArmourRepairesTotal.text = "-"+ (game.statistics.playerArmourRepairs * 2500).ToString("C00");
                enemyBullet4FiredTotal.text = (game.statistics.enemyBulletType4Destroyed * 500).ToString("C00");
                enemyBullet3FiredTotal.text = (game.statistics.enemyBulletType3Destroyed * 1500).ToString("C00");
                enemyType1KilledTotal.text = (game.statistics.enemyType1Killed * 10000).ToString("C00");
                enemyType2KilledTotal.text = (game.statistics.enemyType2Killed * 25000).ToString("C00");
                enemyType3KilledTotal.text = (game.statistics.enemyType3Killed * 25000).ToString("C00");
                enemyType4KilledTotal.text = (game.statistics.enemyType4Killed * 30000).ToString("C00");

                wavesCompleted.text = game.statistics.wave.ToString();
                int subtotal = ((game.statistics.enemyBulletType4Destroyed * 500) + (game.statistics.enemyBulletType3Destroyed * 1500) + (game.statistics.enemyType1Killed * 10000) + (game.statistics.enemyType2Killed * 25000) + (game.statistics.enemyType3Killed * 25000) + (game.statistics.enemyType4Killed * 30000)) - ((game.statistics.playerBulletsFired * 500) + (game.statistics.playerArmourRepairs * 2500));
                subTotal.text = subtotal.ToString("C00");
                total.text = (subtotal * game.statistics.wave).ToString("C00");
                break;
        }
    }
}
