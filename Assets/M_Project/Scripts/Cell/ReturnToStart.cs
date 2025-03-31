using UnityEngine;

public class ReturnToStart : Cell
{
    [SerializeField] private StartCell startCell;
    private ActionManager actionManager;
    private void Start()
    {
        actionManager = FindObjectOfType<ActionManager>();
    }
    public override void Active(Player player)
    {
        RemovePlayer(player);
        startCell.AddPlayer(player);
        player.isOnOuterPath = !player.isOnOuterPath; // change circle path
        actionManager.EndStep();
    }
}
