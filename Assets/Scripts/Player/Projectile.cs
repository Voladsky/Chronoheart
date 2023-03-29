using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectile_speed;
    [SerializeField] private float life_time;
    [SerializeField] private float damage;

    private void Start()
    {
        Debug.Log(transform.localEulerAngles);
        Invoke("DestroyProjectile", life_time);
    }
    private void Update()
    {
        this.GetComponent<Rigidbody2D>().velocity = new Vector2(transform.localScale.x * projectile_speed, 0f);
        var collisions = Physics2D.OverlapCircleAll(transform.position, gameObject.GetComponent<SpriteRenderer>().size.normalized.x);
        if (collisions.Length != 0)
        {
            var enemies = collisions.Select(x => x.GetComponent<Health>()).Where(x => x != null).ToList();
            if (enemies.Count != 0)
            {
                foreach (var enemy in enemies) enemy.TakeDamage(damage);
                DestroyProjectile();
            }
        }

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
