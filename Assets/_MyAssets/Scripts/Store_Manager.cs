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
                if (game.statistics.money >= game.store.cannonCost && player.weapon.upgradeLevel < 9)
                {
                    player.weapon.upgradeLevel++;
                    player.weapon.fireRate -= 0.2f;
                    player.weapon.bulletSpeed += 1;
                    Debug.Log("Player weapon fireRate: " + player.weapon.fireRate);
                    Debug.Log("Player weapon bulletSpeed: " + player.weapon.bulletSpeed);

                    game.statistics.BankWithdrawal(game.store.cannonCost);
                    game.store.cannonCost *= 2;
                    game.screenManager.SetStore();
                }
                else
                    Debug.Log("Player purchased all of the cannon upgrades.");
                break;
            case 1: // Shield
                if (game.statistics.money >= game.store.shieldCost && player.shield.upgradeLevel < 9)
                {
                    player.shield.upgradeLevel++;
                    player.shield.rechargeRate += .025f;
                    Debug.Log("Player shield rechargeTime: " + player.shield.rechargeRate);

                    game.statistics.BankWithdrawal(game.store.shieldCost);
                    game.store.shieldCost *= 2;
                    game.screenManager.SetStore();
                }
                else
                    Debug.Log("Player purchased all of the Shield upgrades.");
                break;
            case 2: // Treads
                if (game.statistics.money >= game.store.treadCost && player.movement.upgradeLevel < 9)
                {
                    player.movement.upgradeLevel++;
                    player.movement.speed += 0.5f;
                    Debug.Log("Player movement speed: " + player.movement.speed);
                    game.statistics.BankWithdrawal(game.store.treadCost);
                    game.store.treadCost *= 2;
                    game.screenManager.SetStore();
                }
                else
                    Debug.Log("Player purchased all of the Treads upgrades.");
                break;
            case 3: // Armour
                if (game.statistics.money >= game.store.armourCost && player.armour.upgradeLevel < 9)
                {
                    player.armour.upgradeLevel++;
                    player.armour.maxArmour++;
                    Debug.Log("Player armour: " + player.armour.maxArmour);
                    player.armour.currentArmour = player.armour.maxArmour;
                    game.statistics.BankWithdrawal(game.store.armourCost);
                    game.store.armourCost *= 2;
                    game.screenManager.SetStore();
                }
                else
                    Debug.Log("Player purchased all of the Armour upgrades.");
                break;
        }        
    }
}
