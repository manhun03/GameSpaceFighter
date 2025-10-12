using System.Collections;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private GameObject EggPrefaps;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        StartCoroutine(SpawmEgg());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawmEgg()
    {
        while (true)
        {
            Instantiate(EggPrefaps, transform.position, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(2, 7));
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(gameObject);
        }
    }
}
