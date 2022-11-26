using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using UnityEngine;

public class Savepoint : MonoBehaviour
{
    [SerializeField] float cooldown = 0.5f;
    private float lastSwitch = -10f;


    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && Time.time - lastSwitch > cooldown)
        {
            lastSwitch = Time.time;

            string s = $"{transform.position.x}|{transform.position.y}";

            PlayerPrefs.SetString("PlayerSavePosition", s);

            // Animation
            Debug.Log("Saved");
        }
    }
}
