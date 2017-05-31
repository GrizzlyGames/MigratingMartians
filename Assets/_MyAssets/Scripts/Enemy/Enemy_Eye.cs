using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Eye : MonoBehaviour
{
    private float distance;
    private Player_Manager player;

    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player_Manager>();        
    }

    private void Update()
    {
        distance = (this.transform.position - player.transform.position).magnitude;

        if (this.transform.rotation.z > -80 && this.transform.rotation.z < 80 && player.transform != null)
        {
            this.transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((player.transform.position.y - transform.position.y - 2), (player.transform.position.x - transform.position.x)) * Mathf.Rad2Deg + 90);
            this.transform.Translate(Vector2.zero * distance * Time.deltaTime);
        }
    }
}
