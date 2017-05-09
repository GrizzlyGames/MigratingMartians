using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shield : MonoBehaviour
{
    public GameObject shieldGO;
    private Player_Manager player;

    private void Start()
    {
        player = GetComponent<Player_Manager>();
        shieldGO.SetActive(false);
    }

    private void Update()
    {
        if (player.shield.isActive)
        {
            shieldGO.SetActive(true);
            player.shield.time -= Time.deltaTime;            
            if (player.shield.time <= 0)
            {
                player.shield.isActive = false;
                shieldGO.SetActive(false);
            }
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
