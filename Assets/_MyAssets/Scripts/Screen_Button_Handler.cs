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
}
