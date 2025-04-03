using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CityUIElement : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI cityNameText;
    [SerializeField] private TextMeshProUGUI sellPriceText;
    [SerializeField] private TextMeshProUGUI rentPriceText;
    [SerializeField] private Button sellButton;

    private Assets currentCity;
    private Action<Assets> onSellAction;

    public void Initialize(Assets city, Action<Assets> onSell)
    {
        currentCity = city;
        cityNameText.text = city.cellData.CellName;
        sellPriceText.text = $"Sell: {city.cellData.price}$";
        rentPriceText.text = $"Rent: {city.GetPayPrice()}$";

        onSellAction = onSell;
        sellButton.onClick.RemoveAllListeners();
        sellButton.onClick.AddListener(() => onSellAction?.Invoke(currentCity));
    }

    public void ResetUI()
    {
        cityNameText.text = "";
        sellPriceText.text = "";
        rentPriceText.text = "";
        sellButton.onClick.RemoveAllListeners();
    }
}
