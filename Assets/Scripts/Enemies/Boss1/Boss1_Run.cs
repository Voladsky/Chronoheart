using UnityEngine;

public class Boss1_Run : StateMachineBehaviour
{
    [SerializeField] float speed = 2.5f;
    [SerializeField] float attackRange = 3f;

    [SerializeField] bool isHandsOff = false;
    private float timeInState;
    [SerializeField] float coolDown=100f;

    Transform player;
    Rigidbody2D rb;
    BossBall boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<BossBall>();

    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss.LookAtPlayer();

        Vector2 target = new Vector2(player.position.x, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);

        timeInState += Time.deltaTime;
        Debug.Log(timeInState);

        if (timeInState >= coolDown)
        {
            isHandsOff = !isHandsOff;
            animator.SetBool("isHandsOff", !animator.GetBool("isHandsOff"));
            timeInState = 0;
            return;
        }

        if (Vector2.Distance(player.position, rb.position) <= attackRange)
        {
            animator.SetTrigger("attack");
        }       
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("attack");
    }
}