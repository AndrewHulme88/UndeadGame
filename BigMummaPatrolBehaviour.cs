using Unity.VisualScripting;
using UnityEngine;

public class BigMummaPatrolBehaviour : StateMachineBehaviour
{
    [SerializeField] float speed = 5f;

    private GameObject[] patrolPoints;
    private int randomPoint;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        patrolPoints = GameObject.FindGameObjectsWithTag("PatrolPoint");

        PickNewPatrolPoint();
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.transform.position = Vector2.MoveTowards(animator.transform.position, patrolPoints[randomPoint].transform.position, speed * Time.deltaTime);

        if(Vector2.Distance(animator.transform.position, patrolPoints[randomPoint].transform.position) < 0.1f)
        {
            PickNewPatrolPoint();
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    public void PickNewPatrolPoint()
    {
        randomPoint = Random.Range(0, patrolPoints.Length);
    }
}
