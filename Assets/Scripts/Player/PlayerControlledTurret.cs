using UnityEngine;

public class PlayerControlledTurret : MonoBehaviour
{
    [Header("Cài đặt turret")]
    public GameObject weaponPrefab;              // Prefab của viên đạn
    public GameObject[] barrelHardpoints;        // Các nòng súng
    public float shotCooldown = 0.2f;            // Thời gian giữa 2 lần bắn

    private int barrelIndex = 0;                 // Nòng hiện tại
    private float nextFireTime = 0f;             // Thời điểm bắn kế tiếp

    private AudioManager audioManager;           // Khai báo AudioManager

    void Update()
    {
        
        if (Input.GetMouseButton(1) && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + shotCooldown;
        }
    }
    private void Start()
    {
        audioManager = FindAnyObjectByType<AudioManager>();     // gọi audioManager

    }
    void Fire()
    {
        audioManager.PlayPlayerGun();           // Phát âm thanh bắn súng


        if (weaponPrefab == null || barrelHardpoints == null || barrelHardpoints.Length == 0)
            return;

        // Tạo viên đạn tại vị trí nòng hiện tại
        Instantiate(
            weaponPrefab,
            barrelHardpoints[barrelIndex].transform.position,
            barrelHardpoints[barrelIndex].transform.rotation
        );

        // Luân phiên qua các nòng
        barrelIndex++;
        if (barrelIndex >= barrelHardpoints.Length)
            barrelIndex = 0;
            

    }
}
