using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("DiceRoll")]
    [SerializeField]private GameObject panelDiceRoll;
    [SerializeField] private TextMeshProUGUI nameView;

    private void Start()
    {
        panelDiceRoll.SetActive(false);
    }
    public void OpenDiceRollPanel(string name)
    {
        panelDiceRoll.SetActive(true);
        nameView.text = name;
    }
    public void CloseDiceRollPanel() => panelDiceRoll.SetActive(false);
}
