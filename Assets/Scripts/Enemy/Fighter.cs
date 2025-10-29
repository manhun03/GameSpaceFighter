using UnityEngine;

public class Fighter : Enemy
{
    [Header("Chuyển động")]
    public float moveSpeed = 5f;
    public float amplitude = 2f;
    public float frequency = 2f;
    public bool moveRight = true;

    private Vector3 startPos;
    private float elapsedTime;

    [Header("Tấn công")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private float detectRange = 15f;
    //[SerializeField] private float speedCircleBullet = 10f;

    Transform player;
    private float shootTimer = 0f;

    protected override void Start()
    {
        startPos = transform.position;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHp = maxHp;

    }

    void Update()
    {
        MovePattern();
        DetectAndAttack();
    }

    private void MovePattern()
    {
        elapsedTime += Time.deltaTime;
        float xDir = moveRight ? 1f : -1f;
        float x = startPos.x + xDir * moveSpeed * elapsedTime;
        float y = startPos.y + Mathf.Sin(elapsedTime * frequency) * amplitude;
        Vector3 newPos = new Vector3(x, y, startPos.z);

        Vector3 direction = newPos - transform.position;
        transform.position = newPos;

        if (direction.sqrMagnitude > 0.0001f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle - 90f);
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
        else
        {
            // Reset timer nếu muốn chỉ bắn khi vào phạm vi
            shootTimer = 0f;
        }
    }

    private void ShootCircle()
    {
        if (bulletPrefab == null || firePoint == null)
        {
            return;
        }

        const int bulletCount = 10;
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

            angle += angleStep;
        }
    }
}
