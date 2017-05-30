using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Player_Manager : MonoBehaviour
{
    public bool isMobile = true;
    public Game_Manager game;

    public GameObject normalBulletGO;
    public GameObject specialBulletGO;

    public Input input = new Input();
    public class Input
    {
        public Vector2 mStartPosition;
        public float mSwipeStartTime;

        public Vector3 mousePosition;
        public Vector3 direction;
        public float distanceFromObject;
    }

    public Movement movement = new Movement();
    public class Movement
    {
        public float speed;
        public int upgradeLevel = 0;
    }

    public Armour armour = new Armour();
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
    public class Weapon
    {
        public float normalTime;
        public float specialTime;
        public float normalFireRate;
        public float specialFireRate;
        public float bulletSpeed;
        public int upgradeLevel = 0;
        public Game_Manager _game;
        public AudioSource audioSource;
        public GameObject projectileGO;
        public GameObject specialBullet;
        public Transform trashCollocter;
        public Transform spawnTransform;
        public Transform turretTransform;
        public bool canShootNormal;
        public bool canShootSpecial;
        public Image normalReloadImage;
        public Image specialReloadImage;
        public Image bulletImage;

        public void Shoot()
        {
            if (canShootNormal)
            {
                _game.statistics.playerBulletsFired++;
                _game.statistics.money -= 500;
                canShootNormal = false;
                normalReloadImage.fillAmount = 0;
                audioSource.Play();
                GameObject bullet = Instantiate(projectileGO, spawnTransform.position, spawnTransform.rotation) as GameObject;
                bullet.GetComponent<Player_Bullet>().speed = bulletSpeed;
                bullet.transform.SetParent(trashCollocter);
                bullet.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
            }
        }
        public void SpecialShot()
        {
            if (canShootSpecial)
            {
                specialReloadImage.fillAmount = 0;
                _game.statistics.playerSpecialBulletsFired++;
                _game.statistics.money -= 2500;
                canShootSpecial = false;
                audioSource.Play();
                GameObject bullet = Instantiate(specialBullet, spawnTransform.position, spawnTransform.rotation) as GameObject;
                bullet.GetComponent<Player_Bullet>().speed = bulletSpeed * 2;
                bullet.transform.SetParent(trashCollocter);
                bullet.transform.localScale = new Vector3(1, 1, 1);
            }
        }

        public void UpdateNormalDisplay()
        {
            normalReloadImage.fillAmount = normalTime / normalFireRate;
        }

        public void UpdateSpecialDisplay()
        {
            specialReloadImage.fillAmount = specialTime / specialFireRate;
        }
    }

    public Shield shield = new Shield();
    public class Shield
    {
        public bool isActive;
        public float shieldTotal;
        public float rechargeRate;
        public int upgradeLevel = 0;

        public Image image;
        public Text shieldHUDText;
        public CircleCollider2D collider;

        public void UpdateDisplay()
        {
            float amount = ((float)shieldTotal / (float)1.0f);
            shieldHUDText.text = amount.ToString("P00");
            image.fillAmount = amount;
        }
    }

    public Image healthImage;
    public Image normalReloadImage;
    public Image specialReloadImage;
    public Image shieldImage;

    private void Start()
    {
        armour.image = healthImage;
        weapon.normalReloadImage = normalReloadImage;
        weapon.specialReloadImage = specialReloadImage;
        shield.image = shieldImage;
        armour.armourHUDText = GameObject.Find("armourHUDText").GetComponent<Text>();
        shield.shieldHUDText = GameObject.Find("shieldHUDText").GetComponent<Text>();
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        weapon._game = game;
        weapon.specialBullet = specialBulletGO;
        weapon.trashCollocter = game.trashCollocter;
        weapon.audioSource = GetComponent<AudioSource>();
        weapon.turretTransform = transform.GetChild(0).GetChild(0);
        weapon.spawnTransform = weapon.turretTransform.GetChild(0).GetChild(0);
        weapon.projectileGO = normalBulletGO;
        shield.collider = transform.GetChild(1).GetComponent<CircleCollider2D>();
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
        weapon.normalFireRate = 3;
        weapon.specialFireRate = 15;
        weapon.bulletSpeed = 5;

        transform.GetChild(1).gameObject.SetActive(true); // enable shield
        shield.upgradeLevel = 0;
        shield.isActive = true;
        shield.shieldTotal = 1;
        shield.rechargeRate = 0.05f;
    }
    public void WaveReset()
    {
        game.statistics.waveTime = 0;
        Debug.Log("wave time: " + game.statistics.waveTime);

        weapon.canShootNormal = false;
        weapon.canShootSpecial = false;

        armour.currentArmour = armour.maxArmour;
        armour.UpdateDisplay();
        int armourRepaired = armour.maxArmour - armour.currentArmour;
        game.statistics.money -= (2500 * armourRepaired);
        game.statistics.playerArmourRepairs += armourRepaired;        

        shield.shieldTotal = 1;
        shield.UpdateDisplay();
        transform.GetChild(1).gameObject.SetActive(true); // enable shield

        // reload weapons
        weapon.normalTime = weapon.normalFireRate;
        weapon.specialTime = weapon.specialFireRate;
    }
}