using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
public class Screen_Manager : MonoBehaviour
{
    public Text HighScoreText;

    public Text bankText;
    public Text cannonLevelText;
    public Text cannonCostText;
    public Text shieldLevelText;
    public Text shieldCostText;
    public Text treadsLevelText;
    public Text treadsCostText;
    public Text armourLevelText;
    public Text armourCostText;
    public Text waveText;


    public GameObject highScorePanel;
    public Text previousHighScoreText;
    public Text newHighScoreText;
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
        cannonLevelText.text = "LVL: " + (player.weapon.upgradeLevel + 1).ToString("N00") + "/10";
        cannonCostText.text = "-" + game.store.cannonCost.ToString("C00");
        shieldLevelText.text = "LVL: " + (player.shield.upgradeLevel + 1).ToString("N00") + "/10";
        shieldCostText.text = "-" + game.store.shieldCost.ToString("C00");
        treadsLevelText.text = "LVL: " + (player.movement.upgradeLevel + 1).ToString("N00") + "/10";
        treadsCostText.text = "-" + game.store.treadCost.ToString("C00");
        armourLevelText.text = "LVL: " + (player.armour.upgradeLevel + 1).ToString("N00") + "/10";
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
        switch (screenIndex)
        {
            case 0: // Splash
                player.SetVariables();
                game.statistics.wave = 1;
                game.statistics.money = 0;
                break;
            case 1: // Home    
                break;
            case 2: // Score          
                break;
            case 3: // Game-Menu                
                foreach (Transform child in game.trashCollocter)
                {
                    Destroy(child.gameObject);
                }
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
                {
                    StartCoroutine(game.WaveStart());
                }
                else
                    ScreenChanger(5);
                break;
            case 5: // Game Over
                if (Advertisement.IsReady("rewardedVideo"))
                {
                    var options = new ShowOptions { resultCallback = HandleShowResult };
                    Advertisement.Show("rewardedVideo", options);
                }
                foreach (Transform child in game.trashCollocter)
                {
                    Destroy(child.gameObject);
                }
                playerBulletsFired.text = game.statistics.playerBulletsFired.ToString();
                playerArmourRepaires.text = game.statistics.playerArmourRepairs.ToString();
                enemyBullet4Fired.text = game.statistics.enemyBulletType4Destroyed.ToString();
                enemyBullet3Fired.text = game.statistics.enemyBulletType3Destroyed.ToString();
                enemyType1Killed.text = game.statistics.enemyType1Killed.ToString();
                enemyType2Killed.text = game.statistics.enemyType2Killed.ToString();
                enemyType3Killed.text = game.statistics.enemyType3Killed.ToString();
                enemyType4Killed.text = game.statistics.enemyType4Killed.ToString();

                playerBulletsFiredTotal.text = "-" + (game.statistics.playerBulletsFired * 500).ToString("C00");
                playerArmourRepairesTotal.text = "-" + (game.statistics.playerArmourRepairs * 2500).ToString("C00");
                enemyBullet4FiredTotal.text = (game.statistics.enemyBulletType4Destroyed * 500).ToString("C00");
                enemyBullet3FiredTotal.text = (game.statistics.enemyBulletType3Destroyed * 1500).ToString("C00");
                enemyType1KilledTotal.text = (game.statistics.enemyType1Killed * 10000).ToString("C00");
                enemyType2KilledTotal.text = (game.statistics.enemyType2Killed * 25000).ToString("C00");
                enemyType3KilledTotal.text = (game.statistics.enemyType3Killed * 25000).ToString("C00");
                enemyType4KilledTotal.text = (game.statistics.enemyType4Killed * 30000).ToString("C00");

                wavesCompleted.text = game.statistics.wave.ToString();
                int subtotal = ((game.statistics.enemyBulletType4Destroyed * 500) + (game.statistics.enemyBulletType3Destroyed * 1500) + (game.statistics.enemyType1Killed * 10000) + (game.statistics.enemyType2Killed * 25000) + (game.statistics.enemyType3Killed * 25000) + (game.statistics.enemyType4Killed * 30000)) - ((game.statistics.playerBulletsFired * 500) + (game.statistics.playerArmourRepairs * 2500));
                subTotal.text = subtotal.ToString("C00");
                int _total = subtotal * game.statistics.wave;
                total.text = _total.ToString("C00");

                if (PlayerPrefs.HasKey("HighScore"))
                {
                    if (_total > PlayerPrefs.GetInt("HighScore"))
                    {
                        highScorePanel.SetActive(true);
                        previousHighScoreText.text = "Prevous High Score\n" + PlayerPrefs.GetInt("HighScore").ToString("C00");
                        newHighScoreText.text = "New High Score\n" + _total.ToString("C00");
                        PlayerPrefs.SetInt("HighScore", _total);
                        HighScoreText.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore").ToString("C00");
                    }
                    else
                    {
                        HighScoreText.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore").ToString("C00");
                        highScorePanel.SetActive(false);
                    }
                }
                else
                {
                    highScorePanel.SetActive(true);
                    previousHighScoreText.text = "Prevous High Score\n" + PlayerPrefs.GetInt("HighScore").ToString("C00");
                    newHighScoreText.text = "New High Score\n" + _total.ToString("C00");
                    PlayerPrefs.SetInt("HighScore", _total);
                    HighScoreText.text = "HIGH SCORE\n" + PlayerPrefs.GetInt("HighScore").ToString("C00");
                }
                break;
        }
    }

    private void HandleShowResult(ShowResult result)
    {
        switch (result)
        {
            case ShowResult.Finished:
                Debug.Log("The ad was successfully shown.");
                //
                // YOUR CODE TO REWARD THE GAMER
                // Give coins etc.
                break;
            case ShowResult.Skipped:
                Debug.Log("The ad was skipped before reaching the end.");
                break;
            case ShowResult.Failed:
                Debug.LogError("The ad failed to be shown.");
                break;
        }
    }
}
