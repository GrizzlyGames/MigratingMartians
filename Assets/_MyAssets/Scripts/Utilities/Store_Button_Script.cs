using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Store_Button_Script : MonoBehaviour {

    public int type = 0;

    public Player_Manager player;
    private Game_Manager game;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
    }

    public void Upgrade()
    {
        switch (type)
        {
            case 0:
                player.health.maxHealth++;                
                player.health.currentHealth = player.health.maxHealth;
                Debug.Log("Player Max Health: " + player.health.maxHealth);
                break;
            case 1:
                player.weapon.fireRate -= 0.1F;
                break;
            case 2:
                player.weapon.bulletSpeed++;
                break;
            case 3:
                player.shield.rechargeTime += 0.025f;
                break;
            default:
                player.movement.speed++;
                break;
        }

        game.stateIndex = 2;
        game.StateChecker();
    }
}
