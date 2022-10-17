using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character, IDamageable
{
    public int startingHealth;
    public int CurrentHealth { get; set; }
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
        CurrentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move();
        if (timer > 0) timer -= Time.deltaTime;
        ParseCollision();
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth = (int)Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
    }
    private void ParseCollision()
    {
        Transform range = transform.Find("Range");
        Collider2D collision = Physics2D.OverlapBox(range.position, range.localScale, 0f);
        if (collision != null)
        {
            IDamageable damageable = null;
            collision.gameObject.TryGetComponent<IDamageable>(out damageable);
            if (!collision.isTrigger && damageable != null)
            {
                Attack(damageable, damage);
            }
        }
    }

    private void Attack(IDamageable entity, int damage)
    {
        if (timer <= 0) {
            Debug.Log("ATK");
            entity.TakeDamage(damage);
            timer = cooldown;
        }
    }

}
