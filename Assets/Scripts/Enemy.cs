using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    public int damage;
    public float cooldown;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<EnemyController>();
        controller.JumpingPower = jumpingPower;
        controller.Speed = speed;
        timer = cooldown;
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move();
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(timer);
        IDamageable damageable = null;
        collision.gameObject.TryGetComponent<IDamageable>(out damageable);
        if (damageable != null)
        {
            Attack(damageable, damage);
        }
    }

    private void Attack(IDamageable entity, int damage)
    {
        if (timer > 0) timer -= Time.deltaTime;
        else
        {
            Debug.Log("ATK");
            entity.TakeDamage(damage);
            timer = cooldown;
        }
    }

}
