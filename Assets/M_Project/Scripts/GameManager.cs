using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private UIManager uiManager;
    [SerializeField] private Vector3 diceRollCameraPos;

    private DiceManager _diceManager;
    private List<Player> _players = new List<Player>();

    private int currentPlayerStep;
    public void Initialize(DiceManager diceManager, List<Player> players)
    {
        if (players == null || players.Count == 0)
        {
            Debug.LogError("Помилка: список гравців порожній!");
            return;
        }

        _diceManager = diceManager;
        _players = players;
        currentPlayerStep = Random.Range(0, _players.Count);
        StartNewStepPlayer();
    }
    
    public void StartNewStepPlayer()
    {
        uiManager.OpenDiceRollPanel(_players[currentPlayerStep].playerName);
        Camera.main.GetComponent<CameraController>().RotateToDice(); 
    }
    private async void DiceRollForStep()
    {
        uiManager.CloseDiceRollPanel();
        //Debug.Log("Кидаємо кубики...");
        int result = await _diceManager.GetNumber(); // Очікуємо, поки обидва кубики впадуть
        
        //Debug.Log($"Гравець кидає {result}");
        _players[currentPlayerStep].MovePlayer(result, () => {
            currentPlayerStep++;
            if(currentPlayerStep>= _players.Count)
            {
                currentPlayerStep=0;
            }
        });

    }

}
