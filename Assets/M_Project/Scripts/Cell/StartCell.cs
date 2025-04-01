
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
        Debug.LogError(player.playerName + "прийшов на старт");
        actionManager.EndStep();
    }
    public void StartGoing(Player player)
    {
        Debug.LogError(player.playerName + $"пройшов старт. Старий баланс {player.money}");
        player.PassGo(moneyOfStart);
        Debug.LogError(player.playerName + $"Новий баланс {player.money}");

    }
}
