using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Screen_Manager : MonoBehaviour
{
    // Store
    public Text bankText;
    public Text turretLevelText;
    public Text turretCostText;
    public Text shieldLevelText;
    public Text shieldCostText;
    public Text treadLevelText;
    public Text treadCostText;
    public Text armourLevelText;
    public Text armourCostText;
    public Text waveText;

    // Game-Over
    public GameObject deathDetails;
    public GameObject highScorePanel;

    // Score 
    public Text previousHighScoreText;
    public Text previousWaves;

    // Details
    public Text newHighScoreText;
    public Text newWaveText;

    public Text redBulletsQtyText;
    public Text blueBulletsQtyText;
    public Text pinkBulletsQtyText;
    public Text enemyType1Killed;
    public Text enemyType2Killed;
    public Text enemyType3Killed;
    public Text enemyType4Killed;

    public Text totalScore;

    public Text redBulletsTotalText;
    public Text blueBulletsTotalText;
    public Text pinkBulletsTotalText;
    public Text enemyType1KilledTotal;
    public Text enemyType2KilledTotal;
    public Text enemyType3KilledTotal;
    public Text enemyType4KilledTotal;

    public Text highWave;
    public Text highScore;
    public Text lastScore;
    public Text lastWave;

    public GameObject notificationGO;

    public Image playerTreadsImage;
    public SpriteRenderer playerTreadsRenderer;
    public Sprite[] playerTreadsSprite;

    public SpriteRenderer playerArmourRenderer;
    public Image playerArmourImage;
    public Sprite[] playerArmourSprite;

    public SpriteRenderer playerShieldRenderer;
    public Image playerShieldImage;
    public Sprite[] playerShieldSprite;

    public SpriteRenderer playerTurretRenderer;
    public Image playerTurretImage;
    public Sprite[] playerTurretSprite;

    public int screenIndex;
    public GameObject[] sceenGO;

    public Player_Manager player;
    private Game_Manager game;
    public Audio_Manager _audio;

    private void Start()
    {
        ConnectToGoogleServices();
        game = GetComponent<Game_Manager>();
        ScreenChanger(screenIndex);
    }
    private float spawnTimer;
    private float rndDelay;
    private void Update()
    {
        if (screenIndex == 3)
        {
            if (game.statistics.gameStarted)
            {
                if (!game.statistics.waveComplete) // Game running
                {
                    if (game.statistics.waveCompletePending) // Waiting to exit
                    {
                        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 1 && GameObject.FindGameObjectsWithTag("EnemyBullet").Length < 1)
                        {
                            if (player.armour.isAlive)
                            {
                                game.statistics.waveComplete = true;
                                StartCoroutine(game.WaveCompleteDelay());
                            }
                        }
                    }
                    else // GAME RUNNING  Wave is not complete and not pending completion
                    {
                        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 1)
                        {
                            game.SpawnEnemy();
                        }

                        game.statistics.waveTime += Time.deltaTime;


                        if (game.statistics.waveTime >= game.statistics.waveDuration)
                        {
                            game.statistics.waveCompletePending = true;
                        }
                        else
                        {
                            spawnTimer += Time.deltaTime;
                            if (spawnTimer > rndDelay)
                            {
                                spawnTimer = 0;
                                int rndAmt = 1;

                                switch (game.statistics.wave)
                                {
                                    case 1:
                                        rndDelay = Random.Range(3, 7);
                                        rndAmt = 1;
                                        break;
                                    case 2:
                                        rndDelay = Random.Range(4, 8);
                                        rndAmt = Random.Range(1, 3);
                                        break;
                                    case 3:
                                        rndDelay = Random.Range(5, 9);
                                        rndAmt = Random.Range(1, 4);
                                        break;
                                    case 4:
                                        rndDelay = Random.Range(6, 10);
                                        rndAmt = Random.Range(2, 5);
                                        break;
                                    case 5:
                                        rndDelay = Random.Range(7, 11);
                                        rndAmt = Random.Range(2, 6);
                                        break;
                                    case 6:
                                        rndDelay = Random.Range(8, 12);
                                        rndAmt = Random.Range(2, 7);
                                        break;
                                    case 7:
                                        rndDelay = Random.Range(9, 13);
                                        rndAmt = Random.Range(3, 8);
                                        break;
                                    case 8:
                                        rndDelay = Random.Range(10, 14);
                                        rndAmt = Random.Range(3, 9);
                                        break;
                                    case 9:
                                        rndDelay = Random.Range(11, 15);
                                        rndAmt = Random.Range(3, 10);
                                        break;
                                    case 10:
                                        rndDelay = Random.Range(12, 16);
                                        rndAmt = Random.Range(4, 11);
                                        break;
                                    default:
                                        rndDelay = Random.Range(13, 17);
                                        rndAmt = Random.Range(4, 12);
                                        Debug.Log("Random spawn amt: " + rndAmt);
                                        break;
                                }

                                for (int i = 0; i < rndAmt; i++)
                                    StartCoroutine(SpawnEnemyDelay());
                            }
                        }
                    }
                }
            }
        }
    }

    public void SetStore()
    {
        bankText.text = game.statistics.money.ToString("C00");
        if (player.weapon.upgradeLevel < 5)
        {
            turretLevelText.text = "LVL: " + (player.weapon.upgradeLevel + 1).ToString("N00") + "/5";
            playerTurretImage.sprite = playerTurretSprite[player.weapon.upgradeLevel + 1];
            turretCostText.text = "-" + game.store.cannonCost.ToString("C00");
        }
        else
        {
            turretCostText.text = "SOLD";
        }

        if (player.shield.upgradeLevel < 5)
        {
            shieldLevelText.text = "LVL: " + (player.shield.upgradeLevel + 1).ToString("N00") + "/5";
            playerShieldImage.sprite = playerShieldSprite[player.shield.upgradeLevel + 1];
            shieldCostText.text = "-" + game.store.shieldCost.ToString("C00");
        }
        else
        {
            shieldCostText.text = "SOLD";
        }
        if (player.movement.upgradeLevel < 5)
        {
            treadLevelText.text = "LVL: " + (player.movement.upgradeLevel + 1).ToString("N00") + "/5";
            playerTreadsImage.sprite = playerTreadsSprite[player.movement.upgradeLevel + 1];
            treadCostText.text = "-" + game.store.treadCost.ToString("C00");
        }
        else
        {
            treadCostText.text = "SOLD";
        }

        if (player.armour.upgradeLevel < 5)
        {
            armourLevelText.text = "LVL: " + (player.armour.upgradeLevel + 1).ToString("N00") + "/5";
            playerArmourImage.sprite = playerArmourSprite[player.armour.upgradeLevel + 1];
            armourCostText.text = "-" + game.store.armourCost.ToString("C00");
        }
        else
        {
            armourCostText.text = "SOLD";
        }

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
                player.ResetVariables();
                game.statistics.ResetVariables();
                game.store.ResetPrices();
                break;
            case 1: // Home    
                break;
            case 2: // Game-Menu         
                game.statistics.gameStarted = false;
                foreach (Transform child in game.trashCollocter)
                {
                    Destroy(child.gameObject);
                }
                SetStore();
                break;
            case 3: // Gameplay
                StartCoroutine(game.WaveStart());
                rndDelay = Random.Range(0.5f, 3);
                if (player.weapon.upgradeLevel <= 5)
                    playerTurretRenderer.sprite = playerTurretSprite[player.weapon.upgradeLevel];
                else
                    player.weapon.upgradeLevel = 5;

                if (player.shield.upgradeLevel <= 5)
                {
                    player.shield.shieldSpriteRenderer.sprite = player.shield.shieldSprites[player.shield.upgradeLevel];
                    playerShieldRenderer.sprite = playerShieldSprite[player.shield.upgradeLevel];
                }

                else
                {
                    player.shield.upgradeLevel = 5;
                }


                if (player.movement.upgradeLevel <= 5)
                    playerTreadsRenderer.sprite = playerTreadsSprite[player.movement.upgradeLevel];
                else
                    player.movement.upgradeLevel = 5;

                if (player.armour.upgradeLevel <= 5)
                    playerArmourRenderer.sprite = playerArmourSprite[player.armour.upgradeLevel];
                else
                    player.armour.upgradeLevel = 5;
                break;
            case 4: // Game Over
                game.statistics.gameScore = ((game.statistics.pinkBulletsDestroyed * 1000) + (game.statistics.blueBulletsDestroyed * 2500) + (game.statistics.redBulletsDestroyed * 5000) + (game.statistics.enemyType1Killed * 10000) + (game.statistics.enemyType2Killed * 25000) + (game.statistics.enemyType3Killed * 25000) + (game.statistics.enemyType4Killed * 30000));
                
                foreach (Transform child in game.trashCollocter)
                {
                    Destroy(child.gameObject);
                }

                if (PlayerPrefs.HasKey("HighScore"))
                {
                    if (game.statistics.gameScore > PlayerPrefs.GetInt("HighScore"))
                    {
                        highScorePanel.SetActive(true);
                        deathDetails.SetActive(false);
                        previousHighScoreText.text = "Prevous High Score\n" + PlayerPrefs.GetInt("HighScore").ToString("C00");
                        newHighScoreText.text = "New High Score\n" + game.statistics.gameScore.ToString("C00");
                        PlayerPrefs.SetInt("HighScore", game.statistics.gameScore);
                        PlayerPrefs.SetInt("HighWave", game.statistics.wave);
                    }
                    else
                    {
                        deathDetails.SetActive(true);
                        highScorePanel.SetActive(false);
                    }
                }
                else // Set highscore
                {
                    highScorePanel.SetActive(true);
                    deathDetails.SetActive(false);
                    previousHighScoreText.text = "Prevous High Score\n" + PlayerPrefs.GetInt("HighScore").ToString("C00");
                    newHighScoreText.text = "New High Score\n" + game.statistics.gameScore.ToString("C00");
                    PlayerPrefs.SetInt("HighScore", game.statistics.gameScore);
                    PlayerPrefs.SetInt("HighWave", game.statistics.wave);
                }

                pinkBulletsQtyText.text = game.statistics.pinkBulletsDestroyed.ToString("N000");
                blueBulletsQtyText.text = game.statistics.blueBulletsDestroyed.ToString("N000");
                redBulletsQtyText.text = game.statistics.redBulletsDestroyed.ToString("N000");
                enemyType1Killed.text = game.statistics.enemyType1Killed.ToString("N000");
                enemyType2Killed.text = game.statistics.enemyType2Killed.ToString("N000");
                enemyType3Killed.text = game.statistics.enemyType3Killed.ToString("N000");
                enemyType4Killed.text = game.statistics.enemyType4Killed.ToString("N000");

                pinkBulletsTotalText.text = (game.statistics.pinkBulletsDestroyed * 1000).ToString("C00");
                blueBulletsTotalText.text = (game.statistics.blueBulletsDestroyed * 2500).ToString("C00");
                redBulletsTotalText.text = (game.statistics.redBulletsDestroyed * 5000).ToString("C00");
                enemyType1KilledTotal.text = (game.statistics.enemyType1Killed * 10000).ToString("C00");
                enemyType2KilledTotal.text = (game.statistics.enemyType2Killed * 25000).ToString("C00");
                enemyType3KilledTotal.text = (game.statistics.enemyType3Killed * 25000).ToString("C00");
                enemyType4KilledTotal.text = (game.statistics.enemyType4Killed * 30000).ToString("C00");

                // Updates scores page
                lastScore.text = "SCORE\n" + game.statistics.gameScore.ToString("C00");
                lastWave.text = "WAVE\n" + game.statistics.wave.ToString("N00");

                // death details total score
                totalScore.text = game.statistics.gameScore.ToString("C00");
                break;
            case 5: // Score
                highScore.text = "SCORE\n" + PlayerPrefs.GetInt("HighScore").ToString("C00");
                highWave.text = "WAVE\n" + PlayerPrefs.GetInt("HighWave").ToString("N00");
                break;
        }
    }
    private IEnumerator SpawnEnemyDelay()
    {
        float rndTime = Random.Range(0.5f, 2);
        yield return new WaitForSeconds(rndTime);
        if (GameObject.FindGameObjectsWithTag("Enemy").Length < 21)
        {
            if (!game.statistics.waveCompletePending)
                game.SpawnEnemy();
        }
        else
            Debug.Log("Too many enemies.");
    }

    private bool isConnectedToGoogleServices;

    public bool ConnectToGoogleServices()
    {
        if (!isConnectedToGoogleServices)
            Social.localUser.Authenticate((bool success) =>
            {
                isConnectedToGoogleServices = success;
            }) ;
        return isConnectedToGoogleServices;
    }
}
