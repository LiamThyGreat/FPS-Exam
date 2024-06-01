using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameArrow : BaseArrow
{
    [SerializeField] private GameObject fireEffect;
    [SerializeField] private float fireDamagePerSecond;
    [SerializeField] private float tickInterval;
    [SerializeField] private float duration;

    private bool hitEnemy;

    protected override void OnCollisionEnter(Collision collision)
    {
        base.OnCollisionEnter(collision);

        if (collision.gameObject.CompareTag("Enemy"))
        {
            hitEnemy = true;

            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                StartCoroutine(ApplyFireDamage(collision.gameObject));
            }
        }
        else if (collision.gameObject.CompareTag("Dummy"))
        {
            hitEnemy = false;

            DummyScript dummyScript = collision.gameObject.GetComponent<DummyScript>();
            if (dummyScript != null)
            {
                StartCoroutine(ApplyFireDamage(collision.gameObject));
            }
        }

        if (fireEffect != null)
        {
            Instantiate(fireEffect, transform.position, Quaternion.identity);
        }
    }

    private IEnumerator ApplyFireDamage(GameObject collision)
    {
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            yield return new WaitForSeconds(tickInterval);
            elapsedTime += tickInterval;

            if (hitEnemy)
            {
                collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(fireDamagePerSecond);
            }
            else
            {
                collision.gameObject.GetComponent<DummyScript>().Damaged(fireDamagePerSecond);
            }
        }

        if(elapsedTime > duration)
        {
            Destroy(gameObject);
        }
    }
}
