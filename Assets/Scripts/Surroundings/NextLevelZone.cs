using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelZone : MonoBehaviour
{
    [SerializeField] int nextSceneIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerPrefs.DeleteKey("PlayerSavePosition");
            PlayerPrefs.SetInt("PlayerSaveLevel", nextSceneIndex);
            StartCoroutine(LevelLoader.instance.LoadLevel(nextSceneIndex));
        }
    }
}
