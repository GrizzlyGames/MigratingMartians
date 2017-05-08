using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch_State : MonoBehaviour {

    private Game_Manager game;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
    }

    public void Switch(int index)
    {
        game.stateIndex = index;
        game.StateChecker();
    }
}
