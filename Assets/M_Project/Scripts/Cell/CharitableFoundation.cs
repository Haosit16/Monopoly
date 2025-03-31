public class CharitableFoundation : Cell
{

    private ActionManager actionManager;
    public override void Active(Player player)
    {
        actionManager.CharitableFoundation(player);
    }
    void Start()
    {
        actionManager = FindObjectOfType<ActionManager>();
    }
}
