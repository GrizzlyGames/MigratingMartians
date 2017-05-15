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
        public bool waveStart = false;
        public int wave = 1;
        public int money = 0;
        public int enemiesSpawned = 0;
        public int enemiesKilled = 0;

        public int playerBulletsFired = 0;
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
            Debug.Log("Bank Deposit: " + amt + "\n" +
                      "Bank Total: " + money);
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
        public int shieldCost = 150000;
        public int treadCost = 25000;
        public int cannonCost = 50000;
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

    public void EnemyKilled()
    {
        statistics.enemiesKilled++;
        if (statistics.enemiesKilled >= statistics.enemiesSpawned)
        {
            statistics.waveStart = false;
            player.weapon.time = player.weapon.fireRate;
            player.weapon.reloadImage.fillAmount = player.weapon.time / player.weapon.fireRate;
            StartCoroutine(WaveCompleteDelay());
        }
    }
    public void SpawnEnemy()
    {
        if (screenManager.screenIndex == 4)
        {
            int rnd = Random.Range(0, Enemies.Length);
            Transform trans = GameObject.FindGameObjectWithTag("Player").transform;
            if (!trans)
                trans = GameObject.Find("Player").transform;

            Vector3 screenVec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

            GameObject go = Instantiate(Enemies[rnd], new Vector3(Random.Range(-screenVec.x, screenVec.x), screenVec.y, 0), Quaternion.identity) as GameObject;
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
            Debug.Log("Enemy Type " + (rnd + 1) + "\n" +
                      "flySpeed: " + go.GetComponent<Enemy_AI>().flySpeed + "\n" +
                      "fireRate: " + go.GetComponent<Enemy_AI>().fireRate + "\n" +
                      "bulletSpeed: " + go.GetComponent<Enemy_AI>().bulletSpeed);
        }
    }

    public IEnumerator WaveStart()
    {
        topPanelGO.SetActive(true);
        notificationText.text = "WAVE " + statistics.wave.ToString();
        yield return new WaitForSeconds(0.01f);
        statistics.enemiesSpawned = 0;
        statistics.enemiesKilled = 0;
        player.PlayerReset();
        player.armour.UpdateStatsBar();
        player.weapon.UpdateDisplay();
        player.shield.UpdateDisplay();

        yield return new WaitForSeconds(1);
        notificationText.text = "3";
        yield return new WaitForSeconds(1);
        notificationText.text = "2";
        yield return new WaitForSeconds(1);
        notificationText.text = "1";
        yield return new WaitForSeconds(1);
        notificationText.text = "FIGHT!";
        yield return new WaitForSeconds(1);
        statistics.waveStart = true;
        topPanelGO.SetActive(false);
        statistics.enemiesSpawned = statistics.wave + 1;
        SpawnEnemy();
        for (int i = 0; i < statistics.wave; i++)
        {
            int delay = Random.Range(2, 6);
            yield return new WaitForSeconds(delay);
            SpawnEnemy();
        }
    }
    IEnumerator WaveCompleteDelay()
    {
        topPanelGO.SetActive(true);
        notificationText.text = "WAVE COMPLETE!";
        yield return new WaitForSeconds(3);
        screenManager.screenIndex = 1;
        statistics.wave++;
        screenManager.ScreenChanger(3);
    }
}
