using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IDamageable
{
    public int startingHealth;
    public int CurrentHealth { get; set; }
    public int damage;
    private float cooldown;
    private float timer;
    // Start is called before the first frame update
    void Start()
    {
        // define PlayerController
        controller = gameObject.GetComponent<PlayerController>();
        controller.JumpingPower = jumpingPower;
        controller.Speed = speed;
        // define BasicAttacker
        attacker = gameObject.GetComponent<BasicAttacker>();
        // set cur health
        CurrentHealth = startingHealth;
        // start ticking
        StartCoroutine(decreaseHealth());
        timer = 0;
        cooldown = GameObject.Find("Timer").GetComponent<Timer>().BPM_Timer;
    }
    // Update is called once per frame
    void Update()
    {
        controller.Move();
        if (timer > 0) timer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && timer <= 0)
        {
            attacker.Attack(damage);
            timer = cooldown;
        }
    }
    private IEnumerator decreaseHealth()
    {
        while (CurrentHealth > 0)
        {
            TakeDamage(1);
            yield return new WaitForSeconds(1);
        }
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth = (int)Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
        if (CurrentHealth == 0) Destroy(gameObject);
    }
}
