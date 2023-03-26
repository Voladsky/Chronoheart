using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool CurTick { get; set; } = true;
    public float BPM_Timer;
    Color[] clrs;

    [SerializeField] Image timerImage;
    [SerializeField] int startState = 0;
    [SerializeField] float offSet;
    void Start()
    {
        clrs = new Color[] { Color.black, Color.yellow };
        StartCoroutine(Time_Dec());
    }
    private IEnumerator Time_Dec()
    {
        int i = startState;
        timerImage.color = clrs[i];
        CurTick = startState != 0;
        i = (i + 1) % 2;
        yield return new WaitForSeconds(offSet);

        while (true) 
        {           
            timerImage.color = clrs[i];
            CurTick = !CurTick;
            i = (i + 1) % 2;
            yield return new WaitForSeconds(BPM_Timer);
        }
    }

}
