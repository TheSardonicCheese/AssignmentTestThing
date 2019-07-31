using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleUIManager : MonoBehaviour
{
    private Button attackButton;
    private Button defendButton;
    private Button healButton;

    public Image pHealthBarFill;
    public Image eHealthBarFill;

    public BattleManager bManager;

    public event System.Action CallAttack;
    public event System.Action CallDefend;
    public event System.Action CallHeal;

    public Text[] combatLogLines;
    public List<string> combatLog;

    private void Awake()
    {
        //good practice to subscribe to events invoid awake, because you don't know the order of events
        bManager = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<BattleManager>();
        //original event onleft, subscriber onright
        bManager.UpdateHealth += UpdateHealthBar;
    }

    void Start()
    {
        StartCoroutine(DebugLogTest());
    }

    public void UpdateHealthBar(bool isPlayer, float health)
    {
        //checkif its the player, if so update their health, otherwise update enemy health
        //will handle fill amount back in therespective scripts calling this function
        if(isPlayer)
        {
            pHealthBarFill.fillAmount = health;
        }
        else
        {
            eHealthBarFill.fillAmount = health;
        }
    }

    public void CallAttackEvent()
    {
        Debug.Log("attack!");
        CallAttack();
    }
    public void CallDefendEvent()
    {
        Debug.Log("defend!");
        CallDefend();
    }
    public void CallHealEvent()
    {
        Debug.Log("heal!");
        CallHeal();
    }
    public void UpdateCombatLog(string incLog)
    {
        //add string olist
        combatLog.Insert(0, incLog);
        //if list is > array delete last entry
        if(combatLog.Count > combatLogLines.Length)
        {
            combatLog.RemoveAt(combatLog.Count - 1);
        }
        //loop though array and set the text to thestrings
        for(int i = 0; i < combatLog.Count; i++)
        {
            combatLogLines[i].text = combatLog[i];
        }
        StartCoroutine(DebugLogTest());
    }
    IEnumerator DebugLogTest()
    {
        int randomNumber = Random.Range(1, 1000);
        yield return new WaitForSeconds(3f);
        UpdateCombatLog(randomNumber.ToString());
    }
}
