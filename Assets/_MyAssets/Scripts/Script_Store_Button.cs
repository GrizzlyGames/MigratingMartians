using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Script_Store_Button : MonoBehaviour
{
    public GameObject openGO;
    public GameObject closeGO;
    public GameObject notificationGO;

    private Game_Manager game;

    private void Start()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        openGO.SetActive(false);
        closeGO.SetActive(true);
    }

    public void UpgradeSelected(int cost)
    {
        if (game.status.money >= cost)
        {
            game.status.money -= cost;
            openGO.SetActive(true);
            closeGO.SetActive(false);
        }
        else
            notificationGO.SetActive(true);
        Debug.Log("remain money: " + game.status.money);
    }
}
