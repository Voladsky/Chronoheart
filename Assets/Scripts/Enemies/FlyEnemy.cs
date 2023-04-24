using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyEnemy : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private float waitFor;
    [SerializeField]
    private float power;
    private void Start()
    {
        StartCoroutine(Fly());
    }

    private IEnumerator Fly()
    {
        bool upordown = true;
        while (true)
        {
            rb.velocity.Set(rb.velocity.x, 0);
            if (upordown) 
                rb.AddForce(Vector2.up * power, ForceMode2D.Impulse);
            else 
                rb.AddForce(Vector2.down * power, ForceMode2D.Impulse);
            upordown = !upordown;
            yield return new WaitForSeconds(waitFor);
            
        }
    }
}
