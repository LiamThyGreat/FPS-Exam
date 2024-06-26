using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] GameObject deathCanvas;
    [SerializeField] Image healthBar;
    [SerializeField] float maxHealth;

    private float currentHealth;
    public bool isBlocking;

    SceneLoader sceneLoader;

    private void Start()
    {
        if(deathCanvas == null)
        {
            Debug.Log("No Death Canvas");
        }
        else
        {
            deathCanvas.SetActive(false);
        }

        sceneLoader = FindObjectOfType<SceneLoader>();
        
        currentHealth = maxHealth;
    }

    public void TakeDamage(float damageToTake)
    {
        if (!isBlocking)
        {
            currentHealth -= damageToTake;
            currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);
            UpdateHealthBar();

            Debug.Log(currentHealth);

            if (currentHealth <= 0f)
            {
                if (deathCanvas != null)
                {
                    deathCanvas.SetActive(true);
                    Time.timeScale = 0f;
                    gameObject.SetActive(false);
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    sceneLoader.ReloadScene();
                }
            }
        }
    }

    private void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / maxHealth;
    }
}
