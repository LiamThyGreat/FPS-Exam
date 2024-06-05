using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyScript : MonoBehaviour
{
    [SerializeField] Transform lookAtTransform;

    [Header("Health")]
    [SerializeField] float dummyMaxHealth;
    [SerializeField] float healingWaitTime;
    [SerializeField] float healingAmountPerTime;

    [Header("Health Bar")]
    [SerializeField] Image dummyHealthBar;

    private float dummyCurrentHealth;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();

        dummyCurrentHealth = dummyMaxHealth;
    }
    void Update()
    {
        transform.LookAt(lookAtTransform);

        dummyCurrentHealth = Mathf.Clamp(dummyCurrentHealth, 0, dummyMaxHealth);
        UpdateHealthBar();
    }

    public void Damaged(float damageTotake)
    {
        dummyCurrentHealth -= damageTotake;
        dummyCurrentHealth = Mathf.Clamp(dummyCurrentHealth, 0f, dummyMaxHealth);
        anim.SetTrigger("Hit");
        UpdateHealthBar();

        if(dummyCurrentHealth<= 0f)
        {
            StartCoroutine(AddHealthOverTime());
        }
    }

    void UpdateHealthBar()
    {
        dummyHealthBar.fillAmount = dummyCurrentHealth / dummyMaxHealth;
    }

    IEnumerator AddHealthOverTime()
    {
        while (dummyCurrentHealth < dummyMaxHealth)
        {
            dummyCurrentHealth += healingAmountPerTime;
            dummyCurrentHealth = Mathf.Clamp(dummyCurrentHealth, 0, dummyMaxHealth); // Ensure health doesn't exceed maxHealth
            yield return new WaitForSeconds(healingWaitTime);
        }
    }
}

