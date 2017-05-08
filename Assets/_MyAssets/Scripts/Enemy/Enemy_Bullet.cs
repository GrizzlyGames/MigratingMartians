using UnityEngine;
using System.Collections;
[RequireComponent(typeof(ClampToScreen_Script))]
public class Enemy_Bullet : MonoBehaviour
{
    private ClampToScreen_Script clamp;
    private Vector3 playerPosition;

    void Awake()
    {
        clamp = GetComponent<ClampToScreen_Script>();
        playerPosition = GameObject.Find("Player").transform.position;
    }

    private void Update()
    {
        if (this.transform.position.y <= clamp.GetLimitations().z)
        {
            Destroy(gameObject);
        }
        this.transform.position = Vector2.MoveTowards(transform.position, playerPosition, 1 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
            Destroy(this.gameObject);
    }
}
