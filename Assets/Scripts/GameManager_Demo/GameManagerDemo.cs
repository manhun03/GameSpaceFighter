using UnityEngine;

public class GameManagerDemo : MonoBehaviour
{
    public static GameManagerDemo Instance;

    [Header("Cấu hình Boss")]
    public int bountyScore = 1000;
    public GameObject bossPrefab;
    public Transform bossSpawnPoint;

    [Header("Enemy Spawners")]
    [SerializeField] private GameObject[] enemySpawners;

    private bool bossSpawned = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 🔹 Được gọi từ ScoreKeeper khi điểm thay đổi
    public void CheckForBossSpawn(int currentScore)
    {
        if (bossSpawned) return;

        if (currentScore >= bountyScore)
        {
            SpawnBoss();
            DisableEnemySpawners();
        }
    }

    private void SpawnBoss()
    {
        if (bossPrefab != null && bossSpawnPoint != null)
        {
            Instantiate(bossPrefab, bossSpawnPoint.position, Quaternion.identity);
            bossSpawned = true;
            Debug.Log("🚀 Boss đã xuất hiện!");
        }
        else
        {
            Debug.LogWarning("⚠️ Boss Prefab hoặc Boss Spawn Point chưa được gán!");
        }
    }

    private void DisableEnemySpawners()
    {
        if (enemySpawners == null || enemySpawners.Length == 0)
            return;

        foreach (GameObject spawner in enemySpawners)
        {
            if (spawner != null)
                spawner.SetActive(false);
        }
    }

    public void ResetGame()
    {
        bossSpawned = false;
        if (ScoreKeeper.Instance != null)
            ScoreKeeper.Instance.ResetScore();

        Debug.Log("GameManager đã reset game.");
    }
}
