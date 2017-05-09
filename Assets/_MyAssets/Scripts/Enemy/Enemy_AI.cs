using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(ClampToScreen_Script))]
public class Enemy_AI : MonoBehaviour
{
    public int type = 0;
    public int health;
    public int maxHealth = 1;
    public float speed = 1;
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
            game.status.money += 100;
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
