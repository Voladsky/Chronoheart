using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public bool CurTick { get; set; } = true;
    public float BPM_Timer;
    [SerializeField] Sprite[] images;

    [SerializeField] Image timerImage;
    [SerializeField] int startState = 0;
    [SerializeField] float offSet;
    void Start()
    {
        StartCoroutine(Time_Dec());
    }
    private IEnumerator Time_Dec()
    {
        int i = startState;
        timerImage.sprite = images[i];
        CurTick = startState != 0;
        i = (i + 1) % 2;
        yield return new WaitForSeconds(offSet);

        while (true) 
        {           
            timerImage.sprite = images[i];
            CurTick = !CurTick;
            i = (i + 1) % 2;
            yield return new WaitForSeconds(BPM_Timer);
        }
    }

}
