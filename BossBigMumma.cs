using UnityEngine;
using UnityEngine.UI;

public class BossBigMumma : MonoBehaviour
{
    [SerializeField] GameObject landingParticles;
    [SerializeField] ParticleSystem dustParticleLeft;
    [SerializeField] ParticleSystem dustParticleRight;
    [SerializeField] GameObject landingParticleSpawnPoint;

    public int health = 20;
    public Enemy[] enemies;
    public float spawnOffset = 2f;
    public int damage = 1;

    private int halfHealth;
    private Animator anim;
    private Slider healthBar;
    private SceneTransitions sceneTransitions;
    private CircleCollider2D circleCollider;
    private BigMummaPatrolBehaviour patrolBehaviour;

    private void Start()
    {
        halfHealth = health / 2;
        anim = GetComponent<Animator>();
        healthBar = FindFirstObjectByType<Slider>();
        healthBar.maxValue = health;
        healthBar.value = health;
        sceneTransitions = FindFirstObjectByType<SceneTransitions>();
        circleCollider = GetComponent<CircleCollider2D>();
        patrolBehaviour = GetComponent<Animator>().GetBehaviour<BigMummaPatrolBehaviour>();
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        healthBar.value = health;

        if (health <= 0)
        {
            Destroy(this.gameObject);
            healthBar.gameObject.SetActive(false);
            sceneTransitions.LoadScene("Win");
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("World"))
        {
            patrolBehaviour.PickNewPatrolPoint();
        }
    }

    public void Land()
    {
        Instantiate(landingParticles, landingParticleSpawnPoint.transform.position, landingParticleSpawnPoint.transform.rotation);
    }

    public void TurnOnCollider()
    {
        circleCollider.enabled = true;
    }

    public void DustParticleLeft()
    {
        dustParticleLeft.Play();
    }

    public void DustParticleRight()
    {
        dustParticleRight.Play();
    }
}
