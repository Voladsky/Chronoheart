using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FlyingEnemy : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;
    [SerializeField] private int contactDamage;

    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    [Header("Player")]
    [SerializeField] private Transform player;

    [Header("Enemy")]
    [SerializeField] private Transform enemy;
    //References
    private Animator anim;
    private Health playerHealth;
    private EnemyBehaviour enemyPatrol;
    private RangeWeapon rangeWeapon;

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
                anim.SetTrigger("meleeAttack");
            }
        }

        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }

    private bool PlayerInSight()
    {
        if ((player.position-enemy.position).magnitude<=range)
        {
            rangeWeapon.Attack(transform.localScale.x);
            return true;
        }
        return false;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
            playerHealth.TakeDamage(damage);
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
        Destroy(GetComponent<Rigidbody2D>());
        onEnemyDie.Invoke();
        GetComponent<SpriteRenderer>().sortingLayerName = "Other";
    }
}
