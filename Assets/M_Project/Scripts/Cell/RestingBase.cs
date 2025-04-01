
using UnityEngine;

public class RestingBase : Cell
{
    public int removeMoney;
    private ActionManager actionManager;
    private void Start()
    {
        actionManager = FindObjectOfType<ActionManager>();
    }
    public override void Active(Player player)
    {
        player.SkipStep();
        player.RemoveMoney(removeMoney);
        Debug.LogError($"{player.playerName} на базі");
        actionManager.EndStep();
    }
}
