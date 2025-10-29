using UnityEngine;

public class Rocket : MonoBehaviour
{
    [Header("Cấu hình")]
    public float speed = 10f;
    private Vector2 direction = Vector2.right;
    [SerializeField] private float dmg = 2.0f;
    [SerializeField] GameObject takeDmgEffect;
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;

        // Xoay mặt theo hướng bay
        if (direction.x < 0)
        {
            // Bay sang trái → xoay 180 độ theo trục Y
            transform.rotation = Quaternion.Euler(0, 180f, 0);
        }
        else
        {
            // Bay sang phải → xoay về hướng mặc định
            transform.rotation = Quaternion.identity;
        }
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            NewMonoBehaviourScript player = collision.GetComponent<NewMonoBehaviourScript>();
            GameObject effect = Instantiate(takeDmgEffect, transform.position, Quaternion.identity);
            effect.transform.parent = null;
            Destroy(effect, 0.3f);
            if (player != null)
            {
                player.TakeDamage(dmg);
                Destroy(gameObject);
            }
        }
    }
}
