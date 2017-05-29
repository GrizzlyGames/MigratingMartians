using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game_Manager : MonoBehaviour
{
    public Transform trashCollocter;
    public float difficultyMultiplier = 0.05f;
    public GameObject topPanelGO;
    public Text notificationText;

    public Player_Manager player;
    public Screen_Manager screenManager;

    public Statistics statistics = new Statistics();
    public class Statistics
    {
        public bool waveComplete;
        public bool waveCompletePending;
        public bool gameStarted;
        public float waveTime;
        public int waveDuration;
        public int wave;
        public int money;
        public int gameScore;

        public int playerBulletsFired = 0;
        public int playerSpecialBulletsFired = 0;
        public int playerArmourRepairs = 0;
        public int enemyBulletType3Destroyed = 0;
        public int enemyBulletType4Destroyed = 0;
        public int enemyType1Killed = 0;
        public int enemyType2Killed = 0;
        public int enemyType3Killed = 0;
        public int enemyType4Killed = 0;

        public void BankDeposit(int amt)
        {
            money += amt;
            //Debug.Log("Bank Deposit: " + amt + "\n" + "Bank Total: " + money);
        }
        public void BankWithdrawal(int amt)
        {
            money -= amt;
            Debug.Log("Bank Withdrawal: -" + amt + "\n" +
                      "Bank Total: " + money);
        }
    }

    public Store store = new Store();
    public class Store
    {
        public int shieldCost = 250000;
        public int treadCost = 25000;
        public int cannonCost = 1250000;
        public int armourCost = 75000;
    }

    public bool resetPrefs = true;
    public GameObject[] Enemies;

    private void Start()
    {
        if (resetPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
    }

    public void SpawnEnemy()
    {
        Transform trans = GameObject.FindGameObjectWithTag("Player").transform;
        int rnd = Random.Range(0, Enemies.Length);
        Vector3 screenVec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
        GameObject go = Instantiate(Enemies[rnd], new Vector3(Random.Range((float)-screenVec.x, (float)screenVec.x), (float)screenVec.y, 0), Quaternion.identity) as GameObject;
        go.transform.SetParent(trashCollocter.transform);
        switch (rnd)
        {
            case 0:
                go.GetComponent<Enemy_AI>().maxHealth = 1;
                go.GetComponent<Enemy_AI>().flySpeed = 1.5f + (statistics.wave * (difficultyMultiplier * 2));
                break;
            case 1:
                go.GetComponent<Enemy_AI>().maxHealth = 2;
                go.GetComponent<Enemy_AI>().flySpeed = 0.75f + (statistics.wave * difficultyMultiplier);
                go.GetComponent<Enemy_AI>().fireRate = 7 - (statistics.wave * difficultyMultiplier);
                go.GetComponent<Enemy_AI>().bulletSpeed = 1 + (statistics.wave * difficultyMultiplier);
                go.GetComponent<Enemy_AI>().shootTime = 5;
                break;
            case 2:
                go.GetComponent<Enemy_AI>().maxHealth = 2;
                go.GetComponent<Enemy_AI>().flySpeed = 0.75f + (statistics.wave * difficultyMultiplier);
                go.GetComponent<Enemy_AI>().fireRate = 5 - (statistics.wave * difficultyMultiplier);
                go.GetComponent<Enemy_AI>().bulletSpeed = 0.75f + (statistics.wave * difficultyMultiplier);
                go.GetComponent<Enemy_AI>().shootTime = 3;
                break;
            case 3:
                go.GetComponent<Enemy_AI>().maxHealth = 3;
                go.GetComponent<Enemy_AI>().flySpeed = 0.5f + (statistics.wave * difficultyMultiplier);
                go.GetComponent<Enemy_AI>().fireRate = 3 - (statistics.wave * difficultyMultiplier);
                go.GetComponent<Enemy_AI>().bulletSpeed = 0.5f + (statistics.wave * difficultyMultiplier);
                go.GetComponent<Enemy_AI>().shootTime = 1.5f;
                break;
        }
    }

    public IEnumerator WaveStart()
    {   
        topPanelGO.SetActive(true);
        notificationText.text = "WAVE " + statistics.wave.ToString();
        yield return new WaitForSeconds(0.01f);
        player.WaveReset();
        yield return new WaitForSeconds(1);
        notificationText.text = "3";
        yield return new WaitForSeconds(1);
        notificationText.text = "2";
        yield return new WaitForSeconds(1);
        notificationText.text = "1";
        yield return new WaitForSeconds(1);
        notificationText.text = "FIGHT!";
        yield return new WaitForSeconds(1);
        topPanelGO.SetActive(false);
        statistics.waveComplete = false;
        statistics.waveCompletePending = false;


        if (statistics.wave < 4)
            statistics.waveDuration = 15;
        else if (statistics.wave >= 4 && statistics.wave < 7)
            statistics.waveDuration = 30;
        else if (statistics.wave >= 7 && statistics.wave < 10)
            statistics.waveDuration = 45;
        else
            statistics.waveDuration = 60;

        statistics.gameStarted = true;
    }
    public IEnumerator WaveCompleteDelay()
    {
        statistics.gameStarted = false;
        statistics.waveComplete = true;
        topPanelGO.SetActive(true);
        notificationText.text = "WAVE COMPLETE!";
        yield return new WaitForSeconds(3);
        screenManager.screenIndex = 1;
        statistics.wave++;
        screenManager.ScreenChanger(3);
    }
}
