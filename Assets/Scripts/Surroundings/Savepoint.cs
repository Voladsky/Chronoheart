using System.Collections;
using UnityEngine;
using TMPro;

public class Savepoint : MonoBehaviour
{
    [SerializeField] float cooldown = 0.5f;
    private float lastSwitch = -10f;
    [SerializeField] Health playerHealth;
    [SerializeField] TextMeshProUGUI savedText;
    [SerializeField] Animator animator;

    private void Awake()
    {
        savedText.text = "Saved!";
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

            animator.Play("SavepointWork");
            Debug.Log("Saved");
        }
    }

    IEnumerator ShowText()
    {
        savedText.text = "Saved!";

        savedText.faceColor = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(5);

        savedText.faceColor = new Color32(255, 255, 255, 0);
    }
    
}
