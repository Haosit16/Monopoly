using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transition : Cell
{
    public Transition whereToGoCell;
    private ActionManager actionManager;
    private void Start()
    {
        actionManager = FindObjectOfType<ActionManager>();
    }
    public override void Active(Player player)
    {
        RemovePlayer(player);
        whereToGoCell.AddPlayer(player);
        player.isOnOuterPath = !player.isOnOuterPath;
        actionManager.EndStep();
    }
}
