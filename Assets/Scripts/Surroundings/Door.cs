using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float timeToMove = 2f;

    private Vector3 startPosition;
    private Vector3 endPosition;

    [SerializeField] AudioClip closeSound;
    [SerializeField] AudioClip openSound;

    private void Start()
    {
        startPosition = transform.position;
        endPosition = startPosition - new Vector3(0, 2, 0);
    }

    public void Close()
    {
        if (endPosition == transform.position)
        {
            return;
        }
        SoundManager.Instance.PlaySound(closeSound);
        StartCoroutine(LerpPos(startPosition, endPosition, timeToMove));
    }

    public void Open()
    {
        if (startPosition == transform.position)
        {
            return;
        }
        SoundManager.Instance.PlaySound(openSound);
        StartCoroutine(LerpPos(endPosition, startPosition, timeToMove));
    }

    IEnumerator LerpPos(Vector3 start, Vector3 end, float timeToMove)
    {
        float t = 0;
        while (t < 1)
        {
            transform.position = Vector3.Lerp(start, end, curve.Evaluate(t));
            t = t + Time.deltaTime / timeToMove;
            yield return new WaitForEndOfFrame();
        }
        transform.position = end;
    }
}
