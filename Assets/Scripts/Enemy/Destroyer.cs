using UnityEngine;

public class Destroyer : Enemy
{
    [Header("Chuyển động")]
    public float moveSpeed = 5f;

    private bool moveRight;
    private Vector3 startPos;
    private float elapsedTime;

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    protected override void Start()
    {
        startPos = transform.position;
        currentHp = maxHp;

        // Tự xác định hướng bay theo vị trí spawn
        moveRight = transform.position.x < 0f;
    }

    void Update()
    {
        MoveStraight();
    }

    private void MoveStraight()
    {
        float xDir = moveRight ? 1f : -1f;
        transform.position += new Vector3(xDir * moveSpeed * Time.deltaTime, 0f, 0f);

        float angle = moveRight ? -90f : 90f;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
