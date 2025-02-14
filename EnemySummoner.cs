using System.Collections;
using UnityEngine;

public class EnemySummoner : Enemy
{
    [SerializeField] float minX;
    [SerializeField] float maxX;
    [SerializeField] float minY;
    [SerializeField] float maxY;
    [SerializeField] float timeBetweenSumons = 7f;
    [SerializeField] Enemy enemyToSummon;
    [SerializeField] float attackSpeed = 1f;
    [SerializeField] float stopDistance = 3f;
    [SerializeField] GameObject jumpParticles;
    [SerializeField] ParticleSystem dustParticles;

    private Vector2 targetPosition;
    private Animator anim;
    private float summonTime;
    private float attackTime;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();

        PickNewTargetPosition();
    }

    private void Update()
    {
        if(player != null)
        {
            if (Vector2.Distance(targetPosition, transform.position) > 0.5f)
            {
                transform.position = Vector2.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
                anim.SetBool("isRunning", true);

                if (!dustParticles.isPlaying)
                {
                    dustParticles.Play();
                }
            }
            else
            {
                anim.SetBool("isRunning", false);

                if (dustParticles.isPlaying)
                {
                    dustParticles.Stop();
                }

                if (Time.time >= summonTime)
                {
                    summonTime = Time.time + timeBetweenSumons;
                    anim.SetTrigger("summon");
                }
            }

            if (Vector2.Distance(transform.position, player.position) <= stopDistance)
            {
                if (Time.time >= attackTime)
                {
                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    private void PickNewTargetPosition()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        targetPosition = new Vector2(randomX, randomY);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("World"))
        {
            PickNewTargetPosition();
        }
    }

    public void Summon()
    {
        if (player != null)
        {
            Instantiate(jumpParticles, transform.position, transform.rotation);
            Instantiate(enemyToSummon, transform.position, transform.rotation);
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<PlayerController>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0f;
        while (percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
