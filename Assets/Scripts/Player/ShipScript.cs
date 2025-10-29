using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private GameObject[] BulletList;
    [SerializeField] private int CurrentTierBullet;
    [SerializeField] private GameObject VFX;

    [Header("HP Settings")]
    [SerializeField] private float maxHp = 50f;
    [SerializeField] private float currentHp;
    [SerializeField] private Image hpBar;

    [Header("Shield Settings")]
    [SerializeField] private float maxShield = 50f;
    [SerializeField] private float currentShield;
    [SerializeField] private Image shiledBar;
    [SerializeField] GameObject shieldObject; 
    
    private Animator animator;
    private const string flashWhiteAnim = "FlashWhite";
    void Start()
    {
        animator = GetComponent<Animator>();
        currentHp = maxHp;
        currentShield = maxShield;
        updateHPBar();
        updateShiledBar();
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


    //Hàm hiệu ứng nổ 
    private void OnDestroy()
    {
        if (gameObject.scene.isLoaded)
        {
            var vfx = Instantiate(VFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
        }
    }

    public void TakeDamage(float dmg)
    {
        if (currentShield > 0)
        {
            currentShield -= dmg;
            if (currentShield < 0)
            {
                float leftoverDamage = -currentShield;
                currentShield = 0;
                currentHp -= leftoverDamage;
            }
            updateShiledBar();
            if (currentShield <= 0 && shieldObject != null)
                shieldObject.SetActive(false);
        }
        else
        {
            currentHp -= dmg;
            animator.SetTrigger(flashWhiteAnim);
        }
        currentHp = Mathf.Max(currentHp, 0);
        currentShield = Mathf.Max(currentShield, 0);

        updateHPBar();
        if (currentHp <= 0)
            Die();
    }
    public void Die()
    {
        Destroy(gameObject);
    }

    private void updateHPBar()
    {
        if (hpBar != null)
            hpBar.fillAmount = currentHp / maxHp;
    }
    private void updateShiledBar()
    {
        if(shiledBar != null)
            shiledBar.fillAmount = currentShield / maxShield;
    }
}
