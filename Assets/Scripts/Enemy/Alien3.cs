using UnityEngine;
using UnityEngine.TestTools;

public class Alien3 : Enemy
{
    [Header("Tấn công")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float shootInterval = 2f;
    [SerializeField] private float detectRange = 10f;

    Transform player;

    private float shootTimer;

    protected override void Start()
    {
        base.Start();
        shootTimer = shootInterval;
        player = GameObject.FindGameObjectWithTag("Player")?.transform;
        currentHp = maxHp;
    }
    void Update()
    {
        DetectAndAttack();
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
