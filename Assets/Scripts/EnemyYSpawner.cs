using UnityEngine;
using System.Collections;

public class EnemyYSpawner : MonoBehaviour
{
    public GameObject enemyPrefab;   // Prefab máy bay địch
    public float spawnInterval = 5f; // thời gian giữa các lần spawn
    public int enemyCount = 3;       // số enemy spawn mỗi lần
    public float xRange = 5f;        // phạm vi random theo trục X
    public float startY = 6f;        // vị trí spawn theo trục Y (trên màn hình)

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true) // spawn vô hạn
        {
            for (int i = 0; i < enemyCount; i++)
            {
                // random vị trí theo X, Y cố định
                float randomX = Random.Range(-xRange, xRange);
                Vector3 spawnPos = new Vector3(randomX, startY, 0);

                Instantiate(enemyPrefab, spawnPos, Quaternion.identity);

                yield return new WaitForSeconds(0.3f); // delay giữa từng enemy trong 1 đợt
            }

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
