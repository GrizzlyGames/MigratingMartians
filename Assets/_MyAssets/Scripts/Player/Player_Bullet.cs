using UnityEngine;
using System.Collections;

public class Player_Bullet : MonoBehaviour
{
    [HideInInspector]
    public float speed;

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
                    Destroy(other.gameObject);
                    break;
                case 4:
                    Destroy(other.gameObject);
                    break;
            }
        }
    }

}

