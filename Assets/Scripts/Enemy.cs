using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public int damage;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<EnemyController>();
        controller.JumpingPower = jumpingPower;
        controller.Speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = null;
        collision.gameObject.TryGetComponent<IDamageable>(out damageable);
        if (damageable != null) damageable.TakeDamage(damage);
    }

}
