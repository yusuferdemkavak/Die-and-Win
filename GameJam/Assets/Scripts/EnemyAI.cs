using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public bool isNoticedYet;
    public bool isDead;

    public int moveSpeed;

    private int randomPatrollPoint;

    public float raycastLenght;
    public float retreatDistance;
    public float stoppingDistance;
    public float startTimeBetweenShots;
    public float projectileForce;
    public float startWaitTime;
    public float viewRadius = 5;
    public float viewAngle = 135;

    private float timeBetweenShots;
    private float waitTime;

    public GameObject projectilePrefab;
    public GameObject blood;

    public Transform firePoint;
    public Transform[] patrollPoints;

    private Transform target;
    private Transform teamMate;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        teamMate = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Transform>();

        timeBetweenShots = startTimeBetweenShots;
        waitTime = startWaitTime;

        randomPatrollPoint = Random.Range(0, patrollPoints.Length);

        isDead = false;
    }

    void Update()
    {
        //Rotating towards the player
        if (isNoticedYet)
        {
            Vector2 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }
        else
        {
            Vector2 direction = patrollPoints[randomPatrollPoint].position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);
        }

        //Chasing the player, reatreating from the player
        if (isNoticedYet)
        {
            if (Vector2.Distance(transform.position, target.transform.position) > stoppingDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, moveSpeed * Time.deltaTime);
            }
            else if (Vector2.Distance(transform.position, target.transform.position) < stoppingDistance && Vector2.Distance(transform.position, target.transform.position) > retreatDistance)
            {
                transform.position = this.transform.position;
            }
            else if (Vector2.Distance(transform.position, target.transform.position) < retreatDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, -moveSpeed * Time.deltaTime);
            }
        }
        //Patrolling
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, patrollPoints[randomPatrollPoint].position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, patrollPoints[randomPatrollPoint].position) < 0.2f)
            {
                if (waitTime <= 0)
                {
                    randomPatrollPoint = Random.Range(0, patrollPoints.Length);
                    waitTime = startWaitTime;
                }
                else
                {
                    waitTime -= Time.deltaTime;
                }
            }
        }
        
        //Shooting to the player
        if(timeBetweenShots <= 0 && isNoticedYet!)
        {
            Shoot();
            timeBetweenShots = startTimeBetweenShots;
        }
        else
        {
            timeBetweenShots -= Time.deltaTime;
        }

        if (isDead)
        {
            Instantiate(blood, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * projectileForce, ForceMode2D.Impulse);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            isNoticedYet = true;
        }
    }
}
