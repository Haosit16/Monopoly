using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("DiceRoll")]
    [SerializeField]private GameObject panelDiceRoll;
    [SerializeField] private TextMeshProUGUI nameView;
    [Header("DiceRollLuckyThrow")]
    [SerializeField]private GameObject panelDiceRollLuckyThrow;
    [SerializeField] private TextMeshProUGUI nameViewLuckyThrow;

    private void Start()
    {
        panelDiceRoll.SetActive(false);
    }
    public void OpenDiceRollPanel(string name)
    {
        panelDiceRoll.SetActive(true);
        nameView.text = name;
    }

    public void OpenDiceRollLuckyThrow(string name)
    {
        panelDiceRollLuckyThrow.SetActive(true);
        nameViewLuckyThrow.text = name;
    }
    public void CloseDiceRollPanel() => panelDiceRoll.SetActive(false);
    public void CloseDiceRollLuckyThrow() => panelDiceRollLuckyThrow.SetActive(false);
}
