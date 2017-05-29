using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shield : MonoBehaviour
{
    public GameObject shield;
    private Player_Manager player;

    private void Start()
    {
        player = GetComponent<Player_Manager>();
    }

    private void Update()
    {
        if (player.shield.shieldTotal < 1.0f) // If player shield is less than full
        {
            player.shield.collider.enabled = false;
            shield.SetActive(false);
            player.shield.isActive = false;
            if (player.armour.isAlive)
                player.shield.shieldTotal += (player.shield.rechargeRate * Time.deltaTime);
        }
        else
        {
            player.shield.collider.enabled = true;
            shield.SetActive(true);
            player.shield.isActive = true;
        }
        player.shield.UpdateDisplay();
    }
}
