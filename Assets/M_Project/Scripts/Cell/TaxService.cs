public class TaxService : Cell
{
    private ActionManager actionManager;
    private void Start()
    {
        actionManager = FindObjectOfType<ActionManager>();
    }
    public override void Active(Player player)
    {
        actionManager.OpenPayTax(player);
    }
}
