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
        controller = gameObject.GetComponent<PlayerController>();
        controller.JumpingPower = jumpingPower;
        controller.Speed = speed;
        CurrentHealth = startingHealth;
        StartCoroutine(decreaseHealth());
        timer = 0;
        cooldown = GameObject.Find("Timer").GetComponent<Timer>().BPM_Timer;
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move();
        if (Input.GetMouseButton(0)) ParseCollision();
        if (timer > 0) timer -= Time.deltaTime;
    }
    public void TakeDamage(int damage)
    {
        CurrentHealth = (int)Mathf.Clamp(CurrentHealth - damage, 0, startingHealth);
    }

    private IEnumerator decreaseHealth()
    {
        while (CurrentHealth > 0)
        {
            TakeDamage(1);
            yield return new WaitForSeconds(1);
        }
    }

    private void ParseCollision()
    {
        Transform range = transform.Find("Range");
        Collider2D collision = Physics2D.OverlapBox(range.position, range.localScale, 0f);
        if (collision != null) {
            IDamageable damageable = null;
            collision.gameObject.TryGetComponent<IDamageable>(out damageable);
            if (!collision.isTrigger && damageable != null) {
                Attack(damageable, damage);
            }
        }
    }
    private void Attack(IDamageable entity, int damage)
    {
        if (timer <= 0)
        {
            Debug.Log("PLAYER_ATK");
            entity.TakeDamage(damage);
            Debug.Log(entity.CurrentHealth);
            timer = cooldown;
        }
    }
}
