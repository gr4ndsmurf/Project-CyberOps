using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Weapon : MonoBehaviour
{
    // Add this script to any weapon gameobject in WeaponSystem gameobject.
    [Header("Gun Upperbody Object From WeaponHolder")]
    [Tooltip("Select the gameobject in WeaponHolder")]
    [SerializeField] private GameObject gun;

    [Header("Bullet Object Pooling")]
    [Tooltip("Select armPoint in this gameobject")]
    [SerializeField] private GameObject armPoint;
    [Tooltip("it depends on how many bullets you want in your gun.")]
    [SerializeField] private int POOL_SIZE = 10;
    [Tooltip("Select any bullet in Prefabs folder.")]
    [SerializeField] private GameObject bulletPrefab;
    [Tooltip("Select firePoint gameobject in this gun object.")]
    [SerializeField] private Transform firePointTransform;
    [Tooltip("Default: Pistol=1500 Rifle=2500")]
    [SerializeField] private float bulletSpeed;
    private Queue<GameObject> bulletPool;

    [Header("Gun Effects")]
    [Tooltip("Write your gunsoundname in AudioManager")]
    [SerializeField] private string GunSoundName;
    [SerializeField] private string GunMuzzleFlashName;
    [SerializeField] private Animator muzzleFlash;
    [Tooltip("Default: Pistol:250 Rifle:400")]
    [SerializeField] private float recoilPower = 400f;

    [Header("Auto Fire")]
    private bool shootingDelayed;

    [Header("Ammo System")]
    [Tooltip("Default: Pistol=12 Rifle=25")]
    [SerializeField] private int maxAmmo;
    public int currentAmmo;
    [Tooltip("Default: 3")]
    [SerializeField] private float reloadTime = 1f;
    public bool isReloading = false;

    [Header("Ammo Box")]
    public int currentAmmoBox = 0;
    [Tooltip("Default: Pistol=3 Rifle=5")]
    public int maxAmmoBox;

    [Header("Player")]
    [SerializeField] private GameObject player;

    private void Start()
    {
        bulletPool = new Queue<GameObject>();

        for (int i = 0; i < POOL_SIZE; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }

        currentAmmo = maxAmmo;
    }
    void Update()
    {
        if (gun.activeInHierarchy && GameManager.Instance.canAttack)
        {
            currentAmmoBox = Mathf.Clamp(currentAmmoBox, 0, maxAmmoBox);

            WeaponAiming();

            if (Input.GetKeyDown(KeyCode.R) && currentAmmo < maxAmmo)
            {
                StartCoroutine(Reload());
            }

            if (isReloading)
            {
                return;
            }

            if (currentAmmo <= 0)
            {
                StartCoroutine(Reload());
                return;
            }

            WeaponShooting();
        }

        
    }

    IEnumerator Reload()
    {
        if (currentAmmoBox > 0)
        {
            isReloading = true;
            Debug.Log("Reloading...");

            AudioManager.Instance.Play("ReloadSound");

            currentAmmoBox--;

            yield return new WaitForSeconds(reloadTime);

            currentAmmo = maxAmmo;
            isReloading = false;
        }
        else
        {
            Debug.Log("not enough ammo boxes");
        }
    }

    private void WeaponShooting()
    {
        if (Input.GetMouseButton(0))
        {
            if (shootingDelayed == false)
            {
                StartCoroutine(shootingDelay());
                shootingDelayed = true;
                Vector3 mousePosition = Input.mousePosition;
                Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
                Vector3 shootDirection = (mouseWorldPosition - transform.position).normalized;

                GameObject bullet = GetBulletFromPool();

                if (bullet != null)
                {
                    bullet.transform.position = firePointTransform.position;
                    bullet.transform.rotation = firePointTransform.rotation;
                    bullet.SetActive(true);
                    //bullet.GetComponent<BoxCollider2D>().enabled = true;

                    currentAmmo--;

                    muzzleFlash.Play(GunMuzzleFlashName);
                    muzzleFlash.keepAnimatorStateOnDisable = true;
                    AudioManager.Instance.Play(GunSoundName);
                    if (recoilPower <= 250)
                    {
                        CameraShaker.Instance.ShakeOnce(2.5f, 1.5f, .1f, .1f);
                    }
                    else if (recoilPower > 250)
                    {
                        CameraShaker.Instance.ShakeOnce(3f, 1.75f, .1f, .1f);
                    }

                    Recoil();

                    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                    bulletRigidbody.velocity = new Vector2(shootDirection.x,shootDirection.y).normalized * bulletSpeed * Time.deltaTime;

                    StartCoroutine(DisableBulletAfterDelay(bullet, 2f));
                }
            }
        }
    }

    private void Recoil()
    {
        if (player.transform.position.x < firePointTransform.position.x)
        {
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
            playerRB.AddForce(new Vector2(-recoilPower, 0),ForceMode2D.Force);
        }
        else if (player.transform.position.x > firePointTransform.position.x)
        {
            Rigidbody2D playerRB = player.GetComponent<Rigidbody2D>();
            playerRB.AddForce(new Vector2(recoilPower, 0),ForceMode2D.Force);
        }
    }

    IEnumerator shootingDelay()
    {
        yield return new WaitForSeconds(0.25f);
        shootingDelayed = false;
    }
    private IEnumerator DisableBulletAfterDelay(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        ReturnBulletToPool(bullet);
    }

    private GameObject GetBulletFromPool()
    {
        if (bulletPool.Count > 0)
        {
            GameObject bullet = bulletPool.Dequeue();
            bullet.SetActive(true);
            return bullet;
        }

        return null;
    }
    private void ReturnBulletToPool(GameObject bullet)
    {
        bullet.SetActive(false);
        bulletPool.Enqueue(bullet);
    }

    private void WeaponAiming()
    {
        Vector3 dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        armPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        //Flip
        Vector3 aimLocalScale = transform.localScale;
        Vector3 armPointLocalScale = armPoint.transform.localScale;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.x = -1f;
            armPointLocalScale.x = -1f;
            armPointLocalScale.y = -1f;
            Vector3 newpos = new Vector3(0.05f, 0.05f, 0f);
            transform.localPosition = new Vector3(0.05f, 0.05f, 0f);
            gun.transform.localPosition = new Vector3(0.05f, 0.05f, 0f);
        }
        else
        {
            aimLocalScale.x = +1f;
            armPointLocalScale.x = +1f;
            armPointLocalScale.y = +1f;
            transform.localPosition = new Vector3(0f, 0.05f, 0f);
            gun.transform.localPosition = new Vector3(0f, 0.05f, 0f);
        }
        transform.localScale = aimLocalScale;
        armPoint.transform.localScale = armPointLocalScale;
        gun.transform.localScale = aimLocalScale;
    }
}
