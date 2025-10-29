using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float speed = 8f;
    [SerializeField] float lifeTime = 3f;
    [SerializeField] float damage = 5f;
    Vector2 direction;
    [SerializeField] GameObject takeDmgEffect; 

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }

    void Update()
    {
        // Dùng World space để không bị lệch hướng khi spawn
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NewMonoBehaviourScript player = collision.GetComponent<NewMonoBehaviourScript>();
            GameObject effect = Instantiate(takeDmgEffect, transform.position, Quaternion.identity);
            effect.transform.parent = null;

            Destroy(effect, 0.5f);

            if (player != null)
            {
                player.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
