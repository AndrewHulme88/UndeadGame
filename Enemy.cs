using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int health = 1;

    public float speed = 5f;
    public float timeBetweenAttacks = 3f;
    public int damage = 1;

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
            Destroy(gameObject);
        }
    }
}
