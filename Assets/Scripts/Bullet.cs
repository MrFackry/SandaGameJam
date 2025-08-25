using UnityEngine;

public class Bullet : MonoBehaviour
{
    private PlayerHealt playerHealt;
    [SerializeField] private float damage = 10f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerHealt = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealt>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Bala choco con " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("Bullet"))
        {
            gameObject.SetActive(false);
        }
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHealt != null)
            {
                playerHealt.TakeDamage(damage);
            }
        }
    }
}
