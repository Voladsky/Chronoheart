using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool CurTick { get; set; } = true;
    public float BPM_Timer { get; set; } = 0.46875f;
    Color[] clrs;

    [SerializeField] Image timerImage;
    void Start()
    {
        clrs = new Color[] { Color.black, Color.yellow };
        StartCoroutine(Time_Dec());
    }
    private IEnumerator Time_Dec()
    {
        int i = 0;
        while (true) {
            timerImage.color = clrs[i];
            CurTick = !CurTick;
            i = (i + 1) % 2;
            yield return new WaitForSeconds(BPM_Timer);
        }
    }

}
