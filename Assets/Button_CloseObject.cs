using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_CloseObject : MonoBehaviour {
    public GameObject enable;

public void DisableGameobject()
    {
        this.gameObject.SetActive(false);
        enable.SetActive(true);
    }
}
