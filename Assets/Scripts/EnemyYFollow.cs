using UnityEngine;

public class EnemyYFollow : MonoBehaviour
{
    public float speed = 3f;      // tốc độ bay xuống
    public float destroyY = -6f;  // vị trí Y để hủy enemy khi ra khỏi màn hình

    void Update()
    {
        // Di chuyển thẳng xuống trục Y
        transform.Translate(Vector2.down * speed * Time.deltaTime);

        // Nếu vượt quá màn hình thì xóa object để tiết kiệm tài nguyên
        if (transform.position.y < destroyY)
        {
            Destroy(gameObject);
        }
    }
}
