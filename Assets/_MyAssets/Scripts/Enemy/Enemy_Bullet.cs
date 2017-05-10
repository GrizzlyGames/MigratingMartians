using UnityEngine;
using System.Collections;
[RequireComponent(typeof(ClampToScreen_Script))]
public class Enemy_Bullet : MonoBehaviour
{
    public int type = 0;
    public float speed = 1;
    public SpriteRenderer spriteRenderer;
    private ClampToScreen_Script clamp;
    private Vector3 playerPosition;
    private Game_Manager game;

    void Awake()
    {
        clamp = GetComponent<ClampToScreen_Script>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerPosition = GameObject.Find("Player").transform.position;
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        speed = game.statistics.wave * 0.25f;
    }

    private void Start()
    {
        switch (type)
        {
            case 2:
                spriteRenderer.color = new Color32(255, 0, 255, 255); // Pink
                break;
            case 3:
                spriteRenderer.color = new Color32(0, 255, 255, 255); // light blue
                break;
            case 4:
                spriteRenderer.color = new Color32(255, 0, 0, 255); // red
                break;
        }
    }
    private float time;

    private void Update()
    {
        if (this.transform.position.y <= clamp.GetLimitations().z)
        {
            Destroy(gameObject);
        }
        switch (type)
        {
            case 2:
                this.transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
                time += Time.deltaTime;
                if (time >= 5)
                    Destroy(this.gameObject);
                break;
            case 3:
                this.transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
                break;
            case 4:
                playerPosition = GameObject.Find("Player").transform.position;
                if (this.transform.position.y <= clamp.GetLimitations().z * 0.9f)
                    Destroy(gameObject);
                this.transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
                break;
        }        
    }
}
