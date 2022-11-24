using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class DrawHP : MonoBehaviour
{
    [SerializeField] private TMP_Text mesh;
    [SerializeField] private Health health;

    private void Update()
    {
        mesh.text = $"{(int)(health.currentHealth / 60)}:{(int)(health.currentHealth % 60)}";
    }
}
