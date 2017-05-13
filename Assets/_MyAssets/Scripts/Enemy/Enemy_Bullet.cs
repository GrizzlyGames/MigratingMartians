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
                spriteRenderer.color = new Color32(255, 0, 0, 255); // red
                this.transform.localScale = new Vector3(this.transform.localScale.x - 0.2f, this.transform.localScale.y - 0.2f, 1);
                break;
            case 3:
                spriteRenderer.color = new Color32(0, 255, 255, 255); // light blue
                this.transform.localScale = new Vector3(this.transform.localScale.x + 0.2f, this.transform.localScale.y + 0.2f, 1);
                break;
            case 4:
                spriteRenderer.color = new Color32(255, 0, 255, 255); // Pink
                this.transform.localScale = new Vector3(this.transform.localScale.x + 0.5f, this.transform.localScale.y + 0.5f, 1);
                break;
        }
    }

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
                if (this.transform.position.y <= clamp.GetLimitations().z * 0.6f)
                    Destroy(gameObject);
                break;
            case 3:
                this.transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
                break;
            case 4:
                playerPosition = GameObject.Find("Player").transform.position;                
                this.transform.position = Vector2.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
                break;
        }
        if (this.transform.position.y <= clamp.GetLimitations().z * 0.85f)
            Destroy(gameObject);
    }
}
