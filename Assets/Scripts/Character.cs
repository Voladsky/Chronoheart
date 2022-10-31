using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Main abstract class for characters: player, enemy, boss etc.
/// </summary>
public class Character : MonoBehaviour
{
    public IController controller;
    public float speed;
    public float jumpingPower;
}
