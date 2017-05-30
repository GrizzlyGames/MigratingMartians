using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Store_Manager : MonoBehaviour
{
    private Game_Manager game;
    public Player_Manager player;
    public Screen_Manager screen;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
    }

    public void Purchase(int i)
    {
        switch (i)
        {
            case 0: // Cannon
                if (game.statistics.money >= game.store.cannonCost && player.weapon.upgradeLevel < 5)
                {
                    player.weapon.upgradeLevel++;
                    if (player.weapon.upgradeLevel == 5)
                    {
                        screen.turretCostText.text = "SOLD";
                    }
                    player.weapon.normalFireRate -= 0.4f;
                    player.weapon.specialFireRate -= 1;
                    player.weapon.bulletSpeed += 1;

                    game.statistics.BankWithdrawal(game.store.cannonCost);
                    game.store.cannonCost *= 2;
                    game.screenManager.SetStore();
                }
                break;
            case 1: // Shield
                if (game.statistics.money >= game.store.shieldCost && player.shield.upgradeLevel < 5)
                {
                    player.shield.upgradeLevel++;
                    if (player.shield.upgradeLevel == 5)
                    {
                        screen.shieldCostText.text = "SOLD";
                    }
                    player.shield.rechargeRate += .025f;
                    Debug.Log("Player shield rechargeTime: " + player.shield.rechargeRate);

                    game.statistics.BankWithdrawal(game.store.shieldCost);
                    game.store.shieldCost *= 2;
                    game.screenManager.SetStore();
                }
                break;
            case 2: // Treads
                if (game.statistics.money >= game.store.treadCost && player.movement.upgradeLevel < 5)
                {
                    player.movement.upgradeLevel++;
                    if (player.movement.upgradeLevel == 5)
                    {
                        screen.treadCostText.text = "SOLD";
                    }
                    player.movement.speed += 0.5f;
                    game.statistics.BankWithdrawal(game.store.treadCost);
                    game.store.treadCost *= 2;
                    game.screenManager.SetStore();
                }
                break;
            case 3: // Armour
                if (game.statistics.money >= game.store.armourCost && player.armour.upgradeLevel < 5)
                {
                    player.armour.upgradeLevel++;
                    if (player.armour.upgradeLevel == 5)
                    {
                        screen.armourCostText.text = "SOLD";
                    }
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
