using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public bool CurTick { get; set; } = true;
    public float BPM_Timer { get; set; } = 0.375f;
    Color[] clrs;
    // Start is called before the first frame update
    void Start()
    {
        clrs = new Color[] { Color.black, Color.yellow };
        StartCoroutine(Time_Dec());
    }
    private IEnumerator Time_Dec()
    {
        int i = 0;
        while (true) {
            gameObject.GetComponent<SpriteRenderer>().color = clrs[i];
            CurTick = !CurTick;
            i = (i + 1) % 2;
            yield return new WaitForSeconds(BPM_Timer);
        }
    }

}
