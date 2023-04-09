using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;
using System;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float life_time;
    [SerializeField] private float damage;
    [SerializeField] private int[] forbidden_layers;
    private bool tookDamage;

    private void Start()
    {
        Invoke("DestroyProjectile", life_time);
    }
    private void Update()
    {
        
        var collisions = Physics2D.OverlapCircleAll(transform.position, gameObject.GetComponent<SpriteRenderer>().size.normalized.x);
        if (collisions.Length != 0)
        {
            var enemies = collisions.Select(x => x.GetComponent<Health>()).Where(x => x != null).ToList();
            if (enemies.Count != 0)
            {
                foreach (var enemy in enemies) 
                    if (!forbidden_layers.Contains(enemy.gameObject.layer))
                    {
                        enemy.TakeDamage(damage);
                        tookDamage = true;
                    }

                if (tookDamage)
                {
                    DestroyProjectile();
                }              
            }
        }

    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }
}
