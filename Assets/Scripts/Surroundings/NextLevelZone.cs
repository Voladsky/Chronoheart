using UnityEngine;

public class NextLevelZone : MonoBehaviour
{
    [SerializeField] int nextSceneIndex;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            StartCoroutine(LevelLoader.instance.LoadLevel(nextSceneIndex));
        }
    }
}
