using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] MovingPlatform platform;
    [SerializeField] bool isOff = false;
    [SerializeField] float cooldown = 0.5f;
    private float lastSwitch = -10f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && Time.time - lastSwitch > cooldown)
        {
            lastSwitch = Time.time;
            if (isOff)
            {
                platform.SetMovement(true);
                platform.Move();
                isOff = false;
            }
            else
            {
                isOff = true;
                platform.SetMovement(false);
            }
        }
    }
}
