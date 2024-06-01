using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] float speed;
    [SerializeField] float patrolDistance;
    [SerializeField] float shootingRange;
    [SerializeField] float shootingInterval;
    [SerializeField] float bulletSpeed;

    private Transform player;

    private Vector2 startPosition;
    private bool movingRight = true;
    private float nextShotTime;
    private bool isInPlayerRange;

    Rigidbody rb;

    void Start()
    {
        startPosition = transform.position;
        nextShotTime = Time.time;

        isInPlayerRange = false;

        rb = GetComponent<Rigidbody>();

        player = GameObject.FindGameObjectWithTag("Player")?.transform;
    }

    void Update()
    {
        if (!isInPlayerRange)
        {
            Patrol();
        }

        DetectAndShootPlayer();
    }

    void Patrol()
    {
        if (movingRight)
        {
            rb.MovePosition(transform.position + Vector3.right * speed * Time.deltaTime);
            if (Vector3.Distance(startPosition, transform.position) >= patrolDistance)
            {
                movingRight = false;
            }
        }
        else
        {
            rb.MovePosition(transform.position + Vector3.left * speed * Time.deltaTime);
            if (Vector3.Distance(startPosition, transform.position) >= patrolDistance)
            {
                movingRight = true;
            }
        }
    }

    void DetectAndShootPlayer()
    {
        if (player != null && Vector3.Distance(transform.position, player.position) <= shootingRange)
        {
            rb.velocity = Vector3.zero;
            transform.LookAt(player);

            isInPlayerRange = true;

            if (Time.time >= nextShotTime)
            {
                ShootAtPlayer();
                nextShotTime = Time.time + shootingInterval;
            }
        }
        else
        {
            isInPlayerRange = false;
        }
    }

    void ShootAtPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = direction * bulletSpeed;
    }
}
