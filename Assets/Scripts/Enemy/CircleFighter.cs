using UnityEngine;

public class CircleFighter : Enemy
{
    [Header("Cấu hình quỹ đạo tròn")]
    public Transform center;       // tâm vòng tròn
    public float radius = 5f;      // bán kính vòng tròn
    public float angularSpeed = 90f; // tốc độ góc (độ / giây)
    public bool clockwise = true;  // bay thuận hay ngược chiều kim đồng hồ

    private float angle;           // góc hiện tại (độ)

    [Header("Tấn công")]
    //[SerializeField] private float enterDmg = 10f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private float detectRange = 10f;
    [SerializeField] private float speedCircleBullet = 10f;

    Transform player;

    private float shootTimer;

    protected override void Start()
    {
        if (center == null)
        {
            GameObject centerObj = new GameObject("CenterPoint");
            centerObj.transform.position = transform.position;
            center = centerObj.transform;
        }

        // Tính góc ban đầu dựa trên vị trí hiện tại
        Vector3 offset = transform.position - center.position;
        angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
        player = GameObject.FindGameObjectWithTag("PlayerTag")?.transform;

        currentHp = maxHp;
    }

    void Update()
    {
        // Tăng hoặc giảm góc theo thời gian
        float direction = clockwise ? -1f : 1f;
        angle += direction * angularSpeed * Time.deltaTime;

        // Tính toạ độ mới trên đường tròn
        float rad = angle * Mathf.Deg2Rad;
        float x = center.position.x + Mathf.Cos(rad) * radius;
        float y = center.position.y + Mathf.Sin(rad) * radius;
        Vector3 newPos = new Vector3(x, y, transform.position.z);

        // Vector hướng di chuyển (từ vị trí cũ đến vị trí mới)
        Vector3 dir = newPos - transform.position;

        // Di chuyển
        transform.position = newPos;

        // Xoay mũi máy bay theo hướng bay
        if (dir.sqrMagnitude > 0.0001f)
        {
            float rotZ = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ - 90f); // -90f nếu sprite hướng lên
        }
        DetectAndAttack();
    }
    private void ShootCircle()
    {
        const int bulletCount = 5;
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
            if (enemyBullet != null)
            {
                enemyBullet.SetDirection(bulletDirection);
            }

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.linearVelocity = bulletDirection * speedCircleBullet;
            }

            angle += angleStep;
        }
    }
    private void DetectAndAttack()
    {
        if (player == null) return;

        float distance = Vector2.Distance(transform.position, player.position);
        if (distance <= detectRange)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                ShootCircle();
                shootTimer = shootInterval;
            }
        }
    }
}
