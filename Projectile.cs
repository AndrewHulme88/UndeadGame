using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float lifeTime = 5f;
    [SerializeField] int damage = 1;
    [SerializeField] GameObject particles;

    void Start()
    {
        Invoke("DestroyProjectile", lifeTime);
    }

    void Update()
    {
        transform.Translate(Vector2.up * speed *  Time.deltaTime);
    }

    private void DestroyProjectile()
    {
        Instantiate(particles, transform.position, transform.rotation);

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().TakeDamage(damage);
            DestroyProjectile();
        }
        
        if (collision.tag == "BossBigMumma")
        {
            collision.GetComponent<BossBigMumma>().TakeDamage(damage);
            DestroyProjectile();
        }

        if(collision.tag == "World")
        {
            DestroyProjectile();
        }
    }
}
