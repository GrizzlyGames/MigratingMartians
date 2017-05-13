using UnityEngine;
using System.Collections;

public class Player_Bullet : MonoBehaviour
{
    [HideInInspector]
    public float speed;

    [HideInInspector]
    public Game_Manager game;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
    }

    private void Update()
    {
        transform.Translate((Vector2.up * speed * Time.deltaTime));
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            switch (other.transform.GetComponent<Enemy_Bullet>().type)
            {
                case 2:
                    Destroy(this.gameObject);
                    break;
                case 3:
                    game.statistics.enemyBulletType3Destroyed++;
                    game.statistics.BankDeposit(1500);
                    Destroy(other.gameObject);
                    break;
                case 4:
                    game.statistics.enemyBulletType4Destroyed++;
                    game.statistics.BankDeposit(500);
                    Destroy(other.gameObject);
                    break;
            }
        }
    }

}

