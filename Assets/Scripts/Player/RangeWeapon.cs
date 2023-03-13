using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeWeapon : MonoBehaviour
{
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform range;
    public void Attack(float scale)
    {
        Debug.Log(scale);
        projectile.transform.localScale = new Vector3(scale, 1, 1);
        Instantiate(projectile, range.position, range.rotation);
    }
}
