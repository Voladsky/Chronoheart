using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectile_speed;
    [SerializeField] private float life_time;

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
}
