using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class ChasePlayer : EnemyController
{
    public float SeeDistance = 5f;
    private Transform target;
    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector3.Distance(transform.position, target.transform.position) < SeeDistance))
        {
            {
                transform.LookAt (target.transform.position);
                transform.Translate(new Vector2(Speed * Time.deltaTime,0));
            }
                
        }
        
    }
}
