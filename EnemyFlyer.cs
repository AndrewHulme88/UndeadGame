using UnityEngine;

public class EnemyFlyer : Enemy
{
    [SerializeField] float stopDistance = 3f;
    [SerializeField] Transform shotPoint;
    [SerializeField] GameObject enemyProjectile;

    private Animator anim;
    private float attackTime;

    public override void Start()
    {
        base.Start();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(player != null)
        {
            if(Vector2.Distance(transform.position, player.transform.position) > stopDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }

            if(Time.time >= attackTime)
            {
                attackTime = Time.time + timeBetweenAttacks;
                anim.SetTrigger("attack");
            }
        }
    }

    public void RangedAttack()
    {
        Vector2 direction = player.position - shotPoint.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion rotation = Quaternion.Euler(0, 0, angle - 90);
        shotPoint.rotation = rotation;

        Instantiate(enemyProjectile, shotPoint.position, shotPoint.rotation);
    }
}
