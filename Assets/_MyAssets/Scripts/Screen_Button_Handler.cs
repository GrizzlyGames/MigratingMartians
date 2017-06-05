using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_Button_Handler : MonoBehaviour {

    [SerializeField]
    private Screen_Manager screen;

    public void ChangeScreen(int index)
    {        
        screen.ScreenChanger(index);
    }

    public void LaunchLeaderBoard()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowLeaderboardUI();
        }
        else
            Debug.Log("User not authenticated.");
    }

    public void LaunchAchievements()
    {
        if (Social.localUser.authenticated)
        {
            Social.ShowAchievementsUI();
        }
        else
            Debug.Log("User not authenticated.");
    }
}
