using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;

    public int maxEnemyCount = 20;

    public bool chestOpen = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!enemyPrefab)
        {
            return;
        }
        //InvokeRepeating("SpawnEnemy", 2, 3);
        Debug.Log("Coroutine Started" + Time.time);
        StartCoroutine(SpawnEnemies(2));
    }

    void SpawnEnemy()
    {
        var positionOffset = Random.insideUnitSphere * 5;
        Instantiate(enemyPrefab, transform.position + positionOffset, transform.rotation);
    }

    IEnumerator SpawnEnemies(float SpawnInterval)
    {
        Debug.Log("before yield" + Time.time);
        while(true)
        {
            var enemyCount = GameObject.FindGameObjectsWithTag("Dementor").Length;

            if(enemyCount < maxEnemyCount && chestOpen == false)
            SpawnEnemy();
            yield return new WaitForSeconds(SpawnInterval);
            Debug.Log("after yield" + Time.time);
        }
        
    }
}
