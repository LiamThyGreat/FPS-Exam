using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyBullet : MonoBehaviour
{
    [SerializeField] float damage;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(damage);

            Destroy(gameObject);
        }
    }
}
