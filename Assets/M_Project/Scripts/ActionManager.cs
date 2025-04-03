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
    [SerializeField] private Button upgrade;
    [Header("Card")]
    [SerializeField] private TextMeshProUGUI textHeaderView;
    [SerializeField] private RectTransform cardRectTransform;
    [SerializeField] private TextMeshProUGUI textCardView;
    [SerializeField] private Button cardEndStep;
    [Header("Cards config")]
    [SerializeField] private Fond[] fondCards;
    [SerializeField] private FortunaCard[] fortunaCards;
    [Header("LuckyAccident")]
    [SerializeField] private GameObject panelPrizeView;
    [SerializeField] private TextMeshProUGUI resultView;
    [SerializeField] private Button LuckyEndStep;
    [Header("Bonus")]
    [SerializeField] private GameObject panelBonus;
    [SerializeField] private TextMeshProUGUI bonusView;
    [SerializeField] private Button bonusEndStep;
    [Header("Tax")]
    [SerializeField] private GameObject paneTax;
    [SerializeField] private Button pay10Price, payFixPrice;
    [Header("Animation Settings")]
    [SerializeField] private float animationDuration = 1f;
    [SerializeField] private Ease animationEase = Ease.OutCubic;


    private Player _playerOwner;
    private Player _player;
    private Assets _cell;
    private CityAssetManager _cityAssetManager;

    private void Start()
    {
        buy.onClick.AddListener(Buy);
        pay.onClick.AddListener(PayOwner);
        endStep.onClick.AddListener(EndStep);
        cardEndStep.onClick.AddListener(EndStep);
        LuckyEndStep.onClick.AddListener(EndStep);
        bonusEndStep.onClick.AddListener(EndStep);
        upgrade.onClick.AddListener(Upgrade);

        pay10Price.onClick.AddListener(() => PayTax(false)); // pay 10%
        payFixPrice.onClick.AddListener(() => PayTax(true)); //pay 200000

        actionPanel.SetActive(false);
        cardEndStep.gameObject.SetActive(false);
        upgrade.gameObject.SetActive(false);
        buy.gameObject.SetActive(false);
        pay.gameObject.SetActive(false);
        endStep.gameObject.SetActive(false);
        paneTax.gameObject.SetActive(false);

        // Початкове положення за межами екрану
        cardRectTransform.anchoredPosition = new Vector2(-Screen.width, cardRectTransform.anchoredPosition.y);

        _cityAssetManager = FindObjectOfType<CityAssetManager>();
    }
    public void OpenHasOwner(Assets cell, Player player, Player playerOwner)
    {
        _playerOwner = playerOwner;
        _player = player;
        _cell = cell;

        actionPanel.SetActive(true);
        buy.gameObject.SetActive(false);
        pay.gameObject.SetActive(true);
        endStep.gameObject.SetActive(false);

        nameView.text = player.playerName;
        cellView.text = cell.cellData.CellName;
        priceView.text = _cell.GetPayPrice().ToString();

    }
    public void OpenUpgradeCell(Assets cell, Player player)
    {
        _player = player;
        _cell = cell;

        actionPanel.SetActive(true);
        buy.gameObject.SetActive(false);
        upgrade.gameObject.SetActive(true);

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
    private void PayOwner()
    {
        if(_player.RemoveMoney(_cell.GetPayPrice()))
        {
            _playerOwner.AddMoney(_cell.GetPayPrice());
            actionPanel.SetActive(false);
            EndStep();
        }
        else
        {
            _cityAssetManager.UpdateCityUI(_player, true);
        }
    }
    private void PayTax(bool change)
    {
        if (change == true)
        {
            if (_player.RemoveMoney(200000))
            {

                paneTax.SetActive(false);
                EndStep();
            }
        }
        else
        {
            int totalCapitalPrice = 0;
            foreach(var capital in _player.ownedAssets)
            {
                totalCapitalPrice += capital.cellData.price;
            }
            totalCapitalPrice = (100 / totalCapitalPrice) * 10;
            if (_player.RemoveMoney(totalCapitalPrice))
            {

                paneTax.SetActive(false);
                EndStep();
            }
        }
        
    }
    private void Upgrade()
    {
        if (_player.RemoveMoney(_cell.cellData.priceUpgrade))
        {
            _cell.Upgrade();
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
    public void ViewTotalPrize(int randomNumber)
    {
        if (randomNumber >= 5 && randomNumber <= 9)
        {
            _player.AddMoney(50000);
            panelPrizeView.gameObject.SetActive(true);
            resultView.text = $"Ви виграли 50000";
        }
        else if (randomNumber == 3 || randomNumber == 4)
        {
            _player.AddMoney(100000);
            panelPrizeView.gameObject.SetActive(true);
            resultView.text = $"Ви виграли 100000";
        }
        else if (randomNumber == 10 || randomNumber == 11)
        {
            _player.AddMoney(100000);
            panelPrizeView.gameObject.SetActive(true);
            resultView.text = $"Ви виграли 100000";
        }
        else if (randomNumber == 2 || randomNumber == 12)
        {
            _player.AddMoney(200000);
            panelPrizeView.gameObject.SetActive(true);
            resultView.text = $"Ви виграли 200000";
        }
        else
        {
            panelPrizeView.gameObject.SetActive(true);
            resultView.text = $"Ви виграли 0";
        }
    }
    public void OpenBonusView(int moneyBonus)
    {
        panelBonus.SetActive(true);
        bonusView.text = $"Ваш бонус {moneyBonus}";
    }
    public void EndStep()
    {
        gameManager.StartNewStepPlayer();
        actionPanel.SetActive(false);
        panelPrizeView.SetActive(false);
        panelBonus.SetActive(false);
        cardRectTransform.anchoredPosition = new Vector2(-Screen.width, cardRectTransform.anchoredPosition.y);

    }

}
