using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] int damage = 1;
    [SerializeField] GameObject particles;

    private PlayerController playerController;
    private Vector2 targetPosition;

    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        targetPosition = playerController.transform.position;
    }

    void Update()
    {
        if(playerController != null)
        {
            if(Vector2.Distance(transform.position, targetPosition) > 0.01f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
            }
            else
            {
                Instantiate(particles, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }

    private void DestroyProjectile()
    {
        Instantiate(particles, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerController.TakeDamage(damage);
            Instantiate(particles, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        if (collision.tag == "World")
        {
            DestroyProjectile();
        }
    }
}
