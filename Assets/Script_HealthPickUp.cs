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
        if (player.armour.currentArmour < player.armour.maxArmour)
        {
            player.armour.currentArmour += 1;
            player.armour.UpdateStatsBar();
            Destroy(this.gameObject);
        }
    }
}
