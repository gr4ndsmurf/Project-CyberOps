using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Weapon : MonoBehaviour
{
    [Header("Bullet Object Pooling")]
    [SerializeField] private GameObject armPoint;
    [SerializeField] private int POOL_SIZE = 10;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointTransform;
    [SerializeField] private float bulletSpeed;
    private Queue<GameObject> bulletPool;

    [Header("Gun Effects")]
    [SerializeField] private string GunSoundName;
    [SerializeField] private string GunMuzzleFlashName;
    [SerializeField] private Animator muzzleFlash;

    [Header("Auto Fire")]
    private bool shootingDelayed;

    [Header("Ammo System")]
    [SerializeField] private int maxAmmo;
    [SerializeField] private int currentAmmo;
    [SerializeField] private float reloadTime = 1f;
    private bool isReloading = false;

    [Header("Ammo Box")]
    public int currentAmmoBox = 0;
    public int maxAmmoBox;

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

    IEnumerator Reload()
    {
        if (currentAmmoBox > 0)
        {
            isReloading = true;
            Debug.Log("Reloading...");

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

                    currentAmmo--;

                    muzzleFlash.Play(GunMuzzleFlashName);
                    AudioManager.Instance.Play(GunSoundName);
                    CameraShaker.Instance.ShakeOnce(1f, 4f, .1f, .5f);

                    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                    bulletRigidbody.velocity = new Vector2(shootDirection.x,shootDirection.y).normalized * bulletSpeed;

                    StartCoroutine(DisableBulletAfterDelay(bullet, 2f));
                }
            }
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
        var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        armPoint.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        //Flip
        Vector3 aimLocalScale = transform.localScale;
        Vector3 armPointLocalScale = armPoint.transform.localScale;
        if (angle > 90 || angle < -90)
        {
            aimLocalScale.x = -1f;
            armPointLocalScale.x = -1f;
            armPointLocalScale.y = -1f;
            transform.localPosition = new Vector3(0.05f, transform.localPosition.y, transform.localPosition.z);
        }
        else
        {
            aimLocalScale.x = +1f;
            armPointLocalScale.x = +1f;
            armPointLocalScale.y = +1f;
            transform.localPosition = new Vector3(0f, transform.localPosition.y, transform.localPosition.z);
        }
        transform.localScale = aimLocalScale;
        armPoint.transform.localScale = armPointLocalScale;
    }
}
