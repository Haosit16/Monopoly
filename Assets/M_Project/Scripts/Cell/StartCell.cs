
using UnityEngine;

public class StartCell : Cell
{
    [SerializeField] private int moneyOfStart;
    private ActionManager actionManager;
    private void Start()
    {
        actionManager = FindObjectOfType<ActionManager>();
    }
    public override void Active(Player player)
    {
        player.PassGo(moneyOfStart);
        Debug.LogError(player.playerName + "������� �� �����");
        actionManager.EndStep();
    }
    public void StartGoing(Player player)
    {
        Debug.LogError(player.playerName + $"������� �����. ������ ������ {player.money}");
        player.PassGo(moneyOfStart);
        Debug.LogError(player.playerName + $"����� ������ {player.money}");

    }
}
