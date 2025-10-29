using UnityEngine;

public class Striker : Enemy
{
    [Header("Cấu hình di chuyển")]
    public float moveSpeed = 5f;          // tốc độ di chuyển
    public float moveRange = 5f;          // khoảng cách qua lại
    public bool startMovingRight = true;  // hướng ban đầu

    private Vector3 startPos;
    private int moveDir;                  // 1 = phải, -1 = trái

    [Header("Tấn công")]
    //[SerializeField] private float enterDmg = 10f;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private float detectRange = 10f;

    Transform player;

    private float shootTimer;


    protected override void Start()
    {
        base.Start();
        startPos = transform.position;
        moveDir = startMovingRight ? 1 : -1;
        shootTimer = shootInterval;
        player = GameObject.FindGameObjectWithTag("PlayerTag")?.transform;
        currentHp = maxHp;
    }

    void Update()
    {
        Move();
        DetectAndAttack();
    }

    private void Move()
    {
        // di chuyển qua lại
        transform.position += Vector3.right * moveDir * moveSpeed * Time.deltaTime;

        if (transform.position.x > startPos.x + moveRange)
            moveDir = -1;
        else if (transform.position.x < startPos.x - moveRange)
            moveDir = 1;
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
                ShootNormal();
                shootTimer = shootInterval;
            }
        }
    }
    private void ShootNormal()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);

        Vector2 direction = (player.position - firePoint.position).normalized;

        bullet.GetComponent<EnemyBullet>().SetDirection(direction);
    }
}
