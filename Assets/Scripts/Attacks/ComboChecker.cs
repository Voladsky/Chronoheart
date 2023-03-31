using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using System.Timers;

public class ComboChecker : MonoBehaviour
{
    [SerializeField] PlayerAttack playerAttack;
    [SerializeField] Timer timer;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] float comboCooldown;
    [SerializeField] private RangeWeapon weapon;
    HashSet<string> combos;
    string curCombo;
    bool registered;

    private float cooldown;
    private float attack_timer;

    enum ATK_BUTTONS { NONE, LEFT_CLICK, RIGHT_CLICK, MOVE_UP, MOVE_DOWN, MOVE_LEFT, MOVE_RIGHT, ARROW_UP, ARROW_DOWN }
    void Start()
    {
        curCombo = "";
        combos = new HashSet<string> { "00", "01", "20", "60", "30" };
        registered = false;
        //StartCoroutine(MyFixedUpd());
    }

    private void Update()
    {
        comboCooldown -= Time.deltaTime;
        if (timer.CurTick)
        {
            var btn = ParseKey();
            if (btn != ATK_BUTTONS.NONE)
            {
                curCombo += (int)(btn - 1);
                Debug.Log(curCombo);
                var possibles = combos.Where(x => curCombo.Contains(x));
                if (possibles.Count() > 0 && comboCooldown < 0)
                {
                    curCombo = possibles.First();
                    PerformCombo(curCombo);
                    curCombo = "";
                }
                else if (curCombo.Length > 0 && curCombo.Last() == '0')
                {
                    playerAttack.Attack(2, false);
                }
                else if (curCombo.Length > 0 && curCombo.Last() == '1')
                {
                    weapon.Attack(transform.localScale.normalized.x);
                }
            }
        }
        else
        {
            curCombo = "";
            var btn = ParseKey();
            if (btn != ATK_BUTTONS.NONE)
            {
                curCombo += (int)(btn - 1);
            }
                if (curCombo.Length > 0 && curCombo.Last() == '0')
            {
                playerAttack.Attack(2, false);
            }
            if (curCombo.Length > 0 && curCombo.Last() == '1')
            {
                weapon.Attack(transform.localScale.normalized.x);
            }
        }
    }
    ATK_BUTTONS ParseKey()
    {
        if (Input.GetMouseButtonDown(0)) return ATK_BUTTONS.LEFT_CLICK;
        if (Input.GetMouseButtonDown(1)) return ATK_BUTTONS.RIGHT_CLICK;
        if (Input.GetKeyDown("space")) return ATK_BUTTONS.MOVE_UP;
        if (Input.GetKeyDown("s")) return ATK_BUTTONS.MOVE_DOWN; 
        if (Input.GetKeyDown("a")) return ATK_BUTTONS.MOVE_LEFT;
        if (Input.GetKeyDown("d")) return ATK_BUTTONS.MOVE_RIGHT;
        if (Input.GetKeyDown("w")) return ATK_BUTTONS.ARROW_UP;
        return ATK_BUTTONS.NONE;
    }

    IEnumerator ShowText(string text)
    {
        comboText.text = text;

        comboText.faceColor = new Color32(255, 255, 255, 255);

        yield return new WaitForSeconds(1);

        comboText.faceColor = new Color32(255, 255, 255, 0);
    }

    void PerformCombo(string cmb)
    {
        switch (cmb)
        {
            case "00":
                StartCoroutine(ShowText("Heavy attack!"));
                playerAttack.Attack(100, true);
                break;
            case "01":
                StartCoroutine(ShowText("Long range attack!"));
                weapon.ComboAttack(GetComponent<Player>().transform.localScale.x);
                break;
            case "20":
                StartCoroutine(ShowText("Move down attack!"));
                playerAttack.ComboAttack23(100);
                break;
            case "60":
                StartCoroutine(ShowText("Arrow up attack!"));
                playerAttack.ComboAttack60(2);
                break;
            case "30":
                playerAttack.ComboAttack30(2);
                StartCoroutine(ShowText("Arrow down attack!"));
                break;

        }
    }
}
