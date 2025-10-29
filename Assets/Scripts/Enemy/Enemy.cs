using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Cấu hình máu")]
    public float maxHp = 100f;
    [SerializeField] protected float currentHp;

    [Header("Cấu hình sát thương")]
    public float dmg = 2f;

    public int scoreValue = 100;
    protected virtual void Start()
    {
        currentHp = maxHp;
    }

    public virtual void TakeDamage(float dmg)
    {
        currentHp -= dmg;
        currentHp = Mathf.Max(currentHp, 0);
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        if (GameManagerDemo.Instance != null)
            GameManagerDemo.Instance.AddScore(scoreValue);

        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        NewMonoBehaviourScript player = collision.GetComponent<NewMonoBehaviourScript>();
        if (collision.CompareTag("Player"))
        {
            if (player != null)
            {
                player.TakeDamage(dmg);
            }
        }
    }
}
