using UnityEngine;

public class BigMummaChaseBehaviour : StateMachineBehaviour
{
    [SerializeField] float speed = 15f;

    private GameObject player;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if(player != null)
        {
            animator.transform.position = Vector2.MoveTowards(animator.transform.position, player.transform.position, speed * Time.deltaTime);
        }
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
