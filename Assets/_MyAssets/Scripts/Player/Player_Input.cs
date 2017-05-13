using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Player_Manager))]
public class Player_Input : MonoBehaviour
{
    private Player_Manager player;

    private void Start()
    {
        player = GetComponent<Player_Manager>();
    }

    private void Update()
    {
        if (player.armour.isAlive)
        {
            if (Input.GetMouseButtonDown(0))
            {
                player.input.mStartPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                player.input.mSwipeStartTime = Time.time;
            }
            if (Input.GetMouseButtonUp(0))
            {
                float deltaTime = Time.time - player.input.mSwipeStartTime;
                Vector2 endPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
                Vector2 swipeVector = endPosition - player.input.mStartPosition;
                float velocity = swipeVector.magnitude / deltaTime;
                if (velocity > player.input.mMinVelocity && swipeVector.magnitude > player.input.mMinSwipeDist)
                {
                    player.weapon.Shoot();
                }
            }
            if (Input.GetMouseButtonDown(1))
            {
                player.weapon.Shoot();
            }

            player.input.mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z - Camera.main.transform.position.z));
            player.weapon.turretTransform.eulerAngles = new Vector3(0, 0, Mathf.Atan2((player.input.mousePosition.y - transform.position.y), (player.input.mousePosition.x - transform.position.x)) * Mathf.Rad2Deg - 90);
            player.input.distanceFromObject = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).magnitude;
            player.weapon.turretTransform.Translate(player.input.direction * player.input.distanceFromObject * Time.deltaTime);
        }
    }
}
