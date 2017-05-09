using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Player_Manager : MonoBehaviour
{
    public int state = 0;
    public GameObject[] stateGO;

    public bool isEnabled = true;

    private Game_Manager game;

    public GameObject projectile;

    public Input input = new Input();
    public class Input
    {
        public float mMinSwipeDist = 10.0f;
        public float mMinVelocity = 500.0f;

        public Vector2 mStartPosition;
        public float mSwipeStartTime;

        public Vector3 mousePosition;
        public Vector3 direction;
        public float distanceFromObject;
    }

    public Movement movement = new Movement();
    public class Movement
    {
        public float speed = 1;
    }

    public Health health = new Health();
    public class Health
    {
        public bool isAlive = true;
        public int currentHealth = 3;
        public int maxHealth = 3;
        public Image image;

        public void UpdateBar()
        {
            image.fillAmount = ((float)currentHealth / (float)maxHealth);
        }
    }

    public Weapon weapon = new Weapon();
    public class Weapon
    {
        public float time = 0;
        public float fireRate = 2;
        public float bulletSpeed = 10;
        public AudioSource audioSource;
        public GameObject projectileGO;
        public Transform spawnTransform;
        public Transform turretTransform;
        public bool canShoot;
        public Image reloadImage;
        public Image bulletImage;

        public void Shoot()
        {
            if (canShoot)
            {
                canShoot = false;
                reloadImage.fillAmount = 0;
                audioSource.Play();
                GameObject go = Instantiate(projectileGO, spawnTransform.position, spawnTransform.rotation) as GameObject;
                go.GetComponent<Player_Bullet>().speed = bulletSpeed;
            }
        }

        public void UpdateDisplay()
        {
            reloadImage.fillAmount = time / fireRate;
        }
    }

    public Shield shield = new Shield();
    public class Shield
    {
        public bool isActive = false;
        public float time = 1.0f;
        public float duration = 3.0f;
        public int durability = 1;
        public float rechargeTime = 0.05f;
        public Image image;
        public SpriteRenderer sprite;

        public void UpdateDisplay()
        {
            float amount = ((float)time / (float)duration);
            image.fillAmount = amount;
            float alpha = (float)255 * (float)amount;
            sprite.color = new Color32(0, 255, 255, (byte)alpha);
        }
    }

    private void StateChecker()
    {
        for (int i = 0; i < stateGO.Length; i++)
        {
            if (i == state)
                stateGO[i].SetActive(true);
            else
                stateGO[i].SetActive(false);
        }
    }

    private void Awake()
    {
        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        weapon.audioSource = GetComponent<AudioSource>();
        weapon.turretTransform = transform.GetChild(0).GetChild(0);
        weapon.spawnTransform = weapon.turretTransform.GetChild(0);
        weapon.projectileGO = projectile;
        health.image =  GameObject.Find("Health Image").GetComponent<Image>();
        health.currentHealth = health.maxHealth;
        weapon.reloadImage = GameObject.Find("Reload Image").GetComponent<Image>();
        shield.sprite = GameObject.Find("Shield Sprite").GetComponent<SpriteRenderer>();
        shield.image = GameObject.Find("Shield Image").GetComponent<Image>();
        shield.time = shield.duration;
        shield.UpdateDisplay();
    }
    private void Update()
    {
        #region Weapon
        if (!weapon.canShoot && game.status.waveStart)
        {
            weapon.time += Time.deltaTime;
            weapon.UpdateDisplay();
            if (Mathf.Abs((int)weapon.time) >= weapon.fireRate)
            {
                weapon.canShoot = true;
                weapon.time = 0;
            }
        }
        #endregion
        #region Movement
        float translation = UnityEngine.Input.GetAxis("Horizontal") * movement.speed;
        translation *= Time.deltaTime;
        transform.position = new Vector3(this.transform.position.x + translation, this.transform.position.y, 0);
        #endregion
    }

    public void SetEnabled(bool state)
    {
        isEnabled = state;
        if (state)
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            this.transform.GetChild(0).gameObject.SetActive(false);
            this.transform.GetChild(1).gameObject.SetActive(false);
        }
    }
    private void OnMouseDown()
    {
        if (!shield.isActive && shield.time >= shield.duration)
        {
            shield.time = shield.duration;
            shield.isActive = true;
        }
    }
}