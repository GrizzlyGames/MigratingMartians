using UnityEngine;
using System.Collections;

public class DestroySelfByTime_Script : MonoBehaviour {

    public bool autoDestroy;
    public float destoryTime;

    private void Start()
    {
        StartCoroutine("DestroyDelay");
    }

    IEnumerator DestroyDelay()
    {
        yield return new WaitForSeconds(destoryTime);
        Destroy(gameObject);
    }
}
