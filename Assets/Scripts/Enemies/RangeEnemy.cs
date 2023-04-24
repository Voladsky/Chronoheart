using UnityEngine;
using UnityEngine.Events;

public class RangeEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int contactDamage;
    [SerializeField] private RangeWeapon rangeWeapon;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Player")]
    [SerializeField] private Transform player;

    //References
    private Animator anim;
    private Health playerHealth;
    private EnemyBehaviour enemyPatrol;

    [SerializeField] private UnityEvent onEnemyDie;

    [SerializeField] AudioClip attackSound;
    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyBehaviour>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                SoundManager.Instance.PlaySoundWithRandomValues(attackSound);
                cooldownTimer = 0;
                //anim.SetTrigger("meleeAttack");
                Debug.Log(transform.localScale.x);
                rangeWeapon.Attack(transform.localScale.x);
            }
        }

        //if (enemyPatrol != null)
        //    enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
           Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
           0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();
        else if (Mathf.Abs(player.position.x - transform.position.x) < range && player.position.y < transform.position.y) {
            hit = Physics2D.Raycast(player.position, Vector2.up, transform.position.y - player.position.y);
            if (hit.collider == null)
                playerHealth = hit.transform.GetComponent<Health>();
        }
        return hit.collider != null;
#if false
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();



        return hit.collider != null;
#endif
    }
    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Ray(transform.position, new Vector2(transform.localScale.x, 0)));
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && contactDamage != 0)
        {
            collision.GetComponent<Health>().TakeDamage(contactDamage);
        }
    }

    private void OnDisable()
    {
        contactDamage = 0;

        Rigidbody2D rb;
        if (TryGetComponent<Rigidbody2D>(out rb))
        {
            rb.velocity = Vector2.zero;
        }
                     
        onEnemyDie.Invoke();
        GetComponent<SpriteRenderer>().sortingLayerName = "Other";
        gameObject.layer = 12;
        transform.GetChild(0).gameObject.layer = 12;
    }
}
