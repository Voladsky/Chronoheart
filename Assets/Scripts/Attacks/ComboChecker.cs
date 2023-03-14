using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ComboChecker : MonoBehaviour
{
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] Timer timer;
    [SerializeField] TextMeshProUGUI comboText;
    HashSet<string> combos;
    string curCombo;
    bool registered;

    enum ATK_BUTTONS { NONE, LEFT_CLICK, RIGHT_CLICK, MOVE_UP, MOVE_DOWN, MOVE_LEFT, MOVE_RIGHT }
    ATK_BUTTONS lastButtonInTick;
    void Start()
    {
        curCombo = "";
        combos = new HashSet<string> { "00", "01", "450", "550", "320", "230" };
        lastButtonInTick = ATK_BUTTONS.NONE;
        registered = false;
        StartCoroutine(MyFixedUpd());
    }
    IEnumerator MyFixedUpd()
    {
        while (true)
        {
            if (timer.CurTick)
            {
                registered = false;
                var btn = ParseKey();
                if (btn != ATK_BUTTONS.NONE)
                {
                    lastButtonInTick = btn;
                    curCombo += (int)(btn - 1);
                    if (combos.Contains(curCombo))
                    {
                        StartCoroutine(ShowText());
                        playerAttack.Attack(100, true);
                        curCombo = "";
                    }
                    yield return new WaitUntil(() => !timer.CurTick);
                }
                else yield return new WaitUntil(() => timer.CurTick);
            }
            else
            {
                var btn = ParseKey();
                if (btn != ATK_BUTTONS.NONE)
                {
                    curCombo = "";
                    yield return new WaitUntil(() => timer.CurTick);
                }
                if (lastButtonInTick == ATK_BUTTONS.NONE && !registered)
                {
                    curCombo = "";
                    yield return new WaitUntil(() => timer.CurTick);
                }
                else
                {
                    registered = true;
                    lastButtonInTick = ATK_BUTTONS.NONE;
                    yield return new WaitUntil(() => !timer.CurTick);
                }
            }
        }
    }

    ATK_BUTTONS ParseKey()
    {
        if (Input.GetMouseButtonDown(0)) return ATK_BUTTONS.LEFT_CLICK;
        if (Input.GetMouseButtonDown(1)) return ATK_BUTTONS.RIGHT_CLICK;
        if (Input.GetAxis("Vertical") == 1) return ATK_BUTTONS.MOVE_UP;
        if (Input.GetAxis("Horizontal") == -1) return ATK_BUTTONS.MOVE_LEFT;
        if (Input.GetAxis("Horizontal") == -1) return ATK_BUTTONS.MOVE_DOWN;
        if (Input.GetAxis("Horizontal") == 1) return ATK_BUTTONS.MOVE_RIGHT;
        else return ATK_BUTTONS.NONE;
    }

    IEnumerator ShowText()
    {
        comboText.text = $"CCCOMBO!!!";

        comboText.faceColor = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(1);

        comboText.faceColor = new Color32(255, 255, 255, 0);
    }
}
