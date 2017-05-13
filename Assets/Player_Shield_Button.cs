using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Shield_Button : MonoBehaviour {

    public Player_Manager player;

    public void ActivateShield()
    {
        player.shield.isActive = true;
    }
}
