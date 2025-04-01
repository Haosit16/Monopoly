using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : Cell
{
    [SerializeField] private int moneyOfStart;
    private ActionManager actionManager;
    private void Start()
    {
        actionManager = FindObjectOfType<ActionManager>();
    }
    public override void Active(Player player)
    {
        player.AddMoney(moneyOfStart);
        Debug.LogError(player.playerName + "прийшов на бонус");
        actionManager.OpenBonusView(100000);
    }
    public void Passed(Player player)
    {
        player.AddMoney(moneyOfStart);
        Debug.LogError(player.playerName + "пройшов бонус");
    }
}
