using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Game_Manager : MonoBehaviour
{
    public Transform trashCollocter;
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

        public int redBulletsDestroyed;
        public int blueBulletsDestroyed;
        public int pinkBulletsDestroyed;
        public int enemyType1Killed;
        public int enemyType2Killed;
        public int enemyType3Killed;
        public int enemyType4Killed;

        public void ResetVariables()
        {
            waveComplete = false;
            waveCompletePending = false;
            gameStarted = false;
            waveTime = 0;
            waveDuration = 15;
            wave = 1;
            money = 0;
            gameScore = 0;

            redBulletsDestroyed = 0;
            blueBulletsDestroyed = 0;
            pinkBulletsDestroyed = 0;
            enemyType1Killed = 0;
            enemyType2Killed = 0;
            enemyType3Killed = 0;
            enemyType4Killed = 0;
        }

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
    [System.Serializable]
    public class Store
    {
        public int shieldCost;
        public int treadCost;
        public int cannonCost;
        public int armourCost;

        public void ResetPrices()
        {
            shieldCost = 250000;
            treadCost = 50000;
            cannonCost = 150000;
            armourCost = 100000;
        }
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

        if (statistics.wave < 20)
        {
            switch (rnd)
            {
                case 0:
                    go.GetComponent<Enemy_AI>().maxHealth = 1;
                    go.GetComponent<Enemy_AI>().flySpeed = 1 + (statistics.wave * 0.25f);
                    break;
                case 1:
                    go.GetComponent<Enemy_AI>().maxHealth = 2;
                    go.GetComponent<Enemy_AI>().flySpeed = 0.4f + (statistics.wave * 0.1f);
                    go.GetComponent<Enemy_AI>().fireRate = 7.25f - (statistics.wave * 0.25f);
                    go.GetComponent<Enemy_AI>().bulletSpeed = 0.75f + (statistics.wave * 0.2f);
                    break;
                case 2:
                    go.GetComponent<Enemy_AI>().maxHealth = 2;
                    go.GetComponent<Enemy_AI>().flySpeed = 0.4f + (statistics.wave * 0.1f);
                    go.GetComponent<Enemy_AI>().fireRate = 6 - (statistics.wave * 0.25f);
                    go.GetComponent<Enemy_AI>().bulletSpeed = 0.5f + (statistics.wave * 0.2f);
                    break;
                case 3:
                    go.GetComponent<Enemy_AI>().maxHealth = 3;
                    go.GetComponent<Enemy_AI>().flySpeed = 0.2f + (statistics.wave * 0.1f);
                    go.GetComponent<Enemy_AI>().fireRate = 3 - (statistics.wave * 0.05f);
                    go.GetComponent<Enemy_AI>().bulletSpeed = 0.15f + (statistics.wave * 0.1f);
                    break;
            }
        }
        else
        {
            switch (rnd)
            {
                case 0:
                    go.GetComponent<Enemy_AI>().maxHealth = 1;
                    go.GetComponent<Enemy_AI>().flySpeed = 6;
                    break;
                case 1:
                    go.GetComponent<Enemy_AI>().maxHealth = 2;
                    go.GetComponent<Enemy_AI>().flySpeed = 2.4f;
                    go.GetComponent<Enemy_AI>().fireRate = 2.25f;
                    go.GetComponent<Enemy_AI>().bulletSpeed = 4.75f;
                    break;
                case 2:
                    go.GetComponent<Enemy_AI>().maxHealth = 2;
                    go.GetComponent<Enemy_AI>().flySpeed = 2.4f;
                    go.GetComponent<Enemy_AI>().fireRate = 1;
                    go.GetComponent<Enemy_AI>().bulletSpeed = 4.5f;
                    break;
                case 3:
                    go.GetComponent<Enemy_AI>().maxHealth = 3;
                    go.GetComponent<Enemy_AI>().flySpeed = 2.2f;
                    go.GetComponent<Enemy_AI>().fireRate = 2;
                    go.GetComponent<Enemy_AI>().bulletSpeed = 2.1f;
                    break;
            }
        }

        go.GetComponent<Enemy_AI>().shootTime = 0;
    }

    public IEnumerator WaveStart()
    {
        topPanelGO.SetActive(true);
        if(statistics.wave == 1)
        {
            notificationText.text = "Tilt to drive, tap to shoot, swipe for special.";
            yield return new WaitForSeconds(5);
        }

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


        switch (statistics.wave)
        {
            case 1:
                statistics.waveDuration = 10;
                break;
            case 2:
                statistics.waveDuration = 15;
                break;
            case 3:
                statistics.waveDuration = 20;
                break;
            case 4:
                statistics.waveDuration = 25;
                break;
            case 5:
                statistics.waveDuration = 30;
                break;
            case 6:
                statistics.waveDuration = 35;
                break;
            case 7:
                statistics.waveDuration = 40;
                break;
            case 8:
                statistics.waveDuration = 45;
                break;
            case 9:
                statistics.waveDuration = 50;
                break;
            case 10:
                statistics.waveDuration = 55;
                break;
            default:
                statistics.waveDuration = 60;
                break;
        }

        statistics.gameStarted = true;
    }
    public IEnumerator WaveCompleteDelay()
    {
        statistics.gameStarted = false;
        statistics.waveComplete = true;
        topPanelGO.SetActive(true);
        notificationText.text = "WAVE COMPLETE!";
        yield return new WaitForSeconds(3);
        statistics.wave++;
        screenManager.ScreenChanger(2);
    }
}
