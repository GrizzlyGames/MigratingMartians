using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(ClampToScreen_Script))]
public class Enemy_AI : MonoBehaviour
{
    public int type;
    public int health;
    public int maxHealth = 1;
    public float speed;
    public bool canShoot = false;
    public GameObject bullet;
    public GameObject explosion;

    private float shootTime;
    private float moveTime;
    private Image healthImage;
    private Vector2 destination;
    private Game_Manager game;
    private Player_Manager player;
    private ClampToScreen_Script clamp;

    private void Start()
    {
        shootTime = Random.Range(2, 5);
        health = maxHealth;
        healthImage = transform.GetChild(1).GetChild(0).GetChild(0).GetComponent<Image>();
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        player = GameObject.Find("Player").GetComponent<Player_Manager>();
        clamp = GetComponent<ClampToScreen_Script>();
        speed *= (game.statistics.wave * 0.25f);
    }
    private void Update()
    {
        if (canShoot)
        {
            if (transform.position.y > clamp.GetLimitations().y)
            {
                MoveDown();
            }
            else
            {
                moveTime += Time.deltaTime;
                if (moveTime >= shootTime && player.health.isAlive)
                {
                    moveTime = 0;
                    shootTime = Random.Range(2, 5);
                    destination.x = Random.Range(clamp.GetLimitations().w, clamp.GetLimitations().x);
                    destination.y = Random.Range(0, clamp.GetLimitations().y);
                    GameObject go = Instantiate(bullet, this.transform.position, Quaternion.identity) as GameObject;
                    go.GetComponent<Enemy_Bullet>().type = type;
                    switch (type)
                    {
                        case 2:
                            go.GetComponent<Enemy_Bullet>().speed = 0.5f;
                            break;
                        case 3:
                            go.GetComponent<Enemy_Bullet>().speed = 1;
                            break;
                        case 4:
                            go.GetComponent<Enemy_Bullet>().speed = 0.25f;
                            break;
                    }
                    
                }
                transform.position = Vector2.MoveTowards(transform.position, destination, speed * Time.deltaTime);
            }
        }
        else
        {
            if (this.transform.position.y > clamp.GetLimitations().z)
                MoveDown();
            else
            {
                Death();
            }

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("PlayerBullet"))
        {
            Destroy(collision.gameObject);
            switch (type)
            {
                case 0:
                    game.statistics.money += 5000;
                    Debug.Log(game.statistics.money);
                    break;
                case 1:
                    game.statistics.money += 7500;
                    Debug.Log(game.statistics.money);
                    break;
                case 2:
                    game.statistics.money += 7500;
                    Debug.Log(game.statistics.money);
                    break;
                case 3:
                    game.statistics.money += 10000;
                    Debug.Log(game.statistics.money);
                    break;
            }
            health--;
            healthImage.fillAmount = (float)health / (float)maxHealth;
            if (health < 1)
                Death();
        }
    }
    private void MoveDown()
    {
        this.transform.Translate(transform.up * -speed * Time.deltaTime);
    }
    public void Death()
    {
        Instantiate(explosion, this.transform.position, Quaternion.identity);
        game.EnemyKilled();
        Destroy(this.gameObject);
    }
}
