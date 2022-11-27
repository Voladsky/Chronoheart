using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class Savepoint : MonoBehaviour
{
    [SerializeField] float cooldown = 0.5f;
    private float lastSwitch = -10f;
    [SerializeField] Health playerHealth;
    [SerializeField] TextMeshProUGUI savedText;

    private void Awake()
    {
        savedText.faceColor = new Color32(255, 255, 255, 0);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.E) && Time.time - lastSwitch > cooldown)
        {
            lastSwitch = Time.time;

            string s = $"{transform.position.x}|{transform.position.y}";

            PlayerPrefs.SetString("PlayerSavePosition", s);

            playerHealth.AddHealth(1e6f);
          
            StartCoroutine(ShowText());

            // Animation
            Debug.Log("Saved");
        }
    }

    IEnumerator ShowText()
    {
        savedText.faceColor = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(5);

        savedText.faceColor = new Color32(255, 255, 255, 0);
    }
    
}
