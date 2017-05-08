using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_HealthPickUp : MonoBehaviour
{
    private void OnMouseDown()
    {
        Player_Manager player = GameObject.Find("Player").GetComponent<Player_Manager>();
        if (player.health.currentHealth < player.health.maxHealth)
        {
            player.health.currentHealth += 1;
            player.health.UpdateBar();
            Destroy(this.gameObject);
        }            
    }
}
