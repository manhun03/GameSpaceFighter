using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyerBoss : Enemy
{
    [Header("Cấu hình di chuyển")]
    public float moveSpeed = 5f;
    public float moveRange = 5f;
    public bool startMovingRight = true;

    private Vector3 startPos;
    private int moveDir;

    [Header("Tấn công")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform[] firePoint;
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private float detectRange = 10f;
    [SerializeField] private float speedCircleBullet = 5f;
    [SerializeField] private Transform[] circleFighterPoint;
    [SerializeField] private GameObject circleFighter;

    [Header("Laser")]
    [SerializeField] private float defDistanceRay = 100f;
    public LineRenderer m_lineRenderer;
    public Transform lazeFirePoint;
    [SerializeField] private float laserDamage = 20f;
    [SerializeField] private float laserDuration = 2f;
    [SerializeField] private LayerMask hitMask; // Player, Ground

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

        m_lineRenderer.enabled = false;
    }

    private void Update()
    {
        Move();
        DetectAndAttack();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        animator?.SetTrigger(flashRedAnim);
    }

    private void Move()
    {
        transform.position += Vector3.right * moveDir * moveSpeed * Time.deltaTime;

        if (transform.position.x > startPos.x + moveRange)
            moveDir = -1;
        else if (transform.position.x < startPos.x - moveRange)
            moveDir = 1;

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
    private void SpawnSingleBullet()
    {
        if (firePoint == null || firePoint.Length == 0) return;

        Transform point = firePoint[0];
        GameObject bullet = Instantiate(bulletPrefab, point.position, Quaternion.identity);

        if (player != null)
        {
            Vector2 direction = (player.position - point.position).normalized;
            bullet.GetComponent<EnemyBullet>()?.SetDirection(direction);

            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            if (rb != null)
                rb.linearVelocity = direction * 10f;
        }
    }

    private IEnumerator ShootBurst()
    {
        int burstCount = 5;        // Xả 5 viên mỗi lần
        float fireRate = 0.15f;    // 0.15s giữa 2 viên

        for (int i = 0; i < burstCount; i++)
        {
            SpawnSingleBullet();
            yield return new WaitForSeconds(fireRate);
        }
    }

    private void ShootCircleBullet()
    {
        if (firePoint == null || firePoint.Length == 0) return;

        const int bulletCount = 12;
        float angleStep = 360f / bulletCount;
        float angle = 0f;

        for (int i = 0; i < bulletCount; i++)
        {
            float bulletDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float bulletDirY = Mathf.Sin(angle * Mathf.Deg2Rad);
            Vector2 bulletDirection = new Vector2(bulletDirX, bulletDirY).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint[0].position, Quaternion.identity);
            bullet.GetComponent<EnemyBullet>()?.SetDirection(bulletDirection);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = bulletDirection * speedCircleBullet;

            angle += angleStep;
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

    private IEnumerator SweepLaserDown()
    {
        m_lineRenderer.enabled = true;

        float elapsed = 0f;
        float sweepDuration = laserDuration;

        float leftX = startPos.x - moveRange;
        float rightX = startPos.x + moveRange;

        while (elapsed < sweepDuration)
        {
            Vector2 startPosLaser = lazeFirePoint.position;

            float t = Mathf.PingPong(elapsed * 2f, 1f);
            float sweepX = Mathf.Lerp(leftX, rightX, t);

            RaycastHit2D hit = Physics2D.Raycast(startPosLaser, Vector2.down, defDistanceRay, hitMask);

            Vector2 endPosLaser;

            if (hit.collider != null)
            {
                endPosLaser = new Vector2(sweepX, hit.point.y);

                if (hit.collider.CompareTag("Player"))
                {
                    hit.collider.GetComponent<NewMonoBehaviourScript>()?.TakeDamage(laserDamage * Time.deltaTime);
                }
            }
            else
            {
                endPosLaser = new Vector2(sweepX, startPosLaser.y - defDistanceRay);
            }

            Draw2DRay(startPosLaser, endPosLaser);

            float offset = Time.time * 8f;
            m_lineRenderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));

            elapsed += Time.deltaTime;
            yield return null;
        }

        m_lineRenderer.enabled = false;
    }

    void Draw2DRay(Vector2 startPos, Vector2 endPos)
    {
        m_lineRenderer.SetPosition(0, startPos);
        m_lineRenderer.SetPosition(1, endPos);
    }

    private void UseRandomSkill()
    {
        int randomSkill = Random.Range(0, 4);
        switch (randomSkill)
        {
            case 0:
                StartCoroutine(ShootBurst()); 
                break;
            case 1:
                ShootCircleBullet();
                break;
            case 2:
                StartCoroutine(SweepLaserDown());
                break;
            case 3:
                CallCircleFighter();
                break;
        }
    }
}
