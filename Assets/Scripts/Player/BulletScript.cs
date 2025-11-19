using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float Speed;
    [SerializeField] private float DistancesDestroy;
    [SerializeField] private float damage;
    [SerializeField] GameObject boomEffect;

    void Start()
    {
        DistancesDestroy = 10;
    }

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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Striker striker = collision.GetComponent<Striker>();
            CircleFighter circleFighter = collision.GetComponent<CircleFighter>();
            Fighter fighter = collision.GetComponent<Fighter>();
            Stone stone = collision.GetComponent<Stone>();
            Alien3 alien3 = collision.GetComponent<Alien3>();
            CruiserBoss cruiserBoss = collision.GetComponent<CruiserBoss>();
            Destroyer destroyer = collision.GetComponent<Destroyer>();
            DestroyerBoss destroyerBoss = collision.GetComponent<DestroyerBoss>();

            GameObject effect = Instantiate(boomEffect, transform.position, Quaternion.identity);
            effect.transform.parent = null;

            Destroy(effect, 0.5f);

            if (striker != null)
                striker.TakeDamage(damage);
            if (circleFighter != null)
                circleFighter.TakeDamage(damage);
            if(fighter != null)
                fighter.TakeDamage(damage);
            if(stone != null)
                stone.TakeDamage(damage);
            if (alien3 != null)
                alien3.TakeDamage(damage);
            if (destroyer != null) 
                destroyer.TakeDamage(damage);
            if (cruiserBoss != null)
            {
                cruiserBoss.TakeDamage(damage);
            }
            if (destroyerBoss != null)
            {
                destroyerBoss.TakeDamage(damage);
            }


            Destroy(gameObject);
        }
    }
}
