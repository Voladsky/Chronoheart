using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private GameObject combo_projectile;
    [SerializeField] private Transform range;
    public void Attack(float scale)
    {
        projectile.transform.localScale = new Vector3(-scale, 1, 1);
        var collisions = Physics2D.OverlapCircleAll(range.position, projectile.GetComponent<SpriteRenderer>().size.normalized.x);
        if (collisions.Length != 1 || collisions.First().GetComponent<TilemapCollider2D>() == null)
        {
            var hits = Physics2D.RaycastAll(transform.position, Vector2.left * projectile.transform.localScale.x, (transform.position - range.position).magnitude);
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject.layer == 6)
                {
                    Debug.Log(hit.transform.gameObject.name);
                    return;
                }
            }
            Rigidbody2D rb = Instantiate(projectile, range.position, range.rotation).GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(scale, 0.4f) * 2, ForceMode2D.Impulse);
            rb.AddTorque(-2 * scale);

        }
    }

    public void ComboAttack(float scale)
    {
        combo_projectile.transform.localScale = new Vector3(-scale, 1, 1);
        var collisions = Physics2D.OverlapCircleAll(range.position, projectile.GetComponent<SpriteRenderer>().size.normalized.x);
        if (collisions.Length != 1 || collisions.First().GetComponent<TilemapCollider2D>() == null)
        {
            var hits = Physics2D.RaycastAll(transform.position, Vector2.left * projectile.transform.localScale.x, (transform.position - range.position).magnitude);
            foreach (var hit in hits)
            {
                if (hit.transform.gameObject.layer == 6)
                {
                    Debug.Log(hit.transform.gameObject.name);
                    return;
                }
            }
            Rigidbody2D rb = Instantiate(projectile, range.position, range.rotation).GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(scale, 0.4f) * 4, ForceMode2D.Impulse);
            rb.AddTorque(-5 * scale);
        }
    }
}
