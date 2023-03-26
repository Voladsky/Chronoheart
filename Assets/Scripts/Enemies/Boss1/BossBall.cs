using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BossBall : MonoBehaviour
{
    
    [SerializeField] Transform player;

    [SerializeField] bool isFlipped = false;

    [SerializeField] int contactDamage = 5;
    [SerializeField] int attackDamage = 20;

    [SerializeField] Vector3 attackOffset;
    [SerializeField] float attackRange = 1f;
    [SerializeField] LayerMask attackMask;

    [SerializeField] private UnityEvent onEnemyDie;
    [SerializeField] AudioClip attackSound;

    [SerializeField] private float contactDamageTimer = Mathf.Infinity;
    [SerializeField] private float contactDamageCooldown;
    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {         
            colInfo.GetComponent<Health>().TakeDamage(attackDamage);
        }

        SoundManager.Instance.PlaySoundWithRandomValues(attackSound);
    }

    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
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
        Rigidbody2D rb;
        if (TryGetComponent<Rigidbody2D>(out rb))
        {
            rb.simulated = false;
        }

        onEnemyDie.Invoke();
        GetComponent<SpriteRenderer>().sortingLayerName = "Other";
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
