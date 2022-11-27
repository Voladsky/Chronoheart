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

    private void Awake()
    {
        
        if (!PlayerPrefs.HasKey("PlayerSavePosition"))
            return;

        string[] pos = PlayerPrefs.GetString("PlayerSavePosition").Split('|');

        transform.position = new Vector3(float.Parse(pos[0]), float.Parse(pos[1]), 0);    
    }
    private void Start()
    {
        StartCoroutine(DecreaseHealth());
        timer = 0;
        cooldown = GameObject.Find("Timer").GetComponent<Timer>().BPM_Timer;
    }

    private void Update()
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
