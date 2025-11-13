using UnityEngine;
using System.Collections;

public class CruiserBoss : Enemy
{
    [Header("Cấu hình di chuyển")]
    public float moveSpeed = 5f;          // tốc độ di chuyển
    public float moveRange = 5f;          // khoảng cách qua lại
    public bool startMovingRight = true;  // hướng ban đầu

    private Vector3 startPos;
    private int moveDir;                  // 1 = phải, -1 = trái

    [Header("Tấn công")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject rocketPrefab;
    [SerializeField] private Transform[] firePoint;
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private float detectRange = 10f;
    [SerializeField] private float speedCircleBullet = 10f;
    [SerializeField] private Transform[] circleFighterPoint;
    [SerializeField] private GameObject circleFighter;
    [SerializeField] private Transform[] rocketPoint;

    private Transform player;
    private float shootTimer;

    private Animator animator;
    private const string flashRedAnim = "TakeDmg";

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        startPos = transform.position;
        moveDir = startMovingRight ? 1 : -1;
        shootTimer = shootInterval;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHp = maxHp;
        isBoss = true; 
    }

    private void Update()
    {
        Move();
        DetectAndAttack();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);

        if (animator != null)
            animator.SetTrigger(flashRedAnim);
    }
    private void Move()
    {
        transform.position += Vector3.right * moveDir * moveSpeed * Time.deltaTime;

        if (transform.position.x > startPos.x + moveRange)
            moveDir = -1;
        else if (transform.position.x < startPos.x - moveRange)
            moveDir = 1;

        // Xoay mặt boss theo hướng di chuyển
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * moveDir;
        transform.localScale = scale;
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
                UseRandomSkill();
                shootTimer = shootInterval;
            }
        }
    }

    private void ShootDoubleBullet()
    {
        if (firePoint == null || firePoint.Length == 0) return;
        StartCoroutine(BurstFire());
    }

    private IEnumerator BurstFire()
    {
        float fireDuration = 1f;       // Thời gian sấy 1 giây
        float fireRate = 0.15f;        // Bắn mỗi 0.15 giây
        float timer = 0f;

        while (timer < fireDuration)
        {
            foreach (Transform point in firePoint)
            {
                GameObject bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity);

                if (player != null)
                {
                    Vector2 direction = (player.position - point.position).normalized;
                    bullet.GetComponent<EnemyBullet>().SetDirection(direction);
                }
            }

            timer += fireRate;
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void ShootCircleBullet()
    {
        if (firePoint == null || firePoint.Length == 0) return;

        const int bulletCount = 10;
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint[0].position, Quaternion.identity);
            EnemyBullet enemyBullet = bullet.GetComponent<EnemyBullet>();
            if (enemyBullet != null)
                enemyBullet.SetDirection(bulletDirection);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = bulletDirection * speedCircleBullet;

            angle += angleStep;
        }
    }

    private void ShootRocket()
    {
        if (rocketPoint == null || rocketPoint.Length == 0) return;

        foreach (Transform point in rocketPoint)
        {
            GameObject rocket = Instantiate(rocketPrefab, point.position, Quaternion.identity);
            // Hướng bắn phụ thuộc vị trí rocketPoint
            Vector2 dir = (point.position.x < transform.position.x) ? Vector2.right : Vector2.left;
            Rocket rocketScript = rocket.GetComponent<Rocket>();
            if (rocketScript != null)
                rocketScript.SetDirection(dir);
        }
    }

    private void CallCircleFighter()
    {
        if (circleFighterPoint == null || circleFighterPoint.Length == 0) return;

        foreach (Transform point in circleFighterPoint)
        {
            Instantiate(circleFighter, point.position, Quaternion.identity);
        }
    }

    private void UseRandomSkill()
    {
        int randomSkill = Random.Range(0, 4); // 0-3
        switch (randomSkill)
        {
            case 0:
                ShootDoubleBullet();   // sấy đạn đôi
                break;
            case 1:
                ShootCircleBullet();   // bắn vòng tròn
                break;
            case 2:
                ShootRocket();         // bắn rocket
                break;
            case 3:
                CallCircleFighter();   // gọi máy bay con
                break;
        }
    }
}
