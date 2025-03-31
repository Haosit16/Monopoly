using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelUI : MonoBehaviour
{
    [SerializeField] private Image colorView;
    [SerializeField] private TextMeshProUGUI nameView;
    [SerializeField] private TextMeshProUGUI moneyView;
    [SerializeField] private TextMeshProUGUI capitalView;

    public void Initialize(Color color, string playerName, int money, int capital)
    {
        colorView.color = color;
        nameView.text = playerName;
        moneyView.text = money.ToString();
        capitalView.text = capital.ToString();
    }
    public void SetMoney(int money)
    {
        moneyView.text = money.ToString();
    }
    public void SetCapital(int capital)
    {
        capitalView.text = capital.ToString();
    }
}
