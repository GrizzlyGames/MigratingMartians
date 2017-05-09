using UnityEngine;
using System.Collections;
[RequireComponent(typeof(ClampToScreen_Script))]
public class Enemy_Bullet : MonoBehaviour
{
    public int type = 0;
    public SpriteRenderer spriteRenderer;
    private ClampToScreen_Script clamp;
    private Vector3 playerPosition;

    void Awake()
    {
        clamp = GetComponent<ClampToScreen_Script>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerPosition = GameObject.Find("Player").transform.position;        
    }

    private void Start()
    {
        switch (type)
        {
            case 2:
                spriteRenderer.color = new Color32(255, 255, 255, 255);
                break;
            case 3:
                spriteRenderer.color = new Color32(0, 0, 255, 255);
                break;
            case 4:
                spriteRenderer.color = new Color32(0, 255, 0, 255);
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
                this.transform.position = Vector2.MoveTowards(transform.position, playerPosition, 1 * Time.deltaTime);
                break;
            case 3:
                this.transform.position = Vector2.MoveTowards(transform.position, playerPosition, 1 * Time.deltaTime);
                break;
            case 4:
                playerPosition = GameObject.Find("Player").transform.position;
                if (this.transform.position.y <= clamp.GetLimitations().z * 0.9f)
                    Destroy(gameObject);
                this.transform.position = Vector2.MoveTowards(transform.position, playerPosition, 1 * Time.deltaTime);
                break;
        }        
    }
}
