using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;
using static System.MathF;

public class ChasePlayer: MonoBehaviour
{
    public float SeeDistance = 10f;
    private Transform target;
    private EnemyController controller;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
        controller = gameObject.GetComponent<EnemyController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 project = Vector3.Project(target.position - transform.position, new Vector3(1, 0, 0));
        if (project.magnitude < SeeDistance)
        {
            //rotate to look at the player
            if (project.x > 0 && !controller.isFacingRight)
            {
                transform.eulerAngles = new Vector3(0, 0);
                controller.isFacingRight = !controller.isFacingRight;
            }
            else if (project.x < 0 && controller.isFacingRight)
            {
                transform.eulerAngles = new Vector3(0, -180);
                controller.isFacingRight = !controller.isFacingRight;
            }
            //move towards the player
            transform.position += project.normalized * Time.deltaTime * controller.Speed;
        }
    }
}
