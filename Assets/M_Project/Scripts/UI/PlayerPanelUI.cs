using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPanelUI : MonoBehaviour
{
    [SerializeField] private Image colorView;
    [SerializeField] private TextMeshProUGUI nameView;
    [SerializeField] private TextMeshProUGUI moneyView;
    [SerializeField] private TextMeshProUGUI capitalView;
    [SerializeField] private GameObject stepView;

    public void Initialize(Color color, string playerName, int money, int capital)
    {
        colorView.color = color;
        nameView.text = playerName;
        SetCapital(capital);
        SetMoney(money);
        SetActiveStepView(false);
    }
    public void SetMoney(int money)
    {
        moneyView.text = money.ToString() + " грн";
    }
    public void SetCapital(int capital)
    {
        capitalView.text = "К " + capital.ToString() + " грн";
    }
    public void SetActiveStepView(bool value)
    {
        stepView.SetActive(value);
    }
}
