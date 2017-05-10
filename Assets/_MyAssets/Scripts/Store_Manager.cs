using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Store_Manager : MonoBehaviour
{
    private Game_Manager game;
    public Player_Manager player;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();        
    }

    public void Purchase(int i)
    {
        switch (i) 
        {
            case 0: // Cannon
                if (game.statistics.money >= game.store.cannonCost)
                {
                    player.weapon.bulletSpeed *= 2;
                    game.statistics.money -= game.store.cannonCost;
                    game.store.cannonCost *= 2;
                    game.screenManager.OpenStore();
                }
                break;
            case 1: // Shield
                if (game.statistics.money >= game.store.shieldCost)
                {
                    player.shield.rechargeTime *= 2;
                    player.shield.durability += 1;
                    game.statistics.money -= game.store.shieldCost;
                    game.store.armourCost *= 2;
                    game.screenManager.OpenStore();
                }
                break;
            case 2: // Treads
                if (game.statistics.money >= game.store.treadCost)
                {
                    player.movement.speed++;
                    game.statistics.money -= game.store.treadCost;
                    game.store.treadCost *= 2;
                    game.screenManager.OpenStore();
                }
                break;
            case 3: // Armour
                if (game.statistics.money >= game.store.armourCost)
                {
                    player.health.maxHealth += 3;
                    player.health.currentHealth = player.health.maxHealth;
                    game.statistics.money -= game.store.armourCost;
                    game.store.armourCost *= 2;
                    game.screenManager.OpenStore();
                }
                break;
        }        
    }
}
