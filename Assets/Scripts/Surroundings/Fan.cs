using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fan : MonoBehaviour
{
    [SerializeField] float speed = 10;
    [SerializeField] Vector2 direction;
    [SerializeField] bool isOff;
    private ParticleSystem particle;
    [SerializeField] Animator animator;

    private void Start()
    {
        particle = GetComponentInChildren<ParticleSystem>();
        if (isOff)
        {
            animator.Play("FanStop");
            particle.Stop();
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isOff)
        {
            Rigidbody2D rb;
            if (collision.CompareTag("Enemy"))
            {
                rb = collision.GetComponentInParent<Rigidbody2D>();
            }
            else
            {
                rb = collision.GetComponent<Rigidbody2D>();
            }
            rb.AddForce(speed * direction, ForceMode2D.Force);
        }
    }

    public void SetState(bool isOff)
    {
        this.isOff = isOff;

        animator.SetTrigger("Switch");

        if (!isOff)
        {
            particle.Play();
        }
        else
        {
            particle.Stop();
        }
    }
    /*
    private void FixedUpdate()
    {
        if (rbs.Count > 0)
        {
            foreach (var rb in rbs)
            {
                rb.AddForce(speed * direction, ForceMode2D.Impulse);
                Debug.Log("AddForce " + rb.name);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isOff)
        {
            Rigidbody2D rb;
            if (collision.CompareTag("Enemy"))
            {                
                rb = collision.GetComponentInParent<Rigidbody2D>();
            }
            else
            {
                rb = collision.GetComponent<Rigidbody2D>();
            }
            
            rbs.Add(rb);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Rigidbody2D rb;
        if (collision.CompareTag("Enemy"))
        {
            rb = collision.GetComponentInParent<Rigidbody2D>();
        }
        else
        {
            rb = collision.GetComponent<Rigidbody2D>();
        }
        
        if (rbs.Contains(rb))
        {
            rbs.Remove(collision.GetComponent<Rigidbody2D>());
        }
    }
    */
}
