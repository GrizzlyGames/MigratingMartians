using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Player_Manager : MonoBehaviour
{
    public bool isMobile = true;
    public Game_Manager game;

    public Input input = new Input();
    [System.Serializable]
    public class Input
    {
        public Vector2 mStartPosition;
        public float mSwipeStartTime;

        public Vector3 mousePosition;
        public Vector3 direction;
        public float distanceFromObject;
    }

    public Movement movement = new Movement();
    [System.Serializable]
    public class Movement
    {
        public float speed;
        public int upgradeLevel = 0;
    }
        
    public Armour armour = new Armour();
    [System.Serializable]
    public class Armour
    {
        public bool isAlive = true;
        public int currentArmour = 1;
        public int maxArmour = 1;
        public int upgradeLevel = 0;
        public Image image;
        public Text armourHUDText;

        public void UpdateDisplay()
        {
            armourHUDText.text = currentArmour + " / " + maxArmour;
            image.fillAmount = ((float)currentArmour / (float)maxArmour);
        }
    }

    public Weapon weapon = new Weapon();
    [System.Serializable]
    public class Weapon
    {
        public float normalTime;
        public float specialTime;
        public float normalFireRate;
        public float specialFireRate;
        public float bulletSpeed;
        public int upgradeLevel = 0;
        public Game_Manager _game;
        public Animator anim;
        public AudioSource audioSource;
        public GameObject normalBulletGO;
        public GameObject specialBulletGO;
        public Transform trashCollocter;
        public Transform spawnTransform;
        public Transform turretTransform;
        public bool canShootNormal;
        public bool canShootSpecial;

        public Image weaponNormalReloadHUD_Image;
        public Image weaponSpecialReloadHUD_Image;

        public void Shoot()
        {
            if (canShootNormal)
            {
                anim.SetTrigger("shoot");
                canShootNormal = false;
                weaponNormalReloadHUD_Image.fillAmount = 0;
                audioSource.Play();
                GameObject bullet = Instantiate(normalBulletGO, spawnTransform.position, spawnTransform.rotation) as GameObject;
                bullet.GetComponent<Player_Bullet>().speed = bulletSpeed;
                bullet.transform.SetParent(trashCollocter);
                bullet.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
        }
        public void SpecialShot()
        {
            if (canShootSpecial)
            {
                anim.SetTrigger("shoot");
                weaponSpecialReloadHUD_Image.fillAmount = 0;
                canShootSpecial = false;
                audioSource.Play();
                GameObject bullet = Instantiate(specialBulletGO, spawnTransform.position, spawnTransform.rotation) as GameObject;
                bullet.GetComponent<Player_Bullet>().speed = bulletSpeed * 2;
                bullet.transform.SetParent(trashCollocter);
                bullet.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        public void UpdateNormalDisplay()
        {
            weaponNormalReloadHUD_Image.fillAmount = normalTime / normalFireRate;
        }

        public void UpdateSpecialDisplay()
        {
            weaponSpecialReloadHUD_Image.fillAmount = specialTime / specialFireRate;
        }
    }

    public Shield shield = new Shield();
    [System.Serializable]
    public class Shield
    {
        public bool isActive;
        public float shieldTotal;
        public float rechargeRate;
        public int upgradeLevel = 0;

        public GameObject shieldGO;
        public Sprite[] shieldSprites;
        public SpriteRenderer shieldSpriteRenderer;
        public Image shieldHUD_Image;
        public Text shieldHUD_Text;
        public CircleCollider2D collider;

        public void UpdateDisplay()
        {
            float amount = ((float)shieldTotal / (float)1.0f);
            shieldHUD_Text.text = amount.ToString("P00");
            shieldHUD_Image.fillAmount = amount;
        }
    }

    private void Update()
    {
        #region Weapon
        if (!weapon.canShootNormal)
        {
            weapon.normalTime += Time.deltaTime;
            weapon.UpdateNormalDisplay();
            if (weapon.normalTime >= weapon.normalFireRate)
            {
                weapon.canShootNormal = true;
                weapon.normalTime = 0;
            }
        }
        if (!weapon.canShootSpecial)
        {
            weapon.specialTime += Time.deltaTime;
            weapon.UpdateSpecialDisplay();
            if (weapon.specialTime >= weapon.specialFireRate)
            {
                weapon.canShootSpecial = true;
                weapon.specialTime = 0;
            }
        }
        #endregion
        #region Movement
        float translation;
        if (isMobile)
        {
            translation = (UnityEngine.Input.acceleration.x * 2) * movement.speed;
        }
        else
            translation = UnityEngine.Input.GetAxis("Horizontal") * movement.speed;

        translation *= Time.deltaTime;
        transform.position = new Vector3(this.transform.position.x + translation, this.transform.position.y, 0);
        #endregion
    }
    public void ResetVariables()
    {
        transform.GetChild(0).gameObject.SetActive(true); // enable armour

        movement.upgradeLevel = 0;
        movement.speed = 1;

        armour.upgradeLevel = 0;
        armour.isAlive = true;
        armour.maxArmour = 1;
        armour.currentArmour = armour.maxArmour;

        weapon.upgradeLevel = 0;
        weapon.canShootNormal = false;
        weapon.canShootSpecial = false;
        weapon.normalTime = 0;
        weapon.normalFireRate = 2.5f;
        weapon.specialFireRate = 15;
        weapon.bulletSpeed = 5;

        transform.GetChild(1).gameObject.SetActive(true); // enable shield
        shield.shieldSpriteRenderer.sprite = shield.shieldSprites[0];
        shield.upgradeLevel = 0;
        shield.isActive = true;
        shield.shieldTotal = 1;
        shield.rechargeRate = 0.05f;
    }
    public void WaveReset()
    {
        game.statistics.waveTime = 0;
        weapon.canShootNormal = false;
        weapon.canShootSpecial = false;

        armour.currentArmour = armour.maxArmour;
        armour.UpdateDisplay();  

        shield.shieldTotal = 1;
        shield.UpdateDisplay();
        transform.GetChild(1).gameObject.SetActive(true); // enable shield

        // reload weapons
        weapon.normalTime = weapon.normalFireRate;
        weapon.specialTime = weapon.specialFireRate;
    }
}