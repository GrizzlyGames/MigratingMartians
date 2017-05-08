using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utility_DestroyOffScreen : MonoBehaviour {

    private float dist;
    private float leftLimitation;
    private float rightLimitation;
    private float upLimitation;
    private float downLimitation;

    private void Awake()
    {
        dist = (transform.position.z - Camera.main.transform.position.z);
        leftLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        rightLimitation = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        downLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        upLimitation = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;
    }

    private void Update()
    {
        if (transform.position.x > rightLimitation || transform.position.x < leftLimitation || transform.position.y > upLimitation || transform.position.y < downLimitation)
        {
            Destroy(gameObject);
        }
    }
}
