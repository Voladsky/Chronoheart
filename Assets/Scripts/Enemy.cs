using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponent<EnemyController>();
        controller.JumpingPower = jumpingPower;
        controller.Speed = speed;
    }

    // Update is called once per frame
    void Update()
    {
        controller.Move();
    }
}
