using UnityEngine;
using System.Collections;

public class ClampToScreen_Script : MonoBehaviour {
    public float margin = 0.75f;


    private float dist;
    private float leftLimitation;
    private float rightLimitation;
    private float upLimitation;
    private float downLimitation;

    private void Awake()
    {
        dist = (transform.position.z - Camera.main.transform.position.z);
        leftLimitation = (Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x * margin);
        rightLimitation = (Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x * margin);

        upLimitation = (Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y * margin);
        downLimitation = (Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y * margin);
    }

    private void Update()
    {           
        Vector3 clampedPosition = transform.position;

        clampedPosition.x = Mathf.Clamp(transform.position.x, leftLimitation, rightLimitation);
        clampedPosition.y = Mathf.Clamp(transform.position.y, upLimitation, downLimitation);

        transform.position = clampedPosition;
    }

    public Vector4 GetLimitations()
    {
        //Debug.Log("Get Limitation Called." + "\n Left: " + leftLimitation + "\n Right: " + rightLimitation + "\n Up: " + upLimitation + "\n Down: " + downLimitation);
        return (new Vector4(leftLimitation, rightLimitation, upLimitation, downLimitation));
    }
}
