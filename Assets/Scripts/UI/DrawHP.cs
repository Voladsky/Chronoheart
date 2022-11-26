using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;
using System.CodeDom.Compiler;

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
