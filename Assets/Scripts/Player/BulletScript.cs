using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float DistancesDestroy;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DistancesDestroy = 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * Speed);
        DestroyIFReachDistances();
        
    }

    void DestroyIFReachDistances()
    {
        Vector3 CenterScreen = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2),0);
        Debug.Log(Vector3.Distance(CenterScreen, transform.position));
        Debug.Log(transform.position);
        if (Vector3.Distance(CenterScreen, transform.position) > DistancesDestroy)
        {
            Destroy(this.gameObject);
        }
    }
}
