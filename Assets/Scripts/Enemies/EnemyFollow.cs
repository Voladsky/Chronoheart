using UnityEngine;

public class EnemyFollow : EnemyBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;

    [Header("Movement parameters")]
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    [Header("Idle Behaviour")]
    [SerializeField] private float idleDuration;
    [SerializeField] private float seeDistance;
    private float idleTimer;

    [Header("Enemy Animator")]
    [SerializeField] private Animator anim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }
    private void OnDisable()
    {
        anim.SetBool("moving", false);
    }
    private void Start()
    {
        leftEdge.SetParent(null, true);
        rightEdge.SetParent(null, true);
    }
    private void Update()
    {
        Vector3 project = Vector3.Project(player.position - enemy.position, new Vector3(1, 0, 0));
        if (project.magnitude < seeDistance) FollowPlayer(project.normalized);
        else MoveInIdle();
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
        else MoveInDirection((int)project.normalized.x);
    }

    void MoveInIdle()
    {
        if (movingLeft)
        {
            if (enemy.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (enemy.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        anim.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int _direction)
    {
        idleTimer = 0;
        anim.SetBool("moving", true);

        //Make enemy face direction
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * _direction,
            initScale.y, initScale.z);

        //Move in that direction
        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * _direction * speed,
            enemy.position.y, enemy.position.z);
    }
}
