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
                if (game.statistics.money >= game.store.cannonCost && player.weapon.fireRate > 0.5f)
                {
                    player.weapon.fireRate -= 0.1f;
                    player.weapon.bulletSpeed += 0.5f;
                    Debug.Log("Player weapon fireRate: " + player.weapon.fireRate);
                    Debug.Log("Player weapon bulletSpeed: " + player.weapon.bulletSpeed);

                    game.statistics.BankWithdrawal(game.store.cannonCost);
                    game.store.cannonCost *= 2;
                    game.screenManager.SetStore();
                }
                break;
            case 1: // Shield
                if (game.statistics.money >= game.store.shieldCost && player.shield.duration < 5.5f)
                {
                    player.shield.duration += 0.25f;
                    player.shield.rechargeTime += .025f;
                    Debug.Log("Player shield duration: " + player.shield.duration);
                    Debug.Log("Player shield rechargeTime: " + player.shield.rechargeTime);

                    game.statistics.BankWithdrawal(game.store.shieldCost);
                    game.store.shieldCost *= 2;
                    game.screenManager.SetStore();
                }
                break;
            case 2: // Treads
                if (game.statistics.money >= game.store.treadCost && player.movement.speed < 3)
                {
                    player.movement.speed += 0.15f;
                    Debug.Log("Player movement speed: " + player.movement.speed);
                    game.statistics.BankWithdrawal(game.store.treadCost);
                    game.store.treadCost *= 2;
                    game.screenManager.SetStore();
                }
                break;
            case 3: // Armour
                if (game.statistics.money >= game.store.armourCost)
                {
                    player.armour.maxArmour++;
                    Debug.Log("Player armour: " + player.armour.maxArmour);
                    player.armour.currentArmour = player.armour.maxArmour;
                    game.statistics.BankWithdrawal(game.store.armourCost);
                    game.store.armourCost *= 2;
                    game.screenManager.SetStore();
                }
                break;
        }        
    }
}
