using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(ClampToScreen_Script))]
public class Enemy_AI : MonoBehaviour
{
    public int type;
    public int health;
    public int maxHealth;
    public float flySpeed;
    public float bulletSpeed;
    public float fireRate;

    public bool canShoot = false;
    public GameObject bullet;
    public GameObject explosion;


    private float moveTime;
    public float shootTime;
    private Image healthImage;
    private Vector2 destination;
    private Game_Manager game;
    private Player_Manager player;
    private ClampToScreen_Script clamp;

    private void Start()
    {
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
            shootTime += Time.deltaTime;
            if (transform.position.y > clamp.GetLimitations().y)
            {
                MoveDown();
            }
            else
            {
                if (shootTime >= fireRate && player.armour.isAlive)
                {
                    shootTime = 0;
                    GameObject bullet = Instantiate(this.bullet, this.transform.position, Quaternion.identity) as GameObject;
                    bullet.GetComponent<Enemy_Bullet>().type = type;
                    bullet.GetComponent<Enemy_Bullet>().speed = bulletSpeed;
                    bullet.transform.SetParent(game.trashCollocter.transform);
                }

                moveTime += Time.deltaTime;
                if (moveTime >= 3)
                {
                    moveTime = 0;
                    destination.x = Random.Range(clamp.GetLimitations().w, clamp.GetLimitations().x);
                    destination.y = Random.Range(0, clamp.GetLimitations().y);
                }
                transform.position = Vector2.MoveTowards(transform.position, destination, flySpeed * Time.deltaTime);
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
        if (collision.transform.CompareTag("PlayerBullet") || collision.transform.CompareTag("SpecialBullet") || collision.transform.CompareTag("Explosion"))
        {
            switch (type)
            {
                case 1:
                    game.statistics.BankDeposit(2000);
                    break;
                case 2:
                    game.statistics.BankDeposit(12500);
                    break;
                case 3:
                    game.statistics.BankDeposit(12500);
                    break;
                case 4:
                    game.statistics.BankDeposit(10000);
                    break;
            }
            health--;
            healthImage.fillAmount = (float)health / (float)maxHealth;
            if (!collision.transform.CompareTag("Explosion"))
                Destroy(collision.gameObject);
            if (health < 1 || collision.transform.CompareTag("SpecialBullet"))
                Death();
        }
    }
    private void MoveDown()
    {
        this.transform.Translate(transform.up * -flySpeed * Time.deltaTime);
    }
    public void Death()
    {
        switch (type)
        {
            case 1:
                game.statistics.enemyType1Killed++;
                break;
            case 2:
                game.statistics.enemyType2Killed++;
                break;
            case 3:
                game.statistics.enemyType3Killed++;
                break;
            case 4:
                game.statistics.enemyType4Killed++;
                break;
        }
        GameObject go = Instantiate(explosion, this.transform.position, Quaternion.identity) as GameObject;
        go.transform.SetParent(game.trashCollocter);
        Destroy(this.gameObject);
    }
}
