using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurukenScript : MonoBehaviour
{
    [SerializeField] float speed = 20f;

    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = transform.forward * speed; // Set initial velocity

        StartCoroutine(DestroyShurukenAfterTime());
    }

    void OnCollisionEnter(Collision collision)
    {
        FindObjectOfType<ShootShuruken>().canThrowShuruken = true;

        // Destroy the projectile after hitting something
        Destroy(gameObject);
    }

    IEnumerator DestroyShurukenAfterTime()
    {
        yield return new WaitForSeconds(5f);

        FindObjectOfType<ShootShuruken>().canThrowShuruken = true;

        Destroy(gameObject);
    }
}
