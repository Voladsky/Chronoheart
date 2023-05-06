using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossPepelaz : MonoBehaviour
{
    [SerializeField] Transform player;

    public bool isFlipped = false;

    public int contactDamage = 5;

    [SerializeField] private UnityEvent onEnemyDie;
    [SerializeField] private RangeWeapon rangeWeapon;
    [SerializeField] private float contactDamageTimer = Mathf.Infinity;
    [SerializeField] private float contactDamageCooldown;

    [SerializeField] private AudioClip attackSound;

    [SerializeField] private float attackCooldown;
    [SerializeField] private float cooldownTimer;

    [SerializeField] private GameObject microChel;
    [SerializeField] private Transform spawnPoint;

    public bool isAttacking;
    public bool isSpawning;

    [SerializeField] private float spawnCooldown;
    [SerializeField] private float spawnCooldownTimer;

    private void Update()
    {
        if (isAttacking)
            Attack();
        else if (isSpawning)
            Spawn();
        
    }
    public void Attack()
    {
        cooldownTimer += Time.deltaTime;

        if (cooldownTimer >= attackCooldown)
        {
            SoundManager.Instance.PlaySoundWithRandomValues(attackSound);
            cooldownTimer = 0;
            rangeWeapon.Attack(transform.localScale.x);
        }
    }

    public void Spawn()
    {
        spawnCooldownTimer += Time.deltaTime;

        if (spawnCooldownTimer >= spawnCooldown)
        {
            SoundManager.Instance.PlaySoundWithRandomValues(attackSound);
            spawnCooldownTimer = 0;
            Instantiate(microChel, spawnPoint.position, Quaternion.identity);
        }     
    }

    public void LookAtPlayer()
    {
        Vector3 flipped = transform.localScale;
        flipped.z *= -1f;

        if (transform.position.x > player.position.x && isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = false;
        }
        else if (transform.position.x < player.position.x && !isFlipped)
        {
            transform.localScale = flipped;
            transform.Rotate(0f, 180f, 0f);
            isFlipped = true;
        }
    }
    private void OnDisable()
    {
        contactDamage = 0;
        Health playerHealth = player.GetComponent<Health>();
        playerHealth.RestoreHealth();

        Rigidbody2D rb;
        if (TryGetComponent<Rigidbody2D>(out rb))
        {
            rb.velocity = Vector2.zero;
        }

        onEnemyDie.Invoke();
        GetComponent<SpriteRenderer>().sortingLayerName = "Other";
        gameObject.layer = 12;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        contactDamageTimer += Time.deltaTime;
        if (collision.collider.CompareTag("Player") && contactDamage != 0 && contactDamageTimer >= contactDamageCooldown)
        {
            collision.collider.GetComponent<Health>().TakeDamage(contactDamage);
            contactDamageTimer = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        contactDamageTimer = Mathf.Infinity;
    }
}
