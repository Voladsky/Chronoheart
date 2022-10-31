using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour, IController
{
    public float Speed { get; set; }
    public float JumpingPower { get; set; }

    private bool isFacingRight = true;

    private Transform endCheck;

    private void Awake()
    {
        endCheck = gameObject.transform.Find("EndCheck");
    }

    void IController.Move()
    {
        Flip();
        transform.Translate(Vector2.right * Speed * Time.deltaTime);
    }

    private bool IsEnd()
    {
        return !Physics2D.Raycast(endCheck.position, Vector2.down, 0.55f).collider;
    }

    private void Flip()
    {
        if (IsEnd())
        {
            if (isFacingRight)
            {
                transform.eulerAngles = new Vector3(0, -180);
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0);
            }
            isFacingRight = !isFacingRight;
        }
    }
}
