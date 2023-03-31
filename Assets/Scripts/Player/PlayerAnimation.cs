using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;
    [SerializeField] private Health playerHealth;
    [SerializeField] private PlayerAttack playerAttack;
    private Animator animator;

    public string currentAnimaton;
    private bool isAttackPressed;
    private bool isAttacking;

    private bool isRangeAttackPressed;
    private bool isRangeAttacking;

    private bool isComboPressed;
    private bool isComboAttacking;
    private bool isDownCombo;

    [SerializeField]
    private float attackDelay = 0.26875f;
    [SerializeField]
    private float rangeAttackDelay = 0.3f;
    [SerializeField]
    private float comboDelay = 0.3f;
    private float xAxis;

    //Animation States
    const string PLAYER_IDLE = "PlayerIdle";
    const string PLAYER_RUN = "PlayerRun";
    const string PLAYER_JUMP = "PlayerJump";
    const string PLAYER_FALL = "PlayerBeginToFall";
    const string PLAYER_ATTACK = "PlayerAttack";
    const string PLAYER_AIR_ATTACK = "PlayerAirAttack";
    const string PLAYER_RANGE_ATTACK = "PlayerRangeAttack";
    const string PLAYER_MOVE_DOWN_COMBO = "PlayerMoveDownCombo";
    const string PLAYER_ARROW_DOWN_COMBO = "PlayerArrowDownCombo";
    const string PLAYER_ARROW_UP_COMBO = "PlayerArrowUpCombo";

    string curCombo = "";

    private void Start()
    {
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        xAxis = Input.GetAxisRaw("Horizontal");

        if (!playerHealth.dead)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                isAttackPressed = true;
            }
            else if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                isRangeAttackPressed = true;
            }
        }            
    }

    private void FixedUpdate()
    {
        if (player.LastOnGroundTime > 0 && !isAttacking && !isRangeAttacking)
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


        if (player.LastOnGroundTime <= 0 && !isAttacking && !isRangeAttacking && !isDownCombo)
        {           
            if (player._isJumpFalling)
            {
                ChangeAnimationState(PLAYER_FALL);
            }
            else
            {
                if (player.IsJumping)
                {
                    ChangeAnimationState(PLAYER_JUMP);
                }
                else
                {
                    ChangeAnimationState(PLAYER_FALL);
                }
            }
        }
        
        
        
        //attack
        if (isAttackPressed)
        {
            isAttackPressed = false;

            if (!isAttacking)
            {
                isAttacking = true;
                ChangeAnimationState(PLAYER_ATTACK);
                /*
                if (player.LastOnGroundTime > 0)
                {
                    ChangeAnimationState(PLAYER_ATTACK);
                }
                else
                {
                    ChangeAnimationState(PLAYER_AIR_ATTACK);
                }
                */
                Invoke("AttackComplete", attackDelay);
            }
        }
        else if (isRangeAttackPressed)
        {
            isRangeAttackPressed = false;

            if (!isRangeAttacking)
            {
                isRangeAttacking = true;
                ChangeAnimationState(PLAYER_RANGE_ATTACK);
                Invoke("RangeAttackComplete", rangeAttackDelay);
            }
        }

        if (isComboPressed && curCombo != PLAYER_MOVE_DOWN_COMBO)
        {
            isComboPressed = false;

            if (!isComboAttacking)
            {
                isComboAttacking = true;
                ChangeAnimationState(curCombo);
                Invoke("ComboComplete", comboDelay);
            }
        }

        if (player.LastOnGroundTime <= 0 && isDownCombo)
        {
            ChangeAnimationState(PLAYER_MOVE_DOWN_COMBO);
        }
        if (player.LastOnGroundTime > 0)
        {
            isDownCombo = false;
        }
    }

    void AttackComplete()
    {
        isAttacking = false;
    }

    void RangeAttackComplete()
    {
        isRangeAttacking = false;
    }

    void ComboComplete()
    {
        isComboAttacking = false;
    }

    public void ComboPerformed(string combo)
    {
        if (combo == PLAYER_MOVE_DOWN_COMBO)
        {
            isDownCombo = true;
        }
        else
        {
            isComboPressed = true;
        }      
        curCombo = combo;
    }
    void ChangeAnimationState(string newAnimation)
    {
        if (currentAnimaton == newAnimation) return;

        animator.CrossFade(newAnimation, 0f, 0);
        currentAnimaton = newAnimation;
    }
}
