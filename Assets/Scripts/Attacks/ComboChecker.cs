using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboChecker : MonoBehaviour
{
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] Timer timer;
    HashSet<string> combos;
    string curCombo;

    void Start()
    {
        curCombo = "";
        combos = new HashSet<string> { "00", "01" };
    }
    void Update()
    {
        if (timer.CurTick)
        {
            if (Input.GetMouseButtonDown(0)) curCombo += "0";
            else if (Input.GetMouseButtonDown(1)) curCombo += "1";
            if (combos.Contains(curCombo))
            {
                Debug.Log("CCCOMBO!");
                playerAttack.Attack(100);
                curCombo = "";
            }
        }
    }
}
