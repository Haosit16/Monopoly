using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    // ������ �������������� ������
    public string playerName;
    public int playerID;
    public Color playerColor;
    public bool isAI = false;

    // Գ������� ����������
    public int money = 1500; // ��������� ������
    public int capital = 0;
    public bool isBankrupt = false;

    // ����� ����������
    public Image playerToken;
    public float durationToCell = 0.2f; // �������� ���������� �� ���������

    // ��������� �� �������� ���
    public int currentPosition = 0;
    public bool isInJail = false;
    public int turnsInJail = 0;

    // �������� ������
    public List<Assets> ownedAssets = new List<Assets>();
    //public List<PropertyTile> mortgagedProperties = new List<PropertyTile>();

    // ��������� ������
    //public List<Card> cards = new List<Card>();
    //public bool hasJailFreeCard = false;
    

    public PlayerPanelUI _playerPanelUI;

    public bool isOnOuterPath = true;
    private CellManager _cellManager;
    private Cell currentCel; // ��� ��������� ������� �� ��� �� �����������
    private Animator animator;
    private Camera _camera;
    private bool needSkipStep = false;
    private ActionManager actionManager;

    public bool NeedSkipStep { get { return needSkipStep; } }


    // ����������� ������
    public void Initialize(string name, int id, Color color, bool ai, CellManager boardManager, PlayerPanelUI playerPanelUI)
    {
        _camera = Camera.main;
        playerName = name;
        playerID = id;
        playerColor = color;
        isAI = ai;
        _cellManager = boardManager;

        _playerPanelUI = playerPanelUI;
        _playerPanelUI.Initialize(playerColor, playerName, money, capital);
        actionManager = FindObjectOfType<ActionManager>();
        // ������������ ���� 
        if (playerToken != null)
        {
            playerToken.color = playerColor;
        }
        animator = GetComponent<Animator>();
    }
    public void MovePlayer(int steps, Action OnEndMove)
    {
        if (needSkipStep)
        {
            OnEndMove?.Invoke();
            actionManager.EndStep();
            ChangeSkipStep();
            return;
        }
        else
        {
            StartCoroutine(Move(steps, OnEndMove));
            return;
        }
        
    }
    public IEnumerator Move(int steps, Action OnEndMove)
    {
        if (currentCel != null)
            currentCel.RemovePlayer(this);

        List<Cell> currentPath = isOnOuterPath ? _cellManager.outerCirclePath : _cellManager.innerCirclePath;

        animator.SetBool("Walk", true);
        int previousPosition = currentPosition; // �������� ��������� �������
        int targetPosition = (currentPosition + steps) % currentPath.Count;

        for (int i = 1; i <= steps; i++)
        {
            int nextPosition = (currentPosition + i) % currentPath.Count;

            Vector3 startPos = transform.position;
            Vector3 endPos = currentPath[nextPosition].transform.position;

            CheckStartPassed(currentPath, nextPosition);
            CheckBonusPassed(currentPath, nextPosition);

            float time = 0;
            while (time < durationToCell)
            {
                _camera.GetComponent<CameraController>().CameraRotateToPlayer(nextPosition, isOnOuterPath, transform);
                RotateToCell(endPos);
                transform.position = Vector3.Lerp(startPos, endPos, time / durationToCell);
                time += Time.deltaTime;
                yield return null;
            }

            transform.position = endPos;
            yield return new WaitForSeconds(0);
        }
        currentCel = currentPath[targetPosition];

        animator.SetBool("Walk", false);
        currentPosition = targetPosition;
        currentCel.AddPlayer(this);
        currentCel.Active(this);
        OnEndMove?.Invoke();
    }
    private void CheckStartPassed(List<Cell> currentPath, int nextPosition)
    {
        int indexStart = currentPath.IndexOf(currentPath[nextPosition]);
        if (indexStart == 0 && isOnOuterPath) // ���� ������� ����� ������� �������
        {
            if (currentPath[0].gameObject.TryGetComponent(out StartCell startCell))
            {
                startCell.StartGoing(this); // ��������� ����� �������� �������
                Debug.Log($"������� {playerName} ������� ����� � ������� �����!");
            }
        }
    }
    private void CheckBonusPassed(List<Cell> currentPath, int nextPosition)
    {
        if (!isOnOuterPath)
        {
            if (currentPath[nextPosition].gameObject.TryGetComponent(out Bonus bonus))
            {
                bonus.Passed(this);
                Debug.Log($"������� {playerName} ������� bonus � ������� �����!");
            }
        }
    }
    public void TransitionToStart()
    {
        isOnOuterPath = true;
        currentPosition = 0;
    }
    public void Transition(Transition cell)
    {
        isOnOuterPath = !isOnOuterPath;
        currentPosition = cell.linkedCell;
    }
    // ��������� ������ ��� ���������� ����� �����
    public void PassGo(int value)
    {
        money += value;
        //UIManager.instance.ShowFloatingText($"+{goAmount}$", tokenTransform.position, Color.green);
        //UIManager.instance.ShowMessage($"{playerName} ��������� ����� ����� � ������ {goAmount}$!");

        // ���� ��������� ������
        //AudioManager.instance.PlaySound("cash");
    }
    public void AddMoney(int amount)
    {
        money += amount;
        _playerPanelUI.SetMoney(money);
    }
    public bool RemoveMoney(int amount)
    {
        if(money >= amount)
        {
            money -= amount;
            _playerPanelUI.SetMoney(money);
            return true;
        }
        return false;
    }
    // ������ ��������
    public void AddAssets(Assets cell, int capitalAssets)
    {
        ownedAssets.Add(cell);
        capital += capitalAssets;
        _playerPanelUI.SetCapital(capital);
    }
    public void SellAssets(Assets assets)
    {
        AddMoney(assets.cellData.price);
        capital -= assets.cellData.price;
        _playerPanelUI.SetCapital(capital);
        _playerPanelUI.SetMoney(assets.cellData.price);
    }
    private void RotateToCell(Vector3 cellPosition)
    {
        Vector3 direction = cellPosition - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.LookRotation(direction);
    }
    public void SkipStep()
    { 
        needSkipStep = true;
        actionManager.EndStep();
    }
    public void ChangeSkipStep() => needSkipStep = false;
}
