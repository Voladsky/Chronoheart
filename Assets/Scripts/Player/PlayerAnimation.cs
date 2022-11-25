using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;
    private Animator animator;

    private string currentAnimaton;
    private bool isAttackPressed;
    private bool isAttacking;

    [SerializeField]
    private float attackDelay = 0.3f;
    private float xAxis;

    //Animation States
    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_RUN = "PlayerRun";
    const string PLAYER_JUMP = "PlayerJump";
    const string PLAYER_FALL = "PlayerFall";
    const string PLAYER_ATTACK = "PlayerAttack";
    const string PLAYER_AIR_ATTACK = "PlayerAirAttack";

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        /*
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            isAttackPressed = true;
        }
        */
    }

    private void FixedUpdate()
    {
        if (player.LastOnGroundTime > 0 && !isAttacking)
        {
            if (xAxis != 0)
            {
                ChangeAnimationState(PLAYER_RUN);
            }
            else
            {
                ChangeAnimationState(PLAYER_IDLE);
            }
        }


        if (player.LastOnGroundTime <= 0)
        {           
            if (player._isJumpFalling)
            {
                //ChangeAnimationState(PLAYER_FALL);
                Debug.Log("FALLING");
            }
            else
            {
                if (player.IsJumping)
                {
                    //ChangeAnimationState(PLAYER_JUMP);
                    Debug.Log("JUMPING");
                }
                else
                {
                    //ChangeAnimationState(PLAYER_FALL);
                    Debug.Log("FALLING");
                }
            }
        }
        
        
        /*
        //attack
        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking)
            {
                isAttacking = true;

                if (player.LastOnGroundTime > 0)
                {
                    ChangeAnimationState(PLAYER_ATTACK);
                }
                else
                {
                    ChangeAnimationState(PLAYER_AIR_ATTACK);
                }
                Invoke("AttackComplete", attackDelay);
            }
        }
        */
    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.Play(newAnimation);
        currentAnimaton = newAnimation;
    }
}
