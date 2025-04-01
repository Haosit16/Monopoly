using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LuckyThrow : Cell
{
    [SerializeField] private GameObject panel;
    [SerializeField] private TextMeshProUGUI nameView;
    private GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }
    public override void Active(Player player)
    {
        panel.SetActive(true);
        nameView.text = player.playerName;
        gameManager.OpenDiceRollLuckyThrowPanel();
    }
}
