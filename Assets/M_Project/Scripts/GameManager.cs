using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private ActionManager actionManager;
    [SerializeField] private Vector3 diceRollCameraPos;

    private DiceManager _diceManager;
    private List<Player> _players = new List<Player>();

    private int currentPlayerStep;
    public void Initialize(DiceManager diceManager, List<Player> players)
    {
        _diceManager = diceManager;
        _players = players;
        currentPlayerStep = Random.Range(0, _players.Count);
        StartNewStepPlayer();
        foreach (Player player in _players) 
        {
            player.OnGameOver += PlayerGameOver;
        }
    }
    
    public void StartNewStepPlayer()
    {
        foreach (Player p in _players)
        {
            p._playerPanelUI.SetActiveStepView(false);
        }
        _players[currentPlayerStep]._playerPanelUI.SetActiveStepView(true);

        uiManager.OpenDiceRollPanel(_players[currentPlayerStep].playerName);
        Camera.main.GetComponent<CameraController>().RotateToDice();
         
    }
    public async void DiceRollForStep()
    {
        uiManager.CloseDiceRollPanel();
        int result = await _diceManager.GetNumber();

        //Debug.Log($" {result}");
        _players[currentPlayerStep].MovePlayer(result, () => {
            currentPlayerStep++;
            if(currentPlayerStep>= _players.Count)
            {
                currentPlayerStep=0;
            }
        });

    }

    public void OpenDiceRollLuckyThrowPanel()
    {
        uiManager.OpenDiceRollLuckyThrow(_players[currentPlayerStep].playerName);
        Camera.main.GetComponent<CameraController>().RotateToDice();
    }
    public void StartDiceRollForLuckyThrow()
    {
        uiManager.CloseDiceRollLuckyThrow();
        DiceRollForLuckyThrow();
    }
    private async void DiceRollForLuckyThrow()
    {
        int result = await _diceManager.GetNumber();
        actionManager.ViewTotalPrize(result);
    }
    private void PlayerGameOver(Player player)
    {
        _players.Remove(player);
        player.OnGameOver -= PlayerGameOver;
        Destroy(player.gameObject);
    }
}
