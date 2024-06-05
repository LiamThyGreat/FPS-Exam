using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] Transform enemySpawnLocation;
    [SerializeField] KeyCode spawnEnemyKey = KeyCode.P;

    private bool canSpawnEnemy = true;

    private void Update()
    {
        if (Input.GetKey(spawnEnemyKey) && canSpawnEnemy)
        {
            canSpawnEnemy = false;
            Instantiate(enemyPrefab, enemySpawnLocation.position, Quaternion.identity);

            StartCoroutine(SpawnEnemyCooldown());
        }
    }

    IEnumerator SpawnEnemyCooldown()
    {
        yield return new WaitForSeconds(1);

        canSpawnEnemy = true;
    }
}
