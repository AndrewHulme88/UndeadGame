using System.Collections;
using UnityEngine;

public class EnemyMelee : Enemy
{
    [SerializeField] float stopDistance = 3f;
    [SerializeField] float attackSpeed = 8f;
    [SerializeField] ParticleSystem dustParticles;

    private float attackTime = 0f;

    private void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, player.position) > stopDistance)
            {
                MoveTowardsPlayer();
            }
            else
            {
                if(Time.time >= attackTime)
                {
                    if (dustParticles.isPlaying)
                    {
                        dustParticles.Stop();
                    }

                    StartCoroutine(Attack());
                    attackTime = Time.time + timeBetweenAttacks;
                }
            }
        }
    }

    private void MoveTowardsPlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);

        if (!dustParticles.isPlaying)
        {
            dustParticles.Play();
        }
    }

    IEnumerator Attack()
    {
        player.GetComponent<PlayerController>().TakeDamage(damage);

        Vector2 originalPosition = transform.position;
        Vector2 targetPosition = player.position;

        float percent = 0f;
        while(percent <= 1)
        {
            percent += Time.deltaTime * attackSpeed;
            float formula = (-Mathf.Pow(percent, 2) + percent) * 4;
            transform.position = Vector2.Lerp(originalPosition, targetPosition, formula);
            yield return null;
        }
    }
}
