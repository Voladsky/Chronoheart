using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Savepoint : MonoBehaviour
{
    [SerializeField] float cooldown = 1f;
    private float lastSwitch = -1000f;
    [SerializeField] Health playerHealth;
    [SerializeField] TextMeshProUGUI savedText;
    [SerializeField] Animator animator;
    [SerializeField] AudioClip savedSound;

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
            PlayerPrefs.SetInt("PlayerSaveLevel", SceneManager.GetActiveScene().buildIndex);

            playerHealth.AddHealth(1e6f);
          
            StartCoroutine(ShowText());

            animator.Play("SavepointWork");
            SoundManager.Instance.PlaySound(savedSound);
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
