using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Player_Manager))]
public class Player_Health : MonoBehaviour
{
    public GameObject health;
    private Player_Manager player;
    private Game_Manager game;

    private void Start()
    {
        player = GetComponent<Player_Manager>();
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyBullet" || other.gameObject.tag == "Explosion" || other.gameObject.tag == "Enemy")
        {            
            if (other.gameObject.tag == "Enemy")
                other.transform.GetComponent<Enemy_AI>().Death();
            else
            {
                if (!player.shield.isActive)
                {                    
                    player.armour.currentArmour--;
                    player.armour.UpdateStatsBar();
                    int chance = Random.Range(0, 5);
                    if (chance == 0)
                        StartCoroutine(SpawnHealthDelay());
                    if (player.armour.currentArmour < 1)
                        StartCoroutine(DeathDelay());
                }
                else
                {
                    Debug.Log("Player shield.");
                    Destroy(other.gameObject);
                }
            }
            if (other.gameObject.tag == "EnemyBullet")
                Destroy(other.gameObject);
        }
    }

    private IEnumerator SpawnHealthDelay()
    {
        float delay = Random.Range(10, 16);
        yield return new WaitForSeconds(delay);
        if (game.screenManager.screenIndex == 2)
        {
            Vector3 screenVec = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));
            Instantiate(health, new Vector2(Random.Range(-screenVec.x, screenVec.x), Random.Range(-screenVec.y, screenVec.y)), Quaternion.identity);
        }        
    }

    private IEnumerator DeathDelay()
    {
        player.armour.isAlive = false;
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        game.screenManager.screenIndex = 3;
        game.screenManager.ScreenChanger(5);
    }
}
