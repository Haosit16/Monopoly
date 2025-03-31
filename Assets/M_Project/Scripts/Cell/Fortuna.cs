public class Fortuna : Cell
{
    private ActionManager actionManager;
    public override void Active(Player player)
    {
        actionManager.Fortuna(player);
    }
    void Start()
    {
        actionManager = FindObjectOfType<ActionManager>();
    }
}
