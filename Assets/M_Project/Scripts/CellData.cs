using UnityEngine;
[CreateAssetMenu(fileName = "CellData",menuName = "CellData")]
public class CellData : ScriptableObject
{
    public string CellName;
    public int price;
    public int priceToStep;
    public bool canUpgrade;
    public int priceUpgrade;
    public int priceUpgradeToStep;
}
