using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 1;

    public float speed = 5f;
    public float timeBetweenAttacks = 3f;
    public int damage = 1;
    public int pickupChance = 10;
    public GameObject[] pickups;

    [HideInInspector]
    public Transform player;

    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if(health <= 0)
        {
            int randomNumber = Random.Range(0, 101);
            if(randomNumber < pickupChance)
            {
                GameObject randomPickup = pickups[Random.Range(0, pickups.Length)];
                Instantiate(randomPickup, transform.position, transform.rotation);
            }

            Destroy(gameObject);
        }
    }
}
