using UnityEditor;
using UnityEngine;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrentTierBullet;
    [SerializeField] private GameObject VFX;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();

        
    }

    private void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(x, y, 0);

        transform.position += direction.normalized * Time.deltaTime * Speed;

        Vector3 TopLeftPoit = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, 0));

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, TopLeftPoit.x * -1, TopLeftPoit.x),
            Mathf.Clamp(transform.position.y, TopLeftPoit.y * -1, TopLeftPoit.y));
    }

    void Fire()
    {
        if(Input.GetMouseButtonDown(0))
        Instantiate(BulletList[CurrentTierBullet], transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Monster")|| collision.CompareTag("Egg"))
        {
            Destroy(gameObject);

        }

    }

    //Hàm hiệu ứng nổ 
    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
        }
    }
}
