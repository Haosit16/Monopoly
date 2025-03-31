using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class ActionManager : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [Header("UI")]
    [SerializeField] private GameObject actionPanel;
    [SerializeField] private TextMeshProUGUI nameView;
    [SerializeField] private Button buy, endStep, pay;
    [SerializeField] private TextMeshProUGUI cellView;
    [SerializeField] private TextMeshProUGUI priceView;
    [Header("Card")]
    [SerializeField] private TextMeshProUGUI textHeaderView;
    [SerializeField] private RectTransform cardRectTransform;
    [SerializeField] private TextMeshProUGUI textCardView;
    [SerializeField] private Button cardEndStep;
    [Header("Cards config")]
    [SerializeField] private Fond[] fondCards;
    [SerializeField] private FortunaCard[] fortunaCards;
    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private Ease animationEase = Ease.OutCubic;


    private Player _playerOwner;
    private Player _player;
    private Assets _cell;

    private void Start()
    {
        buy.onClick.AddListener(Buy);
        pay.onClick.AddListener(Pay);
        endStep.onClick.AddListener(EndStep);
        cardEndStep.onClick.AddListener(EndStep);

        actionPanel.SetActive(false);
        cardEndStep.gameObject.SetActive(false);

        // Початкове положення за межами екрану
        cardRectTransform.anchoredPosition = new Vector2(-Screen.width, cardRectTransform.anchoredPosition.y);
    }
    public void OpenHasOwner(Assets cell, Player player, Player playerOwner)
    {
        _playerOwner = playerOwner;
        _player = player;
        _cell = cell;

        actionPanel.SetActive(true);
        buy.gameObject.SetActive(false);
        endStep.gameObject.SetActive(true);
        pay.gameObject.SetActive(true);

        nameView.text = player.playerName;
        cellView.text = cell.cellData.CellName;
        priceView.text = _cell.GetPayPrice().ToString();

    }
    public void OpenNoOwnre(Assets cell, Player player)
    {
        _player = player;
        _cell = cell;

        actionPanel.SetActive(true);
        buy.gameObject.SetActive(true);
        endStep.gameObject.SetActive(true);
        pay.gameObject.SetActive(false);

        nameView.text = player.playerName;
        cellView.text = cell.cellData.CellName;
        priceView.text = _cell.cellData.price.ToString();
    }
    private void Buy()
    {
        if (_player.RemoveMoney(_cell.cellData.price))
        {
            _cell.ChangeOwner(_player);
            _player.AddAssets(_cell, _cell.cellData.price);
            actionPanel.SetActive(false);
            EndStep();
        }
    }
    private void Pay()
    {
        if(_player.RemoveMoney(_cell.GetPayPrice()))
        {
            _playerOwner.AddMoney(_cell.GetPayPrice());
            actionPanel.SetActive(false);
            EndStep();
        }
    }
    public void Fortuna(Player player) // foruna
    {
        FortunaCard randomCard = fortunaCards[Random.Range(0, fortunaCards.Length)];
        textHeaderView.text = randomCard.headerCard;
        textCardView.text = randomCard.nameCard + " " + randomCard.money;

        AnimateMapEntrance(player, () => {
            cardEndStep.gameObject.SetActive(true);
            player.AddMoney(randomCard.money);
        });
    }
    public void CharitableFoundation(Player player) // fond
    {
        Fond randomCard = fondCards[Random.Range(0, fondCards.Length)];
        textHeaderView.text = randomCard.headerCard;
        textCardView.text = randomCard.nameFond + " " + randomCard.money;

        AnimateMapEntrance(player, () => {
            cardEndStep.gameObject.SetActive(true);
            player.AddMoney(randomCard.money);
        });
    }
    public void AnimateMapEntrance(Player player, Action onCardView)
    {
        // Послідовна анімація руху та прозорості
        Sequence mapEntranceSequence = DOTween.Sequence();

        // Анімація руху
        mapEntranceSequence.Append(
            cardRectTransform.DOAnchorPosX(0, animationDuration)
            .SetEase(animationEase)
        );

        // Додаткові параметри анімації
        mapEntranceSequence
            .SetUpdate(true) // Продовжити анімацію, навіть коли гра на паузі
            .OnComplete(() => {
                // Дії після завершення анімації
                onCardView?.Invoke();
                Debug.Log("Карта з'явилася!");
            });
    }
    public void EndStep()
    {
        gameManager.StartNewStepPlayer();
        actionPanel.SetActive(false);
        cardRectTransform.anchoredPosition = new Vector2(-Screen.width, cardRectTransform.anchoredPosition.y);

    }

}
