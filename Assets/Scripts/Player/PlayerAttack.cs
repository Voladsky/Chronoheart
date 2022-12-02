using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private Transform range;

    public void Attack(float damage)
    {
        Collider2D collision = Physics2D.OverlapBox(range.position, range.localScale, 0f);
        if (collision != null)
        {
            Health enemy = null;
            collision.gameObject.TryGetComponent<Health>(out enemy);
            if (enemy != null)
            {
                Damage(enemy, damage);
            }
        }
    }
    private void Damage(Health enemy, float damage)
    {
        Debug.Log("PLAYERATK");
        enemy.TakeDamage(damage);
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(range.position, range.localScale.x);       
    }
}
