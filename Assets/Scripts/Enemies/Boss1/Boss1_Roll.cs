using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Boss1_Roll : StateMachineBehaviour
{
    [SerializeField] private float leftEdge;
    [SerializeField] private float rightEdge;

    [SerializeField] private int contactDamage;
    private int originalContactDamage;
    [SerializeField] float speedMultiplier = 1.15f;
    [SerializeField] float originalSpeed = 5f;
    [SerializeField] int dirChangedMax = 6;
    float speed;
    private bool movingLeft;

    Rigidbody2D rb;
    BossBall boss;
    Transform bossPos;

    int dirChangedCount = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody2D>();
        boss = animator.GetComponent<BossBall>();
        bossPos = animator.GetComponent<Transform>();
        speed = originalSpeed;
        movingLeft = !boss.isFlipped;
        dirChangedCount = 0;
        originalContactDamage = boss.contactDamage;
        boss.contactDamage = contactDamage;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (dirChangedCount == dirChangedMax)
        {
            animator.SetTrigger("stopRoll");
        }
        if (movingLeft)
        {
            if (bossPos.position.x >= leftEdge)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (bossPos.position.x <= rightEdge)
                MoveInDirection(1);
            else
                DirectionChange();
        }     
    }

    private void DirectionChange()
    {
        dirChangedCount++;
        speed *= speedMultiplier;
        movingLeft = !movingLeft;
    }
    private void MoveInDirection(int _direction)
    {
        float curEdge;
        if (_direction == -1)
        {
            curEdge = leftEdge-1;
        }
        else
        {
            curEdge = rightEdge+1;
        }
        

        Vector3 flipped = bossPos.localScale;
        flipped.z *= -1f;

        if (bossPos.position.x > curEdge && boss.isFlipped)
        {
            bossPos.localScale = flipped;
            bossPos.Rotate(0f, 180f, 0f);
            boss.isFlipped = false;
        }
        else if (bossPos.position.x < curEdge && !boss.isFlipped)
        {
            bossPos.localScale = flipped;
            bossPos.Rotate(0f, 180f, 0f);
            boss.isFlipped = true;
        }

        Vector2 target = new Vector2(curEdge, rb.position.y);
        Vector2 newPos = Vector2.MoveTowards(rb.position, target, speed * Time.fixedDeltaTime);
        rb.MovePosition(newPos);
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("stopRoll");
        boss.contactDamage = originalContactDamage;
    }
}
