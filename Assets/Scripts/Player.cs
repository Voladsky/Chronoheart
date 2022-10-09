using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character, IDamageable
{
    public int startingHealth;
    public int CurrentHealth { get; set; }
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<PlayerController>();
        controller.JumpingPower = jumpingPower;
        controller.Speed = speed;
        CurrentHealth = startingHealth;
        StartCoroutine(decreaseHealth());
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move();
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
}
