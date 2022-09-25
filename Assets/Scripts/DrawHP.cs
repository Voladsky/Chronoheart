using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Threading;

public class DrawHP : MonoBehaviour
{
    public Health health;
    public TMP_Text mesh;
    private IEnumerator CountDown()
    {
        while (true)
        {
            mesh.text = $"{health.CurrentHealth / 60}:{health.CurrentHealth % 60}";
            yield return new WaitForSeconds(1f);
        }

    }
    private void Start()
    {
        StartCoroutine(CountDown());
    }
}
