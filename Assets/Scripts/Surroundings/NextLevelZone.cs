using UnityEngine;

public class NextLevelZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            LevelLoader.instance.LoadNextLevel();
        }
    }
}
