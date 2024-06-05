using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootShuruken : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] Transform arrowSpawnPoint;
    [SerializeField] KeyCode shootShuruken = KeyCode.Q;
    [SerializeField] float shurukenDamage = 15f;
    [SerializeField] Camera playerCamera;

    public bool canThrowShuruken = true;

    void Update()
    {
        if (Input.GetKey(shootShuruken)&& canThrowShuruken)
        {
            canThrowShuruken = false;
            Shoot();
        }
    }

    void Shoot()
    {
        // Perform a raycast from the center of the screen to get the target point
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
        {
            targetPoint = hit.point;
        }
        else
        {
            targetPoint = ray.GetPoint(1000); // Arbitrary far distance if nothing is hit
        }

        // Calculate the direction
        Vector3 direction = (targetPoint - arrowSpawnPoint.position).normalized;

        // Instantiate and orient the projectile
        GameObject projectile = Instantiate(projectilePrefab, arrowSpawnPoint.position, Quaternion.LookRotation(direction));

        CheckRayCastForEnemy();
    }

    void CheckRayCastForEnemy()
    {
        Ray ray = playerCamera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                Debug.Log("Enemy hit: " + hit.collider.name);

                hit.collider.gameObject.GetComponent<EnemyHealth>().TakeDamage(shurukenDamage);
            }
            else if(hit.collider.CompareTag("Dummy"))
            {
                hit.collider.gameObject.GetComponent<DummyScript>().Damaged(shurukenDamage);
            }
        }
        else
        {
            Debug.Log("No hit detected");
        }
    }
}
