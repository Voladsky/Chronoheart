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
        projectile.transform.localScale = new Vector3(scale, 1, 1);
        var collisions = Physics2D.OverlapCircleAll(range.position, projectile.GetComponent<SpriteRenderer>().size.normalized.x);
        if (collisions.Length != 1 || collisions.First().GetComponent<TilemapCollider2D>() == null)
        {
            Instantiate(projectile, range.position, range.rotation);
        }
    }

    public void ComboAttack(float scale)
    {
        combo_projectile.transform.localScale = new Vector3(scale, 1, 1);
        var collisions = Physics2D.OverlapCircleAll(range.position, combo_projectile.GetComponent<SpriteRenderer>().size.normalized.x);
        if (collisions.Length != 1 || collisions.First().GetComponent<TilemapCollider2D>() == null)
        {
            Instantiate(combo_projectile, range.position, range.rotation);
        }
    }
}
