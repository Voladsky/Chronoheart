using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboChecker : MonoBehaviour
{
    HashSet<string> combos;
    string curCombo;
    void Start()
    {
        curCombo = "";
        combos = new HashSet<string> { "00", "01" };
    }
    void Update()
    {
        if (GameObject.Find("Timer").GetComponent<Timer>().CurTick)
        {
            if (Input.GetMouseButtonDown(0)) curCombo += "0";
            else if (Input.GetMouseButtonDown(1)) curCombo += "1";
            if (combos.Contains(curCombo))
            {
                Debug.Log("CCCOMBO!");
                gameObject.GetComponent<BasicAttacker>().Attack(100);
                curCombo = "";
            }
        }
    }
}
