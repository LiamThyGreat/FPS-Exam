using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseArrow : MonoBehaviour
{
    [SerializeField] protected float arrowDamage;
    [SerializeField] protected float shootSpeed;

    protected Transform enemy;
    protected Rigidbody rb;
    protected Collider bc;

    protected virtual void Awake()
    {
        AudioManager audioManager = FindObjectOfType<AudioManager>();
        if(audioManager != null)
        {
            audioManager.PlaySFX();
        }

        bc = GetComponent<Collider>();
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody component not found on " + gameObject.name);
        }
    }

    protected virtual void Update()
    {
        if(enemy != null)
        {
            transform.position = enemy.transform.position;
        }
    }

    public virtual void Shoot(Vector3 direction)
    {
        if (rb == null)
        {
            Debug.LogError("Rigidbody not initialized for " + gameObject.name);
            return;
        }
        rb.velocity = direction * shootSpeed;
    }

    protected virtual void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            bc.enabled = false;

            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(arrowDamage);

            enemy = collision.gameObject.transform;

            rb.constraints = RigidbodyConstraints.FreezeAll;

            StartCoroutine(DestroyArrow());
        }
        else if (collision.gameObject.CompareTag("Dummy"))
        {
            bc.enabled = false;

            collision.gameObject.GetComponent<DummyScript>().Damaged(arrowDamage);

            rb.constraints = RigidbodyConstraints.FreezeAll;

            StartCoroutine(DestroyArrow());
        }
        else
        {
            Debug.Log("Skibidi");
            rb.constraints = RigidbodyConstraints.FreezeAll;

            StartCoroutine(DestroyArrow());
        }
    }

    IEnumerator DestroyArrow()
    {
        yield return new WaitForSeconds(10f);

        Destroy(gameObject);
    }
}

