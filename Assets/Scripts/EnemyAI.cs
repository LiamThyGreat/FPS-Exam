using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    [SerializeField] GameObject arrowPrefab;
    [SerializeField] Transform arrowSpawnPoint;
    [SerializeField] float walkDuration = 2f;
    [SerializeField] float stopDuration = 1f;
    [SerializeField] float detectionRange = 5f;
    [SerializeField] float shootCooldown = 1f;
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float shootSpeed;

    private EnemyState currentState = EnemyState.WalkingForward;
    private Transform player;
    private float stateTimer;
    private float walkTimer;
    private bool playerDetected;
    private bool readyToShoot = true;

    private void Start()
    {
        player = FindObjectOfType<PlayerHealth>().transform;

        stateTimer = walkDuration;
        walkTimer = walkDuration;
    }

    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        playerDetected = distanceToPlayer <= detectionRange;

        switch (currentState)
        {
            case EnemyState.WalkingForward:
                Walk(Vector3.forward);
                break;
            case EnemyState.WalkingBackward:
                Walk(Vector3.back);
                break;
            case EnemyState.Stopped:
                StopAndShoot();
                break;
        }

        if (playerDetected)
        {
            currentState = EnemyState.Stopped;
        }
        else
        {
            stateTimer -= Time.deltaTime;

            if (stateTimer <= 0)
            {
                if (currentState == EnemyState.WalkingForward)
                {
                    currentState = EnemyState.WalkingBackward;
                }
                else if (currentState == EnemyState.WalkingBackward)
                {
                    currentState = EnemyState.WalkingForward;
                }

                stateTimer = walkTimer;
            }
        }
    }

    private void Walk(Vector3 direction)
    {
        transform.Translate(direction * moveSpeed * Time.deltaTime);
    }

    private void StopAndShoot()
    {
        if (readyToShoot)
        {
            StartCoroutine(ShootArrow());
        }
    }

    private IEnumerator ShootArrow()
    {
        readyToShoot = false;
        yield return new WaitForSeconds(stopDuration);

        Vector3 directionToPlayer = (player.position - arrowSpawnPoint.position).normalized;
        GameObject projectile = Instantiate(arrowPrefab, arrowSpawnPoint.position, Quaternion.LookRotation(directionToPlayer));
        Rigidbody rb = projectile.GetComponent<Rigidbody>();
        rb.velocity = directionToPlayer * shootSpeed;

        yield return new WaitForSeconds(shootCooldown);
        readyToShoot = true;

        // If the player is still in range, continue stopping and shooting
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if (distanceToPlayer > detectionRange)
        {
            currentState = currentState == EnemyState.WalkingForward ? EnemyState.WalkingForward : EnemyState.WalkingBackward;
        }
    }
}
