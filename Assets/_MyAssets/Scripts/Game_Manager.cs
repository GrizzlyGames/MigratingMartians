using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game_Manager : MonoBehaviour
{
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

    [HideInInspector]
    public int screenIndex = 0;
    public GameObject[] stateGO;    
    
    public void EnemyKilled()
    {
        statistics.enemiesSpawned--;
        if (statistics.enemiesSpawned < 1)
        {
            statistics.waveStart = false;
            player.weapon.time = player.weapon.fireRate;
            player.weapon.reloadImage.fillAmount = player.weapon.time / player.weapon.fireRate;
            StartCoroutine(WaveCompleteDelay());
        }
    }
    public void SpawnEnemy()
    {
        int rnd = Random.Range(0, Enemies.Length);

        Transform trans = GameObject.FindGameObjectWithTag("Player").transform;
        if (!trans)
            trans = GameObject.Find("Player").transform;

        Vector3 screenVec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        Instantiate(Enemies[rnd], new Vector3(Random.Range(-screenVec.x, screenVec.x), screenVec.y, 0), Quaternion.identity);
    }

    public IEnumerator WaveStart()
    {
        topPanelGO.SetActive(true);
        notificationText.text = "WAVE " + statistics.wave.ToString();
        yield return new WaitForSeconds(0.01f);
        player.PlayerReset();
        player.health.UpdateStatsBar();
        player.weapon.UpdateDisplay();
        player.shield.UpdateDisplay();

        yield return new WaitForSeconds(2);
        notificationText.text = "5";
        yield return new WaitForSeconds(1);
        notificationText.text = "4";
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
        statistics.enemiesSpawned = statistics.wave;
        for (int i = 0; i < statistics.wave; i++)
        {
            float delay = 0;
            switch (statistics.wave)
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
        topPanelGO.SetActive(true);
        notificationText.text = "WAVE COMPLETE!";
        yield return new WaitForSeconds(3);
        screenIndex = 1;
        statistics.wave++;
        screenManager.ScreenChanger(3);
    }
}
