using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    public float maxHp = 100f;
    protected float currentHp;
    public float dmg = 2f;
    public int scoreValue = 100;
    public bool isBoss = false;

    protected virtual void Start()
    {
        currentHp = maxHp;
    }

    public virtual void TakeDamage(float dmg)
    {
        currentHp -= dmg;
        if (currentHp <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        Debug.Log("Enemy has died!");

        if (ScoreKeeper.Instance != null)
            ScoreKeeper.Instance.ModifyScore(scoreValue);

        Destroy(gameObject);

        if (isBoss)
        {
            Debug.Log("Boss defeated -> GameWin!");
            SceneManager.LoadScene("GameWin");
        }
    }
}
