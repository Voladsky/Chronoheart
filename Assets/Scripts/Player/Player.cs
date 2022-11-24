using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player: MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private PlayerAttack playerAttack;
    [SerializeField] private Health health;
    private float cooldown;
    private float timer;
    
    void Start()
    {
        StartCoroutine(DecreaseHealth());
        timer = 0;
        cooldown = GameObject.Find("Timer").GetComponent<Timer>().BPM_Timer;
    }

    void Update()
    {
        if (timer > 0) timer -= Time.deltaTime;
        if (Input.GetMouseButton(0) && timer <= 0)
        {
            playerAttack.Attack(damage);
            timer = cooldown;
        }
    }
    private IEnumerator DecreaseHealth()
    {
        while (health.currentHealth > 0)
        {
            health.ReduceHealth(1);
            yield return new WaitForSeconds(1);
        }
    }
}
