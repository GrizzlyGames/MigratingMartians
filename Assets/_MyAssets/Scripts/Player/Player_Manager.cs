using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Player_Manager : MonoBehaviour
{
    public Game_Manager game;

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
        public float speed = 0.5f;
    }

    public Health armour = new Health();
    public class Health
    {
        public bool isAlive = true;
        public int currentArmour = 3;
        public int maxArmour = 3;
        public Image image;

        public void UpdateStatsBar()
        {
            image.fillAmount = ((float)currentArmour / (float)maxArmour);
        }
    }

    public Weapon weapon = new Weapon();
    public class Weapon
    {
        public float time = 0;
        public float fireRate = 3;
        public float bulletSpeed = 2;
        public Game_Manager _game;
        public AudioSource audioSource;
        public GameObject projectileGO;
        public Transform trashCollocter;
        public Transform spawnTransform;
        public Transform turretTransform;
        public bool canShoot;
        public Image reloadImage;
        public Image bulletImage;

        public void Shoot()
        {
            if (canShoot)
            {
                _game.statistics.playerBulletsFired++;
                _game.statistics.money -= 500;
                canShoot = false;
                reloadImage.fillAmount = 0;
                audioSource.Play();
                GameObject bullet = Instantiate(projectileGO, spawnTransform.position, spawnTransform.rotation) as GameObject;
                bullet.GetComponent<Player_Bullet>().speed = bulletSpeed;
                bullet.transform.SetParent(trashCollocter);                
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
        public float duration = 3.0f;
        public float time = 3.0f;
        public float rechargeTime = 0.15f;
        public CircleCollider2D collider;
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

    public Image healthImage;
    public Image reloadImage;
    public Image shieldImage;

    private void Start()
    {
        armour.image = healthImage;
        weapon.reloadImage = reloadImage;
        shield.image = shieldImage;

        game = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Game_Manager>();
        weapon._game = game;
        weapon.trashCollocter = game.trashCollocter;
        weapon.audioSource = GetComponent<AudioSource>();
        weapon.turretTransform = transform.GetChild(0).GetChild(0);
        weapon.spawnTransform = weapon.turretTransform.GetChild(0);
        weapon.projectileGO = projectile;
        shield.sprite = transform.GetChild(1).GetComponent<SpriteRenderer>();
        shield.collider = transform.GetChild(1).GetComponent<CircleCollider2D>();
    }
    private void Update()
    {
        #region Weapon
        if (!weapon.canShoot && game.statistics.waveStart)
        {
            weapon.time += Time.deltaTime;
            weapon.UpdateDisplay();
            if (weapon.time >= weapon.fireRate)
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

    private void OnMouseDown()
    {
        if (!shield.isActive && shield.time >= shield.duration)
        {
            shield.isActive = true;
        }
    }

    public void PlayerReset()
    {
        int armourRepaired = armour.maxArmour - armour.currentArmour;
        Debug.Log("Armour repaired " + armourRepaired);
        game.statistics.money -= (2500 * armourRepaired);
        Debug.Log("Armour repair cost: " + (2500 * armourRepaired));
        game.statistics.playerArmourRepairs += armourRepaired;
        armour.currentArmour = armour.maxArmour;
        weapon.time = weapon.fireRate;
        transform.GetChild(0).gameObject.SetActive(true);
        transform.GetChild(1).gameObject.SetActive(true);
    }
}