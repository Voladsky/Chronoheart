using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public int startingHealth;
    public int CurrentHealth { get; private set; }
    private void Awake()
    {
        CurrentHealth = startingHealth;
        StartCoroutine(decreaseHealth());
    }
    public void TakeDamage(float _damage)
    {
        CurrentHealth = (int)Mathf.Clamp(CurrentHealth - _damage, 0, startingHealth);
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
