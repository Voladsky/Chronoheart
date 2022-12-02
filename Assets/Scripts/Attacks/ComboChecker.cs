using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class ComboChecker : MonoBehaviour
{
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] Timer timer;
    [SerializeField] TextMeshProUGUI comboText;
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
            if (combos.Contains(curCombo))
            {
                StartCoroutine(ShowText());
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

    IEnumerator ShowText()
    {
        comboText.text = $"CCCOMBO!!!";

        comboText.faceColor = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(3);

        comboText.faceColor = new Color32(255, 255, 255, 0);
    }
}
