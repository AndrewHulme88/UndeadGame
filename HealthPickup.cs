using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    private PlayerController player;
    [SerializeField] int healAmount = 1;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            player.Heal(healAmount);
            Destroy(gameObject);
        }
    }
}
