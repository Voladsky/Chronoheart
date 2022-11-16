using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] MovingPlatform platform;
    [SerializeField] bool isOff = true;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E))
        {
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
