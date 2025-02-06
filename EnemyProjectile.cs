using Unity.VisualScripting;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float speed = 15f;
    [SerializeField] int damage = 1;

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
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            playerController.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
