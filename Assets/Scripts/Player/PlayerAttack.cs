using UnityEngine;
using System.Linq;
using System.Collections;
using System;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform range;
    [SerializeField] private float attackCooldown;

    [SerializeField] AudioClip attackSound;
    [SerializeField] RangeWeapon rangeWeapon;

    private float cooldownTimer = Mathf.Infinity;
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    public void Attack(float damage, bool noCooldown)
    {
        if (cooldownTimer >= attackCooldown || noCooldown)
        {
            SoundManager.Instance.PlaySoundWithRandomValues(attackSound);
            var collisions = Physics2D.OverlapCircleAll(range.position, range.localScale.x);
            if (collisions.Length != 0)
            {
                var enemies = collisions.Select(x => x.GetComponent<Health>()).Where(x => x != null).ToList();
                if (enemies.Count != 0)
                {
                    cooldownTimer = 0;
                    foreach (var enemy in enemies) Damage(enemy, damage);
                }
            }
        }
    }

    public void RangeAttack()
    {
        if (cooldownTimer >= attackCooldown)
        {
            rangeWeapon.Attack(transform.localScale.x);
            cooldownTimer = 0;
        }
    }

    public void RangeCombo()
    {
        rangeWeapon.ComboAttack(transform.localScale.x);
    }

    public void ComboAttack23(float damage)
    {
        var movement = GetComponent<PlayerMovement>();
        StartCoroutine(PerformCombo23(damage));
    }

    private IEnumerator PerformCombo23(float damage)
    {
        var movement = GetComponent<PlayerMovement>();
        if (movement._isJumpFalling)
            yield return new WaitUntil(() => !movement._isJumpFalling);
        if (movement.IsJumping)
            yield return new WaitUntil(() => !movement.IsJumping);
        Debug.Log("beeep!");
        SoundManager.Instance.PlaySoundWithRandomValues(attackSound);
        var collisions = Physics2D.OverlapCircleAll(transform.position + Vector3.down, range.localScale.x * 2);
        if (collisions.Length != 0)
        {
            var enemies = collisions.Select(x => x.GetComponent<Health>()).Where(x => x != null).ToList();
            if (enemies.Count != 0)
            {
                foreach (var enemy in enemies) Damage(enemy, damage);
            }
        }
    }
    public void ComboAttack60(float damage)
    {
        SoundManager.Instance.PlaySoundWithRandomValues(attackSound);
        var collisions = Physics2D.OverlapBoxAll(range.position + new Vector3(transform.localScale.x, 0, 0), range.localScale * 4, 0f);
        if (collisions.Length != 0)
        {
            var enemies = collisions.Select(x => x.GetComponent<Health>()).Where(x => x != null).ToList();
            if (enemies.Count != 0)
            {
                foreach (var enemy in enemies)
                {
                    if (enemy.GetComponent<Player>() == null)
                    {
                        enemy.GetComponent<Rigidbody2D>().AddForce(Vector2.up * 15 + new Vector2(transform.localScale.x * 1.5f, 0), ForceMode2D.Impulse);
                    }
                    Damage(enemy, damage);
                }
            }
        }
    }

    public void ComboAttack30(float damage)
    {
        SoundManager.Instance.PlaySoundWithRandomValues(attackSound);
        var collisions = Physics2D.OverlapBoxAll(range.position + new Vector3(transform.localScale.x, 0, 0), range.localScale * 4, 0f);
        if (collisions.Length != 0)
        {
            var enemies = collisions.Select(x => x.GetComponent<Health>()).Where(x => x != null).ToList();
            if (enemies.Count != 0)
            {
                foreach (var enemy in enemies)
                {
                    if (enemy.GetComponent<Player>() == null)
                    {
                        enemy.GetComponent<Rigidbody2D>().AddForce(new Vector2(transform.localScale.x * 10, 0), ForceMode2D.Impulse);
                    }
                    Damage(enemy, damage);
                }
            }
        }
    }

    private void Damage(Health enemy, float damage)
    {
        if (enemy.GetComponent<Player>() == null)
            enemy.TakeDamage(damage);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + Vector3.down, range.localScale.x * 2);
        Gizmos.DrawCube(range.position + new Vector3(transform.localScale.x, 0, 0), range.localScale * 4);
    }
}
