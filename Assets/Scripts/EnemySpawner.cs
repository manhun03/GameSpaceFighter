using System.Collections;
using UnityEngine;
using UnityEngine.Splines;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;      // Prefab máy bay địch
    public int enemyCount = 5;          // Số máy bay trong 1 wave
    public float spawnDelay = 0.5f;     // Thời gian giữa từng enemy trong wave
    public Transform spawnParent;       // Nơi chứa enemy trong Hierarchy
    public SplineContainer spline;      // Đường spline cho enemy bay theo
    public float duration = 5f;         // Thời gian enemy bay hết spline
    public float waveInterval = 5f;     // Khoảng thời gian giữa các wave

    void Start()
    {
        StartCoroutine(SpawnLoop());
    }

    private IEnumerator SpawnLoop()
    {
        while (true) // lặp vô hạn
        {
            yield return StartCoroutine(SpawnWave());
            yield return new WaitForSeconds(waveInterval); // nghỉ 5s rồi spawn lại
        }
    }

    private IEnumerator SpawnWave()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity, spawnParent);

            // Gán script path follower
            EnemyPathFollower follower = enemy.GetComponent<EnemyPathFollower>();
            if (follower != null)
            {
                follower.spline = spline;
                follower.duration = duration;
                follower.SetStartOffset(i * 0.1f); // lệch pha để bay thành hàng
            }

            yield return new WaitForSeconds(spawnDelay);
        }
    }
}
