using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shield : MonoBehaviour
{
    private Player_Manager player;

    private void Start()
    {
        player = GetComponent<Player_Manager>();
    }

    private void Update()
    {
        if (player.shield.isActive)
        {
            player.shield.sprite.enabled = true;
            player.shield.collider.enabled = true;
            player.shield.time -= Time.deltaTime;            
            if (player.shield.time <= 0)
            {
                player.shield.isActive = false;
                player.shield.sprite.enabled = false;
                player.shield.collider.enabled = false;
            }
            player.shield.UpdateDisplay();
        }
        else
        {
            if(player.shield.time < player.shield.duration)
            {                
                player.shield.time += Time.deltaTime * player.shield.rechargeTime;                
            }            
        }
        player.shield.UpdateDisplay();
    }
}
