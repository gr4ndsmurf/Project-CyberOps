using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class GangstaController : MonoBehaviour
{
    [Header("States")]
    public GangstaChase chase;
    public GangstaAttack attack;

    [Header("Pathfinding")]
    public Transform target;
    public float activateDistance = 50f;
    public float pathUpdateSeconds = 0.5f;
    private Path path;
    private int currentWaypoint = 0;
    private Seeker seeker;

    [Header("Physics")]
    public float speed = 200f;
    public float idleSpeed = 3f;
    public float chaseSpeed = 750f;
    public float nextWaypointDistance = 3f;
    public float jumpNodeHeightRequirement = 0.25f;
    public float jumpModifier = 0.5f;
    public float jumpCheckOffset = 0.1f;
    private bool isGrounded;

    [Header("Custom Behavior")]
    public bool followEnabled = true;
    public bool jumpEnabled = true;
    public bool directionLookEnabled = true;

    [Header("WeaponSystem")]
    public float damping = 0.01f;

    [SerializeField] private int POOL_SIZE = 10;
    public GameObject bulletPrefab;
    public Transform firePointTransform;
    public float bulletSpeed = 15f;
    public float shootingTime = 0.5f;
    public float shootingDelay = 0.5f;
    [SerializeField] private Animator muzzleFlash;

    public Queue<GameObject> bulletPool;

    public Transform armPoint;


    public Rigidbody2D rb;
    public Animator animator;

    private void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = idleSpeed;

        InvokeRepeating("UpdatePath", 0f, pathUpdateSeconds);

        //Object Pool
        bulletPool = new Queue<GameObject>();

        for (int i = 0; i < POOL_SIZE; i++)
        {
            GameObject bullet = Instantiate(bulletPrefab, Vector3.zero, Quaternion.identity);
            bullet.SetActive(false);
            bulletPool.Enqueue(bullet);
        }
    }

    private void UpdatePath()
    {
        if (followEnabled && TargetInDistance() && seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    public void PathFollow()
    {
        if (path == null)
        {
            return;
        }

        // Reached end of path
        if (currentWaypoint >= path.vectorPath.Count)
        {
            return;
        }

        // See if colliding with anything

        // Direction Calculation
        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        // Jump
        if (jumpEnabled && isGrounded)
        {
            if (direction.y > jumpNodeHeightRequirement)
            {
                rb.AddForce(Vector2.up * speed * jumpModifier);
            }
        }

        // Movement
        rb.AddForce(force);

        // Next Waypoint
        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);
        if (distance < nextWaypointDistance)
        {
            currentWaypoint++;
        }

        // Direction Graphics Handling
        if (directionLookEnabled)
        {
            if (rb.velocity.x <= 0.01f)
            {
                transform.localScale = new Vector3(-1f * Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else if (rb.velocity.x >= -0.01f)
            {
                transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
        }
    }

    public bool TargetInDistance()
    {
        return Vector2.Distance(transform.position, target.transform.position) < activateDistance;
    }

    public void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }

    //Object Pooling
    public void HandleShooting()
    {
        if (GetComponentInChildren<Health>().CurrentHealth > 0 && !GameManager.Instance.isDead)
        {
            GameObject bullet = GetBulletFromPool();

            Vector3 shootDirection = (target.position - transform.position).normalized;
            if (bullet != null)
            {
                bullet.transform.position = firePointTransform.position;
                bullet.transform.rotation = firePointTransform.rotation;
                bullet.SetActive(true);

                muzzleFlash.Play("Rifle_MuzzleFlash");
                muzzleFlash.keepAnimatorStateOnDisable = true;

                Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
                bulletRigidbody.velocity = shootDirection * bulletSpeed;

                StartCoroutine(DisableBulletAfterDelay(bullet, 2f));
            }
        }
    }

    public void CheckVelocity(GangstaController controller)
    {
        if (controller.rb.velocity.x == 0)
        {
            controller.animator.SetBool("Idle", true);
            controller.animator.SetBool("Walk", false);
        }
        else
        {
            controller.animator.SetBool("Idle", false);
            controller.animator.SetBool("Walk", true);
        }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            isGrounded = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Obstacle"))
        {
            isGrounded = false;
        }
    }
}
