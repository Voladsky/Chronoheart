using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Start_Fly : StateMachineBehaviour
{
    [SerializeField] private Vector3 topEdge;
    [SerializeField] private float speed = 10;
    private Transform boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.Find("Boss").transform;
        topEdge = GameObject.Find("LeftEdge").transform.position;
        boss.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (boss.transform.position.y < topEdge.y)
            boss.transform.position = new Vector3(boss.transform.position.x, boss.transform.position.y + Time.deltaTime * speed, boss.transform.position.z);
        else
            animator.SetTrigger("fly");
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("fly");
    }
}
