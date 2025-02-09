using UnityEngine;

public class BossBigMumma : MonoBehaviour
{
    public int health = 20;
    public Enemy[] enemies;
    public float spawnOffset = 2f;
    public int damage = 1;

    private int halfHealth;
    private Animator anim;

    private void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;

        if (health <= 0)
        {
            Destroy(this.gameObject);
        }

        if (health <= halfHealth)
        {
            anim.SetTrigger("stage2");
        }

        Enemy randomEnemy = enemies[Random.Range(0, enemies.Length)];
        Instantiate(randomEnemy, transform.position + new Vector3(spawnOffset, spawnOffset, 0f), transform.rotation);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            collision.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }
}
