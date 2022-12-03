using UnityEngine;

public class BasicAttacker : MonoBehaviour, IAttacker
{
    public void Attack(int damage)
    {
        Transform range = transform.Find("Range");
        Collider2D collision = Physics2D.OverlapBox(range.position, range.localScale, 0f);
        if (collision != null)
        {
            IDamageable damageable = null;
            collision.gameObject.TryGetComponent<IDamageable>(out damageable);
            if (!collision.isTrigger && damageable != null)
            {
                Damage(damageable, damage);
            }
        }
    }
    private void Damage(IDamageable entity, int damage)
    {
        Debug.Log("PLAYERATK");
        entity.TakeDamage(damage);
    }

}
