using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private List<Transform> points;
    [SerializeField] private float timeToMove = 2f;
    [SerializeField] private int currentPoint = 1;
    private Vector3 startPosition;
    private Vector3 endPosition;
    private Rigidbody2D rb;   
    private int dir = 1;

    void Start()
    {
        startPosition = points[0].position;
        endPosition = points[1].position;
        rb = GetComponent<Rigidbody2D>();
        Move();
    }
    public void Move()
    {
        StartCoroutine(LerpPos(startPosition, endPosition, timeToMove));
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
        MoveFinished();
    }
    public void MoveFinished()
    {
        startPosition = endPosition;
        endPosition = points[NextPoint()].position;
        Move();
    }
    int NextPoint()
    {
        currentPoint += dir;
        if (currentPoint == points.Count || currentPoint == -1)
        {
            dir *= -1;
            currentPoint += 2*dir;
        }
        return currentPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.position.y > transform.position.y)
        {
            collision.transform.SetParent(transform);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
