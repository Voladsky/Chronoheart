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
        attacker = gameObject.GetComponent<BasicAttacker>();
        timer = cooldown;
        CurrentHealth = startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move();
        if (timer > 0) timer -= Time.deltaTime;
        else if (timer <= 0)
        {
            attacker.Attack(damage);
            timer = cooldown;
        }
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth = (int)Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
        if (CurrentHealth == 0) Destroy(gameObject);
    }
}
