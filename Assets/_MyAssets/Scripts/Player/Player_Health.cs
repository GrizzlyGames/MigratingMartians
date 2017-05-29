using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(Player_Manager))]
public class Player_Health : MonoBehaviour
{
    public GameObject explosion;
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
        if (other.gameObject.tag == "Enemy")
            other.transform.GetComponent<Enemy_AI>().Death();

        if (other.gameObject.tag == "Explosion" || other.gameObject.tag == "EnemyBullet")
        {
            if (other.gameObject.tag == "EnemyBullet")
                Destroy(other.gameObject);

            if (!player.shield.isActive && player.armour.currentArmour > 0)
            {
                player.armour.currentArmour--;
                player.armour.UpdateDisplay();
                if (player.armour.currentArmour < 1)
                    StartCoroutine(DeathDelay());
            }
            else
            {
                if (player.shield.shieldTotal >= 1)
                    player.shield.shieldTotal = 0;
            }
        }
    }

    private IEnumerator DeathDelay()
    {
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        player.armour.isAlive = false;
        this.transform.GetChild(0).gameObject.SetActive(false);
        this.transform.GetChild(1).gameObject.SetActive(false);
        yield return new WaitForSeconds(5);
        game.screenManager.screenIndex = 3;
        game.screenManager.ScreenChanger(5);
    }
}
