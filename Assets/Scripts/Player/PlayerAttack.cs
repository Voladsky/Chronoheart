using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform range;
    [SerializeField] private float attackCooldown;
    private float cooldownTimer = Mathf.Infinity;
    private void Update()
    {
        cooldownTimer += Time.deltaTime;
    }

    public void Attack(float damage)
    {
        if (cooldownTimer >= attackCooldown)
        {
            var collisions = Physics2D.OverlapBoxAll(range.position, range.localScale, 0f);
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
    private void Damage(Health enemy, float damage)
    {
        enemy.TakeDamage(damage);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(range.position, range.localScale.x);       
    }
}
