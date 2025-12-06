using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    protected AudioManager audioManager;           // Khai báo AudioManager


    public float maxHp = 100f;
    protected float currentHp;
    public float dmg = 2f;
    public int scoreValue = 100;
    public bool isBoss = false;

    protected virtual void Start()
    {
        currentHp = maxHp;
        audioManager = FindAnyObjectByType<AudioManager>();     // gọi audioManager

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

     // Destroy(gameObject);

        if (isBoss)
        {
            Debug.Log("Boss defeated -> GameWin!");
            
            StartCoroutine(PlayWinAndLoadScene()); // gọi hàm phát nhạc win và chuyển cảnh StartCoroutine(PlayWinAndLoadScene()); (2)
        }
        else
            Destroy(gameObject); // Hủy đối tượng kẻ thù nếu không nó sẽ không phát nhạc (3)
    }
    protected IEnumerator PlayWinAndLoadScene()   // vì khi thắng ngay lập tức chuyển cảnh sẽ không kịp nghe âm thanh nên thêm hàm này để có 2s phát nhạc win rồi mới chuyển cảnh   (1)  
    {
        audioManager.PlayWin();           // Phát âm thanh chiến thắng
        yield return new WaitForSeconds(2f); // Chờ 2 giây để âm thanh hoàn thành
        SceneManager.LoadScene("GameWin");
    }
 }


