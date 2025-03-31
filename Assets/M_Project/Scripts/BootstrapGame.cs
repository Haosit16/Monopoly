using UnityEngine;

public class BootstrapGame : MonoBehaviour
{
    [SerializeField] private GameObject panelSelectCountPlayer;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private DiceManager diceManager;
    [SerializeField] private CellManager boardManager;

    [Header("Config")]
    [SerializeField] private PlayerConfig playerConfig;

    [SerializeField] private Transform parentPanelPlayer;

    private PlayerFactory _playerFactory;

    private void Awake()
    {
        InitializeGameManager();
    }
    private void InitializeGameManager()
    {
        _playerFactory = new PlayerFactory();

        panelSelectCountPlayer.SetActive(true);
    }
    public void SelectCountPlayer(int number)
    {
        panelSelectCountPlayer.SetActive(false);
        gameManager.Initialize(diceManager, _playerFactory.Get(number, playerConfig, parentPanelPlayer, boardManager));
    }
}
