using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2_Die : StateMachineBehaviour
{
    private GameObject boss;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        boss = GameObject.Find("Boss");
        var rb = boss.GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Dynamic;
        var script = boss.GetComponent<BossPepelaz>();
        script.contactDamage = 0;
        boss.layer = 12;
        boss.GetComponent<SpriteRenderer>().sortingLayerName = "Other";
        script.onEnemyDie.Invoke();
        var playerHealth = GameObject.Find("Player").GetComponent<Health>();
        playerHealth.RestoreHealth();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
