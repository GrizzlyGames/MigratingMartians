using UnityEngine;
using System.Collections;

public class Player_Bullet : MonoBehaviour
{
    public bool isSpecial = false;

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
            TextPoints_Script pointsScript = other.transform.GetChild(0).GetChild(0).GetComponent<TextPoints_Script>();            
            switch (other.transform.GetComponent<Enemy_Bullet>().type)
            {
                case 2:
                    if (!isSpecial)
                    {
                        Destroy(this.gameObject);
                    }
                    else
                    {
                        game.statistics.redBulletsDestroyed++;
                        game.statistics.BankDeposit(5000);
                        pointsScript.UpdateText("+5000");
                        pointsScript.transform.parent.SetParent(null);
                        pointsScript.Death();
                        pointsScript.SetScale(new Vector3(2, 2, 2));
                        Destroy(other.gameObject);
                    }
                    break;
                case 3:
                    game.statistics.blueBulletsDestroyed++;
                    game.statistics.BankDeposit(2500);
                    pointsScript.UpdateText("+2500");
                    pointsScript.transform.parent.SetParent(null);                    
                    pointsScript.Death();
                    pointsScript.SetScale(new Vector3(1, 1, 1));
                    Destroy(other.gameObject);
                    break;
                case 4:
                    game.statistics.pinkBulletsDestroyed++;
                    game.statistics.BankDeposit(1000);
                    pointsScript.UpdateText("+1000");                                        
                    pointsScript.transform.parent.SetParent(null);
                    pointsScript.Death();
                    pointsScript.SetScale(new Vector3(0.65f, 0.65f, 0.65f));
                    Destroy(other.gameObject);
                    break;
            }
        }
        if (other.gameObject.tag == "Explosion" && !isSpecial)
        {
            Destroy(this.gameObject);
        }
    }

}

