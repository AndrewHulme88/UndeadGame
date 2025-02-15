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
    [SerializeField] GameObject jumpParticles;
    [SerializeField] ParticleSystem dustParticles;

    private Vector2 targetPosition;
    private Animator anim;
    private float summonTime;

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
            DustParticles();
        }
    }

    public void DustParticles()
    {
        Instantiate(enemyToSummon, transform.position, transform.rotation);
    }
}
