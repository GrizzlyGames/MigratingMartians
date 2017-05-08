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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "EnemyBullet")
        {
            Destroy(other.gameObject);
        }
    }
}
