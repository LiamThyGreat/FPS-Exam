using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] Image enemyHealthBar;
    [SerializeField] ParticleSystem deathParticle;
    [SerializeField] float enemyMaxHealth;

    [Header("Player Checking")]
    [SerializeField] GameObject enemyHealthCanvas;
    [SerializeField] float playerCheckRadius;
    [SerializeField] float timeTillCanvasTurnsOff;
    
    private float enemyHealth;
    private bool canvasIsOn = false;
    private bool enemyUnderAttack = false;
    private Transform playerTransform;

    void Start()
    {
        playerTransform = FindObjectOfType<PlayerHealth>().transform;

        enemyHealth = enemyMaxHealth;
        UpdateHealthBar();
    }

    private void Update()
    {
        CheckDistanceToPlayer();
    }

    public void TakeDamage(float damage)
    {
        if (!canvasIsOn)
        {
            enemyHealthCanvas.SetActive(true);
        }

        enemyUnderAttack = true;
        StartCoroutine(CanvasTurnOffRoutine());
        enemyHealth -= damage;
        enemyHealth = Mathf.Clamp(enemyHealth, 0f, enemyMaxHealth);
        UpdateHealthBar();

        if (enemyHealth <= 0f)
        {
            if(deathParticle != null)
            {
                deathParticle.Play();
            }

            Destroy(gameObject);
        }
    }

    void UpdateHealthBar()
    {
        enemyHealthBar.fillAmount = enemyHealth / enemyMaxHealth;
    }

    private void CheckDistanceToPlayer()
    {
        float distanceToPlayer = Vector3.Distance(playerTransform.position, transform.position);

        if(distanceToPlayer <= playerCheckRadius)
        {
            enemyHealthCanvas.SetActive(true);

            canvasIsOn = true;
        }
        else if(!enemyUnderAttack)
        {
            enemyHealthCanvas.SetActive(false);

            canvasIsOn = false;
        }
    }

    IEnumerator CanvasTurnOffRoutine()
    {
        yield return new WaitForSeconds(timeTillCanvasTurnsOff);

        enemyUnderAttack = false;
    }
}
