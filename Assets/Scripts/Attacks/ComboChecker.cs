using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboChecker : MonoBehaviour
{
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] Timer timer;
    HashSet<string> combos;
    string curCombo;
    enum ATK_BUTTONS { NONE, LEFT_CLICK, RIGHT_CLICK }
    void Start()
    {
        curCombo = "";
        combos = new HashSet<string> { "00", "01" };
    }
    void Update()
    {
        ATK_BUTTONS btn = ParseKey();
        if (btn != ATK_BUTTONS.NONE) {
            if (!timer.CurTick) curCombo = "";
            else curCombo += (int)(btn - 1);
            Debug.Log(curCombo);
            if (combos.Contains(curCombo))
            {
                Debug.Log("CCCOMBO!");
                playerAttack.Attack(100);
                curCombo = "";
            }
        }
    }

    ATK_BUTTONS ParseKey()
    {
        if (Input.GetMouseButtonDown(0)) return ATK_BUTTONS.LEFT_CLICK;
        if (Input.GetMouseButtonDown(1)) return ATK_BUTTONS.RIGHT_CLICK;
        else return ATK_BUTTONS.NONE;
    }
}
