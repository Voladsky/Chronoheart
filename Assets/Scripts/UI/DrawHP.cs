using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class DrawHP : MonoBehaviour
{
    public TMP_Text mesh;
    private void Update()
    {
        int health = gameObject.GetComponent<IDamageable>().CurrentHealth;
        mesh.text = $"{health / 60}:{health % 60}";
    }
}
