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
    [SerializeField] private AudioClip rangeAttackclip;
    [SerializeField] private float trajectory = 0.4f;
    public void Attack(float scale)
    {
        Vector3 to_spawn = range.position;
        projectile.transform.localScale = new Vector3(-scale, 1, 1);
        var collisions = Physics2D.OverlapCircleAll(range.position, projectile.GetComponent<SpriteRenderer>().size.normalized.x);
        if (collisions.Length != 1 || collisions.First().GetComponent<TilemapCollider2D>() == null)
        {
            var hit = Physics2D.OverlapPoint(range.position);
            if (hit != null) to_spawn = transform.position; 
            SoundManager.Instance.PlaySoundWithRandomValues(rangeAttackclip);
            Rigidbody2D rb = Instantiate(projectile, to_spawn, range.rotation).GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(scale, trajectory) * 2, ForceMode2D.Impulse);
            rb.AddTorque(-2 * scale);

        }
    }

    public void ComboAttack(float scale)
    {
        Vector3 to_spawn = range.position;
        combo_projectile.transform.localScale = new Vector3(-scale, 1, 1);
        var collisions = Physics2D.OverlapCircleAll(range.position, projectile.GetComponent<SpriteRenderer>().size.normalized.x);
        if (collisions.Length != 1 || collisions.First().GetComponent<TilemapCollider2D>() == null)
        {
            var hits = Physics2D.RaycastAll(transform.position, Vector2.left * projectile.transform.localScale.x, (transform.position - range.position).magnitude);
            foreach (var hit in hits)
            {
                if (hit.rigidbody != null)
                {
                    to_spawn = hit.point;
                    break;
                }
            }
            Rigidbody2D rb = Instantiate(projectile, to_spawn, range.rotation).GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(scale, trajectory) * 4, ForceMode2D.Impulse);
            rb.AddTorque(-5 * scale);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawRay(new Ray(transform.position, Vector2.left * projectile.transform.localScale.x));
    }
}
