using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TextPoints_Script : MonoBehaviour
{
    public bool isBullet = false;
    private Text scoreText;

    private void Start()
    {
        if (!isBullet)
            scoreText = this.transform.GetChild(1).GetComponent<Text>();
        else if (isBullet)
            scoreText = this.transform.GetChild(0).GetComponent<Text>();
        scoreText.text = "";
    }


    public void UpdateText(string message)
    {
        scoreText.text = message;
        StartCoroutine(ClearTextDelay());
    }

    public void Death()
    {
        this.transform.parent.SetParent(null);
        if (!isBullet)
            Destroy(this.transform.GetChild(0).gameObject);            
        StartCoroutine(DeathDelay());
    }

    public void SetScale(Vector3 scale)
    {
        this.transform.localScale = scale;
    }

    public IEnumerator ClearTextDelay()
    {
        yield return new WaitForSeconds(1);
        scoreText.text = "";
    }
    public IEnumerator DeathDelay()
    {
        Debug.Log("death delay called.");
        yield return new WaitForSeconds(1.5f);
        Destroy(this.transform.root.gameObject);
    }
}
