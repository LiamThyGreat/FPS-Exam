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
    
    private float enemyHealth;
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
        }
        else
        {
            enemyHealthCanvas.SetActive(false);
        }
    }
}
