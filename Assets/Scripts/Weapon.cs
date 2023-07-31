using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject armPoint;

    private const int POOL_SIZE = 10;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointTransform;
    [SerializeField] private float bulletSpeed;

    private Queue<GameObject> bulletPool;

    [SerializeField] private string GunSoundName;
    [SerializeField] private string GunMuzzleFlashName;

    private bool shootingDelayed;

    [SerializeField] private Animator muzzleFlash;
    private void Start()
    {
        bulletPool = new Queue<GameObject>();

        for (int i = 0; i < POOL_SIZE; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }
    void Update()
    {
        WeaponAiming();
        WeaponShooting();
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

                muzzleFlash.Play(GunMuzzleFlashName);

                GameObject bullet = GetBulletFromPool();

                if (bullet != null)
                {
                    bullet.transform.position = firePointTransform.position;
                    bullet.transform.rotation = firePointTransform.rotation;
                    bullet.SetActive(true);

                    AudioManager.Instance.Play(GunSoundName);

                    Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                    bulletRigidbody.velocity = shootDirection * bulletSpeed;

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
