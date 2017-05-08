using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canvas_Switch_Script : MonoBehaviour {

    public GameObject openCanvas;
    public GameObject closeCanvas;

	public void Switch()
    {
        openCanvas.SetActive(true);
        closeCanvas.SetActive(false);
    }
}
