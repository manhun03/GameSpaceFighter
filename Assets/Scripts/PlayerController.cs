using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // tốc độ di chuyển
    private Rigidbody2D rb;
    private Vector2 moveInput;

    private float xMin, xMax, yMin, yMax;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // Tính giới hạn camera
        Camera cam = Camera.main;
        Vector3 screenBottomLeft = cam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 screenTopRight = cam.ViewportToWorldPoint(new Vector3(1, 1, 0));

        xMin = screenBottomLeft.x + 0.5f; // cộng thêm nửa chiều rộng player nếu cần
        xMax = screenTopRight.x - 0.5f;
        yMin = screenBottomLeft.y + 0.5f;
        yMax = screenTopRight.y - 0.5f;
    }

    void Update()
    {
        moveInput.x = Input.GetAxisRaw("Horizontal");
        moveInput.y = Input.GetAxisRaw("Vertical");
        moveInput.Normalize();
    }

    void FixedUpdate()
    {
        rb.linearVelocity = moveInput * moveSpeed;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, xMin, xMax);
        pos.y = Mathf.Clamp(pos.y, yMin, yMax);
        transform.position = pos;
    }
}
