using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    }

    void DestroyProjectile()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy_melee = collision.gameObject.GetComponent<Health>();
        if (enemy_melee != null)
        {
            enemy_melee.TakeDamage(damage);
            DestroyProjectile();
        }
    }
}
