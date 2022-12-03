using UnityEngine;
using TMPro;

public class DrawHP : MonoBehaviour
{
    [SerializeField] private TMP_Text mesh;
    [SerializeField] private Health health;

    private void Update()
    {
        int minutes = (int)(health.currentHealth / 60);
        int seconds = (int)(health.currentHealth % 60);
        mesh.text = $"{minutes / 10}{minutes % 10}:{seconds / 10}{seconds % 10}";
    }
}
