using UnityEngine;

public class GameManagerDemo : MonoBehaviour
{
    public static GameManagerDemo Instance;

    [Header("Cấu hình Boss")]
    [Tooltip("Điểm cần đạt để gọi Boss ra")]
    public int bountyScore = 1000;

    [Tooltip("Prefab của Boss sẽ được gọi ra")]
    public GameObject bossPrefab;

    [Tooltip("Vị trí mà Boss sẽ xuất hiện")]
    public Transform bossSpawnPoint;

    private bool bossSpawned = false;

    [Header("Điểm số và kẻ địch")]
    [SerializeField] private GameObject[] enemySpawners; // các spawner sinh enemy
    public int score = 0;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Gọi khi enemy bị tiêu diệt để cộng điểm.
    /// </summary>
    public void AddScore(int amount)
    {
        score += amount;
        Debug.Log($"🏆 Điểm hiện tại: {score}");

        CheckForBossSpawn();
    }

    /// <summary>
    /// Kiểm tra xem đã đủ điểm để gọi boss chưa.
    /// </summary>
    private void CheckForBossSpawn()
    {
        if (!bossSpawned && score >= bountyScore)
        {
            SpawnBoss();
            DisableEnemySpawners();
        }
    }

    /// <summary>
    /// Tạo boss ra vị trí chỉ định.
    /// </summary>
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

    /// <summary>
    /// Tắt toàn bộ các điểm spawn enemy.
    /// </summary>
    private void DisableEnemySpawners()
    {
        if (enemySpawners == null || enemySpawners.Length == 0)
            return;

        foreach (GameObject spawner in enemySpawners)
        {
            if (spawner != null)
                spawner.SetActive(false);
        }

        Debug.Log("❌ Tất cả enemy spawner đã bị tắt.");
    }

    /// <summary>
    /// Lấy điểm hiện tại (nếu cần hiển thị sau này).
    /// </summary>
    public int GetScore()
    {
        return score;
    }
}
