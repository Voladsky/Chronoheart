using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Fly : StateMachineBehaviour
{
    [SerializeField] private Vector3 leftEdge;
    [SerializeField] private Vector3 rightEdge;
    private GameObject boss;
    private BossPepelaz bossScript;
    
    private bool movingLeft;

    private Vector3 initScale = new Vector3(1,1,1);

    [SerializeField] private float speed = 6;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftEdge = GameObject.Find("LeftEdge").transform.position;
        rightEdge = GameObject.Find("RightEdge").transform.position;
        boss = GameObject.Find("Boss");
        bossScript = boss.GetComponent<BossPepelaz>();

        int i = Random.Range(0, 5);
        if (i == 0 || i == 1)
            bossScript.isAttacking = true;
        else if(i == 2 || i == 3)
            bossScript.isSpawning = true;
        else
        {
            bossScript.isAttacking = true;
            bossScript.isSpawning = true;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (movingLeft)
        {
            if (boss.transform.position.x >= leftEdge.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (boss.transform.position.x <= rightEdge.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        movingLeft = !movingLeft;
    }
    private void MoveInDirection(int _direction)
    {
        //Make boss face direction
        boss.transform.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Move in that direction
        boss.transform.position = new Vector3(boss.transform.position.x + Time.deltaTime * _direction * speed,
            boss.transform.position.y, boss.transform.position.z);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        bossScript.isAttacking = false;
        bossScript.isSpawning = false;
    }
}
