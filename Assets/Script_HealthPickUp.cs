using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_HealthPickUp : MonoBehaviour
{
    private float time;
    private void Update()
    {
        if (time < 3.0f)
            time += Time.deltaTime;
        else 
            Destroy(this.gameObject);
    }

    private void OnMouseDown()
    {
        Player_Manager player = GameObject.Find("Player").GetComponent<Player_Manager>();
        if (player.health.currentHealth < player.health.maxHealth)
        {
            player.health.currentHealth += 1;
            player.health.UpdateStatsBar();
            Destroy(this.gameObject);
        }
    }
}
