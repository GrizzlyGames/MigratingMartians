using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Scene_Load_Script : MonoBehaviour {

    [SerializeField]
    private int index;
    
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
