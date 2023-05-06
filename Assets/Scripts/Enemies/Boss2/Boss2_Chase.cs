using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Chase : StateMachineBehaviour
{
    [SerializeField] private Vector3 leftEdge;
    [SerializeField] private Vector3 rightEdge;
    private GameObject boss;
    private BossPepelaz bossScript;

    private bool movingLeft;

    private Vector3 initScale = new Vector3(1, 1, 1);

    [SerializeField] private float speed = 10;
    [SerializeField] private float min_distance;
    private Transform player;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        leftEdge = GameObject.Find("LeftEdge").transform.position;
        rightEdge = GameObject.Find("RightEdge").transform.position;
        boss = GameObject.Find("Boss");
        bossScript = boss.GetComponent<BossPepelaz>();
        player = GameObject.Find("Player").transform;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 project = Vector3.Project(player.position - boss.transform.position, new Vector3(1, 0, 0));
        FollowPlayer(project);
    }
    void FollowPlayer(Vector3 project)
    {
        //rotate to look at the player
        if (project.x > 0 && movingLeft)
        {
            DirectionChange();
        }
        else if (project.x < 0 && !movingLeft)
        {
            DirectionChange();
        }
        //move to player
        else if (project.magnitude > min_distance) MoveInDirection((int)project.normalized.x);
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
        
    }
}
