using UnityEngine;
using System.Collections;

public class ScaleToScreen_Script : MonoBehaviour {

    private float dist;
    private float sizeX;
    private float sizeY;
    private Vector3 scale;

    private void Awake()
    {
        dist = (transform.position.z - Camera.main.transform.position.z);
        sizeX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        sizeY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        if (sizeX > sizeY)
        {
            scale = new Vector3(sizeX * -0.2f, sizeX * -0.2f, 2);
        }
        if (sizeY > sizeX)
        {
            scale = new Vector3(sizeY * -0.2f, sizeY * -0.2f, 2);
        }
    }

    private void Update()
    {
        dist = (transform.position.z - Camera.main.transform.position.z);
        sizeX = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        sizeY = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;

        if(sizeX > sizeY)
        {
            scale = new Vector3(sizeX * -0.2f, sizeX * -0.2f, 2);
        }
        if(sizeY > sizeX)
        {
            scale = new Vector3(sizeY * -0.2f, sizeY * -0.2f, 2);
        }
        
        transform.localScale = scale;
    }
}
