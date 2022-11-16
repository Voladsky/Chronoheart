using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IController
{
    float Speed { get; set; }
    float JumpingPower { get; set; }
    void Move();
}
