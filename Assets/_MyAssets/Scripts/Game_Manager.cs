using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game_Manager : MonoBehaviour
{
    public GameObject menuPanel;
    public GameObject storePanel;

    public Text menuWaveText;
    public Text moneyText;
    public Text upgradeBankText;
    public Text upgradeWaveText;
    
    [SerializeField]
    private Player_Manager player;

    public Status status = new Status();
    public class Status
    {
        public bool waveStart = false;
        public int wave = 1;
        public int money = 0;
        public int enemiesSpawned = 0;

        public GameObject topPanel;
        public Text notificationText;

        public int baseCost = 100;
        public int healthLevel = 0;
        public int reloadLevel = 0;
        public int bulletLevel = 0;
        public int movementLevel = 0;
    }

    public bool resetPrefs = true;
    public GameObject[] Enemies;

    private void Start()
    {
        status.topPanel = stateGO[2].transform.GetChild(0).GetChild(1).gameObject;
        status.notificationText = stateGO[2].transform.GetChild(0).GetChild(1).GetChild(0).GetComponent<Text>();
        if (resetPrefs)
        {
            PlayerPrefs.DeleteAll();
        }
        StateChecker();
    }

    [HideInInspector]
    public int stateIndex = 0;
    public GameObject[] stateGO;
    public void StateChecker()
    {
        for (int i = 0; i < stateGO.Length; i++)
        {
            if (i == stateIndex)
                stateGO[i].SetActive(true);
            else
                stateGO[i].SetActive(false);
        }
        foreach (GameObject element in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            Destroy(element);
        }
        foreach (GameObject element in GameObject.FindGameObjectsWithTag("EnemyBullet"))
        {
            Destroy(element);
        }

        switch (stateIndex)
        {
            case 0: // Play
                player.health.currentHealth = player.health.maxHealth;                
                player.weapon.time = player.weapon.fireRate;                
                player.shield.time = player.shield.duration;                
                player.transform.GetChild(0).gameObject.SetActive(true);
                player.transform.GetChild(1).gameObject.SetActive(true);
                status.wave = 1;
                status.money = 0;
                break;
            case 1: // Intermission           
                menuPanel.SetActive(true);
                storePanel.SetActive(false);
                upgradeBankText.text = "BANK: " + status.money.ToString("C00");
                menuWaveText.text = "WAVE " + status.wave.ToString();
                moneyText.text = "BANK: " + status.money.ToString("C00");
                upgradeWaveText.text = "WAVE " + status.wave.ToString();
                player.shield.time = player.shield.duration;             
                
                break;
            case 2: // Play
                player.health.currentHealth = player.health.maxHealth;
                player.health.UpdateBar();
                player.weapon.UpdateDisplay();
                player.shield.UpdateDisplay();
                player.weapon.canShoot = false;                            
                player.shield.UpdateDisplay();
                StartCoroutine(EnemySpawnDelay());
                break;
        }
    }
    
    public void EnemyKilled()
    {
        status.enemiesSpawned--;
        if (status.enemiesSpawned < 1)
        {
            status.waveStart = false;
            player.weapon.time = player.weapon.fireRate;
            player.weapon.reloadImage.fillAmount = player.weapon.time / player.weapon.fireRate;
            StartCoroutine(WaveCompleteDelay());
        }
    }

    private void SpawnEnemy()
    {
        int rnd = Random.Range(0, Enemies.Length);

        Transform trans = GameObject.FindGameObjectWithTag("Player").transform;
        if (!trans)
            trans = GameObject.Find("Player").transform;

        Vector3 screenVec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Instantiate(Enemies[rnd], new Vector3(Random.Range(-screenVec.x, screenVec.x), screenVec.y, 0), Quaternion.identity);
    }

    IEnumerator EnemySpawnDelay()
    {
        status.topPanel.SetActive(true);
        status.notificationText.text = "WAVE " + status.wave.ToString();
        yield return new WaitForSeconds(2);
        status.notificationText.text = "5";
        yield return new WaitForSeconds(1);
        status.notificationText.text = "4";
        yield return new WaitForSeconds(1);
        status.notificationText.text = "3";
        yield return new WaitForSeconds(1);
        status.notificationText.text = "2";
        yield return new WaitForSeconds(1);
        status.notificationText.text = "1";
        yield return new WaitForSeconds(1);
        status.notificationText.text = "FIGHT!";
        yield return new WaitForSeconds(1);
        status.waveStart = true;
        status.topPanel.SetActive(false);        
        status.enemiesSpawned = status.wave;
        for (int i = 0; i < status.wave; i++)
        {
            float delay = 0;
            switch (status.wave)
            {
                case 1:
                    delay = 1;
                    break;
                case 2:
                    delay = 1;
                    break;
                default:
                    delay = Random.Range(1, 5);
                    break;
            }
            yield return new WaitForSeconds(delay);
            SpawnEnemy();
        }
    }
    IEnumerator WaveCompleteDelay()
    {
        status.topPanel.SetActive(true);
        status.notificationText.text = "WAVE COMPLETE!";
        yield return new WaitForSeconds(3);
        stateIndex = 1;
        status.wave++;
        StateChecker();
    }
}
