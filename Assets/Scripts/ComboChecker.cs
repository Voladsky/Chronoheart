using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboChecker : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (GameObject.Find("Timer").GetComponent<Timer>().CurTick && Input.GetMouseButtonDown(0))
        {
            Debug.Log("gotcha");
        }
    }
}
